//******************************************************************************************************
//  AlternateCommandChannel.cs - Gbtc
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
//  10/16/2014 - J. Ritchie Carroll
//       Generated original version of source code.
//  03/26/2022 - J. Ritchie Carroll
//       Generated original version of source code.
//       Migrated code from VB to C# assisted with Code Converter (VB - C#):
//       https://marketplace.visualstudio.com/items?itemName=SharpDevelopTeam.CodeConverter
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GSF;
using GSF.Communication;
using Infragistics.Win.UltraWinMaskedEdit;

namespace ConnectionTester;

public partial class AlternateCommandChannel
{
    public int NetworkInterface;

    public AlternateCommandChannel()
    {
        // This call is required by the Windows Form Designer.
        InitializeComponent();

        // Initialize serial port selection lists
        foreach (string port in System.IO.Ports.SerialPort.GetPortNames())
            ComboBoxSerialPorts.Items.Add(port);

        foreach (string parity in Enum.GetNames(typeof(System.IO.Ports.Parity)))
            ComboBoxSerialParities.Items.Add(parity);

        foreach (string stopbit in Enum.GetNames(typeof(System.IO.Ports.StopBits)))
            ComboBoxSerialStopBits.Items.Add(stopbit);

        // Default to TCP tab
        TabControlCommunications.Tabs[(int)TransportProtocol.Tcp].Selected = true;
    }

    private void CheckBoxUndefined_CheckedChanged(object sender, EventArgs e)
    {
        // Enable or disable command channel settings tab
        TabControlCommunications.Enabled = !CheckBoxUndefined.Checked;

        // Automatically show change in link label text/format on parent for to cue user of defined state indicator
        if (Visible)
            Forms.PMUConnectionTester.UpdateAlternateCommandChannelLabel();
    }

    private void ButtonBrowse_Click(object sender, EventArgs e)
    {
        OpenFileDialog dialog = Forms.PMUConnectionTester.OpenFileDialog;
            
        dialog.Title = "Open Capture File";
        dialog.Filter = "Captured Files (*.PmuCapture)|*.PmuCapture|All Files|*.*";
        dialog.FileName = "";
            
        if (dialog.ShowDialog(this) == DialogResult.OK)
            TextBoxFileCaptureName.Text = dialog.FileName;
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
                maskedEdit.SelectionStart = 0;
                maskedEdit.SelectionLength = maskedEdit.Text.Length;
                break;
            case TextBox windowsTextBox:
                windowsTextBox.SelectAll();
                break;
        }
    }

    private void TextBox_MouseClick(object sender, MouseEventArgs e) => 
        TextBox_GotFocus(sender, e);

    private void LabelTcpNetworkInterface_Click(object sender, EventArgs e)
    {
        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex = NetworkInterface;

        if (Forms.NetworkInterfaceSelector.ShowDialog(this) == DialogResult.OK)
            NetworkInterface = Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex;

        Forms.NetworkInterfaceSelector.ComboBoxNetworkInterfaces.SelectedIndex = -1;
    }

    public bool IsDefined => !CheckBoxUndefined.Checked;

    public string ConnectionString
    {
        get => CheckBoxUndefined.Checked ? "" :
            TabControlCommunications.SelectedTab.Index switch
            {
                (int)TransportProtocol.Tcp => $"; commandchannel={{protocol=Tcp; server={TextBoxTcpHostIP.Text}; port={TextBoxTcpPort.Text}; interface={Forms.PMUConnectionTester.GetNetworkInterfaceValue(NetworkInterface)}}}",
                (int)TransportProtocol.Serial - 1 => // UDP removed from tab set...
                    $"; commandchannel={{protocol=Serial; port={ComboBoxSerialPorts.Text}; baudrate={ComboBoxSerialBaudRates.Text}; parity={ComboBoxSerialParities.Text}; stopbits={ComboBoxSerialStopBits.Text}; databits={TextBoxSerialDataBits.Text}; dtrenable={CheckBoxSerialDTR.Checked}; rtsenable={CheckBoxSerialRTS.Checked}}}",
                (int)TransportProtocol.File - 1 => // UDP removed from tab set...
                    $"; commandchannel={{protocol=File; file={TextBoxFileCaptureName.Text}}}",
                _ => ""
            };

        set
        {
            Dictionary<string, string> connectionData = value.ParseKeyValuePairs();

            if (connectionData.ContainsKey("commandchannel"))
            {
                CheckBoxUndefined.Checked = false;
                connectionData = connectionData["commandchannel"].ParseKeyValuePairs();
                TransportProtocol protocol = (TransportProtocol)Enum.Parse(typeof(TransportProtocol), connectionData["protocol"], true);

                // Load remaining connection settings
                TabControlCommunications.Tabs[Common.IIf(protocol > TransportProtocol.Tcp, (int)protocol - 1, (int)protocol)].Selected = true;
                    
                switch (protocol)
                {
                    case TransportProtocol.Tcp:
                        TextBoxTcpPort.Text = connectionData["port"];
                        Forms.PMUConnectionTester.AssignHostIP(TextBoxTcpHostIP, connectionData["server"]);
                        NetworkInterface = connectionData.TryGetValue("interface", out string interfaceIP) ? Forms.PMUConnectionTester.GetNetworkInterfaceIndex(interfaceIP) : 0;

                        break;

                    case TransportProtocol.Serial:
                        ComboBoxSerialPorts.Text = connectionData["port"];
                        ComboBoxSerialBaudRates.Text = connectionData["baudrate"];
                        ComboBoxSerialParities.Text = connectionData["parity"];
                        ComboBoxSerialStopBits.Text = connectionData["stopbits"];
                        TextBoxSerialDataBits.Text = connectionData["databits"];
                        CheckBoxSerialDTR.Checked = connectionData["dtrenable"].ParseBoolean();
                        CheckBoxSerialRTS.Checked = connectionData["rtsenable"].ParseBoolean();
                        break;

                    case TransportProtocol.File:
                        TextBoxFileCaptureName.Text = connectionData["file"];
                        break;
                }
            }
            else
            {
                CheckBoxUndefined.Checked = true;
            }
        }
    }
}