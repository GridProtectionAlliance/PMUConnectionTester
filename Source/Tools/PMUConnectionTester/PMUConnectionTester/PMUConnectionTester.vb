'******************************************************************************************************
'  PMUConnectionTester.vb - Gbtc
'
'  Copyright � 2010, Grid Protection Alliance.  All Rights Reserved.
'
'  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
'  the NOTICE file distributed with this work for additional information regarding copyright ownership.
'  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
'  not use this file except in compliance with the License. You may obtain a copy of the License at:
'
'      http://www.opensource.org/licenses/eclipse-1.0.php
'
'  Unless agreed to in writing, the subject software distributed under the License is distributed on an
'  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
'  License for the specific language governing permissions and limitations.
'
'  Code Modification History:
'  ----------------------------------------------------------------------------------------------------
'  03/16/2006 - J. Ritchie Carroll
'       Initial version of source generated.
'  01/21/2011 - AJ Stadlin
'       ShowMessagesTabOnDataException Option added to DataStreamException
'  05/19/2011 - Ritchie
'       Added DST file support.
'
'******************************************************************************************************

Imports Infragistics.Win
Imports Infragistics.Win.UltraWinTree
Imports Infragistics.Win.UltraWinMaskedEdit
Imports Infragistics.UltraChart.Shared.Styles
Imports Infragistics.UltraChart.Resources.Appearance
Imports Infragistics.UltraChart.Core.Layers
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Runtime.Serialization.Formatters
Imports System.Runtime.Serialization.Formatters.Soap
Imports System.Windows.Forms.DialogResult
Imports System.Drawing
Imports System.Threading
Imports System.ComponentModel
Imports TVA
Imports TVA.Collections
Imports TVA.Common
Imports TVA.Communication
Imports TVA.Configuration
Imports TVA.IO.FilePath
Imports TVA.Parsing
Imports TVA.PhasorProtocols
Imports TVA.Reflection
Imports TVA.Reflection.AssemblyInfo
Imports TVA.Units
Imports TVA.Windows
Imports TVA.Windows.Forms

Public Class PMUConnectionTester

#Region " Private Member Declarations "

    Private Const TotalFramesPanel As Integer = 1
    Private Const FramesPerSecondPanel As Integer = 3
    Private Const TotalBytesPanel As Integer = 5
    Private Const BitRatePanel As Integer = 7
    Private Const QueuedBuffersPanel As Integer = 9
    Private Const TextFileWidth As Integer = 75

    Private Enum ChartTabs
        Graph
        Settings
        Messages
        ProtocolSpecific
    End Enum

    Private Enum ProtocolTabs
        Protocol
        ExtraParameters
    End Enum

    ' Phasor parsing variables
    Private WithEvents m_frameParser As MultiProtocolFrameParser
    Private m_configurationFrame As IConfigurationFrame
    Private m_configChangeDetected As Boolean
    Private m_configChangeTime As Long
    Private m_selectedCell As IConfigurationCell
    Private m_lastFrameType As FundamentalFrameType
    Private m_byteEncoding As ByteEncoding
    Private m_byteCount As Integer
    Private m_sqrtOf3 As Single = Convert.ToSingle(System.Math.Sqrt(3))

    ' Application variables
    Friend WithEvents m_applicationSettings As ApplicationSettings
    Private m_applicationName As String
    Private m_lastFileName As String
    Private m_ipStackIsIPv6 As Boolean
    Private m_loopbackAddress As String

    ' Charting data variables
    Private m_frequencyData As DataTable
    Private m_phasorData As DataTable
    Private m_lastPhaseAngle As Angle
    Private m_lastRefresh As Long

    ' Attribute tree data variables
    Private m_attributeFrames As Dictionary(Of FundamentalFrameType, IChannelFrame)
    Private m_associatedNodes As Dictionary(Of String, UltraTreeNode)

    ' File capture variables
    Private m_frameCaptureStream As FileStream
    Private m_frameSampleStream As StreamWriter
    Private m_frameCaptureFileName As String
    Private m_streamDebugCapture As StreamWriter
    Private m_capturedFrames As Byte
    Private m_dataStreamLock As Object
    Private m_lastConnectionFileName As String

#End Region

#Region " Private Methods Implementation "

#Region " Primary Form Event Handlers "

    Private Sub PMUConnectionTester_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' We display a custom message for unhandled exceptions
        GlobalExceptionLogger.ErrorTextMethod = AddressOf UnhandledExceptionErrorMessage

        ' Make sure application settings exist
        m_applicationSettings = New ApplicationSettings

        ' Create a new multi-protocol frame parser
        m_frameParser = New MultiProtocolFrameParser

        ' Initialize attribute tree variables
        m_attributeFrames = New Dictionary(Of FundamentalFrameType, IChannelFrame)
        m_associatedNodes = New Dictionary(Of String, UltraTreeNode)

        m_dataStreamLock = New Object

        ' Make sure a folder exists for PMU Connection Tester files
        Dim personalDataFolder As String = AddPathSuffix(GetApplicationDataFolder())

        If Not Directory.Exists(personalDataFolder) Then
            Directory.CreateDirectory(personalDataFolder)
        End If

        ' Make sure a last run connection file exists in personal folder
        m_lastConnectionFileName = personalDataFolder & "LastRun.PmuConnection"

        If Not File.Exists(m_lastConnectionFileName) Then
            File.Copy(GetAbsolutePath("LastRun.PmuConnection"), m_lastConnectionFileName)
        End If

#If DEBUG Then
        LabelVersion.Text = "DEBUG VERSION"
#Else
        With EntryAssembly.Version
            LabelVersion.Text = "Version " & .Major & "." & .Minor & "." & .Build ' & "." & .Revision
        End With
#End If

        ' Initialize phasor protocol selection list
        Dim protocols As Integer() = CType([Enum].GetValues(GetType(PhasorProtocol)), Integer())

        For x As Integer = 0 To protocols.Length - 1
            ComboBoxProtocols.Items.Add(CType(protocols(x), PhasorProtocol).GetFormattedProtocolName())
        Next

        ' Initialize serial port selection lists
        For Each port As String In Ports.SerialPort.GetPortNames()
            ComboBoxSerialPorts.Items.Add(port)
        Next

        For Each parity As String In [Enum].GetNames(GetType(Ports.Parity))
            ComboBoxSerialParities.Items.Add(parity)
        Next

        For Each stopbit As String In [Enum].GetNames(GetType(Ports.StopBits))
            If stopbit <> "None" Then
                ComboBoxSerialStopBits.Items.Add(stopbit)
            End If
        Next

        ' Properly position extra connection parameters group box.  Since it's
        ' a top-most control we move it off to the side at design time
        GroupBoxProtocolParameters.Location = New Point(198, 63)

        ' Adjust size of comments area of extra connection parameters properties grid
        PropertyGridProtocolParameters.AdjustCommentAreaHeight(6)

        ' Hide protocol panel after comment area size is adjusted - should only be visible if extra parameters tab is selected
        GroupBoxProtocolParameters.Visible = False

        ' Assign application settings to property grid
        PropertyGridApplicationSettings.SelectedObject = m_applicationSettings

        ' Adjust size of comments area and label column ratio of application settings properties grid
        PropertyGridApplicationSettings.AdjustCommentAreaHeight(4)
        PropertyGridApplicationSettings.AdjustLabelRatio(1.62)

        Try
            ' Determine if default IP stack is IPv6
            m_ipStackIsIPv6 = Transport.GetDefaultIPStack() = IPStack.IPv6

            If m_ipStackIsIPv6 Then
                LabelDefaultIPStack.Text = "Default IP Stack: IPv6"
            Else
                LabelDefaultIPStack.Text = "Default IP Stack: IPv4"
            End If
        Catch
            ' Default to IPv4
            m_ipStackIsIPv6 = False
            LabelDefaultIPStack.Text = "Default IP Stack: IPv4"
        End Try

        ' Setup IP mode
        ForceIPv4 = m_applicationSettings.ForceIPv4

        ' Assign default loopback addresses
        TextBoxTcpHostIP.Text = m_loopbackAddress
        TextBoxUdpHostIP.Text = m_loopbackAddress
        AlternateCommandChannel.TextBoxTcpHostIP.Text = m_loopbackAddress

        InitializeChart()
        ComboBoxProtocols.SelectedIndex = 0
        ComboBoxCommands.SelectedIndex = 0
        ComboBoxByteEncodingDisplayFormats.SelectedIndex = 0
        GroupBoxConfigurationFrame.Expanded = False
        GroupBoxHeaderFrame.Expanded = False
        GroupBoxStatus.Expanded = False
        LabelBinaryFrameImage.Tag = LabelBinaryFrameImage.Text
        If ComboBoxSerialPorts.Items.Count > 0 Then ComboBoxSerialPorts.SelectedIndex = 0
        ComboBoxSerialBaudRates.SelectedIndex = 0
        ComboBoxSerialParities.SelectedIndex = 0
        ComboBoxSerialStopBits.SelectedIndex = 0
        LabelSelectedIsRefAngle.Visible = (m_applicationSettings.PhaseAngleGraphStyle = ApplicationSettings.AngleGraphStyle.Relative)

        If Environment.GetCommandLineArgs.Length > 1 Then
            Dim commandFile As String = Environment.GetCommandLineArgs(1)
            Select Case Path.GetExtension(commandFile).ToLower().Trim()
                Case ".pmuconnection"
                    LoadConnectionSettings(commandFile)
                Case ".pmucapture"
                    TextBoxFileCaptureName.Text = commandFile
                    TabControlCommunications.Tabs(TransportProtocol.File).Selected = True
            End Select
        ElseIf m_applicationSettings.RestoreLastConnectionSettings AndAlso File.Exists(m_lastConnectionFileName) Then
            LoadConnectionSettings(m_lastConnectionFileName)
        End If

        Me.RestoreLayout()

    End Sub

    Private Sub PMUConnectionTester_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Me.Hide()
        Application.DoEvents()

        Try
            ' Save application settings
            m_applicationSettings.Save()

            ' Save current connection settings
            SaveConnectionSettings(m_lastConnectionFileName)

            ' Save current window settings
            Me.SaveLayout()
        Catch
            ' Don't want any possible failures during this event to prevent shudown :)
        End Try

        Shutdown()

    End Sub

    Private Sub PMUConnectionTester_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        TimerDelay.Start()

    End Sub

    Private Sub TimerDelay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerDelay.Tick

        ' We show the default IP stack label on startup only after a momentary pause - this prevents the visual side effect
        ' of having the label's grey background stand out over a temporary white background
        LabelDefaultIPStack.Visible = GroupBoxConnection.Expanded
        TimerDelay.Stop()

    End Sub

    Private Sub PMUConnectionTester_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter

        ' We allow file drops from explorer onto connection tester
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub PMUConnectionTester_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop

        Try
            Dim fileNames As Array = TryCast(e.Data.GetData(DataFormats.FileDrop), Array)

            If fileNames IsNot Nothing Then
                ' Only dealing with first file dropped on the form
                Dim fileName As String = fileNames.GetValue(0).ToString()

                If File.Exists(fileName) Then
                    If Path.GetExtension(fileName).ToLower().Trim() = ".pmuconnection" Then
                        ' Deserializing connection settings may take a moment and explorer thread will be pending,
                        ' so we queue load operation on the form's invocation queue
                        Me.BeginInvoke(New Action(Of String)(AddressOf LoadConnectionSettings), fileName)
                    Else
                        ' All other files are assumed to be a capture file
                        TextBoxFileCaptureName.Text = fileName
                        TabControlCommunications.Tabs(TransportProtocol.File).Selected = True
                    End If

                    ' Show form in case explorer is overlapping
                    Me.Activate()
                End If
            End If
        Catch ex As Exception
            AppendStatusMessage(String.Format("Exception occured while dropping file name: {0}", ex.Message))
        End Try

    End Sub

    Private Sub ButtonListen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonListen.Click

        If m_frameParser.Enabled Then
            Disconnect()
        Else
            Connect()
        End If

    End Sub

    Private Sub ButtonSendCommand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSendCommand.Click

        SendDeviceCommand(CType(ComboBoxCommands.SelectedIndex + 1, DeviceCommand))

    End Sub

    Private Sub ComboBoxPmus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPmus.SelectedIndexChanged

        m_selectedCell = Nothing

        If m_configurationFrame IsNot Nothing Then
            m_selectedCell = m_configurationFrame.Cells(ComboBoxPmus.SelectedIndex)

            LabelIDCode.Text = m_selectedCell.IDCode.ToString()
            LabelPhasorCount.Text = m_selectedCell.PhasorDefinitions.Count.ToString()
            LabelAnalogCount.Text = m_selectedCell.AnalogDefinitions.Count.ToString()
            LabelDigitalCount.Text = m_selectedCell.DigitalDefinitions.Count.ToString()
            LabelNominalFrequency.Text = CInt(m_selectedCell.NominalFrequency).ToString()

            ComboBoxVoltagePhasors.Items.Clear()
            ComboBoxCurrentPhasors.Items.Clear()

            With m_selectedCell.PhasorDefinitions
                Dim phasor As IPhasorDefinition
                ComboBoxPhasors.Items.Clear()

                For x As Integer = 0 To .Count - 1
                    phasor = .Item(x)

                    If phasor.PhasorType = PhasorType.Voltage Then
                        ComboBoxPhasors.Items.Add("V: " & phasor.Label)
                        ComboBoxVoltagePhasors.Items.Add(x & ": " & phasor.Label)
                    Else
                        ComboBoxPhasors.Items.Add("I: " & phasor.Label)
                        ComboBoxCurrentPhasors.Items.Add(x & ": " & phasor.Label)
                    End If
                Next
            End With

            If ComboBoxPhasors.Items.Count > 0 Then ComboBoxPhasors.SelectedIndex = 0 Else ComboBoxPhasors.SelectedIndex = -1
            If ComboBoxVoltagePhasors.Items.Count > 0 Then ComboBoxVoltagePhasors.SelectedIndex = 0 Else ComboBoxVoltagePhasors.SelectedIndex = -1
            If ComboBoxCurrentPhasors.Items.Count > 0 Then ComboBoxCurrentPhasors.SelectedIndex = 0 Else ComboBoxCurrentPhasors.SelectedIndex = -1

            InitializePhaseAngleLayer(m_selectedCell.PhasorDefinitions.Count)
        End If

    End Sub

    Private Sub ComboBoxPhasors_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPhasors.SelectedIndexChanged

        m_phasorData.Rows.Clear()
        m_lastPhaseAngle = 0.0!

    End Sub

    Private Sub ComboBoxByteEncodingDisplayFormats_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxByteEncodingDisplayFormats.SelectedIndexChanged

        Select Case ComboBoxByteEncodingDisplayFormats.SelectedIndex
            Case 0
                m_byteEncoding = ByteEncoding.Hexadecimal
            Case 1
                m_byteEncoding = ByteEncoding.Decimal
            Case 2
                m_byteEncoding = ByteEncoding.BigEndianBinary
            Case 3
                m_byteEncoding = ByteEncoding.LittleEndianBinary
            Case 4
                m_byteEncoding = ByteEncoding.Base64
            Case 5
                m_byteEncoding = ByteEncoding.ASCII
            Case Else
                m_byteEncoding = ByteEncoding.Hexadecimal
        End Select

    End Sub

    Private Sub ComboBoxProtocols_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxProtocols.SelectedIndexChanged

        If m_frameParser IsNot Nothing Then
            With ToolTipManagerForExtraParameters
                Dim toolTip As UltraWinToolTip.UltraToolTipInfo = .GetUltraToolTip(TabControlProtocolParameters)

                ' Assign new protocol selection to frame parser - this will retrieve a default set of connection
                ' parameters for the protocol if any are available
                m_frameParser.PhasorProtocol = CType(ComboBoxProtocols.SelectedIndex, PhasorProtocol)

                If m_frameParser.ConnectionParameters Is Nothing Then
                    ' Protocol has no extra connection parameters - hide extra parameters tab
                    TabControlProtocolParameters.Tabs(ProtocolTabs.ExtraParameters).Visible = False
                    PropertyGridProtocolParameters.SelectedObject = Nothing

                    ' Hide tool tip for protocol's with no extra parameters
                    toolTip.Enabled = DefaultableBoolean.False
                    .SetUltraToolTip(TabControlProtocolParameters, toolTip)
                Else
                    ' Protocol has extra connection parameters - show extra parameters tab
                    TabControlProtocolParameters.Tabs(ProtocolTabs.ExtraParameters).Visible = True
                    PropertyGridProtocolParameters.SelectedObject = m_frameParser.ConnectionParameters

                    ' Show tool tip for protocol's with extra parameters (for availability emphasis)
                    Dim targetPoint As New Point( _
                        Location.X + TabControlCommunications.Location.X + TabControlProtocolParameters.Location.X + 100, _
                        Location.Y + TabControlCommunications.Location.Y + TabControlProtocolParameters.Location.Y + 50)

                    toolTip.Enabled = DefaultableBoolean.True
                    .SetUltraToolTip(TabControlProtocolParameters, toolTip)
                    .ShowToolTip(TabControlProtocolParameters, True, targetPoint)
                End If
            End With
        End If

        ' Because users may be unfamiliar with typical FNET device connection requirements, we default
        ' to the typical connection settings when user selects FNET from the list
        If ComboBoxProtocols.SelectedIndex = PhasorProtocol.FNet Then
            If TabControlCommunications.SelectedTab.Index = TransportProtocol.File Then
                TextBoxFileFrameRate.Text = "10"
            Else
                TabControlCommunications.Tabs(TransportProtocol.Tcp).Selected = True
                CheckBoxEstablishTcpServer.Checked = True
            End If
            ComboBoxByteEncodingDisplayFormats.SelectedIndex = 5
        ElseIf ComboBoxProtocols.SelectedIndex = PhasorProtocol.BpaPdcStream Then
            ' If user selects the BPA protocol when a DST file is selected for input, make sure to automatically set the UsePhasorDataFileFormat to true
            If TabControlCommunications.SelectedTab.Index = TransportProtocol.File And GetExtension(TextBoxFileCaptureName.Text).ToLower() = ".dst" Then
                CType(m_frameParser.ConnectionParameters, BpaPdcStream.ConnectionParameters).UsePhasorDataFileFormat = True
            End If
        ElseIf ComboBoxProtocols.SelectedIndex = PhasorProtocol.Macrodyne Then
            ' Macrodyne connections work best when real-time data is disabled before sending other commands
            m_applicationSettings.SkipDisableRealTimeData = False
            PropertyGridApplicationSettings.Refresh()
        End If

    End Sub

    'Private Sub ComboBoxCommands_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxCommands.SelectedIndexChanged

    '    ' Some protocols only support enable and disable real-time data commands...
    '    If ComboBoxProtocols.SelectedIndex >= 4 And ComboBoxCommands.SelectedIndex > 1 Then
    '        MsgBox("This protocol only supports enable and disable real-time data commands.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Invalid Command Selection")
    '        ComboBoxCommands.SelectedIndex = 0
    '    End If

    'End Sub

    Private Sub CheckBoxEstablishTcpServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxEstablishTcpServer.CheckedChanged

        Dim needsHostIP As Boolean = Not CheckBoxEstablishTcpServer.Checked

        TextBoxTcpHostIP.Enabled = needsHostIP
        LabelTcpHostIP.Enabled = needsHostIP

        If Not needsHostIP Then TextBoxTcpHostIP.Text = m_loopbackAddress

    End Sub

    Private Sub CheckBoxRemoteUDPServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRemoteUdpServer.CheckedChanged

        Dim needsHostIP As Boolean = CheckBoxRemoteUdpServer.Checked

        TextBoxUdpHostIP.Enabled = needsHostIP
        LabelUdpHostIP.Enabled = needsHostIP

        TextBoxUdpRemotePort.Enabled = needsHostIP
        LabelUdpRemotePort.Enabled = needsHostIP

        If Not needsHostIP Then TextBoxUdpHostIP.Text = m_loopbackAddress

    End Sub

    Private Sub CheckBoxAutoRepeatPlayback_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAutoRepeatPlayback.CheckStateChanged

        If m_frameParser IsNot Nothing Then
            ' Update state in frame parser
            m_frameParser.AutoRepeatCapturedPlayback = CheckBoxAutoRepeatPlayback.Checked

            ' Change in ExecuteParseOnSeparateThread may not be allowed based on connection settings, so we restore accepted state to application settings
            m_applicationSettings.ExecuteParseOnSeparateThread = m_frameParser.ExecuteParseOnSeparateThread
            PropertyGridApplicationSettings.Refresh()
        End If

    End Sub

    Private Sub ButtonGetStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGetStatus.Click

        TabControlChart.Tabs(ChartTabs.Messages).Selected = True

        If m_frameParser Is Nothing Then
            AppendStatusMessage(Environment.NewLine & "Currently not connected to any device.")
        Else
            AppendStatusMessage(Environment.NewLine & m_frameParser.Status)
        End If

    End Sub

    Private Sub ButtonRestoreDefaultSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRestoreDefaultSettings.Click

        If MsgBox("Are you sure you want to restore the default settings?" & vbCrLf & vbCrLf & _
                  "Note that application will be closed and you will need to restart.", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, _
                  "Restore Default Settings") = MsgBoxResult.Yes Then
            Try
                Dim userSettingsFile As String = Path.Combine(GetApplicationDataFolder(), "Settings.xml")
                Disconnect()
                ConfigurationFile.Current.Save(System.Configuration.ConfigurationSaveMode.Full)
                If File.Exists(userSettingsFile) Then File.Delete(userSettingsFile)
                Shutdown()
            Catch ex As Exception
                MsgBox("Exception occured while trying to restore default settings: " & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
            End Try
        End If

    End Sub

    Private Sub ButtonBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBrowse.Click

        With OpenFileDialog
            .Title = "Open Capture File"
            .Filter = "Captured Files (*.PmuCapture)|*.PmuCapture|BPA Phasor Data Files (*.dst)|*.dst|All Files|*.*"
            .FileName = ""
            .CheckFileExists = True
            If .ShowDialog(Me) = OK Then
                TextBoxFileCaptureName.Text = .FileName()
            End If
        End With

    End Sub

    Private Sub TabControlChart_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles TabControlChart.SelectedTabChanged

        ' We load protocol specific data attribute tree when user selects that tab...
        If e.Tab.Index = ChartTabs.ProtocolSpecific AndAlso m_attributeFrames.Count > 0 _
            AndAlso TreeFrameAttributes.Nodes.Count = 0 Then _
                InitializeAttributeTree()

    End Sub

    Private Sub TabControlProtocolParameters_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles TabControlProtocolParameters.SelectedTabChanged

        Select Case e.Tab.Index
            Case ProtocolTabs.Protocol
                GroupBoxProtocolParameters.Visible = False
                ComboBoxProtocols.Focus()
            Case ProtocolTabs.ExtraParameters
                GroupBoxProtocolParameters.Visible = True
                PropertyGridProtocolParameters.Focus()
        End Select

    End Sub

    Private Sub GroupBoxProtocolParameters_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles GroupBoxProtocolParameters.Paint

        ' Draw two short vertical lines to "complete" fake tab that displays extra connection parameters
        With e.Graphics
            .DrawLine(Pens.Gray, 178, 0, 178, 1)
            .DrawLine(Pens.Gray, 275, 0, 275, 1)
        End With

    End Sub

    Private Sub CalculatedPowerOrVarLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles LabelPowerLabel.Click, LabelVarsLabel.Click

        TabControlChart.Tabs(ChartTabs.Settings).Selected = True
        ComboBoxCurrentPhasors.Select()

    End Sub

    Private Sub LabelAlternateCommandChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelAlternateCommandChannel.Click

        Dim connectionString As String = CurrentConnectionSettings.ConnectionString

        ' Show alternate command channel settings as a modal dialog
        If AlternateCommandChannel.ShowDialog(Me) = Cancel Then
            ' User canceled changes, reapply original settings to dialog
            AlternateCommandChannel.ConnectionString = connectionString
        End If

        UpdateAlternateCommandChannelLabel()

    End Sub

    Private Sub LabelAlternateCommandChannelState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelAlternateCommandChannelState.Click

        ' Assume user simply wants to change defined state when clicking on label
        AlternateCommandChannel.CheckBoxUndefined.Checked = AlternateCommandChannel.IsDefined
        UpdateAlternateCommandChannelLabel()

    End Sub

    Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxDeviceID.GotFocus, TextBoxFileCaptureName.GotFocus, TextBoxFileFrameRate.GotFocus, _
        TextBoxSerialDataBits.GotFocus, TextBoxTcpHostIP.GotFocus, TextBoxTcpPort.GotFocus, _
        TextBoxUdpHostIP.GotFocus, TextBoxUdpLocalPort.GotFocus, TextBoxUdpRemotePort.GotFocus

        ' Select all text box contents upon focus or selection
        Dim maskedEdit As UltraMaskedEdit = TryCast(sender, UltraMaskedEdit)

        If maskedEdit IsNot Nothing Then
            If maskedEdit.EditAs = EditAsType.UseSpecifiedMask Then
                maskedEdit.SelectAll()
            Else
                maskedEdit.SelectionStart = 0
                maskedEdit.SelectionLength = maskedEdit.Text.Length
            End If
        Else
            Dim windowsTextBox As TextBox = TryCast(sender, TextBox)

            If windowsTextBox IsNot Nothing Then
                windowsTextBox.SelectAll()
            End If
        End If

    End Sub

    Private Sub TextBox_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles _
        TextBoxDeviceID.MouseClick, TextBoxFileCaptureName.MouseClick, TextBoxFileFrameRate.MouseClick, _
        TextBoxSerialDataBits.MouseClick, TextBoxTcpHostIP.MouseClick, TextBoxTcpPort.MouseClick, _
        TextBoxUdpHostIP.MouseClick, TextBoxUdpLocalPort.MouseClick, TextBoxUdpRemotePort.MouseClick

        TextBox_GotFocus(sender, e)

    End Sub

    Private Sub TextBoxFileCaptureName_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxFileCaptureName.TextChanged

        If Not String.IsNullOrEmpty(TextBoxFileCaptureName.Text) Then
            If GetExtension(TextBoxFileCaptureName.Text).ToLower() = ".dst" Then
                ComboBoxProtocols.SelectedIndex = CType(PhasorProtocol.BpaPdcStream, Integer)
                CType(m_frameParser.ConnectionParameters, BpaPdcStream.ConnectionParameters).UsePhasorDataFileFormat = True
            End If
        End If

    End Sub

    Private Sub PropertyGridApplicationSettings_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGridApplicationSettings.PropertyValueChanged

        ' Dynamically update visual elements as necessary when application settings are updated
        With e.ChangedItem.PropertyDescriptor
            Select Case .Category
                Case ApplicationSettings.ApplicationSettingsCategory
                    If String.Compare(.Name, "ForceIPv4", True) = 0 Then
                        ' Apply change in IP mode
                        ForceIPv4 = m_applicationSettings.ForceIPv4
                    End If
                Case ApplicationSettings.ConnectionSettingsCategory
                    ' We allow run-time dynamic changes to some frame parsing states
                    If String.Compare(.Name, "ExecuteParseOnSeparateThread", True) = 0 Then
                        m_frameParser.ExecuteParseOnSeparateThread = m_applicationSettings.ExecuteParseOnSeparateThread

                        ' Change in ExecuteParseOnSeparateThread may not be allowed based on connection settings, so we restore accepted state to application settings
                        m_applicationSettings.ExecuteParseOnSeparateThread = m_frameParser.ExecuteParseOnSeparateThread
                    ElseIf String.Compare(.Name, "InjectSimulatedTimestamp", True) = 0 Then
                        m_frameParser.InjectSimulatedTimestamp = m_applicationSettings.InjectSimulatedTimestamp
                    ElseIf String.Compare(.Name, "UseHighResolutionInputTimer", True) = 0 Then
                        m_frameParser.UseHighResolutionInputTimer = m_applicationSettings.UseHighResolutionInputTimer

                        ' Change in UseHighResolutionInputTimer may not be allowed if debugger is attached, so we restore accepted state to application settings
                        m_applicationSettings.UseHighResolutionInputTimer = m_frameParser.UseHighResolutionInputTimer
                    End If
                Case ApplicationSettings.ChartSettingsCategory, ApplicationSettings.PhaseAngleGraphCategory, ApplicationSettings.FrequencyGraphCategory
                    If String.Compare(.Name, "PhaseAngleGraphStyle", True) = 0 Then
                        ' This property only changes data that is getting graphed - no need to reinitialize chart
                        LabelSelectedIsRefAngle.Visible = (m_applicationSettings.PhaseAngleGraphStyle = ApplicationSettings.AngleGraphStyle.Relative)
                    Else
                        InitializeChart()
                    End If

                    ' We show chart tab (if data is available) to display changes on any chart property change
                    If m_selectedCell IsNot Nothing Then TabControlChart.Tabs(ChartTabs.Graph).Selected = True
            End Select
        End With

    End Sub

#Region " Menu Event Handlers "

    Private Sub MenuItemLoadConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemLoadConnection.Click

        With OpenFileDialog
            .Title = "Load Connection Settings"
            .Filter = "Connection Files (*.PmuConnection)|*.PmuConnection|All Files (*.*)|*.*"
            .FileName = ""
            .CheckFileExists = True
            If .ShowDialog(Me) = OK Then
                LoadConnectionSettings(.FileName)
            End If
        End With

    End Sub

    Private Sub MenuItemSaveConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSaveConnection.Click

        With SaveFileDialog
            .Title = "Save Connection Settings"
            .Filter = "Connection Files (*.PmuConnection)|*.PmuConnection|All Files (*.*)|*.*"
            .FileName = ""
            If .ShowDialog(Me) = OK Then
                SaveConnectionSettings(.FileName)
            End If
        End With

    End Sub

    Private Sub MenuItemLoadConfigFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemLoadConfigFile.Click

        With OpenFileDialog
            .Title = "Load Configuration File"
            .Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"
            .FileName = ""
            .CheckFileExists = True
            If .ShowDialog(Me) = OK Then
                Dim configFile As FileStream = File.Open(.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)

                With New SoapFormatter
                    .AssemblyFormat = FormatterAssemblyStyle.Simple
                    .TypeFormat = FormatterTypeStyle.TypesWhenNeeded

                    Try
                        m_configurationFrame = Nothing
                        ReceivedConfigFrame(CType(.Deserialize(configFile), IConfigurationFrame))
                        If m_frameParser IsNot Nothing Then m_frameParser.ConfigurationFrame = m_configurationFrame
                    Catch ex As Exception
                        MsgBox("Failed to deserialize configuration frame: " & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
                    End Try
                End With

                configFile.Close()
            End If
        End With

    End Sub

    Private Sub MenuItemSaveConfigFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSaveConfigFile.Click

        If m_configurationFrame Is Nothing Then
            MsgBox("No configuration file has been received or loaded...", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
        Else
            With SaveFileDialog
                .Title = "Save Configuration File"
                .Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"
                .FileName = ""
                If .ShowDialog(Me) = OK Then
                    Dim configFile As FileStream = File.Create(.FileName)

                    With New SoapFormatter
                        .AssemblyFormat = FormatterAssemblyStyle.Simple
                        .TypeFormat = FormatterTypeStyle.TypesWhenNeeded

                        Try
                            .Serialize(configFile, m_configurationFrame)
                        Catch ex As Exception
                            Dim message As Byte() = Encoding.UTF8.GetBytes(ex.Message)
                            configFile.Write(message, 0, message.Length)
                            MsgBox("Failed to serialize configuration frame: " & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
                        End Try
                    End With

                    configFile.Close()

                    If m_applicationSettings.ShowConfigXmlExplorerAfterSave Then
                        ' Open captured XML sample file in explorer...
                        Process.Start("explorer.exe", .FileName)
                    End If
                End If
            End With
        End If

    End Sub

    Private Sub MenuItemStartCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemStartCapture.Click

        With SaveFileDialog
            .Title = "Set Capture File Name"
            .Filter = "Captured Files (*.PmuCapture)|*.PmuCapture|All Files|*.*"
            .FileName = ""
            If .ShowDialog(Me) = OK Then
                Try
                    m_frameCaptureStream = New FileStream(.FileName, FileMode.Create)
                    MenuItemStartCapture.Enabled = False
                    MenuItemStopCapture.Enabled = True
                Catch ex As Exception
                    MsgBox("Failed to start capturing data to """ & .FileName & """: " & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
                End Try
            End If
        End With

    End Sub

    Private Sub MenuItemStopCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemStopCapture.Click

        If m_frameCaptureStream IsNot Nothing Then m_frameCaptureStream.Close()
        m_frameCaptureStream = Nothing

        MenuItemStopCapture.Enabled = False
        MenuItemStartCapture.Enabled = True

    End Sub

    Private Sub MenuItemStartStreamDebugCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemStartStreamDebugCapture.Click

        With SaveFileDialog
            .Title = "Set Stream Debug Capture File Name"
            .Filter = "Debug Captured Files (*.csv)|*.csv|All Files|*.*"
            .FileName = ""
            If .ShowDialog(Me) = OK Then
                Try
                    m_streamDebugCapture = File.CreateText(.FileName)
                    MenuItemStartStreamDebugCapture.Enabled = False
                    MenuItemStopStreamDebugCapture.Enabled = True
                Catch ex As Exception
                    MsgBox("Failed to start stream debug capture to """ & .FileName & """: " & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
                End Try
            End If
        End With

    End Sub

    Private Sub MenuItemStopStreamDebugCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemStopStreamDebugCapture.Click

        If m_streamDebugCapture IsNot Nothing Then m_streamDebugCapture.Close()
        m_streamDebugCapture = Nothing

        MenuItemStopStreamDebugCapture.Enabled = False
        MenuItemStartStreamDebugCapture.Enabled = True

    End Sub

    Private Sub MenuItemCaptureSampleFrames_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCaptureSampleFrames.Click

        With SaveFileDialog
            .Title = "Set Sample Frames Capture File Name"
            .Filter = "Captured Frames (*.txt)|*.txt|All Files|*.*"
            .FileName = ""
            If .ShowDialog(Me) = OK Then
                m_frameCaptureFileName = .FileName
                MenuItemCancelSampleFrameCapture.Enabled = True
                SyncLock m_dataStreamLock
                    m_capturedFrames = 0
                    m_frameSampleStream = File.CreateText(m_frameCaptureFileName)
                    With m_frameSampleStream
                        .WriteLine("Sample Frame Capture - " & DateTime.Now.ToString())
                        .WriteLine()
                        .WriteLine("Device Connection: " & ConnectionInformation)
                        .WriteLine()
                        .WriteLine(New String("*"c, TextFileWidth))
                        .WriteLine()
                    End With
                End SyncLock
                MsgBox("Note that one sample for each type of frame encountered will be captured until a configuration frame is received.  Reception of a configuration frame will terminate the sample capture.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            End If
        End With

    End Sub

    Private Sub MenuItemCancelSampleFrameCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCancelSampleFrameCapture.Click

        CloseSampleCapture()

    End Sub

    Private Sub MenuItemLocalHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemLocalHelp.Click

        Help.ShowHelp(Me, GetAbsolutePath("PMUConnectionTester.chm"))

    End Sub

    Private Sub MenuItemOnlineHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemOnlineHelp.Click

        Process.Start("http://openpdc.codeplex.com/wikipage?title=Connection%20Tester&referringTitle=Documentation")

    End Sub

    Private Sub MenuItemAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemAbout.Click

        With New AboutDialog
            Dim localNamespace As String = Me.GetType.Namespace
            .SetCompanyUrl("http://www.openpdc.com/")
            .SetCompanyLogo(EntryAssembly.GetEmbeddedResource(localNamespace & ".HelpAboutLogo.png"))
            .SetCompanyDisclaimer(EntryAssembly.GetEmbeddedResource(localNamespace & ".Disclaimer.txt"))
            .ShowDialog(Me)
        End With

    End Sub

    Private Sub MenuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExit.Click

        Me.Close()

    End Sub

    Private Sub MenuItemRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemRefresh.Click

        InitializeAttributeTree()

    End Sub

    Private Sub MenuItemExpandAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExpandAll.Click

        Dim frameKey As String

        With TreeFrameAttributes
            .Refresh()

            With .Nodes
                ' Since expanding nodes can be a time consuming process, we perform expansion on root
                ' nodes one at a time - resting between expansions to allow some UI thread time
                For Each value As Integer In [Enum].GetValues(GetType(FundamentalFrameType))
                    frameKey = "Frame" & (value + 1).ToString()
                    If .Exists(frameKey) Then
                        .Item(frameKey).ExpandAll(ExpandAllType.OnlyNodesWithChildren)
                        Application.DoEvents()
                    End If
                Next
            End With
        End With

    End Sub

    Private Sub MenuItemCollapseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCollapseAll.Click

        With TreeFrameAttributes
            .Refresh()
            .CollapseAll()
        End With

    End Sub

#End Region

#Region " Control Resizing Event Code "

    Private Sub PMUConnectionTester_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

        UpdateApplicationTitle(Nothing)

    End Sub

    Private Sub GroupBoxStatus_ExpandedStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GroupBoxStatus.ExpandedStateChanged

        ToolTipManager.GetUltraToolTip(GroupBoxStatus).Enabled = IIf(GroupBoxStatus.Expanded, DefaultableBoolean.False, DefaultableBoolean.True)

    End Sub

    Private Sub GroupBoxStatus_ExpandedStateChanging(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GroupBoxStatus.ExpandedStateChanging

        If GroupBoxStatus.Expanded Then
            TabControlChart.Height += GroupBoxPanelStatus.Height
            GroupBoxHeaderFrame.Height += GroupBoxPanelStatus.Height
            GroupBoxStatus.Top += GroupBoxPanelStatus.Height
        Else
            TabControlChart.Height -= GroupBoxPanelStatus.Height
            GroupBoxHeaderFrame.Height -= GroupBoxPanelStatus.Height
            GroupBoxStatus.Top -= GroupBoxPanelStatus.Height
        End If

    End Sub

    Private Sub GroupBoxConnection_ExpandedStateChanging(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GroupBoxConnection.ExpandedStateChanging

        If GroupBoxConnection.Expanded Then
            TabControlChart.Top -= GroupBoxPanelConnection.Height
            TabControlChart.Height += GroupBoxPanelConnection.Height
            GroupBoxConfigurationFrame.Top -= GroupBoxPanelConnection.Height
            GroupBoxRealTimePowerVars.Top -= GroupBoxPanelConnection.Height
            GroupBoxHeaderFrame.Top -= GroupBoxPanelConnection.Height
            GroupBoxHeaderFrame.Height += GroupBoxPanelConnection.Height
        Else
            TabControlChart.Height -= GroupBoxPanelConnection.Height
            TabControlChart.Top += GroupBoxPanelConnection.Height
            GroupBoxConfigurationFrame.Top += GroupBoxPanelConnection.Height
            GroupBoxRealTimePowerVars.Top += GroupBoxPanelConnection.Height
            GroupBoxHeaderFrame.Height -= GroupBoxPanelConnection.Height
            GroupBoxHeaderFrame.Top += GroupBoxPanelConnection.Height
        End If

        LabelDefaultIPStack.Visible = Not GroupBoxConnection.Expanded

    End Sub

    Private Sub GroupBoxHeaderFrame_ExpandedStateChanging(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GroupBoxHeaderFrame.ExpandedStateChanging

        If GroupBoxHeaderFrame.Expanded Then
            TabControlChart.Width += GroupBoxPanelHeaderFrame.Width
            GroupBoxHeaderFrame.Left += GroupBoxPanelHeaderFrame.Width
        Else
            TabControlChart.Width -= GroupBoxPanelHeaderFrame.Width
            GroupBoxHeaderFrame.Left -= GroupBoxPanelHeaderFrame.Width
        End If

    End Sub

    Private Sub GroupBoxConfigurationFrame_ExpandedStateChanging(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GroupBoxConfigurationFrame.ExpandedStateChanging

        Const ExtraOffset As Integer = 8

        If GroupBoxConfigurationFrame.Expanded Then
            TabControlChart.Left -= GroupBoxPanelConfigurationFrame.Width + ExtraOffset
            TabControlChart.Width += GroupBoxPanelConfigurationFrame.Width + ExtraOffset
            GroupBoxConfigurationFrame.Left -= ExtraOffset
        Else
            TabControlChart.Left += GroupBoxPanelConfigurationFrame.Width + ExtraOffset
            TabControlChart.Width -= GroupBoxPanelConfigurationFrame.Width + ExtraOffset
            GroupBoxConfigurationFrame.Left += ExtraOffset
        End If

        GroupBoxRealTimePowerVars.Visible = Not GroupBoxConfigurationFrame.Expanded

    End Sub

#End Region

#End Region

#Region " Non-Form Thread Event Handlers "

    ' These functions are invoked from a separate threads, so you must use the "Invoke" method so you can safely manipulate visual control elements
    Private Sub m_frameParser_ReceivedFrameBufferImage(ByVal sender As Object, ByVal e As EventArgs(Of FundamentalFrameType, Byte(), Integer, Integer)) Handles m_frameParser.ReceivedFrameBufferImage

        BeginInvoke(New Action(Of FundamentalFrameType, Byte(), Integer, Integer)(AddressOf ReceivedFrameBufferImage), e.Argument1, e.Argument2, e.Argument3, e.Argument4)

    End Sub

    Private Sub m_frameParser_ReceivedConfigurationFrame(ByVal sender As Object, ByVal e As EventArgs(Of IConfigurationFrame)) Handles m_frameParser.ReceivedConfigurationFrame

        BeginInvoke(New Action(Of IConfigurationFrame)(AddressOf ReceivedConfigFrame), e.Argument)

    End Sub

    Private Sub m_frameParser_ReceivedDataFrame(ByVal sender As Object, ByVal e As EventArgs(Of IDataFrame)) Handles m_frameParser.ReceivedDataFrame

        BeginInvoke(New Action(Of IDataFrame)(AddressOf ReceivedDataFrame), e.Argument)

    End Sub

    Private Sub m_frameParser_ReceivedHeaderFrame(ByVal sender As Object, ByVal e As EventArgs(Of IHeaderFrame)) Handles m_frameParser.ReceivedHeaderFrame

        BeginInvoke(New Action(Of IHeaderFrame)(AddressOf ReceivedHeaderFrame), e.Argument)

    End Sub

    Private Sub m_frameParser_ReceivedCommandFrame(ByVal sender As Object, ByVal e As EventArgs(Of ICommandFrame)) Handles m_frameParser.ReceivedCommandFrame, m_frameParser.SentCommandFrame

        ' Note we use the same function to handle both sent and received command frames...
        BeginInvoke(New Action(Of ICommandFrame)(AddressOf ReceivedCommandFrame), e.Argument)

    End Sub

    Private Sub m_frameParser_ReceivedUndeterminedFrame(ByVal sender As Object, ByVal e As EventArgs(Of IChannelFrame)) Handles m_frameParser.ReceivedUndeterminedFrame

        BeginInvoke(New Action(Of IChannelFrame)(AddressOf ReceivedUndeterminedFrame), e.Argument)

    End Sub

    Private Sub m_frameParser_DataStreamException(ByVal sender As Object, ByVal e As EventArgs(Of System.Exception)) Handles m_frameParser.ParsingException

        BeginInvoke(New Action(Of Exception)(AddressOf DataStreamException), e.Argument)

    End Sub

    Private Sub m_frameParser_ConfigurationChanged() Handles m_frameParser.ConfigurationChanged

        BeginInvoke(New Action(AddressOf ConfigurationChanged))

    End Sub

    Private Sub m_frameParser_Connected() Handles m_frameParser.ConnectionEstablished

        BeginInvoke(New Action(AddressOf Connected))

    End Sub

    Private Sub m_frameParser_ConnectionException(ByVal sender As Object, ByVal e As EventArgs(Of System.Exception, Integer)) Handles m_frameParser.ConnectionException

        BeginInvoke(New Action(Of Exception, Integer)(AddressOf ConnectionException), e.Argument1, e.Argument2)

    End Sub

    Private Sub m_frameParser_ExceededParsingExceptionThreshold(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_frameParser.ExceededParsingExceptionThreshold

        BeginInvoke(New EventHandler(AddressOf ExceededParsingExceptionThreshold), sender, e)

    End Sub

    Private Sub m_frameParser_Disconnected() Handles m_frameParser.ConnectionTerminated

        BeginInvoke(New Action(AddressOf Disconnected))

    End Sub

    Private Sub m_frameParser_ServerStarted() Handles m_frameParser.ServerStarted

        BeginInvoke(New Action(AddressOf ServerStarted))

    End Sub

    Private Sub m_frameParser_ServerStopped() Handles m_frameParser.ServerStopped

        BeginInvoke(New Action(AddressOf ServerStopped))

    End Sub

    Private Sub m_chartSettings_PhaseAngleColorsChanged() Handles m_applicationSettings.PhaseAngleColorsChanged

        BeginInvoke(New Action(AddressOf PhaseAngleColorsChanged))

    End Sub

#End Region

#Region " Thread Delegate Implementations "

    Private Sub ReceivedFrameBufferImage(ByVal frameType As FundamentalFrameType, ByVal binaryImage As Byte(), ByVal offset As Integer, ByVal length As Integer)

        If frameType <> m_lastFrameType Then
            m_lastFrameType = frameType
            LabelFrameType.Text = [Enum].GetName(GetType(FundamentalFrameType), m_lastFrameType)
        End If

        If m_frameSampleStream IsNot Nothing Then CaptureFrameImage(frameType, binaryImage, offset, length)

        ' Note that we exclude command frames from capture stream that are typically "sent", not "received", to make sure frame alignment
        ' stays in-tact - this fixes a bug with IEEE1344 play-back of captured streams that does not include a synchronization-byte
        If m_frameCaptureStream IsNot Nothing AndAlso frameType <> FundamentalFrameType.CommandFrame Then m_frameCaptureStream.Write(binaryImage, offset, length)

        If length > 0 Then
            If GroupBoxStatus.Expanded Then
                If TypeOf m_byteEncoding Is ByteEncoding.ASCIIEncoding Then
                    ' We handle ASCII encoding as a special case removing any control characters, no spacing
                    ' between charaters and allowing the entire frame to be displayed
                    LabelBinaryFrameImage.Text = m_byteEncoding.GetString(binaryImage, offset, length).RemoveControlCharacters()
                Else
                    ' For all others, we display bytes in the specified encoding format up to specified maximum display bytes
                    LabelBinaryFrameImage.Text = _
                        m_byteEncoding.GetString(binaryImage, offset, Min(length, m_applicationSettings.MaximumFrameDisplayBytes), " "c) & _
                        IIf(length > m_applicationSettings.MaximumFrameDisplayBytes, " ...", "")
                End If
            End If

            ' We update statistics after processing about 30 frames of data...
            If m_byteCount >= length * 30 Then
                StatusBar.Panels(TotalFramesPanel).Text = m_frameParser.TotalFramesReceived.ToString()
                StatusBar.Panels(FramesPerSecondPanel).Text = m_frameParser.CalculatedFrameRate.ToString("0.0000")
                StatusBar.Panels(TotalBytesPanel).Text = m_frameParser.TotalBytesReceived.ToString()
                StatusBar.Panels(BitRatePanel).Text = m_frameParser.MegaBitRate.ToString("0.0000")
                StatusBar.Panels(QueuedBuffersPanel).Text = m_frameParser.QueuedBuffers.ToString()
                m_byteCount = 0
                Application.DoEvents()
            End If

            m_byteCount += length
        End If

    End Sub

    Private Sub CaptureFrameImage(ByVal frameType As FundamentalFrameType, ByVal binaryImage As Byte(), ByVal offset As Integer, ByVal length As Integer)

        Dim frameBit As Byte = CByte(2 ^ frameType)

        ' Make sure only one of each type of encountered frame is captured...
        If (m_capturedFrames And frameBit) = 0 Then
            m_capturedFrames = (m_capturedFrames Or frameBit)

            SyncLock m_dataStreamLock
                With m_frameSampleStream
                    Dim row As String

                    .WriteLine("Captured Frame Type: " & [Enum].GetName(GetType(FundamentalFrameType), frameType) & " (0x" & frameBit.ToString("X2") & ")")
                    .WriteLine("   Frame Image Size: " & length & " bytes")
                    .WriteLine()
                    .WriteLine(">> Decimal Image:")

                    For Each row In ByteEncoding.Decimal.GetString(binaryImage, offset, length, " "c).GetSegments((TextFileWidth \ 4) * 4)
                        .WriteLine(row)
                    Next

                    .WriteLine()
                    .WriteLine(">> Hexadecimal Image:")

                    For Each row In ByteEncoding.Hexadecimal.GetString(binaryImage, offset, length, " "c).GetSegments((TextFileWidth \ 3) * 3)
                        .WriteLine(row)
                    Next

                    .WriteLine()
                    .WriteLine(">> Big Endian Binary Image:")

                    For Each row In ByteEncoding.BigEndianBinary.GetString(binaryImage, offset, length, " "c).GetSegments((TextFileWidth \ 9) * 9)
                        .WriteLine(row)
                    Next

                    .WriteLine()
                    .WriteLine(">> ASCII Character Extraction:")

                    For Each row In Encoding.ASCII.GetString(binaryImage, offset, length).RemoveControlCharacters().GetSegments(TextFileWidth)
                        .WriteLine(row)
                    Next

                    .WriteLine()
                    .WriteLine(New String("*"c, TextFileWidth))
                    .WriteLine()

                    ' We stop the capture once a config frame is encountered...
                    If frameType = FundamentalFrameType.ConfigurationFrame Then CloseSampleCapture()
                End With
            End SyncLock
        End If

    End Sub

    Private Sub CloseSampleCapture()

        MenuItemCancelSampleFrameCapture.Enabled = False

        If m_frameSampleStream IsNot Nothing Then
            SyncLock m_dataStreamLock
                With m_frameSampleStream
                    .WriteLine("Frames Types Encountered (As Or'd Bits): 0x" & m_capturedFrames.ToString("X2"))
                    .WriteLine("File closed at " & DateTime.Now.ToString())
                    .Close()
                    m_frameSampleStream = Nothing

                    ' Open captured sample file in notepad...
                    Process.Start("notepad.exe", m_frameCaptureFileName)
                End With
            End SyncLock
        End If

    End Sub

    Private Sub ReceivedConfigFrame(ByVal frame As IConfigurationFrame)

        LabelTime.Text = frame.TimeTag.ToString()

        SyncLock m_attributeFrames
            m_attributeFrames(frame.FrameType) = frame
        End SyncLock

        If m_configurationFrame Is Nothing Then
            ' Cache config frame reference for future use...
            m_configurationFrame = frame

            GroupBoxConfigurationFrame.Expanded = True
            TextBoxDeviceID.Text = frame.IDCode.ToString()
            ChartDataDisplay.TitleTop.Text = "Configured frame rate: " & frame.FrameRate & " frames/second"

            ' Load PMU list from new configuration frame
            With ComboBoxPmus
                With .Items
                    .Clear()

                    For Each cell As IConfigurationCell In frame.Cells
                        .Add(cell.StationName.ToNonNullString(cell.IDLabel.ToNonNullString("Device " & cell.IDCode)))
                    Next
                End With

                If .Items.Count > 0 Then .SelectedIndex = 0
            End With

            MenuItemSaveConfigFile.Enabled = True

            ' Handle debug stream if it's open
            If m_streamDebugCapture IsNot Nothing Then
                ' We write the header line in the file from the configuration frame
                Dim cell As IConfigurationCell
                Dim x, y As Integer
                Dim station, label As String

                m_streamDebugCapture.Write("Timestamp,")

                For x = 0 To frame.Cells.Count - 1
                    cell = frame.Cells(x)
                    station = cell.StationName.RemoveControlCharacters().Trim()

                    m_streamDebugCapture.Write(station & " Status,")

                    For y = 0 To cell.PhasorDefinitions.Count - 1
                        With cell.PhasorDefinitions(y)
                            label = station & " " & [Enum].GetName(GetType(PhasorType), .PhasorType) & y
                            m_streamDebugCapture.Write(label & " Angle " & .Label & "," & label & " Magnitude " & .Label & ",")
                        End With
                    Next

                    m_streamDebugCapture.Write(station & " Frequency," & station & " DfDt,")

                    For y = 0 To cell.AnalogDefinitions.Count - 1
                        m_streamDebugCapture.Write(station & " Analog" & y & " " & cell.AnalogDefinitions(y).Label & ",")
                    Next

                    For y = 0 To cell.DigitalDefinitions.Count - 1
                        m_streamDebugCapture.Write(station & " Digital" & y & " " & cell.DigitalDefinitions(y).Label & ",")
                    Next
                Next

                m_streamDebugCapture.Write("EOF")
            End If
        End If

    End Sub

    Private Sub ReceivedDataFrame(ByVal frame As IDataFrame)

        LabelTime.Text = frame.TimeTag.ToString()

        SyncLock m_attributeFrames
            m_attributeFrames(frame.FrameType) = frame
        End SyncLock

        If m_selectedCell IsNot Nothing And frame.Cells.Count > 0 Then
            Dim cell As IDataCell = frame.Cells(ComboBoxPmus.SelectedIndex)
            Dim frequency As Double
            Dim phasorCount As Integer
            Dim phasorIndex As Integer = ComboBoxPhasors.SelectedIndex

            If cell.FrequencyValue IsNot Nothing Then frequency = cell.FrequencyValue.Frequency
            If cell.PhasorValues IsNot Nothing Then phasorCount = cell.PhasorValues.Count

            ' Plot real-time frequency trend
            m_frequencyData.Rows.Add(New Object() {frequency})

            Do While m_frequencyData.Rows.Count > m_applicationSettings.FrequencyPointsToPlot
                m_frequencyData.Rows.RemoveAt(0)
            Loop

            ' Plot real-time phasor trends
            If phasorIndex < phasorCount AndAlso phasorIndex > -1 AndAlso phasorCount > 0 Then
                Try
                    Dim phasor As IPhasorValue = cell.PhasorValues(phasorIndex)

                    If Math.Abs((phasor.Angle - m_lastPhaseAngle).ToDegrees()) >= 0.5! OrElse m_phasorData.Rows.Count < 2 Then
                        Dim row As DataRow = m_phasorData.NewRow()

                        If m_applicationSettings.PhaseAngleGraphStyle = ApplicationSettings.AngleGraphStyle.Raw Then
                            ' Plot raw phase angles
                            For x As Integer = 0 To phasorCount - 1
                                If m_phasorData.Columns.Count > x Then row(x) = cell.PhasorValues(x).Angle.ToDegrees()
                            Next
                        Else
                            ' Plot relative phase angles
                            For x As Integer = 0 To phasorCount - 1
                                If m_phasorData.Columns.Count > x Then row(x) = (phasor.Angle - cell.PhasorValues(x).Angle).ToDegrees()
                            Next
                        End If

                        m_phasorData.Rows.Add(row)

                        Do While m_phasorData.Rows.Count > m_applicationSettings.PhaseAnglePointsToPlot
                            m_phasorData.Rows.RemoveAt(0)
                        Loop

                        If GroupBoxConfigurationFrame.Expanded Then
                            ' Update power and vars calculations
                            If ComboBoxCurrentPhasors.SelectedIndex > -1 AndAlso ComboBoxVoltagePhasors.SelectedIndex > -1 Then
                                Dim selectedVoltageIndex As Integer = Convert.ToInt32(ComboBoxVoltagePhasors.Items(ComboBoxVoltagePhasors.SelectedIndex).ToString.Split(":"c)(0))
                                Dim selectedCurrentIndex As Integer = Convert.ToInt32(ComboBoxCurrentPhasors.Items(ComboBoxCurrentPhasors.SelectedIndex).ToString.Split(":"c)(0))

                                If selectedVoltageIndex < phasorCount AndAlso selectedCurrentIndex < phasorCount Then
                                    Try
                                        Dim voltagePhasor As IPhasorValue = cell.PhasorValues(selectedVoltageIndex)
                                        Dim currentPhasor As IPhasorValue = cell.PhasorValues(selectedCurrentIndex)

                                        LabelPower.Text = (PhasorValueBase.CalculatePower(voltagePhasor, currentPhasor) / 1000000).ToString("0.0000") & " MW"
                                        LabelVars.Text = (PhasorValueBase.CalculateVars(voltagePhasor, currentPhasor) / 1000000).ToString("0.0000") & " MVars"
                                    Catch
                                        ' Skip calculation if something is not available or fails to calculate properly
                                        LabelPower.Text = "--- MW"
                                        LabelVars.Text = "--- MVars"
                                    End Try
                                End If
                            End If
                        End If

                        m_lastPhaseAngle = phasor.Angle
                    End If

                    If GroupBoxStatus.Expanded Then
                        LabelFrequency.Text = frequency.ToString("0.0000") & " Hz"
                        LabelAngle.Text = phasor.Angle.ToDegrees().ToString() & "�"
                        If phasor.Type = PhasorType.Voltage Then
                            ' Most PMU's are setup such that voltage magnitudes need to multiplied by the SQRT(3)
                            LabelMagnitude.Text = (phasor.Magnitude / 1000).ToString("0.0000") & " (" & (phasor.Magnitude * m_sqrtOf3 / 1000).ToString("0.0000") & ") kV"
                        Else
                            LabelMagnitude.Text = phasor.Magnitude.ToString("0.0000") & " Amperes"
                        End If
                    End If
                Catch ex As ArgumentOutOfRangeException
                Catch ex As IndexOutOfRangeException
                    ' This can happen randomly on occasion when using a large file based input - very odd, so we just ignore it and go on...
                Catch ex As Exception
                    AppendStatusMessage(String.Format("Exception occured while attempting to plot data: {0}", ex.Message))
                End Try
            End If

            ' We only refresh graph every so often
            If Ticks.ToSeconds(Date.Now.Ticks - m_lastRefresh) >= m_applicationSettings.RefreshRate Then
                ChartDataDisplay.DataBind()
                m_lastRefresh = Date.Now.Ticks
            End If
        End If

        ' Handle debug stream if it's open
        If m_streamDebugCapture IsNot Nothing Then
            ' We write the data value line in the file from the data frame
            Dim cell As IDataCell
            Dim x, y As Integer

            m_streamDebugCapture.Write(CDate(frame.Timestamp).ToString("yyyy-MM-dd HH:mm:ss.fff") & ",")

            For x = 0 To frame.Cells.Count - 1
                cell = frame.Cells(x)

                m_streamDebugCapture.Write(cell.StatusFlags.ToString("x") & ",")

                For y = 0 To cell.PhasorValues.Count - 1
                    With cell.PhasorValues(y)
                        m_streamDebugCapture.Write(.Angle.ToDegrees().ToString() & "," & .Magnitude & ",")
                    End With
                Next

                With cell.FrequencyValue
                    m_streamDebugCapture.Write(.Frequency & "," & .DfDt & ",")
                End With

                For y = 0 To cell.AnalogValues.Count - 1
                    m_streamDebugCapture.Write(cell.AnalogValues(y).Value & ",")
                Next

                For y = 0 To cell.DigitalValues.Count - 1
                    m_streamDebugCapture.Write(cell.DigitalValues(y).Value & ",")
                Next
            Next

            m_streamDebugCapture.WriteLine(frame.Timestamp)
        End If

    End Sub

    Private Sub ReceivedHeaderFrame(ByVal frame As IHeaderFrame)

        LabelTime.Text = frame.TimeTag.ToString()

        SyncLock m_attributeFrames
            m_attributeFrames(frame.FrameType) = frame
        End SyncLock

        TextBoxHeaderFrame.Text = frame.HeaderData
        GroupBoxHeaderFrame.Expanded = True

    End Sub

    Private Sub ReceivedCommandFrame(ByVal frame As ICommandFrame)

        LabelTime.Text = frame.TimeTag.ToString()

        SyncLock m_attributeFrames
            m_attributeFrames(frame.FrameType) = frame
        End SyncLock

        ' To make sure "sent" command frames can get captured and displayed, we make sure and
        ' send the frame to the frame buffer image handler
        ReceivedFrameBufferImage(FundamentalFrameType.CommandFrame, frame.BinaryImage(), 0, frame.BinaryLength)

    End Sub

    Private Sub ReceivedUndeterminedFrame(ByVal frame As IChannelFrame)

        LabelTime.Text = frame.TimeTag.ToString()

        SyncLock m_attributeFrames
            m_attributeFrames(frame.FrameType) = frame
        End SyncLock

    End Sub

    Private Sub DataStreamException(ByVal ex As System.Exception)

        AppendStatusMessage("Exception: " & ex.Message)
        If m_applicationSettings.ShowMessagesTabOnDataException Then
            TabControlChart.Tabs(ChartTabs.Messages).Selected = True
        End If

    End Sub

    Private Sub ConfigurationChanged()

        If Not m_configChangeDetected Or (New Ticks(DateTime.UtcNow.Ticks - m_configChangeTime)).ToSeconds() >= 60.0 Then
            m_configChangeDetected = True
            m_configChangeTime = DateTime.UtcNow.Ticks

            AppendStatusMessage("NOTE: Data stream indicates that configuration in source device has changed")

            If m_frameParser.DeviceSupportsCommands AndAlso _
                MsgBox("Data stream indicates that configuration in source device has changed." & vbCrLf & vbCrLf & _
                      "Do you want to request a new configuration frame?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, _
                      "Device Configuration Changed") = MsgBoxResult.Yes Then
                SendDeviceCommand(DeviceCommand.SendConfigurationFrame2)
            End If
        End If

    End Sub

    Private Sub Connected()

        ChartDataDisplay.TitleTop.Text = "Awaiting configuration frame..."
        AppendStatusMessage("Connected to device " & ConnectionInformation)
        TabControlChart.Tabs(ChartTabs.Graph).Selected = True

    End Sub

    Private Sub ConnectionException(ByVal ex As Exception, ByVal connectionAttempts As Integer)

        ChartDataDisplay.TitleTop.Text = "Connection attempt failed..."
        AppendStatusMessage("Device connection error: " & ex.Message)

        If m_applicationSettings.MaximumConnectionAttempts > 0 And connectionAttempts >= m_applicationSettings.MaximumConnectionAttempts Then
            Disconnect()
            MsgBox(ex.Message & vbCrLf & connectionAttempts & IIf(connectionAttempts > 1, " connections ", " connection ") & "attempted.", _
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Device Connection Error")
        ElseIf connectionAttempts > 1 Then
            TabControlChart.Tabs(ChartTabs.Messages).Selected = True
        End If

    End Sub

    Private Sub ExceededParsingExceptionThreshold(ByVal sender As Object, ByVal e As System.EventArgs)

        ' Connection has been terminated, but we still need to clean up display...
        Disconnect()

        If MsgBox("An excessive number of exceptions have occurred on this connection - please verify the correct protocol has been selected." & vbCrLf & vbCrLf & _
                  "Do you want to try the connection again with current settings?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, _
                  "Exception Threshold Exceeded") = MsgBoxResult.Yes Then
            Connect()
        End If

    End Sub

    Private Sub Disconnected()

        If m_frameParser.Enabled Then
            ' Communications layer closed connection (user didn't) - so we terminate gracefully...
            Disconnect()
            AppendStatusMessage("Connection closed by remote device.")
            MsgBox("Connection closed by remote device.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Device Connection Error")
        Else
            AppendStatusMessage("Connection closed.")
        End If

    End Sub

    Private Sub ServerStarted()

        ChartDataDisplay.TitleTop.Text = "Listening for connection..."
        AppendStatusMessage("Local server started.  Listening for connection on " & ConnectionInformation)

    End Sub

    Private Sub ServerStopped()

        AppendStatusMessage("Local server stopped.")

    End Sub

    Private Sub PhaseAngleColorsChanged()

        ' Validate that there is at least one color available for phase angle charts
        With m_applicationSettings.PhaseAngleColors
            If .Count = 0 Then .Add(Color.Black)
        End With

        InitializeChart()
        If m_selectedCell IsNot Nothing Then TabControlChart.Tabs(ChartTabs.Graph).Selected = True

    End Sub

#End Region

#Region " Interface / Data Layer Functions "

    Private Sub LoadConnectionSettings(ByVal filename As String)

        Dim settingsFile As FileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read)

        With New SoapFormatter
            .AssemblyFormat = FormatterAssemblyStyle.Simple
            .TypeFormat = FormatterTypeStyle.TypesWhenNeeded

            Try
                Me.Cursor = Cursors.WaitCursor
                ApplyConnectionSettings(CType(.Deserialize(settingsFile), ConnectionSettings))
                UpdateApplicationTitle(filename)
            Catch ex As Exception
                MsgBox("Failed to open connection settings: " & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End With

        settingsFile.Close()

    End Sub

    Private Sub SaveConnectionSettings(ByVal filename As String)

        Dim settingsFile As FileStream = File.Create(filename)

        With New SoapFormatter
            .AssemblyFormat = FormatterAssemblyStyle.Simple
            .TypeFormat = FormatterTypeStyle.TypesWhenNeeded

            Try
                .Serialize(settingsFile, CurrentConnectionSettings)
                UpdateApplicationTitle(filename)
            Catch ex As Exception
                MsgBox("Failed to save connection settings: " & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation)
            End Try
        End With

        settingsFile.Close()

    End Sub

    Private Sub UpdateApplicationTitle(ByVal filename As String)

        Const Offset As Integer = 50

        If String.IsNullOrEmpty(filename) Then
            filename = m_lastFileName
        Else
            m_lastFileName = filename
        End If

        If String.IsNullOrEmpty(m_applicationName) Then m_applicationName = EntryAssembly.Description

        If Not String.IsNullOrEmpty(filename) AndAlso String.Compare(filename, m_lastConnectionFileName, True) <> 0 Then
            Text = m_applicationName & " - " & TrimFileName(filename, Convert.ToInt32((Width - Offset) / 10))
        Else
            Text = m_applicationName
        End If

    End Sub

    Private ReadOnly Property CurrentConnectionSettings() As ConnectionSettings
        Get
            With New ConnectionSettings
                ' Setup PMU connection parameters based on user selections...
                .PhasorProtocol = CType(ComboBoxProtocols.SelectedIndex, PhasorProtocol)

                Select Case TabControlCommunications.SelectedTab.Index
                    Case TransportProtocol.Tcp
                        .TransportProtocol = TransportProtocol.Tcp
                        .ConnectionString = _
                            "server=" & TextBoxTcpHostIP.Text & _
                            "; port=" & TextBoxTcpPort.Text & _
                            IIf(m_applicationSettings.ForceIPv4, "; interface=0.0.0.0", "") & _
                            "; islistener=" & CheckBoxEstablishTcpServer.Checked.ToString()
                    Case TransportProtocol.Udp
                        .TransportProtocol = TransportProtocol.Udp
                        If CheckBoxRemoteUdpServer.Checked Then
                            .ConnectionString = _
                                "localport=" & TextBoxUdpLocalPort.Text & _
                                "; server=" & TextBoxUdpHostIP.Text & _
                                "; remoteport=" & TextBoxUdpRemotePort.Text & _
                                IIf(m_applicationSettings.ForceIPv4, "; interface=0.0.0.0", "")
                        Else
                            .ConnectionString = _
                                "localport=" & TextBoxUdpLocalPort.Text & _
                                IIf(m_applicationSettings.ForceIPv4, "; interface=0.0.0.0", "")
                        End If
                    Case TransportProtocol.Serial
                        .TransportProtocol = TransportProtocol.Serial
                        .ConnectionString = _
                            "port=" & ComboBoxSerialPorts.Text & _
                            "; baudrate=" & ComboBoxSerialBaudRates.Text & _
                            "; parity=" & ComboBoxSerialParities.Text & _
                            "; stopbits=" & ComboBoxSerialStopBits.Text & _
                            "; databits=" & TextBoxSerialDataBits.Text & _
                            "; dtrenable=" & CheckBoxSerialDTR.Checked.ToString() & _
                            "; rtsenable=" & CheckBoxSerialRTS.Checked.ToString()
                    Case TransportProtocol.File
                        .TransportProtocol = TransportProtocol.File
                        .ConnectionString = "file=" & TextBoxFileCaptureName.Text
                End Select

                ' Append command channel settings
                .ConnectionString &= AlternateCommandChannel.ConnectionString

                ' Include connection specific settings in connection string if they are not set to their default values so that
                ' these settings will ride with their serialized connection file
                If m_applicationSettings.MaximumConnectionAttempts <> 1 Then .ConnectionString &= "; maximumConnectionAttempts = " & m_applicationSettings.MaximumConnectionAttempts
                If Not m_applicationSettings.AutoStartDataParsingSequence Then .ConnectionString &= "; autoStartDataParsingSequence = false"
                If m_applicationSettings.ExecuteParseOnSeparateThread Then .ConnectionString &= "; executeParseOnSeparateThread = true"
                If m_applicationSettings.SkipDisableRealTimeData Then .ConnectionString &= "; skipDisableRealTimeData = true"
                If m_applicationSettings.InjectSimulatedTimestamp Then .ConnectionString &= "; simulateTimestamp = true"
                If m_applicationSettings.UseHighResolutionInputTimer Then .ConnectionString &= "; useHighResolutionInputTimer = true"

                .PmuID = Convert.ToInt32(TextBoxDeviceID.Text)
                .FrameRate = Convert.ToInt32(TextBoxFileFrameRate.Text)
                .AutoRepeatPlayback = CheckBoxAutoRepeatPlayback.Checked
                .ByteEncodingDisplayFormat = ComboBoxByteEncodingDisplayFormats.SelectedIndex
                .ConnectionParameters = m_frameParser.ConnectionParameters

                Return .This
            End With
        End Get
    End Property

    Private Sub ApplyConnectionSettings(ByVal settings As ConnectionSettings)

        Disconnect()

        ' Setup PMU connection parameters based on user selections...
        With settings
            ' Changing protocol causes several UI events - so we assign this first
            ComboBoxProtocols.SelectedIndex = .PhasorProtocol

            ' After assigning protocol, any available extra connection parameters for the protocol
            ' will be set to their defaults. If any connection parameters were serialized into the
            ' connection settings, we'll apply them now...
            If .ConnectionParameters IsNot Nothing Then
                m_frameParser.ConnectionParameters = .ConnectionParameters
                PropertyGridProtocolParameters.SelectedObject = m_frameParser.ConnectionParameters
            End If

            ' Load remaining connection settings
            TabControlCommunications.Tabs(.TransportProtocol).Selected = True

            Dim connectionData As Dictionary(Of String, String) = .ConnectionString.ParseKeyValuePairs()
            Dim setting As String

            Select Case .TransportProtocol
                Case TransportProtocol.Tcp
                    TextBoxTcpPort.Text = connectionData("port")

                    ' Note that old style connection strings may not contain "islistener" key
                    If connectionData.ContainsKey("islistener") AndAlso connectionData("islistener").ParseBoolean() Then
                        TextBoxTcpHostIP.Text = m_loopbackAddress
                        CheckBoxEstablishTcpServer.Checked = True
                    Else
                        AssignHostIP(TextBoxTcpHostIP, connectionData("server"))
                        CheckBoxEstablishTcpServer.Checked = False
                    End If
                Case TransportProtocol.Udp
                    If connectionData.ContainsKey("server") Then
                        TextBoxUdpLocalPort.Text = connectionData("localport")
                        AssignHostIP(TextBoxUdpHostIP, connectionData("server"))
                        TextBoxUdpRemotePort.Text = connectionData("remoteport")
                        CheckBoxRemoteUdpServer.Checked = True
                    Else
                        TextBoxUdpLocalPort.Text = connectionData("localport")
                        TextBoxUdpHostIP.Text = m_loopbackAddress
                        TextBoxUdpRemotePort.Text = "5000"
                        CheckBoxRemoteUdpServer.Checked = False
                    End If
                Case TransportProtocol.Serial
                    ComboBoxSerialPorts.Text = connectionData("port")
                    ComboBoxSerialBaudRates.Text = connectionData("baudrate")
                    ComboBoxSerialParities.Text = connectionData("parity")
                    ComboBoxSerialStopBits.Text = connectionData("stopbits")
                    TextBoxSerialDataBits.Text = connectionData("databits")
                    CheckBoxSerialDTR.Checked = connectionData("dtrenable").ParseBoolean()
                    CheckBoxSerialRTS.Checked = connectionData("rtsenable").ParseBoolean()
                Case TransportProtocol.File
                    TextBoxFileCaptureName.Text = connectionData("file")
            End Select

            ' Apply alternate command channel settings (if any)
            AlternateCommandChannel.ConnectionString = .ConnectionString
            UpdateAlternateCommandChannelLabel()

            ' Check for connection specific settings that may have been serialized into the connection string
            If connectionData.TryGetValue("maximumConnectionAttempts", setting) Then m_applicationSettings.MaximumConnectionAttempts = Integer.Parse(setting)
            If connectionData.TryGetValue("autoStartDataParsingSequence", setting) Then m_applicationSettings.AutoStartDataParsingSequence = setting.ParseBoolean()
            If connectionData.TryGetValue("executeParseOnSeparateThread", setting) Then m_applicationSettings.ExecuteParseOnSeparateThread = setting.ParseBoolean()
            If connectionData.TryGetValue("skipDisableRealTimeData", setting) Then m_applicationSettings.SkipDisableRealTimeData = setting.ParseBoolean()
            If connectionData.TryGetValue("simulateTimestamp", setting) Then m_applicationSettings.InjectSimulatedTimestamp = setting.ParseBoolean()
            If connectionData.TryGetValue("useHighResolutionInputTimer", setting) Then m_applicationSettings.UseHighResolutionInputTimer = setting.ParseBoolean()

            TextBoxDeviceID.Text = .PmuID.ToString()
            TextBoxFileFrameRate.Text = .FrameRate.ToString()
            CheckBoxAutoRepeatPlayback.Checked = .AutoRepeatPlayback
            ComboBoxByteEncodingDisplayFormats.SelectedIndex = .ByteEncodingDisplayFormat
        End With

    End Sub

    Friend Sub AssignHostIP(ByVal maskedEditControl As UltraMaskedEdit, ByVal ipValue As String)

        If m_applicationSettings.ForceIPv4 Then
            Try
                Dim literalAddress As IPAddress

                ' See if this is a literal IP address first
                If IPAddress.TryParse(ipValue, literalAddress) Then
                    ' Check to see if address has an IPv4 style address
                    If literalAddress.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        ' Assign IP address
                        maskedEditControl.Text = literalAddress.ToString()
                        Exit Sub
                    End If
                End If

                ' When forcing IPv4, an input mask is applied and may cause assignment of IPv6 or DNS values loaded
                ' from a saved connection string to fail
                For Each address As IPAddress In Dns.GetHostEntry(ipValue).AddressList
                    ' Check to see if address has an IPv4 style address
                    If address.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        ' Attempt to assign IP address
                        maskedEditControl.Text = address.ToString()
                        Exit For
                    End If
                Next
            Catch
                ' If all else fails, just assign a loopback address
                maskedEditControl.Text = "127.0.0.1"
            End Try
        Else
            maskedEditControl.Text = ipValue
        End If

        If String.IsNullOrEmpty(maskedEditControl.Text) Then maskedEditControl.Text = m_loopbackAddress

    End Sub

    Private WriteOnly Property ForceIPv4() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                ' Attempt to coerce address into IPv4 format then enable IPv4 masks
                AssignHostIP(TextBoxTcpHostIP, TextBoxTcpHostIP.Text)
                TextBoxTcpHostIP.InputMask = "nnn\.nnn\.nnn\.nnn"
                TextBoxTcpHostIP.EditAs = EditAsType.UseSpecifiedMask

                AssignHostIP(TextBoxUdpHostIP, TextBoxUdpHostIP.Text)
                TextBoxUdpHostIP.InputMask = "nnn\.nnn\.nnn\.nnn"
                TextBoxUdpHostIP.EditAs = EditAsType.UseSpecifiedMask

                AssignHostIP(AlternateCommandChannel.TextBoxTcpHostIP, AlternateCommandChannel.TextBoxTcpHostIP.Text)
                AlternateCommandChannel.TextBoxTcpHostIP.InputMask = "nnn\.nnn\.nnn\.nnn"
                AlternateCommandChannel.TextBoxTcpHostIP.EditAs = EditAsType.UseSpecifiedMask

                m_loopbackAddress = "127.0.0.1"
            Else
                ' We remove input mask if we're not forcing IPv4, this allows for free form DNS and IPv6 entry
                TextBoxTcpHostIP.InputMask = ""
                TextBoxTcpHostIP.EditAs = EditAsType.String

                TextBoxUdpHostIP.InputMask = ""
                TextBoxUdpHostIP.EditAs = EditAsType.String

                AlternateCommandChannel.TextBoxTcpHostIP.InputMask = ""
                AlternateCommandChannel.TextBoxTcpHostIP.EditAs = EditAsType.String

                If m_ipStackIsIPv6 Then
                    m_loopbackAddress = "::1"
                Else
                    m_loopbackAddress = "127.0.0.1"
                End If
            End If
        End Set
    End Property

    Private Sub SendDeviceCommand(ByVal command As DeviceCommand)

        m_frameParser.SendDeviceCommand(command)
        AppendStatusMessage("Command """ & [Enum].GetName(GetType(DeviceCommand), command) & """ requested at " & Date.Now)

    End Sub

    Friend Sub UpdateAlternateCommandChannelLabel()

        ' We update the alternate command channel label to indicate if the alternate channel is defined
        If AlternateCommandChannel.IsDefined Then
            LabelAlternateCommandChannelState.BorderStyleOuter = UIElementBorderStyle.InsetSoft
            LabelAlternateCommandChannelState.Appearance.BackColor2 = Color.LightGreen
            LabelAlternateCommandChannelState.Text = "Defined"
        Else
            LabelAlternateCommandChannelState.BorderStyleOuter = UIElementBorderStyle.RaisedSoft
            LabelAlternateCommandChannelState.Appearance.BackColor2 = Color.LightGray
            LabelAlternateCommandChannelState.Text = "Not Defined"
        End If

    End Sub

    Private ReadOnly Property ConnectionInformation() As String
        Get
            With New StringBuilder
                .Append(""""c)
                .Append(m_frameParser.Name)
                .Append(""" at ")
                .Append(Date.Now.ToString())
                .Append(" using ")
                .Append(m_frameParser.PhasorProtocol.GetFormattedProtocolName())
                Return .ToString()
            End With
        End Get
    End Property

    Private Sub Connect()

        Try
            ButtonListen.Enabled = False

            With m_frameParser
                Dim valuesAreValid As Boolean = True

                ' Validate connection parameters if available
                If .ConnectionParameters IsNot Nothing Then valuesAreValid = .ConnectionParameters.ValuesAreValid

                If valuesAreValid Then
                    ClearStatusMessages()

                    ' Setup PMU connection parameters based on user selections...
                    Dim currentSettings As ConnectionSettings = CurrentConnectionSettings

                    .PhasorProtocol = currentSettings.PhasorProtocol
                    .ConnectionParameters = currentSettings.ConnectionParameters
                    .TransportProtocol = currentSettings.TransportProtocol
                    .ConnectionString = currentSettings.ConnectionString
                    .DeviceID = CUShort(currentSettings.PmuID)
                    .AutoStartDataParsingSequence = m_applicationSettings.AutoStartDataParsingSequence
                    .ExecuteParseOnSeparateThread = m_applicationSettings.ExecuteParseOnSeparateThread
                    .SkipDisableRealTimeData = m_applicationSettings.SkipDisableRealTimeData
                    .AllowedParsingExceptions = m_applicationSettings.AllowedParsingExceptions
                    .InjectSimulatedTimestamp = m_applicationSettings.InjectSimulatedTimestamp
                    .UseHighResolutionInputTimer = m_applicationSettings.UseHighResolutionInputTimer
                    .ParsingExceptionWindow = Ticks.FromSeconds(m_applicationSettings.ParsingExceptionWindow)
                    .AutoRepeatCapturedPlayback = currentSettings.AutoRepeatPlayback
                    .DefinedFrameRate = Convert.ToInt32(TextBoxFileFrameRate.Text)
                    .MaximumConnectionAttempts = m_applicationSettings.MaximumConnectionAttempts

                    ' Assignment of some can be affected by other settings, update these settings to reflect possible change
                    m_applicationSettings.MaximumConnectionAttempts = .MaximumConnectionAttempts
                    m_applicationSettings.ExecuteParseOnSeparateThread = .ExecuteParseOnSeparateThread

                    ' Connect to PMU...
                    .Start()

                    ' Timer will be disabled after connection if not using file based input, update setting to reflect possible change
                    m_applicationSettings.UseHighResolutionInputTimer = .UseHighResolutionInputTimer

                    ' Update visual elements based on connection selections
                    TabControlChart.Tabs(ChartTabs.Graph).Selected = True
                    ButtonListen.Text = "Dis&connect"

                    If .DataChannelIsServerBased Then
                        ChartDataDisplay.TitleTop.Text = "Listening for connection..."
                        AppendStatusMessage("Listening for connection on " & ConnectionInformation)
                    Else
                        ChartDataDisplay.TitleTop.Text = "Attempting connection..."
                        AppendStatusMessage("Attempting connection to " & ConnectionInformation)
                    End If

                    ' Clear any existing attribute tree data when establishing a new connection
                    TreeFrameAttributes.SetDataBinding(Nothing, Nothing)

                    ' Only enable send command elements when phasor and/or transport protocols allow this
                    If .DeviceSupportsCommands Then
                        LabelCommand.Enabled = True
                        ComboBoxCommands.Enabled = True
                        ButtonSendCommand.Enabled = True
                    End If

                    ComboBoxProtocols.Enabled = False
                    LabelAlternateCommandChannel.Enabled = False
                    LabelAlternateCommandChannelState.Enabled = False
                    GroupBoxStatus.Expanded = True

                    ' Assign pre-loaded configuration frame (if any)
                    If m_configurationFrame IsNot Nothing Then m_frameParser.ConfigurationFrame = m_configurationFrame
                Else
                    TabControlProtocolParameters.Tabs(ProtocolTabs.ExtraParameters).Selected = True
                    MsgBox("Required additional connection parameters for """ & CType(ComboBoxProtocols.SelectedIndex, PhasorProtocol).GetFormattedProtocolName() & """ are not valid - please correct.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Invalid Connection Parameters")
                End If
            End With
        Finally
            ButtonListen.Enabled = True
        End Try

    End Sub

    Private Sub Disconnect()

        If m_frameCaptureStream IsNot Nothing Then MenuItemStopCapture_Click(Me, Nothing)
        If m_streamDebugCapture IsNot Nothing Then MenuItemStopStreamDebugCapture_Click(Me, Nothing)

        ' Disconnect from PMU...
        m_frameParser.Stop()
        m_configurationFrame = Nothing
        m_selectedCell = Nothing
        m_configChangeDetected = False
        m_configChangeTime = 0
        m_frequencyData.Rows.Clear()
        m_phasorData.Rows.Clear()
        m_lastRefresh = 0
        m_lastFrameType = FundamentalFrameType.Undetermined
        m_attributeFrames.Clear()

        ' Restore window state to normal if it is minimized since disconnect may happen by source,
        ' that is, not initiated by user - so user will need to know about the change in state
        If WindowState = FormWindowState.Minimized Then WindowState = FormWindowState.Normal

        ' Restore all visual elements to their default state
        ButtonListen.Text = "&Connect"
        ComboBoxProtocols.Enabled = True
        LabelAlternateCommandChannel.Enabled = True
        LabelAlternateCommandChannelState.Enabled = True
        ChartDataDisplay.Series.Clear()
        ChartDataDisplay.DataBind()
        ChartDataDisplay.TitleTop.Text = ""
        LabelCommand.Enabled = False
        ComboBoxCommands.Enabled = False
        ButtonSendCommand.Enabled = False
        MenuItemSaveConfigFile.Enabled = False
        GroupBoxConfigurationFrame.Expanded = False
        GroupBoxStatus.Expanded = False
        GroupBoxHeaderFrame.Expanded = False
        TextBoxHeaderFrame.Text = ""
        ComboBoxPhasors.Items.Clear()
        ComboBoxPhasors.Text = ""
        ComboBoxVoltagePhasors.Items.Clear()
        ComboBoxVoltagePhasors.Text = ""
        ComboBoxCurrentPhasors.Items.Clear()
        ComboBoxCurrentPhasors.Text = ""
        ComboBoxPmus.Items.Clear()
        ComboBoxPmus.Text = ""
        LabelBinaryFrameImage.Text = LabelBinaryFrameImage.Tag.ToString()
        StatusBar.Panels(TotalFramesPanel).Text = "0"
        StatusBar.Panels(FramesPerSecondPanel).Text = "0.0000"
        StatusBar.Panels(TotalBytesPanel).Text = "0"
        StatusBar.Panels(BitRatePanel).Text = "0.0000"
        StatusBar.Panels(QueuedBuffersPanel).Text = "0"
        LabelIDCode.Text = "0"
        LabelPhasorCount.Text = "0"
        LabelAnalogCount.Text = "0"
        LabelDigitalCount.Text = "0"
        LabelPower.Text = "0.0000 MW"
        LabelVars.Text = "0.0000 MVars"
        LabelNominalFrequency.Text = "0"
        LabelFrameType.Text = "undetermined"
        LabelTime.Text = "undetermined"
        LabelFrequency.Text = "0.0000 Hz"
        LabelAngle.Text = "0.0000�"
        LabelMagnitude.Text = "0.0000 kV"
        InitializeChart()

    End Sub

    Private Sub Shutdown()

        Disconnect()
        Global.System.Windows.Forms.Application.Exit()
        End

    End Sub

    Private Sub ClearStatusMessages()

        TextBoxMessages.Text = ""

    End Sub

    Private Sub AppendStatusMessage(ByVal message As String)

        With New StringBuilder
            .Append(TextBoxMessages.Text)
            .Append(message)
            .Append(Environment.NewLine)
            TextBoxMessages.Text = Microsoft.VisualBasic.Right(.ToString, TextBoxMessages.MaxLength)
        End With

        With TextBoxMessages
            .SelectionStart = .TextLength
            .ScrollToCaret()
        End With

    End Sub

    Private Function UnhandledExceptionErrorMessage() As String

        Return String.Format("An unexpected exception has occurred in the PMU Connection Tester. This error may have been caused by an inconsistent system state or a programming error.  Details of this problem have been logged to an error file, it may be necessary to restart the application. Please notify us with details of this exception so they are aware of this problem: {0}", GlobalExceptionLogger.LastException.Message)

    End Function

#End Region

#Region " Attribute Tree Display Code "

    Private Sub InitializeAttributeTree()

        TreeFrameAttributes.Refresh()
        Dim attributeTreeDataSet As DataSet = CreateAttributeTreeDataSet()
        Dim attributeTable As DataTable = attributeTreeDataSet.Tables("Attributes")
        Dim attributeFrames As List(Of IChannelFrame)
        Dim lastNodeID, lastCellNodeID, currentNodeID As Integer
        Dim frame, associatedFrame As IChannelFrame

        ' Get a synchronized copy of the current frame set
        SyncLock m_attributeFrames
            attributeFrames = New List(Of IChannelFrame)(m_attributeFrames.Values)
        End SyncLock

        ' For consistency in display, we make sure frames are in desired order
        attributeFrames.Sort(AddressOf CompareByFrameType)

        ' Tag configuration channel nodes by assigning a unique key that will allow virtual associations between nodes
        For Each frame In attributeFrames
            If frame.FrameType = FundamentalFrameType.ConfigurationFrame Then
                Dim configFrame As IConfigurationFrame = DirectCast(frame, IConfigurationFrame)

                configFrame.Tag = System.Guid.NewGuid().ToString()

                ' Tag frame cells collection
                configFrame.Cells.Tag = System.Guid.NewGuid().ToString()

                ' Tag each frame cell item
                For Each cellNode As IConfigurationCell In configFrame.Cells
                    cellNode.Tag = System.Guid.NewGuid().ToString()

                    For Each phasorDefinition As IPhasorDefinition In cellNode.PhasorDefinitions
                        phasorDefinition.Tag = System.Guid.NewGuid().ToString()
                    Next

                    cellNode.FrequencyDefinition.Tag = System.Guid.NewGuid().ToString()

                    For Each analogDefinition As IAnalogDefinition In cellNode.AnalogDefinitions
                        analogDefinition.Tag = System.Guid.NewGuid().ToString()
                    Next

                    For Each digitalDefinition As IDigitalDefinition In cellNode.DigitalDefinitions
                        digitalDefinition.Tag = System.Guid.NewGuid().ToString()
                    Next
                Next

                ' Only configuration nodes need tagging since they will be the destination of all "links"
                Exit For
            End If
        Next

        ' We bind tree before there's any data because this makes the data fill very fast
        With TreeFrameAttributes
            .Override.ShowExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay
            .SetDataBinding(attributeTreeDataSet, "Attributes")
        End With

        ' We hard code column widths because the auto-size function is extremely slow
        For Each column As UltraTreeNodeColumn In TreeFrameAttributes.Nodes.ColumnSetResolved.Columns
            column.LayoutInfo.MinimumCellSize = New Size(200, 15)
        Next

        ' We use fundamental frame type as ID for frames so they are easy to identify - this
        ' is also useful later when we want to determine root node associations to frames
        currentNodeID = FundamentalFrameType.Undetermined + 1

        For Each frame In attributeFrames
            ' Data frames have an associated configuration frame
            If frame.FrameType = FundamentalFrameType.DataFrame Then
                ' See if frame can be cast as a data frame (could be a partially parsed frame, e.g. a common frame header)
                Dim dataFrame As IDataFrame = TryCast(frame, IDataFrame)

                If dataFrame Is Nothing Then
                    associatedFrame = Nothing
                Else
                    associatedFrame = dataFrame.ConfigurationFrame
                End If
            Else
                associatedFrame = Nothing
            End If

            ' Add frame to tree root using its frame type value as the ID
            lastNodeID = AddChannelNode(attributeTable, frame.FrameType + 1, currentNodeID, frame, CDate(frame.Timestamp).ToString("yyyy-MM-dd HH:mm:ss.fff"), associatedFrame)

            ' We add extra detail for non partial cofiguration and data frames...
            Select Case frame.FrameType
                Case FundamentalFrameType.ConfigurationFrame
                    Dim configFrame As IConfigurationFrame = DirectCast(frame, IConfigurationFrame)

                    ' Add frame cells collection object to frame item
                    lastNodeID = AddChannelNode(attributeTable, lastNodeID, currentNodeID, configFrame.Cells, Nothing, Nothing)

                    ' Add each frame cell item to the list
                    For Each cellNode As IConfigurationCell In configFrame.Cells
                        lastCellNodeID = AddChannelNode(attributeTable, lastNodeID, currentNodeID, cellNode, cellNode.StationName, Nothing)

                        For Each phasorDefinition As IPhasorDefinition In cellNode.PhasorDefinitions
                            AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, phasorDefinition, phasorDefinition.Label, Nothing)
                        Next

                        AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, cellNode.FrequencyDefinition, Nothing, Nothing)

                        For Each analogDefinition As IAnalogDefinition In cellNode.AnalogDefinitions
                            AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, analogDefinition, analogDefinition.Label, Nothing)
                        Next

                        For Each digitalDefinition As IDigitalDefinition In cellNode.DigitalDefinitions
                            AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, digitalDefinition, digitalDefinition.Label, Nothing)
                        Next
                    Next
                Case FundamentalFrameType.DataFrame
                    Dim dataFrame As IDataFrame = DirectCast(frame, IDataFrame)

                    ' Add frame cells collection object to frame item
                    If dataFrame.ConfigurationFrame IsNot Nothing Then
                        lastNodeID = AddChannelNode(attributeTable, lastNodeID, currentNodeID, dataFrame.Cells, Nothing, dataFrame.ConfigurationFrame.Cells)

                        ' Add each frame cell item to the list
                        For Each cellNode As IDataCell In dataFrame.Cells
                            lastCellNodeID = AddChannelNode(attributeTable, lastNodeID, currentNodeID, cellNode, cellNode.StationName, cellNode.ConfigurationCell)

                            For Each phasorValue As IPhasorValue In cellNode.PhasorValues
                                AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, phasorValue, phasorValue.Label, phasorValue.Definition)
                            Next

                            AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, cellNode.FrequencyValue, Nothing, cellNode.FrequencyValue.Definition)

                            For Each analogValue As IAnalogValue In cellNode.AnalogValues
                                AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, analogValue, analogValue.Label, analogValue.Definition)
                            Next

                            For Each digitalValue As IDigitalValue In cellNode.DigitalValues
                                AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, digitalValue, digitalValue.Label, digitalValue.Definition)
                            Next
                        Next
                    Else
                        lastNodeID = AddChannelNode(attributeTable, lastNodeID, currentNodeID, dataFrame.Cells, Nothing, Nothing)

                        ' Add each frame cell item to the list
                        For Each cellNode As IDataCell In dataFrame.Cells
                            lastCellNodeID = AddChannelNode(attributeTable, lastNodeID, currentNodeID, cellNode, cellNode.StationName, Nothing)

                            For Each phasorValue As IPhasorValue In cellNode.PhasorValues
                                AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, phasorValue, phasorValue.Label, Nothing)
                            Next

                            AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, cellNode.FrequencyValue, Nothing, Nothing)

                            For Each analogValue As IAnalogValue In cellNode.AnalogValues
                                AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, analogValue, analogValue.Label, Nothing)
                            Next

                            For Each digitalValue As IDigitalValue In cellNode.DigitalValues
                                AddChannelNode(attributeTable, lastCellNodeID, currentNodeID, digitalValue, digitalValue.Label, Nothing)
                            Next
                        Next
                    End If
            End Select
        Next

    End Sub

    Private Function AddChannelNode(ByVal attributeTable As DataTable, ByVal parentID As Integer, ByRef currentNodeID As Integer, ByVal channelNode As IChannel, ByVal channelLabel As String, ByVal associatedChannelNode As IChannel) As Integer

        Dim row As DataRow
        Dim channelNodeID As Integer

        ' Add channel node
        row = attributeTable.NewRow()
        currentNodeID += 1
        channelNodeID = currentNodeID
        row("ID") = channelNodeID

        ' Only frames show up at the root level
        If parentID > FundamentalFrameType.Undetermined Then
            row("ParentID") = parentID
            row("RootElement") = False
        Else
            row("RootElement") = True
        End If

        If channelNode IsNot Nothing Then
            If channelNode.Tag IsNot Nothing Then row("Key") = channelNode.Tag

            If channelLabel Is Nothing Then
                row("Attribute") = channelNode.Attributes("Derived Type").Replace("TVA.PhasorProtocols.", "").Replace("_", ".")
            Else
                row("Attribute") = channelNode.Attributes("Derived Type").Replace("TVA.PhasorProtocols.", "").Replace("_", ".") & " (" & channelLabel & ")"
            End If

            row("ChannelNode") = parentID
            attributeTable.Rows.Add(row)

            With channelNode.Attributes
                ' Add associated channel node reference (i.e., a "link" node), if defined
                If associatedChannelNode IsNot Nothing Then
                    currentNodeID += 1
                    row = attributeTable.NewRow()
                    row("ID") = currentNodeID

                    ' We allow the frame attributes to show up at root level if user doesn't want
                    ' attributes to show up as child nodes
                    If m_applicationSettings.ShowAttributesAsChildren Then
                        row("ParentID") = channelNodeID
                        row("RootElement") = False
                    Else
                        If parentID > FundamentalFrameType.Undetermined Then
                            row("ParentID") = parentID
                            row("RootElement") = False
                        Else
                            row("RootElement") = True
                        End If
                    End If

                    row("AssociatedKey") = associatedChannelNode.Tag
                    row("Attribute") = "     Click for Associated Definition"
                    row("Value") = associatedChannelNode.Attributes("Derived Type").Replace("TVA.PhasorProtocols.", "")
                    row("ChannelNode") = -1
                    attributeTable.Rows.Add(row)
                End If

                ' Add channel node attributes
                For Each attribute As KeyValuePair(Of String, String) In channelNode.Attributes
                    If String.Compare(attribute.Key, "Derived Type", True) <> 0 Then
                        currentNodeID += 1
                        row = attributeTable.NewRow()
                        row("ID") = currentNodeID

                        ' We allow the frame attributes to show up at root level if user doesn't want
                        ' attributes to show up as child nodes
                        If m_applicationSettings.ShowAttributesAsChildren Then
                            row("ParentID") = channelNodeID
                            row("RootElement") = False
                        Else
                            If parentID > FundamentalFrameType.Undetermined Then
                                row("ParentID") = parentID
                                row("RootElement") = False
                            Else
                                row("RootElement") = True
                            End If
                        End If

                        row("Attribute") = "     " & attribute.Key
                        row("Value") = attribute.Value
                        row("ChannelNode") = 0
                        attributeTable.Rows.Add(row)
                    End If
                Next
            End With
        End If

        Return channelNodeID

    End Function

    Private Function CompareByFrameType(ByVal channelFrame1 As IChannelFrame, ByVal channelFrame2 As IChannelFrame) As Integer

        Return channelFrame1.FrameType.CompareTo(channelFrame2.FrameType)

    End Function

    Private Function CreateAttributeTreeDataSet() As DataSet

        Dim attributeTreeDataSet As New DataSet
        Dim attributeTable As New DataTable("Attributes")

        With attributeTable
            With .Columns
                .Add("ID", GetType(Integer))
                .Add("ParentID", GetType(Integer))
                .Add("Key", GetType(String))
                .Add("AssociatedKey", GetType(String))
                .Add("ChannelNode", GetType(Integer))
                .Add("RootElement", GetType(Boolean))
                .Add("Attribute", GetType(String))
                .Add("Value", GetType(String))
            End With
        End With

        attributeTreeDataSet.Tables.Add(attributeTable)
        attributeTreeDataSet.Relations.Add("Attributes", attributeTable.Columns("ID"), attributeTable.Columns("ParentID"))

        Return attributeTreeDataSet

    End Function

#Region " Attribute Tree Events "

    Private Sub TreeFrameAttributes_ColumnSetGenerated(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTree.ColumnSetGeneratedEventArgs) Handles TreeFrameAttributes.ColumnSetGenerated

        ' Hide the ID, ParentID and Key columns since they are relational numbers that won't make sense to user
        With e.ColumnSet
            .Columns("ID").Visible = False
            .Columns("ParentID").Visible = False
            .Columns("Key").Visible = False
            .Columns("AssociatedKey").Visible = False
            .Columns("ChannelNode").Visible = False
            .Columns("RootElement").Visible = False
            .NodeTextColumn = .Columns("Attribute")
        End With

    End Sub

    Private Sub TreeFrameAttributes_InitializeDataNode(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTree.InitializeDataNodeEventArgs) Handles TreeFrameAttributes.InitializeDataNode

        ' Initialize data nodes - this event fires every time a node is added to the tree from a data source
        With e.Node
            Dim rootChannelNode As Integer = Convert.ToInt32(.RootNode.Cells("ChannelNode").Value)

            ' Make sure that only the channel frames show up at the root level of the tree
            If (rootChannelNode > 0 AndAlso rootChannelNode <= FundamentalFrameType.Undetermined) OrElse Convert.ToBoolean(.Cells("RootElement").Value) Then
                Dim channelNode As Integer = Convert.ToInt32(.Cells("ChannelNode").Value)

                ' Track key nodes as they're added
                If Not IsDBNull(.Cells("Key").Value) Then m_associatedNodes.Add(.Cells("Key").Value.ToString(), e.Node)

                If channelNode > 0 Then
                    ' We highlight all channel nodes (actual phasor class instances) to distinguish them from their attributes
                    With .Override
                        .ShowColumns = DefaultableBoolean.False
                        .BorderStyleNode = UIElementBorderStyle.Solid
                        .ItemHeight = 20
                        With .NodeAppearance
                            .BackColor = m_applicationSettings.ChannelNodeBackgroundColor
                            .ForeColor = m_applicationSettings.ChannelNodeForegroundColor
                            .FontData.Bold = DefaultableBoolean.True
                            .FontData.SizeInPoints = 9
                        End With
                    End With

                    ' Assign a tree key to root frames for quick reference later (used during expand all)
                    If channelNode <= FundamentalFrameType.Undetermined Then .Key = "Frame" & channelNode
                ElseIf channelNode = -1 Then
                    ' We make virtual hyperlinks of associated channel nodes
                    .Override.HotTracking = DefaultableBoolean.True
                End If

                ' Set initial tree nodes to desired state - initially expanding nodes makes tree build *much* slower
                .Expanded = (m_applicationSettings.InitialNodeState = ApplicationSettings.NodeState.Expanded)
                If .Expanded Then Application.DoEvents()
            Else
                ' We hide all nodes that are not direct descendants of a channel frame
                .Visible = False
            End If
        End With

    End Sub

    Private Sub TreeFrameAttributes_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeFrameAttributes.MouseMove

        Dim node As UltraTreeNode = TreeFrameAttributes.GetNodeFromPoint(e.X, e.Y)

        ' Show a "hand" cursor over channel nodes acting as "links"
        If node IsNot Nothing Then
            If Convert.ToInt32(node.Cells("ChannelNode").Value) = -1 Then
                node.DataColumnSetResolved.ColumnSettings.CellAppearance.Cursor = Cursors.Hand
            Else
                node.DataColumnSetResolved.ColumnSettings.CellAppearance.Cursor = Cursors.Default
            End If
        End If

    End Sub

    Private Sub TreeFrameAttributes_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeFrameAttributes.MouseClick

        Dim node As UltraTreeNode = TreeFrameAttributes.GetNodeFromPoint(e.X, e.Y)

        ' Handle "clicking" of "link" by bringing associated node into view
        If node IsNot Nothing Then
            If Convert.ToInt32(node.Cells("ChannelNode").Value) = -1 Then
                If Not IsDBNull(node.Cells("AssociatedKey").Value) Then
                    ' Mouse click event needs to return thread to tree control and looking up channel node may
                    ' be time consuming since we'll need to be exapnding nodes, so we queue this work up on a
                    ' different thread.  Actual work to be performed will still be on UI thread via Invoke, but
                    ' this event completes and returns thread control to the tree.  This step also allows calls
                    ' to DoEvents which could otherwise cause problems if called within the local event context.
                    ThreadPool.QueueUserWorkItem(AddressOf LookupAssociatedChannelNode, node.Cells("AssociatedKey").Value.ToString())
                End If
            End If
        End If

    End Sub

    Private Sub LookupAssociatedChannelNode(ByVal state As Object)

        ' UI work must be invoked on UI thread
        BeginInvoke(New Action(Of String)(AddressOf LookupAssociatedChannelNode), state)

    End Sub

    Private Sub LookupAssociatedChannelNode(ByVal associatedKey As String)

        Dim destinationNode As UltraTreeNode

        Do Until destinationNode IsNot Nothing
            ' Lookup associated node (may not be defined yet as nodes are added on demand)
            If m_associatedNodes.TryGetValue(associatedKey, destinationNode) Then
                With destinationNode
                    .BringIntoView(True)
                    .Selected = True
                End With
            Else
                ' Start expanding destination nodes until desired element has been defined
                If m_associatedNodes.Count > 0 Then
                    For Each node As UltraTreeNode In m_associatedNodes.Values
                        If Not node.Expanded Then
                            node.Expanded = True
                            Application.DoEvents()
                            Exit For
                        End If
                    Next
                Else
                    ' Can't start expanding until at least one associated node has been defined
                    Exit Do
                End If
            End If
        Loop

    End Sub

#End Region

#End Region

#Region " Chart Initialization and Display Code "

    Private Sub InitializeChart()

        InitializeFrequencyLayer()

        If m_selectedCell Is Nothing Then
            InitializePhaseAngleLayer(1)
        Else
            InitializePhaseAngleLayer(m_selectedCell.PhasorDefinitions.Count)
        End If

        With ChartDataDisplay
            .BackColor = m_applicationSettings.BackgroundColor
            .Margin = New Padding(0, 0, 0, 0)
            .Padding = .Margin
            With .TitleTop
                .Margins.Top = 0
                .Margins.Bottom = 10
                .Font = New Font("Verdana", 8, FontStyle.Bold, GraphicsUnit.Point)
                .FontColor = m_applicationSettings.ForegroundColor
                .ClipText = False
            End With
            .DataBind()
        End With

    End Sub

    Private Sub InitializeFrequencyLayer()

        Dim frequencyChartArea As New ChartArea
        Dim timeAxis As AxisItem
        Dim frequencyAxis As AxisItem

        With ChartDataDisplay.CompositeChart
            .Legends.Clear()
            .Series.Clear()
            .ChartLayers.Clear()
            .ChartAreas.Clear()
        End With

        m_frequencyData = New DataTable
        m_frequencyData.Columns.Add(New DataColumn("y", GetType(Single)))

        ' We call BeginDataLoad to disable auto-refresh of charts
        m_frequencyData.BeginLoadData()

        frequencyChartArea = New ChartArea
        With frequencyChartArea
            .BoundsMeasureType = MeasureType.Percentage
            .Bounds = New Rectangle(0, 7, 100, 43)
            .Border.Thickness = 0
            .PE.Fill = m_applicationSettings.BackgroundColor
        End With

        ChartDataDisplay.CompositeChart.ChartAreas.Add(frequencyChartArea)

        timeAxis = CreateTimeAxis(10)
        frequencyChartArea.Axes.Add(timeAxis)

        frequencyAxis = CreateFrequencyAxis()
        frequencyChartArea.Axes.Add(frequencyAxis)

        Dim frequencyLayer As ChartLayerAppearance = CreateFrequencyLayer(frequencyChartArea, timeAxis, frequencyAxis)
        ChartDataDisplay.CompositeChart.ChartLayers.Add(frequencyLayer)

    End Sub

    Private Sub InitializePhaseAngleLayer(ByVal phasorCount As Integer)

        Dim phaseAngleChartArea As New ChartArea
        Dim timeAxis As AxisItem
        Dim phaseAngleAxis As AxisItem

        With ChartDataDisplay.CompositeChart
            .Legends.Clear()
            If .Series.Count > 1 Then .Series.RemoveAt(1)
            If .ChartLayers.Count > 1 Then .ChartLayers.RemoveAt(1)
            If .ChartAreas.Count > 1 Then .ChartAreas.RemoveAt(1)
        End With

        m_phasorData = New DataTable
        With m_phasorData
            For x As Integer = 0 To phasorCount - 1
                .Columns.Add(New DataColumn("y" & x, GetType(Single)))
            Next
        End With

        ' We call BeginDataLoad to disable auto-refresh of charts
        m_phasorData.BeginLoadData()

        phaseAngleChartArea = New ChartArea
        With phaseAngleChartArea
            .BoundsMeasureType = MeasureType.Percentage
            .Bounds = New Rectangle(0, 50, IIf(m_applicationSettings.ShowPhaseAngleLegend, 80, 100), 50)
            .Border.Thickness = 0
            .PE.Fill = m_applicationSettings.BackgroundColor
        End With
        ChartDataDisplay.CompositeChart.ChartAreas.Add(phaseAngleChartArea)

        timeAxis = CreateTimeAxis(0)
        phaseAngleChartArea.Axes.Add(timeAxis)

        phaseAngleAxis = CreatePhaseAngleAxis()
        phaseAngleChartArea.Axes.Add(phaseAngleAxis)

        Dim phaseAngleLayer As ChartLayerAppearance = CreatePhaseAngleLayer(phaseAngleChartArea, timeAxis, phaseAngleAxis)
        ChartDataDisplay.CompositeChart.ChartLayers.Add(phaseAngleLayer)

        If m_applicationSettings.ShowPhaseAngleLegend Then
            Dim phasorLegend As CompositeLegend = CreatePhasorLegend()
            ChartDataDisplay.CompositeChart.Legends.Add(phasorLegend)
            phasorLegend.ChartLayers.Add(phaseAngleLayer)
        End If

    End Sub

    Private Function CreateTimeAxis(ByVal extent As Integer) As AxisItem

        Dim axis As New AxisItem

        With axis
            .OrientationType = AxisNumber.X_Axis
            .DataType = AxisDataType.String
            .Visible = True
            .Labels.Visible = False
            .SetLabelAxisType = SetLabelAxisType.ContinuousData
            .LineThickness = 1
            .LineColor = m_applicationSettings.ForegroundColor
            .Extent = extent
            .MinorGridLines.Visible = False
            .MajorGridLines.Visible = False
        End With

        Return axis

    End Function

    Private Function CreateFrequencyAxis() As AxisItem

        Dim axis As New AxisItem

        With axis
            .OrientationType = AxisNumber.Y_Axis
            .DataType = AxisDataType.Numeric
            .Visible = True
            .Labels.ItemFormatString = "<DATA_VALUE:0.0000>"
            .Labels.Font = New Font("Verdana", 8, FontStyle.Bold, GraphicsUnit.Point)
            .Labels.FontColor = m_applicationSettings.ForegroundColor
            .LineThickness = 1
            .LineColor = m_applicationSettings.ForegroundColor
            .Extent = 50
            .MinorGridLines.Visible = False
            .MajorGridLines.Visible = True
            .Margin.Far.Value = 4
            .Margin.Near.Value = 4
            .TickmarkStyle = AxisTickStyle.Percentage
            .TickmarkPercentage = 23%
            .TickmarkInterval = 0.001
            .RangeType = AxisRangeType.Automatic
            .RangeMin = 59.9R
            .RangeMax = 60.1R
        End With

        Return axis

    End Function

    Private Function CreatePhaseAngleAxis() As AxisItem

        Dim axis As New AxisItem

        With axis
            .OrientationType = AxisNumber.Y_Axis
            .DataType = AxisDataType.Numeric
            .Visible = True
            .Labels.ItemFormatString = "<DATA_VALUE:0.#>"
            .Labels.Font = New Font("Verdana", 8, FontStyle.Bold, GraphicsUnit.Point)
            .Labels.FontColor = m_applicationSettings.ForegroundColor
            .Labels.HorizontalAlign = StringAlignment.Near
            .LineThickness = 1
            .LineColor = m_applicationSettings.ForegroundColor
            .Extent = 30
            .MinorGridLines.Visible = False
            .MajorGridLines.Visible = True
            .TickmarkStyle = AxisTickStyle.Percentage
            .TickmarkPercentage = 25%
            .RangeType = AxisRangeType.Custom
            .RangeMin = -180
            .RangeMax = 180
        End With

        Return axis

    End Function

    Private Function CreateFrequencyLayer(ByVal area As ChartArea, ByVal xAxis As AxisItem, ByVal yAxis As AxisItem) As ChartLayerAppearance

        Dim frequencyLayer As New ChartLayerAppearance

        With frequencyLayer
            .ChartType = ChartType.SplineChart
            .ChartArea = area
            .AxisX = xAxis
            .AxisY = yAxis
            With CType(.ChartTypeAppearance, SplineChartAppearance)
                .MidPointAnchors = m_applicationSettings.ShowDataPointsOnGraphs
                .Thickness = m_applicationSettings.TrendLineWidth
            End With
        End With

        Dim frequencyDataSeries As New NumericSeries

        With frequencyDataSeries
            .DataBind(m_frequencyData, "y")

            ' Set frequency color
            .PEs.Add(New PaintElement(m_applicationSettings.FrequencyColor))
        End With

        ChartDataDisplay.CompositeChart.Series.Add(frequencyDataSeries)
        frequencyLayer.Series.Add(frequencyDataSeries)

        Return frequencyLayer

    End Function

    Private Function CreatePhaseAngleLayer(ByVal area As ChartArea, ByVal xAxis As AxisItem, ByVal yAxis As AxisItem) As ChartLayerAppearance

        Dim phaseAngleLayer As New ChartLayerAppearance

        With phaseAngleLayer
            .ChartType = ChartType.SplineChart
            .ChartArea = area
            .AxisX = xAxis
            .AxisY = yAxis
            With CType(.ChartTypeAppearance, SplineChartAppearance)
                .MidPointAnchors = m_applicationSettings.ShowDataPointsOnGraphs
                .Thickness = m_applicationSettings.TrendLineWidth
            End With
        End With

        For x As Integer = 0 To m_phasorData.Columns.Count - 1
            Dim phasorDataSeries As New NumericSeries

            With phasorDataSeries
                .SetNoUpdate(True)
                .DataBind(m_phasorData, "y" & x)

                If m_selectedCell Is Nothing Then
                    .Label = "Phasor " & x + 1
                Else
                    .Label = m_selectedCell.PhasorDefinitions(x).Label
                End If

                ' Set phase angle color (rotating through configured set of colors)
                .PEs.Add(New PaintElement(m_applicationSettings.PhaseAngleColors(x Mod m_applicationSettings.PhaseAngleColors.Count)))
            End With

            ChartDataDisplay.CompositeChart.Series.Add(phasorDataSeries)
            phaseAngleLayer.Series.Add(phasorDataSeries)
        Next

        Return phaseAngleLayer

    End Function

    Private Function CreatePhasorLegend() As CompositeLegend

        Dim phasorLegend As New CompositeLegend

        With phasorLegend
            .Bounds = New Rectangle(79, 50, 20, 48)  '78, 83, 50, 15)
            .BoundsMeasureType = MeasureType.Percentage
            .PE.Fill = m_applicationSettings.LegendBackgroundColor
            With .LabelStyle
                .HorizontalAlign = StringAlignment.Near
                .FontSizeBestFit = True
                '.Font = New Font("Verdana", 8, FontStyle.Bold, GraphicsUnit.Point)
                .ClipText = False
                .WrapText = False
                .FontColor = m_applicationSettings.LegendForegroundColor
                .Trimming = StringTrimming.EllipsisWord
            End With
            With .Border
                .Color = m_applicationSettings.ForegroundColor
                .CornerRadius = 5
                .Thickness = 1
            End With
        End With

        Return phasorLegend

    End Function

#End Region

#End Region

End Class
