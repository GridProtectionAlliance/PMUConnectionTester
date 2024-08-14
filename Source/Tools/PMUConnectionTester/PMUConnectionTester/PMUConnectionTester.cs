//*****************************************************************************************************
//  PMUConnectionTester.cs - Gbtc
//
//  Copyright © 2022, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
//  file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  03/16/2006 - J. Ritchie Carroll
//       Initial version of source generated.
//  01/21/2011 - AJ Stadlin
//      ShowMessagesTabOnDataException Option added to DataStreamException.
//  05/19/2011 - J. Ritchie Carroll
//       Added DST file support.
//  06/01/2012 - J. Ritchie Carroll
//       Added network interface multicast source selection.
//  05/22/2014 - J. Ritchie Carroll
//       Migrated code to use Grid Solutions Framework.
//  03/26/2022 - J. Ritchie Carroll
//       Migrated code from VB to C# assisted with Code Converter (VB - C#):
//       https://marketplace.visualstudio.com/items?itemName=SharpDevelopTeam.CodeConverter
//
//******************************************************************************************************
// ReSharper disable CoVariantArrayConversion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GSF;
using GSF.Collections;
using GSF.Communication;
using GSF.Configuration;
using GSF.Parsing;
using GSF.PhasorProtocols;
using GSF.Threading;
using GSF.Units;
using GSF.Units.EE;
using GSF.Windows.Forms;
using Infragistics.UltraChart.Core.Layers;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.Win;
using Infragistics.Win.UltraWinChart;
using Infragistics.Win.UltraWinMaskedEdit;
using Infragistics.Win.UltraWinToolTip;
using Infragistics.Win.UltraWinTree;
using static GSF.IO.FilePath;
using static GSF.Reflection.AssemblyInfo;
using ImageQueue = GSF.Collections.AsyncQueue<GSF.EventArgs<GSF.PhasorProtocols.FundamentalFrameType, byte[], int, int>>;
using Override = Infragistics.Win.UltraWinTree.Override;

// ReSharper disable LocalizableElement
namespace ConnectionTester;

public partial class PMUConnectionTester
{
    public PMUConnectionTester()
    {
        InitializeComponent();
    }

    #region  [ Private Member Declarations ]

    private const int TotalFramesPanel = 1;
    private const int FramesPerSecondPanel = 3;
    private const int TotalBytesPanel = 5;
    private const int BitRatePanel = 7;
    private const int QueuedBuffersPanel = 9;
    private const int TextFileWidth = 75;

    private enum ChartTabs
    {
        Graph,
        Settings,
        Messages,
        ProtocolSpecific
    }

    private enum ProtocolTabs
    {
        Protocol,
        ExtraParameters
    }

    // Phasor parsing variables
    private MultiProtocolFrameParser m_frameParser;
    private ImageQueue m_imageQueue;
    private ShortSynchronizedOperation m_processConfigFrame;
    private IConfigurationFrame m_updatedConfigFrame;
    private IConfigurationFrame m_configurationFrame;
    private long m_lastConfigProcessedTime;
    private long m_configChangeTime;
    private IConfigurationCell m_selectedCell;
    private FundamentalFrameType m_lastFrameType;
    private ByteEncoding m_byteEncoding;
    private int m_byteCount;
    private readonly float m_sqrtOf3 = Convert.ToSingle(Math.Sqrt(3d));

    // Application variables
    private ApplicationSettings m_applicationSettings;
    private string m_applicationName;
    private string m_lastFileName;
    private bool m_ipStackIsIPv6;
    private string m_loopbackAddress;
    private Tuple<string, string, string>[] m_networkInterfaces;
    private int m_tcpNetworkInterface;
    private int m_udpNetworkInterface;
    private int m_commandNetworkInterface;
    private bool m_formLoaded;
    private bool m_shuttingDown;

    // Charting data variables
    private DataTable m_frequencyData;
    private DataTable m_phasorData;
    private Angle m_lastPhaseAngle;
    private long m_lastRefresh;

    // Attribute tree data variables
    private ConcurrentDictionary<FundamentalFrameType, IChannelFrame> m_attributeFrames;
    private ConcurrentDictionary<string, UltraTreeNode> m_associatedNodes;

    // File capture variables
    private FileStream m_frameCaptureStream;
    private StreamWriter m_frameSampleStream;
    private string m_frameCaptureFileName;
    private StreamWriter m_streamDebugCapture;
    private byte m_capturedFrames;
    private object m_dataStreamLock;
    private string m_lastConnectionFileName;

    #endregion

    #region  [ Private Methods Implementation ]

    #region  [ Primary Form Event Handlers ]

    private void PMUConnectionTester_Load(object sender, EventArgs e)
    {
        // We display a custom message for unhandled exceptions
        GlobalExceptionLogger.ErrorTextMethod = UnhandledExceptionErrorMessage;

        // Make sure application settings exist
        m_applicationSettings = new ApplicationSettings();
        m_applicationSettings.PhaseAngleColorsChanged += m_chartSettings_PhaseAngleColorsChanged;

        // Create a new multi-protocol frame parser
        m_frameParser = new MultiProtocolFrameParser();
        m_frameParser.ReceivedFrameBufferImage += m_frameParser_ReceivedFrameBufferImage;
        m_frameParser.ReceivedConfigurationFrame += m_frameParser_ReceivedConfigurationFrame;
        m_frameParser.ReceivedDataFrame += m_frameParser_ReceivedDataFrame;
        m_frameParser.ReceivedHeaderFrame += m_frameParser_ReceivedHeaderFrame;
        m_frameParser.ReceivedCommandFrame += m_frameParser_ReceivedCommandFrame;
        m_frameParser.SentCommandFrame += m_frameParser_ReceivedCommandFrame;
        m_frameParser.ReceivedUndeterminedFrame += m_frameParser_ReceivedUndeterminedFrame;
        m_frameParser.ParsingException += m_frameParser_DataStreamException;
        m_frameParser.ConfigurationChanged += m_frameParser_ConfigurationChanged;
        m_frameParser.ConnectionEstablished += m_frameParser_Connected;
        m_frameParser.ConnectionException += m_frameParser_ConnectionException;
        m_frameParser.ExceededParsingExceptionThreshold += m_frameParser_ExceededParsingExceptionThreshold;
        m_frameParser.ConnectionTerminated += m_frameParser_Disconnected;
        m_frameParser.ServerStarted += m_frameParser_ServerStarted;
        m_frameParser.ServerStopped += m_frameParser_ServerStopped;

        // Create a synchronized operation for processing configuration frames
        m_processConfigFrame = new ShortSynchronizedOperation(PaceConfigFrameProcessing, ex => AppendStatusMessage($"Exception occurred while attempting to process configuration frame: {ex.Message}"));

        // Initialize attribute tree variables
        m_attributeFrames = new ConcurrentDictionary<FundamentalFrameType, IChannelFrame>();
        m_associatedNodes = new ConcurrentDictionary<string, UltraTreeNode>();
        m_dataStreamLock = new object();

        // Make sure a folder exists for PMU Connection Tester files
        string personalDataFolder = AddPathSuffix(GetApplicationDataFolder());

        if (!Directory.Exists(personalDataFolder))
            Directory.CreateDirectory(personalDataFolder);

        // Make sure a last run connection file exists in personal folder
        m_lastConnectionFileName = $"{personalDataFolder}LastRun.PmuConnection";

        if (!File.Exists(m_lastConnectionFileName))
            File.Copy(GetAbsolutePath("LastRun.PmuConnection"), m_lastConnectionFileName);

    #if DEBUG
        LabelVersion.Text = "DEBUG VERSION";
    #else
        Version version = EntryAssembly.Version;
        LabelVersion.Text = $"Version {version.Major}.{version.Minor}.{version.Build}"; // + "." & version.Revision;
    #endif

        // Initialize phasor protocol selection list
        foreach (PhasorProtocol protocol in Enum.GetValues(typeof(PhasorProtocol)).Cast<PhasorProtocol>())
            ComboBoxProtocols.Items.Add(protocol.GetFormattedProtocolName());

        // Initialize serial port selection lists
        foreach (string port in SerialPort.GetPortNames())
            ComboBoxSerialPorts.Items.Add(port);

        foreach (string parity in Enum.GetNames(typeof(Parity)))
            ComboBoxSerialParities.Items.Add(parity);

        foreach (string stopbit in Enum.GetNames(typeof(StopBits)))
        {
            if (!stopbit.Equals("None", StringComparison.OrdinalIgnoreCase))
                ComboBoxSerialStopBits.Items.Add(stopbit);
        }

        // Properly position extra connection parameters group box.  Since it's
        // a top-most control we move it off to the side at design time
        GroupBoxProtocolParameters.Location = new Point(198, 63);

        // Adjust size of comments area of extra connection parameters properties grid
        PropertyGridProtocolParameters.AdjustCommentAreaHeight(6);

        // Hide protocol panel after comment area size is adjusted - should only be visible if extra parameters tab is selected
        GroupBoxProtocolParameters.Visible = false;

        // Assign application settings to property grid
        PropertyGridApplicationSettings.SelectedObject = m_applicationSettings;

        // Adjust size of comments area and label column ratio of application settings properties grid
        PropertyGridApplicationSettings.AdjustCommentAreaHeight(4);
        PropertyGridApplicationSettings.AdjustLabelRatio(1.62d);

        try
        {
            // Determine if default IP stack is IPv6
            m_ipStackIsIPv6 = Transport.GetDefaultIPStack() == IPStack.IPv6;

            LabelDefaultIPStack.Text = m_ipStackIsIPv6 ?
                "Default System IP Stack: IPv6" :
                "Default System IP Stack: IPv4";
        }
        catch
        {
            // Default to IPv4
            m_ipStackIsIPv6 = false;
            LabelDefaultIPStack.Text = "Default System IP Stack: IPv4";
        }

        // Setup IP mode
        ForceIPv4 = m_applicationSettings.ForceIPv4;

        // Assign default loopback addresses
        TextBoxTcpHostIP.Text = m_loopbackAddress;
        TextBoxUdpHostIP.Text = m_loopbackAddress;

        Forms.AlternateCommandChannel.TextBoxTcpHostIP.Text = m_loopbackAddress;
        Forms.MulticastSourceSelector.TextBoxMulticastSourceIP.Text = m_loopbackAddress;
        Forms.ReceiveFromSourceSelector.TextBoxUdpSourceIP.Text = m_loopbackAddress;

        InitializeNetworkInterfaces();
        InitializeChart();

        ComboBoxProtocols.SelectedIndex = 0;
        ComboBoxCommands.SelectedIndex = 0;
        ComboBoxByteEncodingDisplayFormats.SelectedIndex = 0;
        GroupBoxConfigurationFrame.Expanded = false;
        GroupBoxHeaderFrame.Expanded = false;
        GroupBoxStatus.Expanded = false;
        LabelBinaryFrameImage.Tag = LabelBinaryFrameImage.Text;

        if (ComboBoxSerialPorts.Items.Count > 0)
            ComboBoxSerialPorts.SelectedIndex = 0;

        ComboBoxSerialBaudRates.SelectedIndex = 0;
        ComboBoxSerialParities.SelectedIndex = 0;
        ComboBoxSerialStopBits.SelectedIndex = 0;
        LabelSelectedIsRefAngle.Visible = m_applicationSettings.PhaseAngleGraphStyle == ApplicationSettings.AngleGraphStyle.Relative;

        if (Environment.GetCommandLineArgs().Length > 1)
        {
            string commandFile = Environment.GetCommandLineArgs()[1];

            switch (Path.GetExtension(commandFile).ToLower().Trim())
            {
                case ".pmuconnection":
                    LoadConnectionSettings(commandFile);
                    break;

                case ".pmucapture":
                    TextBoxFileCaptureName.Text = commandFile;
                    TabControlCommunications.Tabs[(int)TransportProtocol.File].Selected = true;
                    break;
            }
        }
        else if (m_applicationSettings.RestoreLastConnectionSettings && File.Exists(m_lastConnectionFileName))
        {
            LoadConnectionSettings(m_lastConnectionFileName);
        }

        this.RestoreLayout();
    }

    private void PMUConnectionTester_FormClosing(object sender, FormClosingEventArgs e)
    {
        Hide();
        Application.DoEvents();

        try
        {
            // Save application settings
            m_applicationSettings.Save();

            // Save current connection settings
            SaveConnectionSettings(m_lastConnectionFileName);

            // Save current window settings
            this.SaveLayout();
        }
        catch
        {
            // Don't want any possible failures during this event to prevent shutdown :)
        }

        Shutdown();
    }

    private void PMUConnectionTester_Shown(object sender, EventArgs e)
    {
        TimerDelay.Start();
    }

    private void TimerDelay_Tick(object sender, EventArgs e)
    {
        // We show the default IP stack label on startup only after a momentary pause - this prevents the visual side effect
        // of having the label's gray background stand out over a temporary white background
        LabelDefaultIPStack.Visible = GroupBoxConnection.Expanded;
        TimerDelay.Stop();

        Forms.SplashScreen.Hide();

        if (m_formLoaded)
            return;

        m_formLoaded = true;
        BeginInvoke(new Action(() => ExtraParametersToolTipVisible(m_frameParser?.ConnectionParameters is not null)));
    }

    // We allow file drops from explorer onto connection tester
    private void PMUConnectionTester_DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
    }

    private void PMUConnectionTester_DragDrop(object sender, DragEventArgs e)
    {
        try
        {
            if (e.Data.GetData(DataFormats.FileDrop) is not Array fileNames)
                return;

            // Only dealing with first file dropped on the form
            string fileName = fileNames.GetValue(0).ToString();

            if (!File.Exists(fileName))
                return;

            if (Path.GetExtension(fileName).ToLower().Trim() == ".pmuconnection")
            {
                // Deserializing connection settings may take a moment and explorer thread will be pending,
                // so we queue load operation on the form's invocation queue
                BeginInvoke(new Action<string>(LoadConnectionSettings), fileName);
            }
            else
            {
                // All other files are assumed to be a capture file
                TextBoxFileCaptureName.Text = fileName;
                TabControlCommunications.Tabs[(int)TransportProtocol.File].Selected = true;
            }

            // Show form in case explorer is overlapping
            Activate();
        }
        catch (Exception ex)
        {
            AppendStatusMessage($"Exception occurred while dropping file name: {ex.Message}");
        }
    }

    private void ButtonListen_Click(object sender, EventArgs e)
    {
        if (m_frameParser.Enabled)
            Disconnect();
        else
            Connect();
    }

    private void ButtonSendCommand_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBoxRawCommand.Visible)
            {
                // Hexadecimal and decimal values are both accepted.
                // Hexadecimal values have to be prefixed with "0x"
                int rawCommandValue;

                if (System.Text.RegularExpressions.Regex.IsMatch(TextBoxRawCommand.Text, @"0[xX][0-9a-fA-F]+\b\Z"))
                {
                    rawCommandValue = Convert.ToInt32(TextBoxRawCommand.Text, 16);
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(TextBoxRawCommand.Text, "[0-9]"))
                {
                    rawCommandValue = Convert.ToInt32(TextBoxRawCommand.Text, 10);

                    if (rawCommandValue > ushort.MaxValue)
                        throw new Exception("The value entered is too big. Maximum value is 65535 (0xFFFF).");
                }
                else
                {
                    throw new Exception("The custom user command value is invalid");
                }

                SendRawDeviceCommand((ushort)rawCommandValue);
            }
            else
            {
                SendDeviceCommand((DeviceCommand)(ComboBoxCommands.SelectedIndex + 1));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ComboBoxPmus_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lastSelectedCellName = GetCellName(m_selectedCell);
        m_selectedCell = null;

        if (m_configurationFrame is null)
            return;

        m_selectedCell = m_configurationFrame.Cells[ComboBoxPmus.SelectedIndex];
        LabelIDCode.Text = m_selectedCell.IDCode.ToString();
        LabelPhasorCount.Text = m_selectedCell.PhasorDefinitions.Count.ToString();
        LabelAnalogCount.Text = m_selectedCell.AnalogDefinitions.Count.ToString();
        LabelDigitalCount.Text = m_selectedCell.DigitalDefinitions.Count.ToString();
        LabelNominalFrequency.Text = ((int)m_selectedCell.NominalFrequency).ToString();
        ComboBoxVoltagePhasors.Items.Clear();
        ComboBoxCurrentPhasors.Items.Clear();

        lock (ComboBoxPhasors)
        {
            // Try to maintain selected phasor index if PMU device selection remains the same
            int selectedIndex = lastSelectedCellName.Equals(GetCellName(m_selectedCell)) ? ComboBoxPhasors.SelectedIndex : -1;

            ComboBoxPhasors.Items.Clear();

            PhasorDefinitionCollection phasorDefinitions = m_selectedCell.PhasorDefinitions;

            for (int i = 0; i < phasorDefinitions.Count; i++)
            {
                IPhasorDefinition phasor = phasorDefinitions[i];

                if (phasor.PhasorType == PhasorType.Voltage)
                {
                    ComboBoxPhasors.Items.Add($"V: {phasor.Label}");
                    ComboBoxVoltagePhasors.Items.Add($"{i}: {phasor.Label}");
                }
                else
                {
                    ComboBoxPhasors.Items.Add($"I: {phasor.Label}");
                    ComboBoxCurrentPhasors.Items.Add($"{i}: {phasor.Label}");
                }
            }

            ComboBoxPhasors.Tag = ComboBoxPhasors.Items.Cast<string>().ToArray();

            if (ComboBoxPhasors.Items.Count > 0)
            {
                if (selectedIndex >= 0 && selectedIndex < ComboBoxPhasors.Items.Count)
                    ComboBoxPhasors.SelectedIndex = selectedIndex;
                else
                    ComboBoxPhasors.SelectedIndex = 0;
            }
            else
            {
                ComboBoxPhasors.SelectedIndex = -1;
            }
        }
        
        if (ComboBoxVoltagePhasors.Items.Count > 0)
            ComboBoxVoltagePhasors.SelectedIndex = 0;
        else
            ComboBoxVoltagePhasors.SelectedIndex = -1;

        if (ComboBoxCurrentPhasors.Items.Count > 0)
            ComboBoxCurrentPhasors.SelectedIndex = 0;
        else
            ComboBoxCurrentPhasors.SelectedIndex = -1;

        InitializePhaseAngleLayer(m_selectedCell.PhasorDefinitions.Count);
    }

    private void ComboBoxPhasors_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_phasorData.Rows.Clear();
        m_lastPhaseAngle = 0.0D;
    }

    private void ComboBoxPhasors_Enter(object sender, EventArgs e)
    {
        lock (ComboBoxPhasors)
            ComboBoxPhasors.SelectAll();
    }

    private void ComboBoxPhasors_DropDownClosed(object sender, EventArgs e)
    {
        lock (ComboBoxPhasors)
        {
            int selectedIndex = GetComboBoxPhasorsSelectedIndex();

            ComboBoxPhasors.Items.Clear();
         
            if (ComboBoxPhasors.Tag is string[] elements)
                ComboBoxPhasors.Items.AddRange(elements);

            ComboBoxPhasors.SelectedIndex = selectedIndex;
        }
    }

    private void ComboBoxPhasors_TextUpdate(object sender, EventArgs e)
    {
        lock (ComboBoxPhasors)
        {
            if (ComboBoxPhasors.Tag is not string[] elements)
                return;

            string searchText = ComboBoxPhasors.Text;

            if (string.IsNullOrEmpty(searchText))
            {
                ComboBoxPhasors.Items.Clear();
                ComboBoxPhasors.Items.AddRange(elements);
                
                if (ComboBoxPhasors.Items.Count > 0)
                    ComboBoxPhasors.SelectedIndex = 0;
                else
                    ComboBoxPhasors.SelectedIndex = -1;

                ComboBoxPhasors.SelectAll();
                return;
            }

            if (!ComboBoxPhasors.DroppedDown)
                ComboBoxPhasors.DroppedDown = true;

            string[] updatedComboBoxItems = elements
                .Where(item => item.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(item => item)
                .ToArray();

            // Removes every element from the combobox control, Combobox.Items.Clear causes the cursor position to reset
            foreach (string element in elements)
                ComboBoxPhasors.Items.Remove(element);

            // Re-adds all the element in order
            ComboBoxPhasors.Items.AddRange(updatedComboBoxItems);
        }
    }

    private int GetComboBoxPhasorsSelectedIndex()
    {
        if (InvokeRequired)
            return (int)Invoke(GetComboBoxPhasorsSelectedIndex);

        lock (ComboBoxPhasors)
        {
            int index = ComboBoxPhasors.SelectedIndex;

            if (index < 0)
                return ComboBoxPhasors.Items.Count > 0 ? 0 : index;

            string selectedItem = ComboBoxPhasors.Items[index].ToString();

            return ComboBoxPhasors.Tag is string[] elements ?
                elements.IndexOf(item => item.Equals(selectedItem)) : index;
        }
    }

    private void ComboBoxByteEncodingDisplayFormats_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_byteEncoding = ComboBoxByteEncodingDisplayFormats.SelectedIndex switch
        {
            0 => ByteEncoding.Hexadecimal,
            1 => ByteEncoding.Decimal,
            2 => ByteEncoding.BigEndianBinary,
            3 => ByteEncoding.LittleEndianBinary,
            4 => ByteEncoding.Base64,
            5 => ByteEncoding.ASCII,
            _ => ByteEncoding.Hexadecimal
        };
    }

    private void ComboBoxProtocols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_frameParser is not null)
        {

            // Assign new protocol selection to frame parser - this will retrieve a default set of connection
            // parameters for the protocol if any are available
            m_frameParser.PhasorProtocol = (PhasorProtocol)ComboBoxProtocols.SelectedIndex;

            if (m_frameParser.ConnectionParameters is null)
            {
                // Protocol has no extra connection parameters - hide extra parameters tab
                TabControlProtocolParameters.Tabs[(int)ProtocolTabs.ExtraParameters].Visible = false;
                PropertyGridProtocolParameters.SelectedObject = null;

                // Hide tool tip for protocol's with no extra parameters
                ExtraParametersToolTipVisible(false);
            }
            else
            {
                // Protocol has extra connection parameters - show extra parameters tab
                TabControlProtocolParameters.Tabs[(int)ProtocolTabs.ExtraParameters].Visible = true;
                PropertyGridProtocolParameters.SelectedObject = m_frameParser.ConnectionParameters;


                // Show tool tip for protocol's with extra parameters (for availability emphasis)
                ExtraParametersToolTipVisible(true);
            }
        }

        switch (ComboBoxProtocols.SelectedIndex)
        {
            case (int)PhasorProtocol.FNET:
            {
                // Because users may be unfamiliar with typical FNET device connection requirements, we default
                // to the typical connection settings when user selects FNET from the list
                if (TabControlCommunications.SelectedTab.Index == (int)TransportProtocol.File)
                {
                    TextBoxFileFrameRate.Text = "10";
                }
                else
                {
                    TabControlCommunications.Tabs[(int)TransportProtocol.Tcp].Selected = true;
                    CheckBoxEstablishTcpServer.Checked = true;
                }

                ComboBoxByteEncodingDisplayFormats.SelectedIndex = 5;
                break;
            }
            case (int)PhasorProtocol.BPAPDCstream:
            {
                // If user selects the BPA protocol when a DST file is selected for input, make sure to automatically set the UsePhasorDataFileFormat to true
                if (TabControlCommunications.SelectedTab.Index == (int)TransportProtocol.File & GetExtension(TextBoxFileCaptureName.Text).ToLower() == ".dst")
                {
                    if (m_frameParser?.ConnectionParameters is GSF.PhasorProtocols.BPAPDCstream.ConnectionParameters connectionParameters)
                        connectionParameters.UsePhasorDataFileFormat = true;
                }

                break;
            }
            case (int)PhasorProtocol.Macrodyne:
            {
                // Macrodyne connections work best when real-time data is disabled before sending other commands
                m_applicationSettings.SkipDisableRealTimeData = false;
                PropertyGridApplicationSettings.Refresh();
                break;
            }
        }
    }

    private void ComboBoxCommands_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Enable and make visible TextBoxCustomCommand only when the "Send Custom Command" command is selected
        TextBoxRawCommand.Visible = ComboBoxCommands.SelectedIndex == 6;
        TextBoxRawCommand.Enabled = ComboBoxCommands.SelectedIndex == 6;

        // Some protocols only support enable and disable real-time data commands...
        // If ComboBoxProtocols.SelectedIndex >= 4 And ComboBoxCommands.SelectedIndex > 1 Then
        // MsgBox("This protocol only supports enable and disable real-time data commands.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Invalid Command Selection")
        // ComboBoxCommands.SelectedIndex = 0
        // End If
    }

    private void CheckBoxEstablishTcpServer_CheckedChanged(object sender, EventArgs e)
    {
        bool needsHostIP = !CheckBoxEstablishTcpServer.Checked;

        TextBoxTcpHostIP.Enabled = needsHostIP;
        LabelTcpHostIP.Enabled = needsHostIP;

        if (!needsHostIP)
            TextBoxTcpHostIP.Text = m_loopbackAddress;
    }

    private void CheckBoxRemoteUDPServer_CheckedChanged(object sender, EventArgs e)
    {
        bool needsHostIP = CheckBoxRemoteUdpServer.Checked;

        TextBoxUdpHostIP.Enabled = needsHostIP;
        LabelUdpHostIP.Enabled = needsHostIP;
        TextBoxUdpRemotePort.Enabled = needsHostIP;
        LabelUdpRemotePort.Enabled = needsHostIP;
        LabelMulticastSource.Enabled = needsHostIP;

        if (!needsHostIP)
            TextBoxUdpHostIP.Text = m_loopbackAddress;
    }

    private void CheckBoxAutoRepeatPlayback_CheckChanged(object sender, EventArgs e)
    {
        if (m_frameParser is null)
            return;

        // Update state in frame parser
        m_frameParser.AutoRepeatCapturedPlayback = CheckBoxAutoRepeatPlayback.Checked;
        PropertyGridApplicationSettings.Refresh();
    }

    private void ButtonGetStatus_Click(object sender, EventArgs e)
    {
        TabControlChart.Tabs[(int)ChartTabs.Messages].Selected = true;

        if (m_frameParser is null)
            AppendStatusMessage($"{Environment.NewLine}Currently not connected to any device.");
        else
            AppendStatusMessage(Environment.NewLine + m_frameParser.Status);
    }

    private void ButtonRestoreDefaultSettings_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(this, $"Are you sure you want to restore the default settings?{Environment.NewLine}{Environment.NewLine}Note that application will be closed and you will need to restart.", "Restore Default Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            string userSettingsFile = Path.Combine(GetApplicationDataFolder(), "Settings.xml");

            Disconnect();

            ConfigurationFile.Current.Save(System.Configuration.ConfigurationSaveMode.Full);

            if (File.Exists(userSettingsFile))
                File.Delete(userSettingsFile);

            Shutdown();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Exception occurred while trying to restore default settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void ButtonBrowse_Click(object sender, EventArgs e)
    {
        OpenFileDialog fileDialog = OpenFileDialog;

        fileDialog.Title = "Open Capture File";
        fileDialog.Filter = "Captured Files (*.PmuCapture)|*.PmuCapture|BPA Phasor Data Files (*.dst)|*.dst|All Files|*.*";
        fileDialog.FileName = "";
        fileDialog.CheckFileExists = true;

        if (fileDialog.ShowDialog(this) == DialogResult.OK)
            TextBoxFileCaptureName.Text = fileDialog.FileName;
    }

    private void TabControlChart_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
    {
        // We load protocol specific data attribute tree when user selects that tab...
        if (e.Tab.Index == (int)ChartTabs.ProtocolSpecific && m_attributeFrames.Count > 0 && TreeFrameAttributes.Nodes.Count == 0)
            InitializeAttributeTree();
    }

    private void TabControlProtocolParameters_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
    {
        switch (e.Tab.Index)
        {
            case (int)ProtocolTabs.Protocol:
                GroupBoxProtocolParameters.Visible = false;
                ComboBoxProtocols.Focus();
                break;

            case (int)ProtocolTabs.ExtraParameters:
                GroupBoxProtocolParameters.Visible = true;
                PropertyGridProtocolParameters.Focus();
                break;
        }
    }

    private void GroupBoxProtocolParameters_Paint(object sender, PaintEventArgs e)
    {
        // Draw two short vertical lines to "complete" fake tab that displays extra connection parameters
        Graphics graphics = e.Graphics;
        graphics.DrawLine(Pens.Gray, 178, 0, 178, 1);
        graphics.DrawLine(Pens.Gray, 275, 0, 275, 1);
    }

    private void CalculatedPowerOrVarLabel_Click(object sender, EventArgs e)
    {
        TabControlChart.Tabs[(int)ChartTabs.Settings].Selected = true;
        ComboBoxCurrentPhasors.Select();
    }

    private void LabelTcpNetworkInterface_Click(object sender, EventArgs e)
    {
        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex = m_tcpNetworkInterface;

        if (Forms.NetworkInterfaceSelector.ShowDialog(this) == DialogResult.OK)
            m_tcpNetworkInterface = Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex;

        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex = -1;
    }

    private void LabelUdpNetworkInterface_Click(object sender, EventArgs e)
    {
        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex = m_udpNetworkInterface;

        if (Forms.NetworkInterfaceSelector.ShowDialog(this) == DialogResult.OK)
            m_udpNetworkInterface = Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex;

        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex = -1;
    }

    private void LabelMulticastSource_Click(object sender, EventArgs e)
    {
        Forms.MulticastSourceSelector.ShowDialog(this);
    }

    private void LabelReceiveFrom_Click(object sender, EventArgs e)
    {
        Forms.ReceiveFromSourceSelector.ShowDialog(this);
    }

    private void LabelAlternateCommandChannel_Click(object sender, EventArgs e)
    {
        string connectionString = CurrentConnectionSettings.ConnectionString;

        Forms.AlternateCommandChannel.NetworkInterface = m_commandNetworkInterface;

        // Show alternate command channel settings as a modal dialog
        if (Forms.AlternateCommandChannel.ShowDialog(this) == DialogResult.Cancel)
        {
            // User canceled changes, reapply original settings to dialog
            Forms.AlternateCommandChannel.ConnectionString = connectionString;
        }
        else
        {
            m_commandNetworkInterface = Forms.AlternateCommandChannel.NetworkInterface;
        }

        UpdateAlternateCommandChannelLabel();
    }

    private void LabelAlternateCommandChannelState_Click(object sender, EventArgs e)
    {
        // Assume user simply wants to change defined state when clicking on label
        Forms.AlternateCommandChannel.CheckBoxUndefined.Checked = Forms.AlternateCommandChannel.IsDefined;
        UpdateAlternateCommandChannelLabel();
    }

    private void TextBox_GotFocus(object sender, EventArgs e)
    {
        switch (sender)
        {
            // Select all text box contents upon focus or selection
            case UltraMaskedEdit { EditAs: EditAsType.UseSpecifiedMask } maskedEdit:
                maskedEdit.SelectAll();
                break;
            case UltraMaskedEdit maskedEdit:
                // maskedEdit.SelectionStart = 0
                // maskedEdit.SelectionLength = maskedEdit.Text.Trim().Length
                maskedEdit.Select(0, maskedEdit.Text.Trim().Length);
                break;
            case TextBox windowsTextBox:
                windowsTextBox.SelectAll();
                break;
        }
    }

    private void TextBox_MouseClick(object sender, MouseEventArgs e)
    {
        TextBox_GotFocus(sender, e);
    }

    private void TextBoxFileCaptureName_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TextBoxFileCaptureName.Text))
            return;

        if (GetExtension(TextBoxFileCaptureName.Text).ToLower() != ".dst")
            return;

        ComboBoxProtocols.SelectedIndex = (int)PhasorProtocol.BPAPDCstream;

        if (m_frameParser.ConnectionParameters is GSF.PhasorProtocols.BPAPDCstream.ConnectionParameters connectionParameters)
            connectionParameters.UsePhasorDataFileFormat = true;
    }

    private void PropertyGridApplicationSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        // Dynamically update visual elements as necessary when application settings are updated
        PropertyDescriptor descriptor = e.ChangedItem.PropertyDescriptor;

        if (descriptor is null)
            return;

        switch (descriptor.Category ?? "")
        {
            case ApplicationSettings.ApplicationSettingsCategory:
            {
                if (string.Equals(descriptor.Name, nameof(ApplicationSettings.ForceIPv4), StringComparison.OrdinalIgnoreCase))
                    ForceIPv4 = m_applicationSettings.ForceIPv4; // Apply change in IP mode

                break;
            }
            case ApplicationSettings.ConnectionSettingsCategory:
            {
                // We allow run-time dynamic changes to some frame parsing states
                if (string.Equals(descriptor.Name, nameof(ApplicationSettings.InjectSimulatedTimestamp), StringComparison.OrdinalIgnoreCase))
                {
                    m_frameParser.InjectSimulatedTimestamp = m_applicationSettings.InjectSimulatedTimestamp;
                }
                else if (string.Equals(descriptor.Name, nameof(ApplicationSettings.UseHighResolutionInputTimer), StringComparison.OrdinalIgnoreCase))
                {
                    m_frameParser.UseHighResolutionInputTimer = m_applicationSettings.UseHighResolutionInputTimer;

                    // Change in UseHighResolutionInputTimer may not be allowed if debugger is attached, so we restore accepted state to application settings
                    m_applicationSettings.UseHighResolutionInputTimer = m_frameParser.UseHighResolutionInputTimer;
                }
                else if (string.Equals(descriptor.Name, nameof(ApplicationSettings.AlternateInterfaces), StringComparison.OrdinalIgnoreCase))
                {
                    InitializeNetworkInterfaces();
                }
                else if (string.Equals(descriptor.Name, nameof(ApplicationSettings.ConfigurationFrameVersion), StringComparison.OrdinalIgnoreCase))
                {
                    m_frameParser.ConfigurationFrameVersion = m_applicationSettings.ConfigurationFrameVersion switch
                    {
                        1 => DeviceCommand.SendConfigurationFrame1,
                        2 => DeviceCommand.SendConfigurationFrame2,
                        3 => DeviceCommand.SendConfigurationFrame3,
                        _ => DeviceCommand.SendLatestConfigurationFrameVersion
                    };
                }

                break;
            }
            case ApplicationSettings.ChartSettingsCategory:
            case ApplicationSettings.PhaseAngleGraphCategory:
            case ApplicationSettings.FrequencyGraphCategory:
            {
                if (string.Equals(descriptor.Name, "PhaseAngleGraphStyle", StringComparison.OrdinalIgnoreCase))
                {
                    // This property only changes data that is getting graphed - no need to reinitialize chart
                    LabelSelectedIsRefAngle.Visible = m_applicationSettings.PhaseAngleGraphStyle == ApplicationSettings.AngleGraphStyle.Relative;
                }
                else
                {
                    InitializeChart();
                }

                // We show chart tab (if data is available) to display changes on any chart property change
                if (m_selectedCell is not null)
                    TabControlChart.Tabs[(int)ChartTabs.Graph].Selected = true;

                break;
            }
        }
    }

    #region [ Menu Event Handlers ]

    private void MenuItemLoadConnection_Click(object sender, EventArgs e)
    {
        OpenFileDialog dialog = OpenFileDialog;

        dialog.Title = "Load Connection Settings";
        dialog.Filter = "Connection Files (*.PmuConnection)|*.PmuConnection|All Files (*.*)|*.*";
        dialog.FileName = "";
        dialog.CheckFileExists = true;

        if (dialog.ShowDialog(this) == DialogResult.OK)
            LoadConnectionSettings(dialog.FileName);
    }

    private void MenuItemSaveConnection_Click(object sender, EventArgs e)
    {
        SaveFileDialog dialog = SaveFileDialog;

        dialog.Title = "Save Connection Settings";
        dialog.Filter = "Connection Files (*.PmuConnection)|*.PmuConnection|All Files (*.*)|*.*";
        dialog.FileName = "";

        if (dialog.ShowDialog(this) == DialogResult.OK)
            SaveConnectionSettings(dialog.FileName);
    }

    private void MenuItemLoadConfigFile_Click(object sender, EventArgs e)
    {
        OpenFileDialog dialog = OpenFileDialog;

        dialog.Title = "Load Configuration File";
        dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
        dialog.FileName = "";
        dialog.CheckFileExists = true;

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        FileStream configFile = File.Open(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        {
            SoapFormatter formatter = new()
            {
                AssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeFormat = FormatterTypeStyle.TypesWhenNeeded,
                Binder = Serialization.LegacyBinder
            };

            try
            {
                m_configurationFrame = null;

                lock (m_processConfigFrame)
                {
                    m_updatedConfigFrame = (IConfigurationFrame)formatter.Deserialize(configFile);
                    m_processConfigFrame.RunOnceAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to deserialize configuration frame: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        configFile.Close();
    }

    private void MenuItemSaveConfigFile_Click(object sender, EventArgs e)
    {
        if (m_configurationFrame is null)
        {
            MessageBox.Show(this, "No configuration file has been received or loaded...", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            SaveFileDialog dialog = SaveFileDialog;

            dialog.Title = "Save Configuration File";
            dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            dialog.FileName = "";

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            FileStream configFile = File.Create(dialog.FileName);
            {
                SoapFormatter formatter = new()
                {
                    AssemblyFormat = FormatterAssemblyStyle.Simple,
                    TypeFormat = FormatterTypeStyle.TypesWhenNeeded
                };

                try
                {
                    formatter.Serialize(configFile, m_configurationFrame);
                }
                catch (Exception ex)
                {
                    byte[] message = Encoding.UTF8.GetBytes(ex.Message);
                    configFile.Write(message, 0, message.Length);
                    MessageBox.Show(this, $"Failed to serialize configuration frame: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            configFile.Close();

            // Open captured XML sample file in explorer...
            if (m_applicationSettings.ShowConfigXmlExplorerAfterSave)
                Process.Start("explorer.exe", dialog.FileName);
        }
    }

    private void MenuItemStartCapture_Click(object sender, EventArgs e)
    {
        SaveFileDialog dialog = SaveFileDialog;

        dialog.Title = "Set Capture File Name";
        dialog.Filter = "Captured Files (*.PmuCapture)|*.PmuCapture|All Files|*.*";
        dialog.FileName = "";

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            m_frameCaptureStream = new FileStream(dialog.FileName, FileMode.Create);
            MenuItemStartCapture.Enabled = false;
            MenuItemStopCapture.Enabled = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Failed to start capturing data to \"{dialog.FileName}\": {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void MenuItemStopCapture_Click(object sender, EventArgs e)
    {
        m_frameCaptureStream?.Close();

        m_frameCaptureStream = null;
        MenuItemStopCapture.Enabled = false;
        MenuItemStartCapture.Enabled = true;
    }

    private void MenuItemStartStreamDebugCapture_Click(object sender, EventArgs e)
    {
        SaveFileDialog dialog = SaveFileDialog;

        dialog.Title = "Set Stream Debug Capture File Name";
        dialog.Filter = "Debug Captured Files (*.csv)|*.csv|All Files|*.*";
        dialog.FileName = "";

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            m_streamDebugCapture = File.CreateText(dialog.FileName);
            MenuItemStartStreamDebugCapture.Enabled = false;
            MenuItemStopStreamDebugCapture.Enabled = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Failed to start stream debug capture to \"{dialog.FileName}\": {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void MenuItemStopStreamDebugCapture_Click(object sender, EventArgs e)
    {
        m_streamDebugCapture?.Close();

        m_streamDebugCapture = null;
        MenuItemStopStreamDebugCapture.Enabled = false;
        MenuItemStartStreamDebugCapture.Enabled = true;
    }

    private void MenuItemCaptureSampleFrames_Click(object sender, EventArgs e)
    {
        SaveFileDialog dialog = SaveFileDialog;

        dialog.Title = "Set Sample Frames Capture File Name";
        dialog.Filter = "Captured Frames (*.txt)|*.txt|All Files|*.*";
        dialog.FileName = "";

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        m_frameCaptureFileName = dialog.FileName;
        MenuItemCancelSampleFrameCapture.Enabled = true;

        lock (m_dataStreamLock)
        {
            m_capturedFrames = 0;

            m_frameSampleStream = File.CreateText(m_frameCaptureFileName);

            m_frameSampleStream.WriteLine($"Sample Frame Capture - {DateTime.Now}");
            m_frameSampleStream.WriteLine();
            m_frameSampleStream.WriteLine($"Device Connection: {ConnectionInformation}");
            m_frameSampleStream.WriteLine();
            m_frameSampleStream.WriteLine(new string('*', TextFileWidth));
            m_frameSampleStream.WriteLine();
        }

        MessageBox.Show(this, "Note that one sample for each type of frame encountered will be captured until a configuration frame is received.  Reception of a configuration frame will terminate the sample capture.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void MenuItemCancelSampleFrameCapture_Click(object sender, EventArgs e)
    {
        CloseSampleCapture();
    }

    private void MenuItemLocalHelp_Click(object sender, EventArgs e)
    {
        Help.ShowHelp(this, GetAbsolutePath("PMUConnectionTester.chm"));
    }

    private void MenuItemOnlineHelp_Click(object sender, EventArgs e)
    {
        Process.Start("https://pmuconnectiontester.info/HELP");
    }

    private void MenuItemAbout_Click(object sender, EventArgs e)
    {
        using AboutDialog aboutDialog = new();

        aboutDialog.SetCompanyLogo(EntryAssembly.GetEmbeddedResource($"{nameof(ConnectionTester)}.HelpAboutLogo.png"));
        aboutDialog.SetCompanyDisclaimer(EntryAssembly.GetEmbeddedResource($"{nameof(ConnectionTester)}.Disclaimer.txt"));

        aboutDialog.ShowDialog(this);
    }

    private void MenuItemExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void MenuItemRefresh_Click(object sender, EventArgs e)
    {
        InitializeAttributeTree();
    }

    private void MenuItemExpandAll_Click(object sender, EventArgs e)
    {
        TreeFrameAttributes.Refresh();
        TreeNodesCollection nodes = TreeFrameAttributes.Nodes;

        // Since expanding nodes can be a time-consuming process, we perform expansion on root
        // nodes one at a time - resting between expansions to allow some UI thread time
        foreach (int value in Enum.GetValues(typeof(FundamentalFrameType)))
        {
            string frameKey = $"Frame{(value + 1)}";

            if (!nodes.Exists(frameKey))
                continue;

            nodes[frameKey].ExpandAll(ExpandAllType.OnlyNodesWithChildren);
            Application.DoEvents();
        }
    }

    private void MenuItemCollapseAll_Click(object sender, EventArgs e)
    {
        TreeFrameAttributes.Refresh();
        TreeFrameAttributes.CollapseAll();
    }

    #endregion

    #region [ Control Resizing Event Code ]

    private void PMUConnectionTester_Resize(object sender, EventArgs e)
    {
        UpdateApplicationTitle(null);
    }

    private void GroupBoxStatus_ExpandedStateChanged(object sender, EventArgs e)
    {
        ToolTipManager.GetUltraToolTip(GroupBoxStatus).Enabled =
            GroupBoxStatus.Expanded ? DefaultableBoolean.False : DefaultableBoolean.True;
    }

    private void GroupBoxStatus_ExpandedStateChanging(object sender, CancelEventArgs e)
    {
        if (GroupBoxStatus.Expanded)
        {
            TabControlChart.Height += GroupBoxPanelStatus.Height;
            GroupBoxHeaderFrame.Height += GroupBoxPanelStatus.Height;
            GroupBoxStatus.Top += GroupBoxPanelStatus.Height;
        }
        else
        {
            TabControlChart.Height -= GroupBoxPanelStatus.Height;
            GroupBoxHeaderFrame.Height -= GroupBoxPanelStatus.Height;
            GroupBoxStatus.Top -= GroupBoxPanelStatus.Height;
        }
    }

    private void GroupBoxConnection_ExpandedStateChanging(object sender, CancelEventArgs e)
    {
        if (GroupBoxConnection.Expanded)
        {
            TabControlChart.Top -= GroupBoxPanelConnection.Height;
            TabControlChart.Height += GroupBoxPanelConnection.Height;
            GroupBoxConfigurationFrame.Top -= GroupBoxPanelConnection.Height;
            GroupBoxRealTimePowerVars.Top -= GroupBoxPanelConnection.Height;
            GroupBoxHeaderFrame.Top -= GroupBoxPanelConnection.Height;
            GroupBoxHeaderFrame.Height += GroupBoxPanelConnection.Height;
        }
        else
        {
            TabControlChart.Height -= GroupBoxPanelConnection.Height;
            TabControlChart.Top += GroupBoxPanelConnection.Height;
            GroupBoxConfigurationFrame.Top += GroupBoxPanelConnection.Height;
            GroupBoxRealTimePowerVars.Top += GroupBoxPanelConnection.Height;
            GroupBoxHeaderFrame.Height -= GroupBoxPanelConnection.Height;
            GroupBoxHeaderFrame.Top += GroupBoxPanelConnection.Height;
        }

        LabelDefaultIPStack.Visible = !GroupBoxConnection.Expanded;
    }

    private void GroupBoxHeaderFrame_ExpandedStateChanging(object sender, CancelEventArgs e)
    {
        if (GroupBoxHeaderFrame.Expanded)
        {
            TabControlChart.Width += GroupBoxPanelHeaderFrame.Width;
            GroupBoxHeaderFrame.Left += GroupBoxPanelHeaderFrame.Width;
        }
        else
        {
            TabControlChart.Width -= GroupBoxPanelHeaderFrame.Width;
            GroupBoxHeaderFrame.Left -= GroupBoxPanelHeaderFrame.Width;
        }
    }

    private void GroupBoxConfigurationFrame_ExpandedStateChanging(object sender, CancelEventArgs e)
    {
        const int ExtraOffset = 8;

        if (GroupBoxConfigurationFrame.Expanded)
        {
            TabControlChart.Left -= GroupBoxPanelConfigurationFrame.Width + ExtraOffset;
            TabControlChart.Width += GroupBoxPanelConfigurationFrame.Width + ExtraOffset;
            GroupBoxConfigurationFrame.Left -= ExtraOffset;
        }
        else
        {
            TabControlChart.Left += GroupBoxPanelConfigurationFrame.Width + ExtraOffset;
            TabControlChart.Width -= GroupBoxPanelConfigurationFrame.Width + ExtraOffset;
            GroupBoxConfigurationFrame.Left += ExtraOffset;
        }

        GroupBoxRealTimePowerVars.Visible = !GroupBoxConfigurationFrame.Expanded;
    }

    #endregion

    #endregion

    #region [ Non-Form Thread Event Handlers ]

    // These functions are invoked from a separate threads, so thread-safe methods, e.g., "Invoke",
    // must be used so that visual control elements can be safely manipulated
    private void m_frameParser_ReceivedFrameBufferImage(object sender, EventArgs<FundamentalFrameType, byte[], int, int> e)
    {
        m_imageQueue?.Enqueue(e);
    }

    private void m_frameParser_ReceivedConfigurationFrame(object sender, EventArgs<IConfigurationFrame> e)
    {
        lock (m_processConfigFrame)
        {
            m_updatedConfigFrame = e.Argument;
            m_processConfigFrame.RunOnceAsync();
        }
    }

    private void m_frameParser_ReceivedDataFrame(object sender, EventArgs<IDataFrame> e)
    {
        BeginInvoke(new Action<IDataFrame>(ReceivedDataFrame), e.Argument);
    }

    private void m_frameParser_ReceivedHeaderFrame(object sender, EventArgs<IHeaderFrame> e)
    {
        BeginInvoke(new Action<IHeaderFrame>(ReceivedHeaderFrame), e.Argument);
    }

    // Note we use the same function to handle both sent and received command frames...
    private void m_frameParser_ReceivedCommandFrame(object sender, EventArgs<ICommandFrame> e)
    {
        BeginInvoke(new Action<ICommandFrame>(ReceivedCommandFrame), e.Argument);
    }

    private void m_frameParser_ReceivedUndeterminedFrame(object sender, EventArgs<IChannelFrame> e)
    {
        BeginInvoke(new Action<IChannelFrame>(ReceivedUndeterminedFrame), e.Argument);
    }

    private void m_frameParser_DataStreamException(object sender, EventArgs<Exception> e)
    {
        BeginInvoke(new Action<Exception>(DataStreamException), e.Argument);
    }

    private void m_frameParser_ConfigurationChanged(object sender, EventArgs e)
    {
        BeginInvoke(new Action(ConfigurationChanged));
    }

    private void m_frameParser_Connected(object sender, EventArgs e)
    {
        BeginInvoke(new Action(Connected));
    }

    private void m_frameParser_ConnectionException(object sender, EventArgs<Exception, int> e)
    {
        BeginInvoke(new Action<Exception, int>(ConnectionException), e.Argument1, e.Argument2);
    }

    private void m_frameParser_ExceededParsingExceptionThreshold(object sender, EventArgs e)
    {
        BeginInvoke(new EventHandler(ExceededParsingExceptionThreshold), sender, e);
    }

    private void m_frameParser_Disconnected(object sender, EventArgs e)
    {
        BeginInvoke(new Action(Disconnected));
    }

    private void m_frameParser_ServerStarted(object sender, EventArgs e)
    {
        BeginInvoke(new Action(ServerStarted));
    }

    private void m_frameParser_ServerStopped(object sender, EventArgs e)
    {
        BeginInvoke(new Action(ServerStopped));
    }

    private void m_imageQueue_ProcessException(object sender, EventArgs<Exception> e)
    {
        BeginInvoke(new Action<Exception>(DataStreamException), e.Argument);
    }

    private void m_chartSettings_PhaseAngleColorsChanged()
    {
        BeginInvoke(new Action(PhaseAngleColorsChanged));
    }

    #endregion

    #region [ Thread Delegate Implementations ]

    private void ProcessReceivedFrameBufferImage(object state)
    {
        if (m_shuttingDown)
            return;

        if (state is not EventArgs<FundamentalFrameType, byte[], int, int> bufferImageArgs)
            return;

        BeginInvoke(new Action<FundamentalFrameType, byte[], int, int>(ReceivedFrameBufferImage), bufferImageArgs.Argument1, bufferImageArgs.Argument2, bufferImageArgs.Argument3, bufferImageArgs.Argument4);

        // For file based input we slow image processing to attempt some level of synchronization with frame reception
        if (m_frameParser.TransportProtocol == TransportProtocol.File)
            Thread.Sleep(1000 / m_frameParser.DefinedFrameRate);
    }

    private void ReceivedFrameBufferImage(FundamentalFrameType frameType, byte[] binaryImage, int offset, int length)
    {
        if (frameType != m_lastFrameType)
        {
            m_lastFrameType = frameType;
            LabelFrameType.Text = Enum.GetName(typeof(FundamentalFrameType), m_lastFrameType);
        }

        if (m_frameSampleStream is not null)
            CaptureFrameImage(frameType, binaryImage, offset, length);

        // Note that we exclude command frames from capture stream that are typically "sent", not "received", to make sure frame alignment
        // stays in-tact - this fixes issue with IEEE-1344 play-back of captured streams that does not include a synchronization-byte
        if (m_frameCaptureStream is not null && frameType != FundamentalFrameType.CommandFrame)
            m_frameCaptureStream.Write(binaryImage, offset, length);

        if (length <= 0)
            return;

        if (GroupBoxStatus.Expanded)
        {
            if (m_byteEncoding is ByteEncoding.ASCIIEncoding)
            {
                // We handle ASCII encoding as a special case removing any control characters, no spacing
                // between characters and allowing the entire frame to be displayed
                LabelBinaryFrameImage.Text = m_byteEncoding.GetString(binaryImage, offset, length).RemoveControlCharacters();
            }
            else
            {
                // For all others, we display bytes in the specified encoding format up to specified maximum display bytes
                LabelBinaryFrameImage.Text = m_byteEncoding.GetString(binaryImage, offset, Math.Min(length, m_applicationSettings.MaximumFrameDisplayBytes), ' ') + (length > m_applicationSettings.MaximumFrameDisplayBytes ? " ..." : "");
            }
        }

        // We update statistics after processing about 30 frames of data...
        if (m_byteCount >= length * 30)
        {
            StatusBar.Panels[TotalFramesPanel].Text = m_frameParser.TotalFramesReceived.ToString();
            StatusBar.Panels[FramesPerSecondPanel].Text = m_frameParser.CalculatedFrameRate.ToString("0.0000");
            StatusBar.Panels[TotalBytesPanel].Text = m_frameParser.TotalBytesReceived.ToString();
            StatusBar.Panels[BitRatePanel].Text = m_frameParser.MegaBitRate.ToString("0.0000");
            StatusBar.Panels[QueuedBuffersPanel].Text = m_frameParser.QueuedBuffers.ToString();

            m_byteCount = 0;
            Application.DoEvents();
        }

        m_byteCount += length;
    }

    private void CaptureFrameImage(FundamentalFrameType frameType, byte[] binaryImage, int offset, int length)
    {
        byte frameBit = (byte)Math.Pow(2.0D, (double)frameType);

        // Make sure only one of each type of encountered frame is captured...
        if ((m_capturedFrames & frameBit) != 0)
            return;

        m_capturedFrames = (byte)(m_capturedFrames | frameBit);

        lock (m_dataStreamLock)
        {
            m_frameSampleStream.WriteLine($"Captured Frame Type: {Enum.GetName(typeof(FundamentalFrameType), frameType)} (0x{frameBit:X2})");
            m_frameSampleStream.WriteLine($"   Frame Image Size: {length} bytes");
            m_frameSampleStream.WriteLine();
            m_frameSampleStream.WriteLine(">> Decimal Image:");

            foreach (string currentRow in ByteEncoding.Decimal.GetString(binaryImage, offset, length, ' ').GetSegments(TextFileWidth / 4 * 4))
                m_frameSampleStream.WriteLine(currentRow);

            m_frameSampleStream.WriteLine();
            m_frameSampleStream.WriteLine(">> Hexadecimal Image:");

            foreach (string currentRow in ByteEncoding.Hexadecimal.GetString(binaryImage, offset, length, ' ').GetSegments(TextFileWidth / 3 * 3))
                m_frameSampleStream.WriteLine(currentRow);

            m_frameSampleStream.WriteLine();
            m_frameSampleStream.WriteLine(">> Big Endian Binary Image:");

            foreach (string currentRow in ByteEncoding.BigEndianBinary.GetString(binaryImage, offset, length, ' ').GetSegments(TextFileWidth / 9 * 9))
                m_frameSampleStream.WriteLine(currentRow);

            m_frameSampleStream.WriteLine();
            m_frameSampleStream.WriteLine(">> ASCII Character Extraction:");

            foreach (string currentRow in Encoding.ASCII.GetString(binaryImage, offset, length).RemoveControlCharacters().GetSegments(TextFileWidth))
                m_frameSampleStream.WriteLine(currentRow);

            m_frameSampleStream.WriteLine();
            m_frameSampleStream.WriteLine(new string('*', TextFileWidth));
            m_frameSampleStream.WriteLine();

            // We stop the capture once a config frame is encountered...
            if (frameType == FundamentalFrameType.ConfigurationFrame)
                CloseSampleCapture();
        }
    }

    private void CloseSampleCapture()
    {
        MenuItemCancelSampleFrameCapture.Enabled = false;

        if (m_frameSampleStream is null)
            return;

        lock (m_dataStreamLock)
        {
            m_frameSampleStream.WriteLine($"Frames Types Encountered (As Or'd Bits): 0x{m_capturedFrames:X2}");
            m_frameSampleStream.WriteLine($"File closed at {DateTime.Now}");
            m_frameSampleStream.Close();
            m_frameSampleStream = null;

            // Open captured sample file in notepad...
            Process.Start("notepad.exe", m_frameCaptureFileName);
        }
    }

    private void UpdateChartTitle(string message)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => UpdateChartTitle(message)));
        }
        else
        {
            ChartDataDisplay.TitleTop.Text = message;
            ChartDataDisplay.Refresh();
            Application.DoEvents();
        }
    }

    private void PaceConfigFrameProcessing()
    {
        const int MinConfigProcessingDelay = 1000;

        if (m_frameParser is null || !m_frameParser.Enabled)
            return;

        ushort timeSinceLastConfig = (ushort)Math.Round(new Ticks(DateTime.UtcNow.Ticks - m_lastConfigProcessedTime).ToMilliseconds() % ushort.MaxValue);

        if (timeSinceLastConfig < MinConfigProcessingDelay)
        {
            // Do not process back-to-back configuration frames too quickly, UI needs time to process and react to detail loading
            Thread.Sleep(MinConfigProcessingDelay - timeSinceLastConfig);

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (m_frameParser is null || !m_frameParser.Enabled)
                return;
        }

        UpdateChartTitle("Loading configuration frame...");
        Invoke(new Action(ReceivedConfigFrame));
        m_lastConfigProcessedTime = DateTime.UtcNow.Ticks;
    }

    private void ReceivedConfigFrame()
    {
        IConfigurationFrame frame;

        lock (m_processConfigFrame)
            frame = m_updatedConfigFrame;

        if (m_frameParser is null || !m_frameParser.Enabled)
        {
            UpdateChartTitle("");
            return;
        }

        if (!ReferenceEquals(m_frameParser.ConfigurationFrame, frame))
            m_frameParser.ConfigurationFrame = frame;

        // Try to maintain original index selection
        int selectedIndex = ComboBoxPmus.SelectedIndex;

        ComboBoxPmus.Items.Clear();

        if (frame is null)
        {
            UpdateChartTitle("No configuration frame loaded");
            return;
        }

        LabelTime.Text = frame.TimeTag.ToString();
        m_attributeFrames[frame.FrameType] = frame;

        // Cache config frame reference for future use...
        m_configChangeTime = DateTime.UtcNow.Ticks;
        m_configurationFrame = frame;
        GroupBoxConfigurationFrame.Expanded = true;
        TextBoxDeviceID.Text = frame.IDCode.ToString();

        // Load PMU list from new configuration frame
        ComboBox pmus = ComboBoxPmus;
        ComboBox.ObjectCollection items = pmus.Items;

        foreach (IConfigurationCell cell in frame.Cells)
            items.Add(GetCellName(cell));

        if (pmus.Items.Count > 0)
        {
            if (selectedIndex >= 0 && selectedIndex < pmus.Items.Count)
                pmus.SelectedIndex = selectedIndex;
            else
                pmus.SelectedIndex = 0;
        }

        MenuItemSaveConfigFile.Enabled = true;

        // Handle debug stream if it's open
        if (m_streamDebugCapture is not null)
        {
            // We write the header line in the file from the configuration frame
            m_streamDebugCapture.Write("Timestamp,");

            foreach (IConfigurationCell cell in frame.Cells)
            {
                string station = cell.StationName.RemoveControlCharacters().Trim();
                m_streamDebugCapture.Write($"{station} Status,");

                for (int i = 0; i < cell.PhasorDefinitions.Count; i++)
                {
                    IPhasorDefinition definition = cell.PhasorDefinitions[i];
                    string label = $"{station} {Enum.GetName(typeof(PhasorType), definition.PhasorType)}{i}";
                    m_streamDebugCapture.Write($"{label} Angle {definition.Label},{label} Magnitude {definition.Label},");
                }

                m_streamDebugCapture.Write($"{station} Frequency,{station} DfDt,");

                for (int i = 0; i < cell.AnalogDefinitions.Count; i++)
                    m_streamDebugCapture.Write($"{station} Analog{i} {cell.AnalogDefinitions[i].Label},");

                for (int i = 0; i < cell.DigitalDefinitions.Count; i++)
                    m_streamDebugCapture.Write($"{station} Digital{i} {cell.DigitalDefinitions[i].Label},");
            }

            m_streamDebugCapture.WriteLine("EOF");
        }

        UpdateChartTitle($"Configured frame rate: {frame.FrameRate} frames/second");
    }

    private static string GetCellName(IConfigurationCell cell)
    {
        return cell?.StationName.ToNonNullString(cell.IDLabel.ToNonNullString($"Device {cell.IDCode}")) ?? "undefined";
    }

    private void ReceivedDataFrame(IDataFrame frame)
    {
        LabelTime.Text = frame.TimeTag.ToString();
        m_attributeFrames[frame.FrameType] = frame;

        if (m_selectedCell is not null & frame.Cells.Count > 0)
        {
            IDataCell cell = frame.Cells[ComboBoxPmus.SelectedIndex];
            double frequency = default;
            int phasorCount = default;

            int phasorIndex = GetComboBoxPhasorsSelectedIndex();

            if (cell.FrequencyValue is not null)
                frequency = cell.FrequencyValue.Frequency;

            if (cell.PhasorValues is not null)
                phasorCount = cell.PhasorValues.Count;

            // Plot real-time frequency trend
            m_frequencyData.Rows.Add(frequency);

            while (m_frequencyData.Rows.Count > m_applicationSettings.FrequencyPointsToPlot)
                m_frequencyData.Rows.RemoveAt(0);

            // Plot real-time phasor trends
            if (phasorIndex < phasorCount && phasorIndex > -1 && phasorCount > 0)
            {
                try
                {
                    IPhasorValue phasor = cell.PhasorValues?[phasorIndex];

                    if (phasor is not null)
                    {
                        if (Math.Abs((phasor.Angle - m_lastPhaseAngle).ToDegrees()) >= 0.5d || m_phasorData.Rows.Count < 2)
                        {
                            DataRow row = m_phasorData.NewRow();

                            if (m_applicationSettings.PhaseAngleGraphStyle == ApplicationSettings.AngleGraphStyle.Raw)
                            {
                                // Plot raw phase angles
                                for (int i = 0; i < phasorCount; i++)
                                {
                                    if (m_phasorData.Columns.Count > i)
                                        row[i] = cell.PhasorValues[i].Angle.ToDegrees();
                                }
                            }
                            else
                            {
                                // Plot relative phase angles
                                for (int i = 0; i < phasorCount; i++)
                                {
                                    if (m_phasorData.Columns.Count > i)
                                        row[i] = (cell.PhasorValues[i].Angle - phasor.Angle).ToRange(-Math.PI, false).ToDegrees();
                                }
                            }

                            m_phasorData.Rows.Add(row);

                            while (m_phasorData.Rows.Count > m_applicationSettings.PhaseAnglePointsToPlot)
                                m_phasorData.Rows.RemoveAt(0);

                            if (GroupBoxConfigurationFrame.Expanded)
                            {
                                // Update power and vars calculations
                                if (ComboBoxCurrentPhasors.SelectedIndex > -1 && ComboBoxVoltagePhasors.SelectedIndex > -1)
                                {
                                    int selectedVoltageIndex = Convert.ToInt32(ComboBoxVoltagePhasors.Items[ComboBoxVoltagePhasors.SelectedIndex].ToString().Split(':')[0]);
                                    int selectedCurrentIndex = Convert.ToInt32(ComboBoxCurrentPhasors.Items[ComboBoxCurrentPhasors.SelectedIndex].ToString().Split(':')[0]);

                                    if (selectedVoltageIndex < phasorCount && selectedCurrentIndex < phasorCount)
                                    {
                                        try
                                        {
                                            IPhasorValue voltagePhasor = cell.PhasorValues[selectedVoltageIndex];
                                            IPhasorValue currentPhasor = cell.PhasorValues[selectedCurrentIndex];

                                            if (voltagePhasor is not null && currentPhasor is not null)
                                            {
                                                LabelPower.Text = $"{PhasorValueBase.CalculatePower(voltagePhasor, currentPhasor) / 1000000:0.0000} MW";
                                                LabelVars.Text = $"{PhasorValueBase.CalculateVars(voltagePhasor, currentPhasor) / 1000000:0.0000} MVars";
                                            }
                                            else
                                            {
                                                LabelPower.Text = "--- MW";
                                                LabelVars.Text = "--- MVars";
                                            }
                                        }
                                        catch
                                        {
                                            // Skip calculation if something is not available or fails to calculate properly
                                            LabelPower.Text = "--- MW";
                                            LabelVars.Text = "--- MVars";
                                        }
                                    }
                                }
                            }

                            m_lastPhaseAngle = phasor.Angle;
                        }

                        if (GroupBoxStatus.Expanded)
                        {
                            LabelFrequency.Text = $"{frequency:0.0000} Hz";
                            LabelAngle.Text = $"{phasor.Angle.ToDegrees()}°";

                            // Most PMU's are setup such that voltage magnitudes need to multiplied by the SQRT(3)
                            LabelMagnitude.Text = phasor.Type == PhasorType.Voltage ?
                                $"{(phasor.Magnitude / 1000d):0.0000} ({(phasor.Magnitude * m_sqrtOf3 / 1000d):0.0000}) kV" :
                                $"{phasor.Magnitude:0.0000} Amperes";
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                catch (IndexOutOfRangeException)
                {
                }
                catch (Exception ex)
                {
                    AppendStatusMessage($"Exception occurred while attempting to plot data: {ex.Message}");
                }
            }

            // We only refresh graph every so often
            if (Ticks.ToSeconds(DateTime.Now.Ticks - m_lastRefresh) >= m_applicationSettings.RefreshRate)
            {
                ChartDataDisplay.DataBind();
                m_lastRefresh = DateTime.Now.Ticks;
            }
        }

        // Handle debug stream if it's open
        if (m_streamDebugCapture is null)
            return;

        // We write the data value line in the file from the data frame
        m_streamDebugCapture.Write($"{frame.Timestamp:yyyy-MM-dd HH:mm:ss.fff},");

        foreach (IDataCell cell in frame.Cells)
        {
            m_streamDebugCapture.Write($"{cell.StatusFlags:x},");

            foreach (IPhasorValue phasorValue in cell.PhasorValues)
                m_streamDebugCapture.Write($"{phasorValue.Angle.ToDegrees()},{phasorValue.Magnitude},");

            IFrequencyValue frequencyValue = cell.FrequencyValue;
            m_streamDebugCapture.Write($"{frequencyValue.Frequency},{frequencyValue.DfDt},");

            foreach (IAnalogValue analogValue in cell.AnalogValues)
                m_streamDebugCapture.Write($"{analogValue.Value},");

            foreach (IDigitalValue digitalValue in cell.DigitalValues)
                m_streamDebugCapture.Write($"{digitalValue.Value},");
        }

        m_streamDebugCapture.WriteLine(frame.Timestamp);
    }

    private void ReceivedHeaderFrame(IHeaderFrame frame)
    {
        LabelTime.Text = frame.TimeTag.ToString();
        m_attributeFrames[frame.FrameType] = frame;
        TextBoxHeaderFrame.Text = frame.HeaderData;
        GroupBoxHeaderFrame.Expanded = true;
    }

    private void ReceivedCommandFrame(ICommandFrame frame)
    {
        LabelTime.Text = frame.TimeTag.ToString();
        m_attributeFrames[frame.FrameType] = frame;

        // To make sure "sent" command frames can get captured and displayed, we make sure and
        // send the frame to the frame buffer image handler
        ReceivedFrameBufferImage(FundamentalFrameType.CommandFrame, frame.BinaryImage(), 0, frame.BinaryLength);
    }

    private void ReceivedUndeterminedFrame(IChannelFrame frame)
    {
        LabelTime.Text = frame.TimeTag.ToString();
        m_attributeFrames[frame.FrameType] = frame;
    }

    private void DataStreamException(Exception ex)
    {
        AppendStatusMessage($"Exception: {ex.Message}");

        if (m_applicationSettings.ShowMessagesTabOnDataException) 
            TabControlChart.Tabs[(int)ChartTabs.Messages].Selected = true;
    }

    private void ConfigurationChanged()
    {
        if (m_configurationFrame is null)
            return;

        if (new Ticks(DateTime.UtcNow.Ticks - m_configChangeTime).ToSeconds() < 60.0D)
            return;

        m_configChangeTime = DateTime.UtcNow.Ticks;
        AppendStatusMessage("NOTE: Data stream indicates that configuration in source device has changed");

        if (m_frameParser.DeviceSupportsCommands && MessageBox.Show(this, $"Data stream indicates that configuration in source device has changed.{Environment.NewLine}{Environment.NewLine}Do you want to request a new configuration frame?", "Device Configuration Changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            SendDeviceCommand(DeviceCommand.SendConfigurationFrame3);
    }

    private void Connected()
    {
        UpdateChartTitle(m_configurationFrame is null ? 
            "Awaiting configuration frame..." : 
            $"Configured frame rate: {m_configurationFrame.FrameRate} frames/second");

        AppendStatusMessage($"Connected to device {ConnectionInformation}");
        TabControlChart.Tabs[(int)ChartTabs.Graph].Selected = true;
    }

    private void ConnectionException(Exception ex, int connectionAttempts)
    {
        UpdateChartTitle("Connection attempt failed...");
        AppendStatusMessage($"Device connection error: {ex.Message}");

        if (m_applicationSettings.MaximumConnectionAttempts > 0 & connectionAttempts >= m_applicationSettings.MaximumConnectionAttempts)
        {
            Disconnect();
            MessageBox.Show(this, $"{ex.Message}{Environment.NewLine}{connectionAttempts}{(connectionAttempts > 1 ? " connections " : " connection ")}attempted.", "Device Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else if (connectionAttempts > 1)
        {
            TabControlChart.Tabs[(int)ChartTabs.Messages].Selected = true;
        }
    }

    private void ExceededParsingExceptionThreshold(object sender, EventArgs e)
    {
        // Connection has been terminated, but we still need to clean up display...
        Disconnect();

        if (MessageBox.Show(this, $"An excessive number of exceptions have occurred on this connection - please verify the correct protocol has been selected.{Environment.NewLine}{Environment.NewLine}Do you want to try the connection again with current settings?", "Exception Threshold Exceeded", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            Connect();
    }

    private void Disconnected()
    {
        if (m_frameParser.Enabled)
        {
            // Communications layer closed connection (user didn't) - so we terminate gracefully...
            Disconnect();
            AppendStatusMessage("Connection closed by remote device.");
            MessageBox.Show(this, "Connection closed by remote device.", "Device Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
            AppendStatusMessage("Connection closed.");
        }
    }

    private void ServerStarted()
    {
        UpdateChartTitle("Listening for connection...");
        AppendStatusMessage($"Local server started.  Listening for connection on {ConnectionInformation}");
    }

    private void ServerStopped() => 
        AppendStatusMessage("Local server stopped.");

    private void PhaseAngleColorsChanged()
    {
        // Validate that there is at least one color available for phase angle charts
        ApplicationSettings.ColorList colorList = m_applicationSettings.PhaseAngleColors;

        if (colorList.Count == 0)
            colorList.Add(Color.Black);

        InitializeChart();

        if (m_selectedCell is not null)
            TabControlChart.Tabs[(int)ChartTabs.Graph].Selected = true;
    }

    #endregion

    #region [ Interface / Data Layer Functions ]

    private void ExtraParametersToolTipVisible(bool show)
    {
        if (!m_formLoaded)
            return;

        UltraToolTipManager extraParameters = ToolTipManagerForExtraParameters;
        UltraToolTipInfo toolTip = extraParameters.GetUltraToolTip(TabControlProtocolParameters);

        if (show)
        {
            Point targetPoint = new(Location.X + TabControlCommunications.Location.X + TabControlProtocolParameters.Location.X + 100, Location.Y + TabControlCommunications.Location.Y + TabControlProtocolParameters.Location.Y + 50);
            toolTip.Enabled = DefaultableBoolean.True;
            extraParameters.SetUltraToolTip(TabControlProtocolParameters, toolTip);
            extraParameters.ShowToolTip(TabControlProtocolParameters, true, targetPoint);
        }
        else
        {
            toolTip.Enabled = DefaultableBoolean.False;
            extraParameters.SetUltraToolTip(TabControlProtocolParameters, toolTip);
        }
    }

    private void LoadConnectionSettings(string filename)
    {
        FileStream settingsFile = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);

        SoapFormatter formatter = new()
        {
            AssemblyFormat = FormatterAssemblyStyle.Simple,
            TypeFormat = FormatterTypeStyle.TypesWhenNeeded,
            Binder = Serialization.LegacyBinder
        };

        try
        {
            Cursor = Cursors.WaitCursor;
            ApplyConnectionSettings((ConnectionSettings)formatter.Deserialize(settingsFile));
            UpdateApplicationTitle(filename);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Failed to open connection settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        finally
        {
            Cursor = Cursors.Default;
        }

        settingsFile.Close();
    }

    private void SaveConnectionSettings(string filename)
    {
        FileStream settingsFile = File.Create(filename);

        SoapFormatter formatter = new()
        {
            AssemblyFormat = FormatterAssemblyStyle.Simple,
            TypeFormat = FormatterTypeStyle.TypesWhenNeeded
        };

        try
        {
            formatter.Serialize(settingsFile, CurrentConnectionSettings);
            UpdateApplicationTitle(filename);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Failed to save connection settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        settingsFile.Close();
    }

    private void UpdateApplicationTitle(string filename)
    {
        const int Offset = 50;

        if (string.IsNullOrEmpty(filename))
            filename = m_lastFileName;
        else
            m_lastFileName = filename;

        if (string.IsNullOrEmpty(m_applicationName))
            m_applicationName = EntryAssembly.Description;

        Text = string.IsNullOrEmpty(filename) || string.Equals(filename, m_lastConnectionFileName, StringComparison.OrdinalIgnoreCase) ?
            m_applicationName :
            $"{m_applicationName} - {TrimFileName(filename, Convert.ToInt32((Width - Offset) / 10d))}";
    }

    private ConnectionSettings CurrentConnectionSettings
    {
        get
        {
            // Setup PMU connection parameters based on user selections...
            ConnectionSettings connectionSettings = new()
            {
                PhasorProtocol = (PhasorProtocol)ComboBoxProtocols.SelectedIndex
            };

            switch (TabControlCommunications.SelectedTab.Index)
            {
                case (int)TransportProtocol.Tcp:
                {
                    connectionSettings.TransportProtocol = TransportProtocol.Tcp;
                    connectionSettings.ConnectionString = $"server={TextBoxTcpHostIP.Text}; port={TextBoxTcpPort.Text}; interface={GetNetworkInterfaceValue(m_tcpNetworkInterface)}; islistener={CheckBoxEstablishTcpServer.Checked}";
                    break;
                }
                case (int)TransportProtocol.Udp:
                {
                    connectionSettings.TransportProtocol = TransportProtocol.Udp;
                    connectionSettings.ConnectionString = CheckBoxRemoteUdpServer.Checked ?
                        $"localport={TextBoxUdpLocalPort.Text}; server={TextBoxUdpHostIP.Text}; remoteport={TextBoxUdpRemotePort.Text}; interface={GetNetworkInterfaceValue(m_udpNetworkInterface)}{Forms.ReceiveFromSourceSelector.ConnectionString}{Forms.MulticastSourceSelector.ConnectionString}" :
                        $"localport={TextBoxUdpLocalPort.Text}; interface={GetNetworkInterfaceValue(m_udpNetworkInterface)}{Forms.ReceiveFromSourceSelector.ConnectionString}";

                    break;
                }
                case (int)TransportProtocol.Serial:
                {
                    connectionSettings.TransportProtocol = TransportProtocol.Serial;
                    connectionSettings.ConnectionString = $"port={ComboBoxSerialPorts.Text}; baudrate={ComboBoxSerialBaudRates.Text}; parity={ComboBoxSerialParities.Text}; stopbits={ComboBoxSerialStopBits.Text}; databits={TextBoxSerialDataBits.Text}; dtrenable={CheckBoxSerialDTR.Checked}; rtsenable={CheckBoxSerialRTS.Checked}";
                    break;
                }
                case (int)TransportProtocol.File:
                {
                    connectionSettings.TransportProtocol = TransportProtocol.File;
                    connectionSettings.ConnectionString = $"file={TextBoxFileCaptureName.Text}";
                    break;
                }
            }

            // Append command channel settings
            connectionSettings.ConnectionString += Forms.AlternateCommandChannel.ConnectionString;

            // Include connection specific settings in connection string if they are not set to their default values so that
            // these settings will ride with their serialized connection file
            if (m_applicationSettings.MaximumConnectionAttempts != 1)
                connectionSettings.ConnectionString += $"; {nameof(ApplicationSettings.MaximumConnectionAttempts)} = {m_applicationSettings.MaximumConnectionAttempts}";

            if (!m_applicationSettings.AutoStartDataParsingSequence)
                connectionSettings.ConnectionString += $"; {nameof(ApplicationSettings.AutoStartDataParsingSequence)} = false";

            if (m_applicationSettings.SkipDisableRealTimeData)
                connectionSettings.ConnectionString += $"; {nameof(ApplicationSettings.SkipDisableRealTimeData)} = true";

            if (!m_applicationSettings.DisableRealTimeDataOnStop)
                connectionSettings.ConnectionString += $"; {nameof(ApplicationSettings.DisableRealTimeDataOnStop)} = false";

            if (m_applicationSettings.InjectSimulatedTimestamp)
                connectionSettings.ConnectionString += "; simulateTimestamp = true";

            if (m_applicationSettings.UseHighResolutionInputTimer)
                connectionSettings.ConnectionString += $"; {nameof(ApplicationSettings.UseHighResolutionInputTimer)} = true";

            if (!m_applicationSettings.KeepCommandChannelOpen)
                connectionSettings.ConnectionString += $"; {nameof(ApplicationSettings.KeepCommandChannelOpen)} = false";

            if (m_applicationSettings.ConfigurationFrameVersion != -1)
                connectionSettings.ConnectionString += $"; {nameof(ApplicationSettings.ConfigurationFrameVersion)} = {m_applicationSettings.ConfigurationFrameVersion}";

            connectionSettings.PmuID = Convert.ToInt32(TextBoxDeviceID.Text);
            connectionSettings.FrameRate = Convert.ToInt32(TextBoxFileFrameRate.Text);
            connectionSettings.AutoRepeatPlayback = CheckBoxAutoRepeatPlayback.Checked;
            connectionSettings.ByteEncodingDisplayFormat = ComboBoxByteEncodingDisplayFormats.SelectedIndex;
            connectionSettings.ConnectionParameters = m_frameParser.ConnectionParameters;

            return connectionSettings;
        }
    }

    private void ApplyConnectionSettings(ConnectionSettings settings)
    {
        Disconnect();

        // Setup PMU connection parameters based on user selections...
        // Changing protocol causes several UI events - so we assign this first
        ComboBoxProtocols.SelectedIndex = (int)settings.PhasorProtocol;

        // After assigning protocol, any available extra connection parameters for the protocol
        // will be set to their defaults. If any connection parameters were serialized into the
        // connection settings, we'll apply them now...
        if (settings.ConnectionParameters is not null)
        {
            m_frameParser.ConnectionParameters = settings.ConnectionParameters;
            PropertyGridProtocolParameters.SelectedObject = m_frameParser.ConnectionParameters;
        }

        // Load remaining connection settings
        TabControlCommunications.Tabs[(int)settings.TransportProtocol].Selected = true;
        Dictionary<string, string> connectionData = settings.ConnectionString.ParseKeyValuePairs();
        string setting;

        switch (settings.TransportProtocol)
        {
            case TransportProtocol.Tcp:
            {
                TextBoxTcpPort.Text = connectionData["port"];

                // Note that old style connection strings may not contain "islistener" key
                if (connectionData.ContainsKey("islistener") && connectionData["islistener"].ParseBoolean())
                {
                    TextBoxTcpHostIP.Text = m_loopbackAddress;
                    CheckBoxEstablishTcpServer.Checked = true;
                }
                else
                {
                    AssignHostIP(TextBoxTcpHostIP, connectionData["server"]);
                    CheckBoxEstablishTcpServer.Checked = false;
                }

                m_tcpNetworkInterface = connectionData.TryGetValue("interface", out setting) ? GetNetworkInterfaceIndex(setting) : 0;

                break;
            }
            case TransportProtocol.Udp:
            {
                if (connectionData.TryGetValue("server", out string server))
                {
                    TextBoxUdpLocalPort.Text = connectionData["localport"];
                    AssignHostIP(TextBoxUdpHostIP, server);
                    TextBoxUdpRemotePort.Text = connectionData["remoteport"];
                    Forms.ReceiveFromSourceSelector.ConnectionString = settings.ConnectionString;
                    Forms.MulticastSourceSelector.ConnectionString = settings.ConnectionString;
                    CheckBoxRemoteUdpServer.Checked = true;
                }
                else
                {
                    TextBoxUdpLocalPort.Text = connectionData["localport"];
                    TextBoxUdpHostIP.Text = m_loopbackAddress;
                    TextBoxUdpRemotePort.Text = "5000";
                    Forms.ReceiveFromSourceSelector.ConnectionString = settings.ConnectionString;
                    Forms.MulticastSourceSelector.ConnectionString = "";
                    CheckBoxRemoteUdpServer.Checked = false;
                }

                m_udpNetworkInterface = connectionData.TryGetValue("interface", out setting) ? GetNetworkInterfaceIndex(setting) : 0;

                break;
            }
            case TransportProtocol.Serial:
            {
                ComboBoxSerialPorts.Text = connectionData["port"];
                ComboBoxSerialBaudRates.Text = connectionData["baudrate"];
                ComboBoxSerialParities.Text = connectionData["parity"];
                ComboBoxSerialStopBits.Text = connectionData["stopbits"];
                TextBoxSerialDataBits.Text = connectionData["databits"];
                CheckBoxSerialDTR.Checked = connectionData["dtrenable"].ParseBoolean();
                CheckBoxSerialRTS.Checked = connectionData["rtsenable"].ParseBoolean();
                break;
            }
            case TransportProtocol.File:
            {
                TextBoxFileCaptureName.Text = connectionData["file"];
                break;
            }
        }

        // Apply alternate command channel settings (if any)
        Forms.AlternateCommandChannel.ConnectionString = settings.ConnectionString;
        UpdateAlternateCommandChannelLabel();

        // Check for connection specific settings that may have been serialized into the connection string
        if (connectionData.TryGetValue(nameof(ApplicationSettings.MaximumConnectionAttempts), out setting))
            m_applicationSettings.MaximumConnectionAttempts = int.Parse(setting);

        if (connectionData.TryGetValue(nameof(ApplicationSettings.AutoStartDataParsingSequence), out setting))
            m_applicationSettings.AutoStartDataParsingSequence = setting.ParseBoolean();

        if (connectionData.TryGetValue(nameof(ApplicationSettings.SkipDisableRealTimeData), out setting))
            m_applicationSettings.SkipDisableRealTimeData = setting.ParseBoolean();

        if (connectionData.TryGetValue(nameof(ApplicationSettings.DisableRealTimeDataOnStop), out setting))
            m_applicationSettings.DisableRealTimeDataOnStop = setting.ParseBoolean();

        if (connectionData.TryGetValue("simulateTimestamp", out setting))
            m_applicationSettings.InjectSimulatedTimestamp = setting.ParseBoolean();

        if (connectionData.TryGetValue(nameof(ApplicationSettings.UseHighResolutionInputTimer), out setting))
            m_applicationSettings.UseHighResolutionInputTimer = setting.ParseBoolean();

        if (connectionData.TryGetValue(nameof(ApplicationSettings.KeepCommandChannelOpen), out setting))
            m_applicationSettings.KeepCommandChannelOpen = setting.ParseBoolean();

        if (connectionData.TryGetValue(nameof(ApplicationSettings.ConfigurationFrameVersion), out setting))
            m_applicationSettings.ConfigurationFrameVersion = int.Parse(setting);

        TextBoxDeviceID.Text = settings.PmuID.ToString();
        TextBoxFileFrameRate.Text = settings.FrameRate.ToString();
        CheckBoxAutoRepeatPlayback.Checked = settings.AutoRepeatPlayback;
        ComboBoxByteEncodingDisplayFormats.SelectedIndex = settings.ByteEncodingDisplayFormat;
    }

    internal void AssignHostIP(UltraMaskedEdit maskedEditControl, string ipValue)
    {
        if (m_applicationSettings.ForceIPv4)
        {
            try
            {
                // See if this is a literal IP address first
                if (IPAddress.TryParse(ipValue, out IPAddress literalAddress))
                {
                    // Check to see if address has an IPv4 style address
                    if (literalAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        // Assign IP address
                        maskedEditControl.Text = literalAddress.ToString();
                        return;
                    }
                }

                // When forcing IPv4, an input mask is applied and may cause assignment of IPv6 or DNS values loaded
                // from a saved connection string to fail
                foreach (IPAddress address in Dns.GetHostEntry(ipValue).AddressList)
                {
                    // Check to see if address has an IPv4 style address
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        // Attempt to assign IP address
                        maskedEditControl.Text = address.ToString();
                        break;
                    }
                }
            }
            catch
            {
                // If all else fails, just assign a loopback address
                maskedEditControl.Text = "127.0.0.1";
            }
        }
        else
        {
            maskedEditControl.Text = ipValue;
        }

        if (string.IsNullOrEmpty(maskedEditControl.Text))
            maskedEditControl.Text = m_loopbackAddress;
    }

    public void InitializeNetworkInterfaces()
    {
        // Load network interfaces
        List<Tuple<string, string, string>> networkInterfaces = [];

        // Make sure "default" NIC is available in network interface list
        string[] nicInterfaces = m_applicationSettings.AlternateInterfaces.ToNonNullNorEmptyString(ApplicationSettings.DefaultAlternateInterfaces).Split(';');

        foreach (string nicInterface in nicInterfaces)
        {
            string[] elem = nicInterface.Split('|');

            switch (elem.Length)
            {
                case 2:
                {
                    string ipV6Address = "::0";

                    try
                    {
                        // Check to see if address has an IPv6 style address
                        IPAddress address = Dns.GetHostEntry(elem[1]).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetworkV6);

                        // Attempt to assign IP address
                        if (address is not null)
                            ipV6Address = address.ToString();
                    }
                    catch
                    {
                        // If all else fails, just assign a loopback address
                        ipV6Address = "::0";
                    }

                    networkInterfaces.Add(new Tuple<string, string, string>(elem[0], elem[1], ipV6Address));
                    break;
                }
                case 3:
                {
                    networkInterfaces.Add(new Tuple<string, string, string>(elem[0], elem[1], elem[2]));
                    break;
                }
            }
        }

        try
        {
            // Add IPs for active, physical NIC's - if IPv4 is not supported, default to "::0", if IPv6 is not supported, default to "0.0.0.0"
            networkInterfaces.AddRange(NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                .Select(nic => new Tuple<string, string, string>(nic.Description, nic.GetIPProperties().UnicastAddresses.FirstOrDefault(info =>
                    info.Address.AddressFamily == AddressFamily.InterNetwork)?.Address.ToNonNullString("::0"),
                    nic.GetIPProperties().UnicastAddresses.FirstOrDefault(info => info.Address.AddressFamily == AddressFamily.InterNetworkV6)?.Address.ToNonNullString("0.0.0.0"))));
        }
        catch
        {
            // Can operate with only default NIC selection if needed
        }

        m_networkInterfaces = networkInterfaces.ToArray();
        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.Items.Clear();

        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.Items.AddRange(m_networkInterfaces);
        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.DisplayMember = "Item1";
    }

    public string GetNetworkInterfaceValue(int index)
    {
        if (index < 0 || index > m_networkInterfaces.Length - 1)
            index = 0;

        return m_applicationSettings.ForceIPv4 ? 
            m_networkInterfaces[index].Item2 : 
            m_networkInterfaces[index].Item3;
    }

    public int GetNetworkInterfaceIndex(string address)
    {
        address = address.ToNonNullString().Trim();

        // Look for matching IPv4 address
        int index = m_networkInterfaces.IndexOf(nic => string.Equals(nic.Item2, address, StringComparison.OrdinalIgnoreCase));

        // If not found, look for matching IPv6 address
        if (index < 0)
            index = m_networkInterfaces.IndexOf(nic => string.Equals(nic.Item3, address, StringComparison.OrdinalIgnoreCase));

        // If not found, assume default network interface
        if (index < 0)
            index = 0;

        return index;
    }

    public bool ForceIPv4
    {
        get => m_applicationSettings.ForceIPv4;
        set
        {
            if (value)
            {
                // Attempt to coerce address into IPv4 format then enable IPv4 masks
                AssignHostIP(TextBoxTcpHostIP, TextBoxTcpHostIP.Text);
                TextBoxTcpHostIP.InputMask = @"nnn\.nnn\.nnn\.nnn";
                TextBoxTcpHostIP.EditAs = EditAsType.UseSpecifiedMask;

                AssignHostIP(TextBoxUdpHostIP, TextBoxUdpHostIP.Text);
                TextBoxUdpHostIP.InputMask = @"nnn\.nnn\.nnn\.nnn";
                TextBoxUdpHostIP.EditAs = EditAsType.UseSpecifiedMask;

                AssignHostIP(Forms.AlternateCommandChannel.TextBoxTcpHostIP, Forms.AlternateCommandChannel.TextBoxTcpHostIP.Text);
                Forms.AlternateCommandChannel.TextBoxTcpHostIP.InputMask = @"nnn\.nnn\.nnn\.nnn";
                Forms.AlternateCommandChannel.TextBoxTcpHostIP.EditAs = EditAsType.UseSpecifiedMask;

                AssignHostIP(Forms.ReceiveFromSourceSelector.TextBoxUdpSourceIP, Forms.ReceiveFromSourceSelector.TextBoxUdpSourceIP.Text);
                Forms.ReceiveFromSourceSelector.TextBoxUdpSourceIP.InputMask = @"nnn\.nnn\.nnn\.nnn";
                Forms.ReceiveFromSourceSelector.TextBoxUdpSourceIP.EditAs = EditAsType.UseSpecifiedMask;

                AssignHostIP(Forms.MulticastSourceSelector.TextBoxMulticastSourceIP, Forms.MulticastSourceSelector.TextBoxMulticastSourceIP.Text);
                Forms.MulticastSourceSelector.TextBoxMulticastSourceIP.InputMask = @"nnn\.nnn\.nnn\.nnn";
                Forms.MulticastSourceSelector.TextBoxMulticastSourceIP.EditAs = EditAsType.UseSpecifiedMask;

                m_loopbackAddress = "127.0.0.1";
            }
            else
            {
                // We remove input mask if we're not forcing IPv4, this allows for free form DNS and IPv6 entry
                TextBoxTcpHostIP.InputMask = "";
                TextBoxTcpHostIP.EditAs = EditAsType.String;
                TextBoxUdpHostIP.InputMask = "";
                TextBoxUdpHostIP.EditAs = EditAsType.String;

                Forms.AlternateCommandChannel.TextBoxTcpHostIP.InputMask = "";
                Forms.AlternateCommandChannel.TextBoxTcpHostIP.EditAs = EditAsType.String;
                Forms.ReceiveFromSourceSelector.TextBoxUdpSourceIP.InputMask = "";
                Forms.ReceiveFromSourceSelector.TextBoxUdpSourceIP.EditAs = EditAsType.String;
                Forms.MulticastSourceSelector.TextBoxMulticastSourceIP.InputMask = "";
                Forms.MulticastSourceSelector.TextBoxMulticastSourceIP.EditAs = EditAsType.String;

                m_loopbackAddress = m_ipStackIsIPv6 ? "::1" : "127.0.0.1";
            }
        }
    }

    private void SendDeviceCommand(DeviceCommand command)
    {
        m_frameParser.SendDeviceCommand(command);
        AppendStatusMessage($"Command \"{Enum.GetName(typeof(DeviceCommand), command)}\" requested at {DateTime.Now}");
    }

    private void SendRawDeviceCommand(ushort rawCommand)
    {
        m_frameParser.SendRawDeviceCommand(rawCommand);
        AppendStatusMessage($"Raw Command \"{rawCommand}\" requested at {DateTime.Now}");
    }

    internal void UpdateAlternateCommandChannelLabel()
    {
        // We update the alternate command channel label to indicate if the alternate channel is defined
        if (Forms.AlternateCommandChannel.IsDefined)
        {
            LabelAlternateCommandChannelState.BorderStyleOuter = UIElementBorderStyle.InsetSoft;
            LabelAlternateCommandChannelState.Appearance.BackColor2 = Color.LightGreen;
            LabelAlternateCommandChannelState.Text = "Defined";
        }
        else
        {
            LabelAlternateCommandChannelState.BorderStyleOuter = UIElementBorderStyle.RaisedSoft;
            LabelAlternateCommandChannelState.Appearance.BackColor2 = Color.LightGray;
            LabelAlternateCommandChannelState.Text = "Not Defined";
        }
    }

    private string ConnectionInformation
    {
        get
        {
            StringBuilder info = new();

            info.Append('"');
            info.Append(m_frameParser.Name);
            info.Append("\" at ");
            info.Append(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            info.Append(" using ");
            info.Append(m_frameParser.PhasorProtocol.GetFormattedProtocolName());

            return info.ToString();
        }
    }

    private void Connect()
    {
        try
        {
            ButtonListen.Enabled = false;
            m_imageQueue = new ImageQueue { ProcessItemFunction = ProcessReceivedFrameBufferImage };
            m_imageQueue.ProcessException += m_imageQueue_ProcessException;

            MultiProtocolFrameParser frameParser = m_frameParser;
            bool valuesAreValid = true;

            // Validate connection parameters if available
            if (frameParser.ConnectionParameters is not null)
                valuesAreValid = frameParser.ConnectionParameters.ValuesAreValid;

            if (valuesAreValid)
            {
                ClearStatusMessages();

                // Setup PMU connection parameters based on user selections...
                ConnectionSettings currentSettings = CurrentConnectionSettings;

                frameParser.PhasorProtocol = currentSettings.PhasorProtocol;
                frameParser.ConnectionParameters = currentSettings.ConnectionParameters;
                frameParser.TransportProtocol = currentSettings.TransportProtocol;
                frameParser.ConnectionString = currentSettings.ConnectionString;
                frameParser.DeviceID = (ushort)currentSettings.PmuID;
                frameParser.AutoStartDataParsingSequence = m_applicationSettings.AutoStartDataParsingSequence;
                frameParser.SkipDisableRealTimeData = m_applicationSettings.SkipDisableRealTimeData;
                frameParser.DisableRealTimeDataOnStop = m_applicationSettings.DisableRealTimeDataOnStop;
                frameParser.AllowedParsingExceptions = m_applicationSettings.AllowedParsingExceptions;
                frameParser.InjectSimulatedTimestamp = m_applicationSettings.InjectSimulatedTimestamp;
                frameParser.UseHighResolutionInputTimer = m_applicationSettings.UseHighResolutionInputTimer;
                frameParser.KeepCommandChannelOpen = m_applicationSettings.KeepCommandChannelOpen;
                frameParser.ConfigurationFrameVersion = m_applicationSettings.ConfigurationFrameVersion switch
                {
                    1 => DeviceCommand.SendConfigurationFrame1,
                    2 => DeviceCommand.SendConfigurationFrame2,
                    3 => DeviceCommand.SendConfigurationFrame3,
                    _ => DeviceCommand.SendLatestConfigurationFrameVersion
                };
                frameParser.ParsingExceptionWindow = Ticks.FromSeconds(m_applicationSettings.ParsingExceptionWindow);
                frameParser.AutoRepeatCapturedPlayback = currentSettings.AutoRepeatPlayback;
                frameParser.DefinedFrameRate = Convert.ToInt32(TextBoxFileFrameRate.Text);
                frameParser.MaximumConnectionAttempts = m_applicationSettings.MaximumConnectionAttempts;

                // Assignment of some can be affected by other settings, update these settings to reflect possible change
                m_applicationSettings.MaximumConnectionAttempts = frameParser.MaximumConnectionAttempts;

                // Connect to PMU...
                frameParser.Start();

                // Timer will be disabled after connection if not using file based input, update setting to reflect possible change
                m_applicationSettings.UseHighResolutionInputTimer = frameParser.UseHighResolutionInputTimer;

                // Update visual elements based on connection selections
                TabControlChart.Tabs[(int)ChartTabs.Graph].Selected = true;
                ButtonListen.Text = "Dis&connect";

                if (frameParser.DataChannelIsServerBased)
                {
                    UpdateChartTitle("Listening for connection...");
                    AppendStatusMessage($"Listening for connection on {ConnectionInformation}");
                }
                else
                {
                    UpdateChartTitle("Attempting connection...");
                    AppendStatusMessage($"Attempting connection to {ConnectionInformation}");
                }

                // Clear any existing attribute tree data when establishing a new connection
                TreeFrameAttributes.SetDataBinding(null, null);

                // Only enable send command elements when phasor and/or transport protocols allow this
                if (frameParser.DeviceSupportsCommands)
                {
                    LabelCommand.Enabled = true;
                    ComboBoxCommands.Enabled = true;
                    ButtonSendCommand.Enabled = true;
                }

                ComboBoxProtocols.Enabled = false;
                LabelAlternateCommandChannel.Enabled = false;
                LabelAlternateCommandChannelState.Enabled = false;
                GroupBoxStatus.Expanded = true;

                // Assign preloaded configuration frame (if any)
                if (m_configurationFrame is not null)
                    m_frameParser.ConfigurationFrame = m_configurationFrame;
            }
            else
            {
                TabControlProtocolParameters.Tabs[(int)ProtocolTabs.ExtraParameters].Selected = true;
                MessageBox.Show(this, $"Required additional connection parameters for \"{((PhasorProtocol)ComboBoxProtocols.SelectedIndex).GetFormattedProtocolName()}\" are not valid - please correct.", "Invalid Connection Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        finally
        {
            ButtonListen.Enabled = true;
        }
    }

    private void Disconnect()
    {
        if (m_frameCaptureStream is not null)
            MenuItemStopCapture_Click(this, null);

        if (m_streamDebugCapture is not null)
            MenuItemStopStreamDebugCapture_Click(this, null);

        // Disconnect from PMU...
        m_frameParser.Stop();
        m_configurationFrame = null;
        m_selectedCell = null;
        m_configChangeTime = 0L;
        m_lastConfigProcessedTime = 0L;
        m_frequencyData.Rows.Clear();
        m_phasorData.Rows.Clear();
        m_lastRefresh = 0L;
        m_lastFrameType = FundamentalFrameType.Undetermined;
        m_attributeFrames.Clear();

        if (m_imageQueue is not null)
            m_imageQueue.ProcessException -= m_imageQueue_ProcessException;

        m_imageQueue = null;

        // Restore window state to normal if it is minimized since disconnect may happen by source,
        // that is, not initiated by user - so user will need to know about the change in state
        if (WindowState == FormWindowState.Minimized)
            WindowState = FormWindowState.Normal;

        // Restore all visual elements to their default state
        ButtonListen.Text = "&Connect";
        ComboBoxProtocols.Enabled = true;
        LabelAlternateCommandChannel.Enabled = true;
        LabelAlternateCommandChannelState.Enabled = true;
        ChartDataDisplay.Series.Clear();
        ChartDataDisplay.DataBind();
        ChartDataDisplay.TitleTop.Text = "";
        LabelCommand.Enabled = false;
        ComboBoxCommands.Enabled = false;
        ButtonSendCommand.Enabled = false;
        MenuItemSaveConfigFile.Enabled = false;
        GroupBoxConfigurationFrame.Expanded = false;
        GroupBoxStatus.Expanded = false;
        GroupBoxHeaderFrame.Expanded = false;
        TextBoxHeaderFrame.Text = "";
        lock (ComboBoxPhasors)
        {
            ComboBoxPhasors.Items.Clear();
            ComboBoxPhasors.Text = "";
        }
        ComboBoxVoltagePhasors.Items.Clear();
        ComboBoxVoltagePhasors.Text = "";
        ComboBoxCurrentPhasors.Items.Clear();
        ComboBoxCurrentPhasors.Text = "";
        ComboBoxPmus.Items.Clear();
        ComboBoxPmus.Text = "";
        LabelBinaryFrameImage.Text = LabelBinaryFrameImage.Tag.ToString();
        StatusBar.Panels[TotalFramesPanel].Text = "0";
        StatusBar.Panels[FramesPerSecondPanel].Text = "0.0000";
        StatusBar.Panels[TotalBytesPanel].Text = "0";
        StatusBar.Panels[BitRatePanel].Text = "0.0000";
        StatusBar.Panels[QueuedBuffersPanel].Text = "0";
        LabelIDCode.Text = "0";
        LabelPhasorCount.Text = "0";
        LabelAnalogCount.Text = "0";
        LabelDigitalCount.Text = "0";
        LabelPower.Text = "0.0000 MW";
        LabelVars.Text = "0.0000 MVars";
        LabelNominalFrequency.Text = "0";
        LabelFrameType.Text = "undetermined";
        LabelTime.Text = "undetermined";
        LabelFrequency.Text = "0.0000 Hz";
        LabelAngle.Text = "0.0000°";
        LabelMagnitude.Text = "0.0000 kV";

        InitializeChart();
    }

    private void Shutdown()
    {
        m_shuttingDown = true;
        Disconnect();
        Application.Exit();
        Environment.Exit(0);
    }

    private void ClearStatusMessages()
    {
        TextBoxMessages.Text = "";
    }

    private void AppendStatusMessage(string message)
    {
        StringBuilder status = new();

        status.Append(TextBoxMessages.Text);
        status.Append(message);
        status.Append(Environment.NewLine);

        TextBoxMessages.Text = status.ToString().SubstringEnd(TextBoxMessages.MaxLength);

        TextBox messages = TextBoxMessages;

        messages.SelectionStart = messages.TextLength;
        messages.ScrollToCaret();
    }

    private string UnhandledExceptionErrorMessage()
    {
        return $"An unexpected exception has occurred in the PMU Connection Tester. This error may have been caused by an inconsistent system state or a programming error. Details of this problem have been logged to an error file; it may be necessary to restart the application. Please notify us with details of this exception so they are aware of this problem: {GlobalExceptionLogger.LastException.Message}";
    }

    #endregion

    #region [ Attribute Tree Display Code ]

    private void InitializeAttributeTree()
    {
        try
        {
            Cursor = Cursors.WaitCursor;

            m_associatedNodes.Clear();
            TreeFrameAttributes.Refresh();

            DataSet attributeTreeDataSet = CreateAttributeTreeDataSet();
            DataTable attributeTable = attributeTreeDataSet.Tables["Attributes"];

            // Get a copy of the current frame set
            List<IChannelFrame> attributeFrames = [..m_attributeFrames.Values];

            // For consistency in display, we make sure frames are in desired order
            attributeFrames.Sort(CompareByFrameType);

            // Tag configuration channel nodes by assigning a unique key that will allow virtual associations between nodes
            foreach (IConfigurationFrame configFrame in attributeFrames.Where(frame => frame.FrameType == FundamentalFrameType.ConfigurationFrame).Cast<IConfigurationFrame>())
            {
                configFrame.Tag = Guid.NewGuid().ToString();

                // Tag frame cells collection
                configFrame.Cells.Tag = Guid.NewGuid().ToString();

                // Tag each frame cell item
                foreach (IConfigurationCell cellNode in configFrame.Cells)
                {
                    cellNode.Tag = Guid.NewGuid().ToString();

                    foreach (IPhasorDefinition phasorDefinition in cellNode.PhasorDefinitions)
                        phasorDefinition.Tag = Guid.NewGuid().ToString();

                    cellNode.FrequencyDefinition.Tag = Guid.NewGuid().ToString();

                    foreach (IAnalogDefinition analogDefinition in cellNode.AnalogDefinitions)
                        analogDefinition.Tag = Guid.NewGuid().ToString();

                    foreach (IDigitalDefinition digitalDefinition in cellNode.DigitalDefinitions)
                        digitalDefinition.Tag = Guid.NewGuid().ToString();
                }

                // Only configuration nodes need tagging since they will be the destination of all "links"
                break;
            }

            // We bind tree before there's any data because this makes the data fill very fast
            UltraTree attributes = TreeFrameAttributes;
            attributes.Override.ShowExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
            attributes.SetDataBinding(attributeTreeDataSet, "Attributes");

            // We hard code column widths because the auto-size function is extremely slow
            foreach (UltraTreeNodeColumn column in TreeFrameAttributes.Nodes.ColumnSetResolved.Columns)
                column.LayoutInfo.MinimumCellSize = new Size(200, 15);

            // We use fundamental frame type as ID for frames, so they are easy to identify - this
            // is also useful later when we want to determine root node associations to frames
            int currentNodeID = (int)FundamentalFrameType.Undetermined + 1;

            foreach (IChannelFrame frame in attributeFrames)
            {
                // Data frames have an associated configuration frame
                IChannelFrame associatedFrame;

                if (frame.FrameType == FundamentalFrameType.DataFrame)
                {
                    // See if frame can be cast as a data frame (could be a partially parsed frame, e.g. a common frame header)
                    associatedFrame = frame is not IDataFrame dataFrame ? null : dataFrame.ConfigurationFrame;
                }
                else
                {
                    associatedFrame = null;
                }

                // Add frame to tree root using its frame type value as the ID
                int lastNodeID = AddChannelNode(attributeTable, (int)frame.FrameType + 1, ref currentNodeID, frame, frame.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"), associatedFrame);

                // We add extra detail for non partial configuration and data frames...
                int lastCellNodeID;

                if (frame.FrameType == FundamentalFrameType.ConfigurationFrame)
                {
                    IConfigurationFrame configFrame = (IConfigurationFrame)frame;

                    // Add frame cells collection object to frame item
                    lastNodeID = AddChannelNode(attributeTable, lastNodeID, ref currentNodeID, configFrame.Cells, null,
                        null);

                    // Add each frame cell item to the list
                    foreach (IConfigurationCell cellNode in configFrame.Cells)
                    {
                        lastCellNodeID = AddChannelNode(attributeTable, lastNodeID, ref currentNodeID, cellNode,
                            cellNode.StationName, null);

                        foreach (IPhasorDefinition phasorDefinition in cellNode.PhasorDefinitions)
                            AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, phasorDefinition,
                                phasorDefinition.Label, null);

                        AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, cellNode.FrequencyDefinition,
                            null, null);

                        foreach (IAnalogDefinition analogDefinition in cellNode.AnalogDefinitions)
                            AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, analogDefinition,
                                analogDefinition.Label, null);

                        foreach (IDigitalDefinition digitalDefinition in cellNode.DigitalDefinitions)
                            AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, digitalDefinition,
                                digitalDefinition.Label, null);
                    }
                }
                else if (frame.FrameType == FundamentalFrameType.DataFrame)
                {
                    IDataFrame dataFrame = (IDataFrame)frame;

                    // Add frame cells collection object to frame item
                    if (dataFrame.ConfigurationFrame is not null)
                    {
                        lastNodeID = AddChannelNode(attributeTable, lastNodeID, ref currentNodeID, dataFrame.Cells,
                            null, dataFrame.ConfigurationFrame.Cells);

                        // Add each frame cell item to the list
                        foreach (IDataCell cellNode in dataFrame.Cells)
                        {
                            lastCellNodeID = AddChannelNode(attributeTable, lastNodeID, ref currentNodeID, cellNode,
                                cellNode.StationName, cellNode.ConfigurationCell);

                            foreach (IPhasorValue phasorValue in cellNode.PhasorValues)
                                AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, phasorValue,
                                    phasorValue.Label, phasorValue.Definition);

                            AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, cellNode.FrequencyValue,
                                null, cellNode.FrequencyValue.Definition);

                            foreach (IAnalogValue analogValue in cellNode.AnalogValues)
                                AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, analogValue,
                                    analogValue.Label, analogValue.Definition);

                            foreach (IDigitalValue digitalValue in cellNode.DigitalValues)
                                AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, digitalValue,
                                    digitalValue.Label, digitalValue.Definition);
                        }
                    }
                    else
                    {
                        lastNodeID = AddChannelNode(attributeTable, lastNodeID, ref currentNodeID, dataFrame.Cells,
                            null, null);

                        // Add each frame cell item to the list
                        foreach (IDataCell cellNode in dataFrame.Cells)
                        {
                            lastCellNodeID = AddChannelNode(attributeTable, lastNodeID, ref currentNodeID, cellNode,
                                cellNode.StationName, null);

                            foreach (IPhasorValue phasorValue in cellNode.PhasorValues)
                                AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, phasorValue,
                                    phasorValue.Label, null);

                            AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, cellNode.FrequencyValue,
                                null, null);

                            foreach (IAnalogValue analogValue in cellNode.AnalogValues)
                                AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, analogValue,
                                    analogValue.Label, null);

                            foreach (IDigitalValue digitalValue in cellNode.DigitalValues)
                                AddChannelNode(attributeTable, lastCellNodeID, ref currentNodeID, digitalValue,
                                    digitalValue.Label, null);
                        }
                    }
                }
            }
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private int AddChannelNode(DataTable attributeTable, int parentID, ref int currentNodeID, IChannel channelNode, string channelLabel, IChannel associatedChannelNode)
    {
        const string PhasorProtocolsNamespace = $"{nameof(GSF)}.{nameof(GSF.PhasorProtocols)}.";

        // Add channel node
        DataRow row = attributeTable.NewRow();
        currentNodeID += 1;

        int channelNodeID = currentNodeID;
        row["ID"] = channelNodeID;

        // Only frames show up at the root level
        if (parentID > (int)FundamentalFrameType.Undetermined)
        {
            row["ParentID"] = parentID;
            row["RootElement"] = false;
        }
        else
        {
            row["RootElement"] = true;
        }

        if (channelNode is null)
            return channelNodeID;

        if (channelNode.Tag is not null)
            row["Key"] = channelNode.Tag;

        if (channelLabel is null)
            row["Attribute"] = channelNode.Attributes["Derived Type"].Replace(PhasorProtocolsNamespace, "").Replace("_", ".");
        else
            row["Attribute"] = $"{channelNode.Attributes["Derived Type"].Replace(PhasorProtocolsNamespace, "").Replace("_", ".")} ({channelLabel})";

        row["ChannelNode"] = parentID;
        attributeTable.Rows.Add(row);

        // Add associated channel node reference (i.e., a "link" node), if defined
        if (associatedChannelNode is not null)
        {
            currentNodeID += 1;
            row = attributeTable.NewRow();
            row["ID"] = currentNodeID;

            // We allow the frame attributes to show up at root level if user doesn't want
            // attributes to show up as child nodes
            if (m_applicationSettings.ShowAttributesAsChildren)
            {
                row["ParentID"] = channelNodeID;
                row["RootElement"] = false;
            }
            else if (parentID > (int)FundamentalFrameType.Undetermined)
            {
                row["ParentID"] = parentID;
                row["RootElement"] = false;
            }
            else
            {
                row["RootElement"] = true;
            }

            row["AssociatedKey"] = associatedChannelNode.Tag;
            row["Attribute"] = "     Click for Associated Definition";
            row["Value"] = associatedChannelNode.Attributes["Derived Type"].Replace(PhasorProtocolsNamespace, "");
            row["ChannelNode"] = -1;

            attributeTable.Rows.Add(row);
        }

        // Add channel node attributes
        foreach (KeyValuePair<string, string> attribute in channelNode.Attributes.Where(attribute => !string.Equals(attribute.Key, "Derived Type", StringComparison.OrdinalIgnoreCase)))
        {
            currentNodeID += 1;
            row = attributeTable.NewRow();
            row["ID"] = currentNodeID;

            // We allow the frame attributes to show up at root level if user doesn't want
            // attributes to show up as child nodes
            if (m_applicationSettings.ShowAttributesAsChildren)
            {
                row["ParentID"] = channelNodeID;
                row["RootElement"] = false;
            }
            else if (parentID > (int)FundamentalFrameType.Undetermined)
            {
                row["ParentID"] = parentID;
                row["RootElement"] = false;
            }
            else
            {
                row["RootElement"] = true;
            }

            row["Attribute"] = $"     {attribute.Key}";
            row["Value"] = attribute.Value;
            row["ChannelNode"] = 0;

            attributeTable.Rows.Add(row);
        }

        return channelNodeID;
    }

    private static int CompareByFrameType(IChannelFrame channelFrame1, IChannelFrame channelFrame2)
    {
        return channelFrame1.FrameType.CompareTo(channelFrame2.FrameType);
    }

    private static DataSet CreateAttributeTreeDataSet()
    {
        DataSet attributeTreeDataSet = new();

        DataTable attributeTable = new("Attributes");
        DataColumnCollection columns = attributeTable.Columns;

        columns.Add("ID", typeof(int));
        columns.Add("ParentID", typeof(int));
        columns.Add("Key", typeof(string));
        columns.Add("AssociatedKey", typeof(string));
        columns.Add("ChannelNode", typeof(int));
        columns.Add("RootElement", typeof(bool));
        columns.Add("Attribute", typeof(string));
        columns.Add("Value", typeof(string));

        attributeTreeDataSet.Tables.Add(attributeTable);
        attributeTreeDataSet.Relations.Add("Attributes", attributeTable.Columns["ID"], attributeTable.Columns["ParentID"]);

        return attributeTreeDataSet;
    }

    #region [ Attribute Tree Events ]

    private void TreeFrameAttributes_ColumnSetGenerated(object sender, ColumnSetGeneratedEventArgs e)
    {
        // Hide the ID, ParentID and Key columns since they are relational numbers that won't make sense to user
        UltraTreeColumnSet columnSet = e.ColumnSet;

        columnSet.Columns["ID"].Visible = false;
        columnSet.Columns["ParentID"].Visible = false;
        columnSet.Columns["Key"].Visible = false;
        columnSet.Columns["AssociatedKey"].Visible = false;
        columnSet.Columns["ChannelNode"].Visible = false;
        columnSet.Columns["RootElement"].Visible = false;

        columnSet.NodeTextColumn = columnSet.Columns["Attribute"];
    }

    private void TreeFrameAttributes_BeforeDataNodesCollectionPopulated(object sender, BeforeDataNodesCollectionPopulatedEventArgs e)
    {
        if (e.Nodes.ParentNode is null)
            Cursor = Cursors.WaitCursor;
    }

    private void TreeFrameAttributes_AfterDataNodesCollectionPopulated(object sender, AfterDataNodesCollectionPopulatedEventArgs e)
    {
        if (e.Nodes.ParentNode is null)
            Cursor = Cursors.Default;
    }

    private void TreeFrameAttributes_InitializeDataNode(object sender, InitializeDataNodeEventArgs e)
    {
        // Initialize data nodes - this event fires every time a node is added to the tree from a data source
        UltraTreeNode node = e.Node;
        int rootChannelNode = Convert.ToInt32(node.RootNode.Cells["ChannelNode"].Value);

        // Make sure that only the channel frames show up at the root level of the tree
        if (rootChannelNode is > 0 and <= (int)FundamentalFrameType.Undetermined || Convert.ToBoolean(node.Cells["RootElement"].Value))
        {
            int channelNode = Convert.ToInt32(node.Cells["ChannelNode"].Value);

            // Track key nodes as they're added
            if (node.Cells["Key"].Value is not DBNull)
                m_associatedNodes.TryAdd(node.Cells["Key"].Value.ToString(), e.Node);

            switch (channelNode)
            {
                case > 0:
                {
                    // We highlight all channel nodes (actual phasor class instances) to distinguish them from their attributes
                    Override nodeOverride = node.Override;

                    nodeOverride.ShowColumns = DefaultableBoolean.False;
                    nodeOverride.BorderStyleNode = UIElementBorderStyle.Solid;
                    nodeOverride.ItemHeight = 20;

                    AppearanceBase nodeAppearance = nodeOverride.NodeAppearance;

                    nodeAppearance.BackColor = m_applicationSettings.ChannelNodeBackgroundColor;
                    nodeAppearance.ForeColor = m_applicationSettings.ChannelNodeForegroundColor;
                    nodeAppearance.FontData.Bold = DefaultableBoolean.True;
                    nodeAppearance.FontData.SizeInPoints = 9f;

                    // Assign a tree key to root frames for quick reference later (used during expand all)
                    if (channelNode <= (int)FundamentalFrameType.Undetermined)
                        node.Key = $"Frame{channelNode}";

                    break;
                }
                case -1:
                {
                    // We make virtual hyperlinks of associated channel nodes
                    node.Override.HotTracking = DefaultableBoolean.True;
                    break;
                }
            }

            // Set initial tree nodes to desired state - initially expanding nodes makes tree build *much* slower
            node.Expanded = m_applicationSettings.InitialNodeState == ApplicationSettings.NodeState.Expanded;

            if (node.Expanded)
                Application.DoEvents();
        }
        else
        {
            // We hide all nodes that are not direct descendants of a channel frame
            node.Visible = false;
        }
    }

    private void TreeFrameAttributes_MouseMove(object sender, MouseEventArgs e)
    {
        // Show a "hand" cursor over channel nodes acting as "links"
        UltraTreeNode node = TreeFrameAttributes.GetNodeFromPoint(e.X, e.Y);

        if (node is null)
            return;

        node.DataColumnSetResolved.ColumnSettings.CellAppearance.Cursor = Convert.ToInt32(node.Cells["ChannelNode"].Value) == -1 ?
            Cursors.Hand :
            Cursors.Default;
    }

    private void TreeFrameAttributes_MouseClick(object sender, MouseEventArgs e)
    {
        // Handle "clicking" of "link" by bringing associated node into view
        UltraTreeNode node = TreeFrameAttributes.GetNodeFromPoint(e.X, e.Y);

        if (node is null)
            return;

        if (Convert.ToInt32(node.Cells["ChannelNode"].Value) != -1)
            return;

        if (node.Cells["AssociatedKey"].Value is DBNull)
            return;

        // Mouse click event needs to return thread to tree control and looking up channel node may
        // be time-consuming since we'll need to be expanding nodes, so we queue this work up on a
        // different thread.  Actual work to be performed will still be on UI thread via Invoke, but
        // this event completes and returns thread control to the tree.  This step also allows calls
        // to DoEvents which could otherwise cause problems if called within the local event context.
        ThreadPool.QueueUserWorkItem(LookupAssociatedChannelNode, node.Cells["AssociatedKey"].Value.ToString());
    }

    // UI work must be invoked on UI thread
    private void LookupAssociatedChannelNode(object state)
    {
        BeginInvoke(new Action<string>(LookupAssociatedChannelNode), state);
    }

    private void LookupAssociatedChannelNode(string associatedKey)
    {
        UltraTreeNode destinationNode = null;

        while (destinationNode is null)
        {
            if (m_associatedNodes.TryGetValue(associatedKey, out destinationNode))
            {
                // Lookup associated node (may not be defined yet as nodes are added on demand)
                destinationNode.BringIntoView(true);
                destinationNode.Selected = true;
            }
            else if (m_associatedNodes.Count > 0)
            {
                // Start expanding destination nodes until desired element has been defined
                foreach (UltraTreeNode node in m_associatedNodes.Values)
                {
                    if (!node.Expanded)
                    {
                        node.Expanded = true;
                        Application.DoEvents();
                        break;
                    }
                }
            }
            else
            {
                // Can't start expanding until at least one associated node has been defined
                break;
            }
        }
    }

    #endregion

    #endregion

    #region [ Chart Initialization and Display Code ]

    private void InitializeChart()
    {
        InitializeFrequencyLayer();

        InitializePhaseAngleLayer(m_selectedCell is null ? 1 : m_selectedCell.PhasorDefinitions.Count);

        UltraChart chart = ChartDataDisplay;

        chart.BackColor = m_applicationSettings.BackgroundColor;
        chart.Margin = new Padding(0, 0, 0, 0);
        chart.Padding = chart.Margin;

        TitleAppearance titleTop = chart.TitleTop;

        titleTop.Margins.Top = 0;
        titleTop.Margins.Bottom = 10;
        titleTop.Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point);
        titleTop.FontColor = m_applicationSettings.ForegroundColor;
        titleTop.ClipText = false;

        chart.DataBind();
    }

    private void InitializeFrequencyLayer()
    {
        CompositeChartAppearance appearance = ChartDataDisplay.CompositeChart;

        appearance.Legends.Clear();
        appearance.Series.Clear();
        appearance.ChartLayers.Clear();
        appearance.ChartAreas.Clear();

        m_frequencyData = new DataTable();
        m_frequencyData.Columns.Add(new DataColumn("y", typeof(float)));

        // We call BeginDataLoad to disable auto-refresh of charts
        m_frequencyData.BeginLoadData();

        ChartArea frequencyChartArea = new()
        {
            BoundsMeasureType = MeasureType.Percentage,
            Bounds = new Rectangle(0, 7, 100, 43),
            Border = { Thickness = 0 },
            PE = { Fill = m_applicationSettings.BackgroundColor }
        };

        ChartDataDisplay.CompositeChart.ChartAreas.Add(frequencyChartArea);

        AxisItem timeAxis = CreateTimeAxis(10);
        frequencyChartArea.Axes.Add(timeAxis);
        AxisItem frequencyAxis = CreateFrequencyAxis();
        frequencyChartArea.Axes.Add(frequencyAxis);

        ChartLayerAppearance frequencyLayer = CreateFrequencyLayer(frequencyChartArea, timeAxis, frequencyAxis);
        ChartDataDisplay.CompositeChart.ChartLayers.Add(frequencyLayer);
    }

    private void InitializePhaseAngleLayer(int phasorCount)
    {
        CompositeChartAppearance appearance = ChartDataDisplay.CompositeChart;

        appearance.Legends.Clear();

        if (appearance.Series.Count > 1)
            appearance.Series.RemoveAt(1);

        if (appearance.ChartLayers.Count > 1)
            appearance.ChartLayers.RemoveAt(1);

        if (appearance.ChartAreas.Count > 1)
            appearance.ChartAreas.RemoveAt(1);

        m_phasorData = new DataTable();

        for (int i = 0; i < phasorCount; i++)
            m_phasorData.Columns.Add(new DataColumn($"y{i}", typeof(float)));

        // We call BeginDataLoad to disable auto-refresh of charts
        m_phasorData.BeginLoadData();

        ChartArea phaseAngleChartArea = new()
        {
            BoundsMeasureType = MeasureType.Percentage,
            Bounds = new Rectangle(0, 50, m_applicationSettings.ShowPhaseAngleLegend ? 80 : 100, 50),
            Border = { Thickness = 0 },
            PE = { Fill = m_applicationSettings.BackgroundColor }
        };

        ChartDataDisplay.CompositeChart.ChartAreas.Add(phaseAngleChartArea);

        AxisItem timeAxis = CreateTimeAxis(0);
        phaseAngleChartArea.Axes.Add(timeAxis);
        AxisItem phaseAngleAxis = CreatePhaseAngleAxis();
        phaseAngleChartArea.Axes.Add(phaseAngleAxis);

        ChartLayerAppearance phaseAngleLayer = CreatePhaseAngleLayer(phaseAngleChartArea, timeAxis, phaseAngleAxis);
        ChartDataDisplay.CompositeChart.ChartLayers.Add(phaseAngleLayer);

        if (!m_applicationSettings.ShowPhaseAngleLegend)
            return;

        CompositeLegend phasorLegend = CreatePhasorLegend();
        ChartDataDisplay.CompositeChart.Legends.Add(phasorLegend);
        phasorLegend.ChartLayers.Add(phaseAngleLayer);
    }

    private AxisItem CreateTimeAxis(int extent) => new()
    {
        OrientationType = AxisNumber.X_Axis,
        DataType = AxisDataType.String,
        Visible = true,
        Labels = { Visible = false },
        SetLabelAxisType = SetLabelAxisType.ContinuousData,
        LineThickness = 1,
        LineColor = m_applicationSettings.ForegroundColor,
        Extent = extent,
        MinorGridLines = { Visible = false },
        MajorGridLines = { Visible = false }
    };

    private AxisItem CreateFrequencyAxis()
    {
        AxisItem axis = new()
        {
            OrientationType = AxisNumber.Y_Axis,
            DataType = AxisDataType.Numeric,
            Visible = true,
            Labels =
            {
                ItemFormatString = "<DATA_VALUE:0.0000>",
                Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point),
                FontColor = m_applicationSettings.ForegroundColor
            },
            LineThickness = 1,
            LineColor = m_applicationSettings.ForegroundColor,
            Extent = 50,
            MinorGridLines = { Visible = false },
            MajorGridLines = { Visible = true },
            TickmarkStyle = AxisTickStyle.Percentage,
            TickmarkPercentage = 23d,
            TickmarkInterval = 0.001d,
            RangeType = AxisRangeType.Automatic,
            RangeMin = 59.9d,
            RangeMax = 60.1d
        };

        axis.Margin.Far.Value = 4d;
        axis.Margin.Near.Value = 4d;

        return axis;
    }

    private AxisItem CreatePhaseAngleAxis() => new()
    {
        OrientationType = AxisNumber.Y_Axis,
        DataType = AxisDataType.Numeric,
        Visible = true,
        Labels =
        {
            ItemFormatString = "<DATA_VALUE:0.#>",
            Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point),
            FontColor = m_applicationSettings.ForegroundColor,
            HorizontalAlign = StringAlignment.Near
        },
        LineThickness = 1,
        LineColor = m_applicationSettings.ForegroundColor,
        Extent = 30,
        MinorGridLines = { Visible = false },
        MajorGridLines = { Visible = true },
        TickmarkStyle = AxisTickStyle.Percentage,
        TickmarkPercentage = 25d,
        RangeType = AxisRangeType.Custom,
        RangeMin = -180,
        RangeMax = 180d
    };

    private ChartLayerAppearance CreateFrequencyLayer(ChartArea area, AxisItem xAxis, AxisItem yAxis)
    {
        ChartLayerAppearance frequencyLayer = new()
        {
            ChartType = ChartType.SplineChart,
            ChartArea = area,
            AxisX = xAxis,
            AxisY = yAxis
        };

        SplineChartAppearance appearance = (SplineChartAppearance)frequencyLayer.ChartTypeAppearance;
        appearance.MidPointAnchors = m_applicationSettings.ShowDataPointsOnGraphs;
        appearance.Thickness = m_applicationSettings.TrendLineWidth;

        NumericSeries frequencyDataSeries = new();
        frequencyDataSeries.DataBind(m_frequencyData, "y");

        // Set frequency color
        frequencyDataSeries.PEs.Add(new PaintElement(m_applicationSettings.FrequencyColor));
        ChartDataDisplay.CompositeChart.Series.Add(frequencyDataSeries);
        frequencyLayer.Series.Add(frequencyDataSeries);

        return frequencyLayer;
    }

    private ChartLayerAppearance CreatePhaseAngleLayer(ChartArea area, AxisItem xAxis, AxisItem yAxis)
    {
        ChartLayerAppearance phaseAngleLayer = new()
        {
            ChartType = ChartType.SplineChart,
            ChartArea = area,
            AxisX = xAxis,
            AxisY = yAxis
        };

        SplineChartAppearance appearance = (SplineChartAppearance)phaseAngleLayer.ChartTypeAppearance;
        appearance.MidPointAnchors = m_applicationSettings.ShowDataPointsOnGraphs;
        appearance.Thickness = m_applicationSettings.TrendLineWidth;

        for (int i = 0; i < m_phasorData.Columns.Count; i++)
        {
            NumericSeries phasorDataSeries = new();

            phasorDataSeries.SetNoUpdate(true);
            phasorDataSeries.DataBind(m_phasorData, $"y{i}");
            phasorDataSeries.Label = m_selectedCell is null ?
                $"Phasor {(i + 1)}" :
                m_selectedCell.PhasorDefinitions[i].Label;

            // Set phase angle color (rotating through configured set of colors)
            phasorDataSeries.PEs.Add(new PaintElement(m_applicationSettings.PhaseAngleColors[i % m_applicationSettings.PhaseAngleColors.Count]));
            ChartDataDisplay.CompositeChart.Series.Add(phasorDataSeries);
            phaseAngleLayer.Series.Add(phasorDataSeries);
        }

        return phaseAngleLayer;
    }

    private CompositeLegend CreatePhasorLegend()
    {
        CompositeLegend phasorLegend = new();
        phasorLegend.Bounds = new Rectangle(79, 50, 20, 48); // 78, 83, 50, 15)
        phasorLegend.BoundsMeasureType = MeasureType.Percentage;
        phasorLegend.PE.Fill = m_applicationSettings.LegendBackgroundColor;

        LabelStyle style = phasorLegend.LabelStyle;
        style.HorizontalAlign = StringAlignment.Near;
        style.FontSizeBestFit = true;
        //style.Font = new("Verdana", 8, FontStyle.Bold, GraphicsUnit.Point);
        style.ClipText = false;
        style.WrapText = false;
        style.FontColor = m_applicationSettings.LegendForegroundColor;
        style.Trimming = StringTrimming.EllipsisWord;

        BorderAppearance appearance = phasorLegend.Border;
        appearance.Color = m_applicationSettings.ForegroundColor;
        appearance.CornerRadius = 5;
        appearance.Thickness = 1;

        return phasorLegend;
    }

    #endregion

    #endregion
}
