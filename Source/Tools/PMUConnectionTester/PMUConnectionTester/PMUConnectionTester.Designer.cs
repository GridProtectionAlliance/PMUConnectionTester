using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ConnectionTester
{
    public partial class PMUConnectionTester : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing && components is object)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var Appearance1 = new Infragistics.Win.Appearance();
            var Appearance2 = new Infragistics.Win.Appearance();
            var Appearance3 = new Infragistics.Win.Appearance();
            var UltraToolTipInfo9 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            var Appearance63 = new Infragistics.Win.Appearance();
            var Appearance64 = new Infragistics.Win.Appearance();
            var Appearance36 = new Infragistics.Win.Appearance();
            var Appearance37 = new Infragistics.Win.Appearance();
            var Appearance38 = new Infragistics.Win.Appearance();
            var UltraToolTipInfo3 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Enter host DNS name, IPv4 or IPv6 address", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            var Appearance39 = new Infragistics.Win.Appearance();
            var Appearance42 = new Infragistics.Win.Appearance();
            var Appearance43 = new Infragistics.Win.Appearance();
            var Appearance44 = new Infragistics.Win.Appearance();
            var Appearance45 = new Infragistics.Win.Appearance();
            var Appearance46 = new Infragistics.Win.Appearance();
            var UltraToolTipInfo4 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Enter host DNS name, IPv4 or IPv6 address", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            var Appearance47 = new Infragistics.Win.Appearance();
            var Appearance48 = new Infragistics.Win.Appearance();
            var UltraToolTipInfo5 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("This enables the Request to Send (RTS) signal during serial communication.", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            var UltraToolTipInfo6 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("This enables the Data Terminal Ready (DTR) signal during serial communication.", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            var Appearance49 = new Infragistics.Win.Appearance();
            var Appearance50 = new Infragistics.Win.Appearance();
            var Appearance51 = new Infragistics.Win.Appearance();
            var Appearance52 = new Infragistics.Win.Appearance();
            var Appearance53 = new Infragistics.Win.Appearance();
            var Appearance54 = new Infragistics.Win.Appearance();
            var Appearance55 = new Infragistics.Win.Appearance();
            var Appearance56 = new Infragistics.Win.Appearance();
            var Appearance57 = new Infragistics.Win.Appearance();
            var PaintElement1 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            var Appearance33 = new Infragistics.Win.Appearance();
            var Appearance58 = new Infragistics.Win.Appearance();
            var Appearance34 = new Infragistics.Win.Appearance();
            var UltraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var UltraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var UltraToolTipInfo2 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("This protocol has additional connection parameters.  Click here to view.", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.False);
            var Appearance35 = new Infragistics.Win.Appearance();
            var UltraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var UltraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var UltraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var UltraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var Appearance19 = new Infragistics.Win.Appearance();
            var UltraToolTipInfo1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Keeping this data window displayed may cause the graph to fall behind real-time", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.True);
            var Appearance12 = new Infragistics.Win.Appearance();
            var Appearance13 = new Infragistics.Win.Appearance();
            var Appearance14 = new Infragistics.Win.Appearance();
            var Appearance15 = new Infragistics.Win.Appearance();
            var Appearance16 = new Infragistics.Win.Appearance();
            var Appearance17 = new Infragistics.Win.Appearance();
            var Appearance18 = new Infragistics.Win.Appearance();
            var UltraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var UltraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var Appearance29 = new Infragistics.Win.Appearance();
            var UltraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var UltraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var Appearance30 = new Infragistics.Win.Appearance();
            var UltraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var UltraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var Appearance31 = new Infragistics.Win.Appearance();
            var UltraStatusPanel7 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var UltraStatusPanel8 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var UltraStatusPanel9 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var UltraStatusPanel10 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            var Appearance32 = new Infragistics.Win.Appearance();
            var Appearance59 = new Infragistics.Win.Appearance();
            var Appearance60 = new Infragistics.Win.Appearance();
            var UltraToolTipInfo7 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Click link to change source voltage and current phasors for power / vars calculat" + "ions...", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            var Appearance61 = new Infragistics.Win.Appearance();
            var Appearance62 = new Infragistics.Win.Appearance();
            var UltraToolTipInfo8 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Click link to change source voltage and current phasors for power / vars calculat" + "ions...", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            var Appearance28 = new Infragistics.Win.Appearance();
            var Appearance27 = new Infragistics.Win.Appearance();
            var Appearance20 = new Infragistics.Win.Appearance();
            var Appearance21 = new Infragistics.Win.Appearance();
            var Appearance22 = new Infragistics.Win.Appearance();
            var Appearance23 = new Infragistics.Win.Appearance();
            var Appearance24 = new Infragistics.Win.Appearance();
            var Appearance25 = new Infragistics.Win.Appearance();
            var Appearance26 = new Infragistics.Win.Appearance();
            var Appearance7 = new Infragistics.Win.Appearance();
            var UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var Appearance8 = new Infragistics.Win.Appearance();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(PMUConnectionTester));
            var UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var Appearance9 = new Infragistics.Win.Appearance();
            var UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var Appearance10 = new Infragistics.Win.Appearance();
            var UltraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var Appearance11 = new Infragistics.Win.Appearance();
            var Appearance5 = new Infragistics.Win.Appearance();
            var Appearance6 = new Infragistics.Win.Appearance();
            var Appearance4 = new Infragistics.Win.Appearance();
            var Appearance40 = new Infragistics.Win.Appearance();
            var Appearance41 = new Infragistics.Win.Appearance();
            TabPageControl9 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            TextBoxRawCommand = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            LabelAlternateCommandChannelState = new Infragistics.Win.Misc.UltraLabel();
            LabelAlternateCommandChannelState.Click += new EventHandler(LabelAlternateCommandChannelState_Click);
            LabelAlternateCommandChannel = new Infragistics.Win.Misc.UltraLabel();
            LabelAlternateCommandChannel.Click += new EventHandler(LabelAlternateCommandChannel_Click);
            LabelVersion = new Infragistics.Win.Misc.UltraLabel();
            ButtonListen = new Infragistics.Win.Misc.UltraButton();
            ButtonListen.Click += new EventHandler(ButtonListen_Click);
            ComboBoxCommands = new ComboBox();
            ComboBoxCommands.SelectedIndexChanged += new EventHandler(ComboBoxCommands_SelectedIndexChanged);
            LabelCommand = new Infragistics.Win.Misc.UltraLabel();
            LabelDeviceID = new Infragistics.Win.Misc.UltraLabel();
            TextBoxDeviceID = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxDeviceID.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxDeviceID.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            ButtonSendCommand = new Infragistics.Win.Misc.UltraButton();
            ButtonSendCommand.Click += new EventHandler(ButtonSendCommand_Click);
            ComboBoxProtocols = new ComboBox();
            ComboBoxProtocols.SelectedIndexChanged += new EventHandler(ComboBoxProtocols_SelectedIndexChanged);
            TabPageControl10 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            TabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            LabelTcpNetworkInterface = new Infragistics.Win.Misc.UltraLabel();
            LabelTcpNetworkInterface.Click += new EventHandler(LabelTcpNetworkInterface_Click);
            CheckBoxEstablishTcpServer = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            CheckBoxEstablishTcpServer.CheckedChanged += new EventHandler(CheckBoxEstablishTcpServer_CheckedChanged);
            LabelTcpHostIP = new Infragistics.Win.Misc.UltraLabel();
            TextBoxTcpHostIP = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxTcpHostIP.GotFocus += new EventHandler(TextBox_GotFocus);
            LabelTcpPort = new Infragistics.Win.Misc.UltraLabel();
            TextBoxTcpPort = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxTcpPort.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxTcpPort.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            TabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            LabelReceiveFrom = new Infragistics.Win.Misc.UltraLabel();
            LabelReceiveFrom.Click += new EventHandler(LabelReceiveFrom_Click);
            LabelUdpNetworkInterface = new Infragistics.Win.Misc.UltraLabel();
            LabelUdpNetworkInterface.Click += new EventHandler(LabelUdpNetworkInterface_Click);
            GroupBoxRemoteUDPServer = new Infragistics.Win.Misc.UltraGroupBox();
            LabelMulticastSource = new Infragistics.Win.Misc.UltraLabel();
            LabelMulticastSource.Click += new EventHandler(LabelMulticastSource_Click);
            LabelUdpHostIP = new Infragistics.Win.Misc.UltraLabel();
            TextBoxUdpHostIP = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxUdpHostIP.GotFocus += new EventHandler(TextBox_GotFocus);
            LabelUdpRemotePort = new Infragistics.Win.Misc.UltraLabel();
            TextBoxUdpRemotePort = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxUdpRemotePort.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxUdpRemotePort.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            CheckBoxRemoteUdpServer = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            CheckBoxRemoteUdpServer.CheckedChanged += new EventHandler(CheckBoxRemoteUDPServer_CheckedChanged);
            LabelUdpLocalPort = new Infragistics.Win.Misc.UltraLabel();
            TextBoxUdpLocalPort = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxUdpLocalPort.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxUdpLocalPort.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            TabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            CheckBoxSerialRTS = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            CheckBoxSerialDTR = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            TextBoxSerialDataBits = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxSerialDataBits.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxSerialDataBits.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            ComboBoxSerialStopBits = new ComboBox();
            ComboBoxSerialParities = new ComboBox();
            LabelSerialParity = new Infragistics.Win.Misc.UltraLabel();
            ComboBoxSerialBaudRates = new ComboBox();
            LabelSerialBaudRate = new Infragistics.Win.Misc.UltraLabel();
            ComboBoxSerialPorts = new ComboBox();
            LabelSerialPort = new Infragistics.Win.Misc.UltraLabel();
            LabelSerialStopBits = new Infragistics.Win.Misc.UltraLabel();
            LabelSerialDataBits = new Infragistics.Win.Misc.UltraLabel();
            TabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            CheckBoxAutoRepeatPlayback = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            CheckBoxAutoRepeatPlayback.CheckStateChanged += new EventHandler(CheckBoxAutoRepeatPlayback_CheckChanged);
            LabelFrameRate = new Infragistics.Win.Misc.UltraLabel();
            TextBoxFileFrameRate = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxFileFrameRate.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxFileFrameRate.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            ButtonBrowse = new Button();
            ButtonBrowse.Click += new EventHandler(ButtonBrowse_Click);
            TextBoxFileCaptureName = new TextBox();
            TextBoxFileCaptureName.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxFileCaptureName.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            TextBoxFileCaptureName.TextChanged += new EventHandler(TextBoxFileCaptureName_TextChanged);
            LabelCaptureFile = new Infragistics.Win.Misc.UltraLabel();
            LabelFramesPerSecond = new Infragistics.Win.Misc.UltraLabel();
            LabelReplayCapturedFile = new Infragistics.Win.Misc.UltraLabel();
            TabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            ChartDataDisplay = new Infragistics.Win.UltraWinChart.UltraChart();
            TabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            ButtonRestoreDefaultSettings = new Infragistics.Win.Misc.UltraButton();
            ButtonRestoreDefaultSettings.Click += new EventHandler(ButtonRestoreDefaultSettings_Click);
            GroupBoxPowerVarCalculations = new Infragistics.Win.Misc.UltraGroupBox();
            ComboBoxCurrentPhasors = new ComboBox();
            LabelCurrentPhasor = new Infragistics.Win.Misc.UltraLabel();
            ComboBoxVoltagePhasors = new ComboBox();
            LabelVoltagePhasor = new Infragistics.Win.Misc.UltraLabel();
            PropertyGridApplicationSettings = new PropertyGrid();
            PropertyGridApplicationSettings.PropertyValueChanged += new PropertyValueChangedEventHandler(PropertyGridApplicationSettings_PropertyValueChanged);
            ButtonGetStatus = new Infragistics.Win.Misc.UltraButton();
            ButtonGetStatus.Click += new EventHandler(ButtonGetStatus_Click);
            TabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            TextBoxMessages = new TextBox();
            TabPageControl8 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            TreeFrameAttributes = new Infragistics.Win.UltraWinTree.UltraTree();
            TreeFrameAttributes.ColumnSetGenerated += new Infragistics.Win.UltraWinTree.ColumnSetGeneratedEventHandler(TreeFrameAttributes_ColumnSetGenerated);
            TreeFrameAttributes.BeforeDataNodesCollectionPopulated += new Infragistics.Win.UltraWinTree.BeforeDataNodesCollectionPopulatedEventHandler(TreeFrameAttributes_BeforeDataNodesCollectionPopulated);
            TreeFrameAttributes.AfterDataNodesCollectionPopulated += new Infragistics.Win.UltraWinTree.AfterDataNodesCollectionPopulatedEventHandler(TreeFrameAttributes_AfterDataNodesCollectionPopulated);
            TreeFrameAttributes.InitializeDataNode += new Infragistics.Win.UltraWinTree.InitializeDataNodeEventHandler(TreeFrameAttributes_InitializeDataNode);
            TreeFrameAttributes.MouseMove += new MouseEventHandler(TreeFrameAttributes_MouseMove);
            TreeFrameAttributes.MouseClick += new MouseEventHandler(TreeFrameAttributes_MouseClick);
            ContextMenuForTree = new ContextMenuStrip(components);
            MenuItemRefresh = new ToolStripMenuItem();
            MenuItemRefresh.Click += new EventHandler(MenuItemRefresh_Click);
            ToolStripSeparator5 = new ToolStripSeparator();
            MenuItemExpandAll = new ToolStripMenuItem();
            MenuItemExpandAll.Click += new EventHandler(MenuItemExpandAll_Click);
            MenuItemCollapseAll = new ToolStripMenuItem();
            MenuItemCollapseAll.Click += new EventHandler(MenuItemCollapseAll_Click);
            GroupBoxConnection = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            GroupBoxConnection.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(GroupBoxConnection_ExpandedStateChanging);
            GroupBoxPanelConnection = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            TabControlProtocolParameters = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            TabControlProtocolParameters.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(TabControlProtocolParameters_SelectedTabChanged);
            TabSharedControlsPageProtocolParameters = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            TabControlCommunications = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            TabSharedControlsPageCommunications = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            GroupBoxStatus = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            GroupBoxStatus.ExpandedStateChanged += new EventHandler(GroupBoxStatus_ExpandedStateChanged);
            GroupBoxStatus.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(GroupBoxStatus_ExpandedStateChanging);
            GroupBoxPanelStatus = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            LabelBinaryFrameImage = new Infragistics.Win.Misc.UltraLabel();
            LabelDisplayFormat = new Infragistics.Win.Misc.UltraLabel();
            ComboBoxByteEncodingDisplayFormats = new ComboBox();
            ComboBoxByteEncodingDisplayFormats.SelectedIndexChanged += new EventHandler(ComboBoxByteEncodingDisplayFormats_SelectedIndexChanged);
            LabelFrameType = new Infragistics.Win.Misc.UltraLabel();
            LabelFrameTypeLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelFrequency = new Infragistics.Win.Misc.UltraLabel();
            LabelFrequencyLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelMagnitude = new Infragistics.Win.Misc.UltraLabel();
            LabelMagnitudeLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelAngle = new Infragistics.Win.Misc.UltraLabel();
            LabelAngleLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelTime = new Infragistics.Win.Misc.UltraLabel();
            LabelTimeLabel = new Infragistics.Win.Misc.UltraLabel();
            StatusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            ToolTipManager = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
            LabelVarsLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelVarsLabel.Click += new EventHandler(CalculatedPowerOrVarLabel_Click);
            LabelPowerLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelPowerLabel.Click += new EventHandler(CalculatedPowerOrVarLabel_Click);
            OpenFileDialog = new OpenFileDialog();
            GroupBoxHeaderFrame = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            GroupBoxHeaderFrame.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(GroupBoxHeaderFrame_ExpandedStateChanging);
            GroupBoxPanelHeaderFrame = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            TextBoxHeaderFrame = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            SaveFileDialog = new SaveFileDialog();
            GroupBoxConfigurationFrame = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            GroupBoxConfigurationFrame.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(GroupBoxConfigurationFrame_ExpandedStateChanging);
            GroupBoxPanelConfigurationFrame = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            LabelPhasorCount = new Infragistics.Win.Misc.UltraLabel();
            LabelPhasorCountLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelHz = new Infragistics.Win.Misc.UltraLabel();
            LabelNominalFrequency = new Infragistics.Win.Misc.UltraLabel();
            LabelNominalFrequencyLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelDigitalCount = new Infragistics.Win.Misc.UltraLabel();
            LabelDigitalCountLabel = new Infragistics.Win.Misc.UltraLabel();
            LabelAnalogCount = new Infragistics.Win.Misc.UltraLabel();
            LabelAnalogCountLabel = new Infragistics.Win.Misc.UltraLabel();
            ComboBoxPmus = new ComboBox();
            ComboBoxPmus.SelectedIndexChanged += new EventHandler(ComboBoxPmus_SelectedIndexChanged);
            LabelIDCode = new Infragistics.Win.Misc.UltraLabel();
            LabelIDCodeLabel = new Infragistics.Win.Misc.UltraLabel();
            ComboBoxPhasors = new ComboBox();
            ComboBoxPhasors.SelectedIndexChanged += new EventHandler(ComboBoxPhasors_SelectedIndexChanged);
            LabelPmuList = new Infragistics.Win.Misc.UltraLabel();
            LabelSelectedIsRefAngle = new Infragistics.Win.Misc.UltraLabel();
            LabelPhasor = new Infragistics.Win.Misc.UltraLabel();
            MenuStripMain = new MenuStrip();
            MenuItemFile = new ToolStripMenuItem();
            MenuItemConnection = new ToolStripMenuItem();
            MenuItemLoadConnection = new ToolStripMenuItem();
            MenuItemLoadConnection.Click += new EventHandler(MenuItemLoadConnection_Click);
            MenuItemSaveConnection = new ToolStripMenuItem();
            MenuItemSaveConnection.Click += new EventHandler(MenuItemSaveConnection_Click);
            MenuItemConfigFile = new ToolStripMenuItem();
            MenuItemLoadConfigFile = new ToolStripMenuItem();
            MenuItemLoadConfigFile.Click += new EventHandler(MenuItemLoadConfigFile_Click);
            MenuItemSaveConfigFile = new ToolStripMenuItem();
            MenuItemSaveConfigFile.Click += new EventHandler(MenuItemSaveConfigFile_Click);
            MenuItemCapture = new ToolStripMenuItem();
            MenuItemStartCapture = new ToolStripMenuItem();
            MenuItemStartCapture.Click += new EventHandler(MenuItemStartCapture_Click);
            MenuItemStopCapture = new ToolStripMenuItem();
            MenuItemStopCapture.Click += new EventHandler(MenuItemStopCapture_Click);
            ToolStripSeparator3 = new ToolStripSeparator();
            MenuItemStartStreamDebugCapture = new ToolStripMenuItem();
            MenuItemStartStreamDebugCapture.Click += new EventHandler(MenuItemStartStreamDebugCapture_Click);
            MenuItemStopStreamDebugCapture = new ToolStripMenuItem();
            MenuItemStopStreamDebugCapture.Click += new EventHandler(MenuItemStopStreamDebugCapture_Click);
            ToolStripSeparator6 = new ToolStripSeparator();
            MenuItemCaptureSampleFrames = new ToolStripMenuItem();
            MenuItemCaptureSampleFrames.Click += new EventHandler(MenuItemCaptureSampleFrames_Click);
            MenuItemCancelSampleFrameCapture = new ToolStripMenuItem();
            MenuItemCancelSampleFrameCapture.Click += new EventHandler(MenuItemCancelSampleFrameCapture_Click);
            ToolStripSeparator1 = new ToolStripSeparator();
            MenuItemExit = new ToolStripMenuItem();
            MenuItemExit.Click += new EventHandler(MenuItemExit_Click);
            MenuItemHelp = new ToolStripMenuItem();
            MenuItemLocalHelp = new ToolStripMenuItem();
            MenuItemLocalHelp.Click += new EventHandler(MenuItemLocalHelp_Click);
            MenuItemOnlineHelp = new ToolStripMenuItem();
            MenuItemOnlineHelp.Click += new EventHandler(MenuItemOnlineHelp_Click);
            ToolStripSeparator2 = new ToolStripSeparator();
            MenuItemAbout = new ToolStripMenuItem();
            MenuItemAbout.Click += new EventHandler(MenuItemAbout_Click);
            TabSharedControlsPageChart = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            TabControlChart = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            TabControlChart.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(TabControlChart_SelectedTabChanged);
            GroupBoxRealTimePowerVars = new Infragistics.Win.Misc.UltraGroupBox();
            LabelVars = new Infragistics.Win.Misc.UltraLabel();
            LabelPower = new Infragistics.Win.Misc.UltraLabel();
            ToolTipManagerForExtraParameters = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
            GroupBoxProtocolParameters = new Infragistics.Win.Misc.UltraGroupBox();
            GroupBoxProtocolParameters.Paint += new PaintEventHandler(GroupBoxProtocolParameters_Paint);
            LabelTabMask = new Infragistics.Win.Misc.UltraLabel();
            PropertyGridProtocolParameters = new PropertyGrid();
            GlobalExceptionLogger = new GSF.ErrorManagement.ErrorLogger(components);
            LabelDefaultIPStack = new Infragistics.Win.Misc.UltraLabel();
            TimerDelay = new Timer(components);
            TimerDelay.Tick += new EventHandler(TimerDelay_Tick);
            LabelReceiveFrom = new Infragistics.Win.Misc.UltraLabel();
            LabelReceiveFrom.Click += new EventHandler(LabelReceiveFrom_Click);
            TabPageControl9.SuspendLayout();
            TabPageControl1.SuspendLayout();
            TabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroupBoxRemoteUDPServer).BeginInit();
            GroupBoxRemoteUDPServer.SuspendLayout();
            TabPageControl3.SuspendLayout();
            TabPageControl4.SuspendLayout();
            TabPageControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ChartDataDisplay).BeginInit();
            TabPageControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroupBoxPowerVarCalculations).BeginInit();
            GroupBoxPowerVarCalculations.SuspendLayout();
            TabPageControl7.SuspendLayout();
            TabPageControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TreeFrameAttributes).BeginInit();
            ContextMenuForTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroupBoxConnection).BeginInit();
            GroupBoxConnection.SuspendLayout();
            GroupBoxPanelConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TabControlProtocolParameters).BeginInit();
            TabControlProtocolParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TabControlCommunications).BeginInit();
            TabControlCommunications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroupBoxStatus).BeginInit();
            GroupBoxStatus.SuspendLayout();
            GroupBoxPanelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroupBoxHeaderFrame).BeginInit();
            GroupBoxHeaderFrame.SuspendLayout();
            GroupBoxPanelHeaderFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TextBoxHeaderFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GroupBoxConfigurationFrame).BeginInit();
            GroupBoxConfigurationFrame.SuspendLayout();
            GroupBoxPanelConfigurationFrame.SuspendLayout();
            MenuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TabControlChart).BeginInit();
            TabControlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroupBoxRealTimePowerVars).BeginInit();
            GroupBoxRealTimePowerVars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GroupBoxProtocolParameters).BeginInit();
            GroupBoxProtocolParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GlobalExceptionLogger).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GlobalExceptionLogger.ErrorLog).BeginInit();
            SuspendLayout();
            // 
            // TabPageControl9
            // 
            TabPageControl9.Controls.Add(TextBoxRawCommand);
            TabPageControl9.Controls.Add(LabelAlternateCommandChannelState);
            TabPageControl9.Controls.Add(LabelAlternateCommandChannel);
            TabPageControl9.Controls.Add(LabelVersion);
            TabPageControl9.Controls.Add(ButtonListen);
            TabPageControl9.Controls.Add(ComboBoxCommands);
            TabPageControl9.Controls.Add(LabelCommand);
            TabPageControl9.Controls.Add(LabelDeviceID);
            TabPageControl9.Controls.Add(TextBoxDeviceID);
            TabPageControl9.Controls.Add(ButtonSendCommand);
            TabPageControl9.Controls.Add(ComboBoxProtocols);
            TabPageControl9.Location = new Point(1, 20);
            TabPageControl9.Name = "TabPageControl9";
            TabPageControl9.Size = new Size(360, 96);
            // 
            // TextBoxRawCommand
            // 
            TextBoxRawCommand.AutoSize = false;
            TextBoxRawCommand.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxRawCommand.InputMask = "aaaaaa";
            TextBoxRawCommand.Location = new Point(166, 44);
            TextBoxRawCommand.Name = "TextBoxRawCommand";
            TextBoxRawCommand.PromptChar = ' ';
            TextBoxRawCommand.Size = new Size(64, 20);
            TextBoxRawCommand.TabIndex = 20;
            TextBoxRawCommand.Text = "513";

            // 
            // LabelAlternateCommandChannelState
            // 
            LabelAlternateCommandChannelState.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance1.BackColor = SystemColors.Control;
            Appearance1.BackColor2 = Color.LightGray;
            Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            Appearance1.TextHAlignAsString = "Center";
            Appearance1.TextVAlignAsString = "Middle";
            LabelAlternateCommandChannelState.Appearance = Appearance1;
            LabelAlternateCommandChannelState.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
            LabelAlternateCommandChannelState.Location = new Point(248, 79);
            LabelAlternateCommandChannelState.Name = "LabelAlternateCommandChannelState";
            LabelAlternateCommandChannelState.Size = new Size(108, 14);
            LabelAlternateCommandChannelState.TabIndex = 19;
            LabelAlternateCommandChannelState.Text = "Not Defined";
            // 
            // LabelAlternateCommandChannel
            // 
            LabelAlternateCommandChannel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance2.ForeColor = SystemColors.HotTrack;
            Appearance2.TextHAlignAsString = "Center";
            Appearance2.TextVAlignAsString = "Middle";
            LabelAlternateCommandChannel.Appearance = Appearance2;
            LabelAlternateCommandChannel.Cursor = Cursors.Hand;
            LabelAlternateCommandChannel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            Appearance3.FontData.UnderlineAsString = "True";
            LabelAlternateCommandChannel.HotTrackAppearance = Appearance3;
            LabelAlternateCommandChannel.Location = new Point(246, 49);
            LabelAlternateCommandChannel.Name = "LabelAlternateCommandChannel";
            LabelAlternateCommandChannel.Size = new Size(109, 27);
            LabelAlternateCommandChannel.TabIndex = 18;
            LabelAlternateCommandChannel.Text = "Configure Alternate Command Channel";
            UltraToolTipInfo9.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            UltraToolTipInfo9.ToolTipTextFormatted = "This link allows configuration of an alternate communications channel for sending" + " commands.<br/>For example, this is used for implementing the UDP_T or UDP_U fea" + "ture for SEL relays.";

            UltraToolTipInfo9.ToolTipTitle = "Note";
            ToolTipManager.SetUltraToolTip(LabelAlternateCommandChannel, UltraToolTipInfo9);
            LabelAlternateCommandChannel.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 
            // LabelVersion
            // 
            LabelVersion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance63.FontData.SizeInPoints = 7.25f;
            Appearance63.TextHAlignAsString = "Right";
            LabelVersion.Appearance = Appearance63;
            LabelVersion.Location = new Point(228, 30);
            LabelVersion.Name = "LabelVersion";
            LabelVersion.Size = new Size(126, 15);
            LabelVersion.TabIndex = 16;
            LabelVersion.Text = "Version: 0.0.0.0";
            // 
            // ButtonListen
            // 
            ButtonListen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonListen.Location = new Point(246, 7);
            ButtonListen.Name = "ButtonListen";
            ButtonListen.Size = new Size(110, 23);
            ButtonListen.TabIndex = 15;
            ButtonListen.Text = "&Connect";
            // 
            // ComboBoxCommands
            // 
            ComboBoxCommands.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxCommands.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxCommands.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxCommands.Enabled = false;
            ComboBoxCommands.Items.AddRange(new object[] { "Disable Real-time Data", "Enable Real-time Data", "Send Header Frame", "Send Config Frame 1", "Send Config Frame 2", "Send Config Frame 3", "Send Raw Command" });
            ComboBoxCommands.Location = new Point(6, 72);
            ComboBoxCommands.Name = "ComboBoxCommands";
            ComboBoxCommands.Size = new Size(154, 21);
            ComboBoxCommands.TabIndex = 13;
            // 
            // LabelCommand
            // 
            LabelCommand.Enabled = false;
            LabelCommand.Location = new Point(6, 57);
            LabelCommand.Name = "LabelCommand";
            LabelCommand.Size = new Size(100, 23);
            LabelCommand.TabIndex = 12;
            LabelCommand.Text = "Comm&and:";
            // 
            // LabelDeviceID
            // 
            Appearance64.TextHAlignAsString = "Right";
            LabelDeviceID.Appearance = Appearance64;
            LabelDeviceID.Location = new Point(3, 37);
            LabelDeviceID.Name = "LabelDeviceID";
            LabelDeviceID.Size = new Size(87, 23);
            LabelDeviceID.TabIndex = 10;
            LabelDeviceID.Text = "&Device ID Code:";
            // 
            // TextBoxDeviceID
            // 
            TextBoxDeviceID.AutoSize = false;
            TextBoxDeviceID.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxDeviceID.InputMask = "-nnnnn";
            TextBoxDeviceID.Location = new Point(96, 34);
            TextBoxDeviceID.Name = "TextBoxDeviceID";
            TextBoxDeviceID.PromptChar = ' ';
            TextBoxDeviceID.Size = new Size(45, 20);
            TextBoxDeviceID.TabIndex = 11;
            TextBoxDeviceID.Text = "1";
            // 
            // ButtonSendCommand
            // 
            ButtonSendCommand.Enabled = false;
            ButtonSendCommand.Location = new Point(166, 70);
            ButtonSendCommand.Name = "ButtonSendCommand";
            ButtonSendCommand.Size = new Size(65, 23);
            ButtonSendCommand.TabIndex = 14;
            ButtonSendCommand.Text = "Se&nd";
            // 
            // ComboBoxProtocols
            // 
            ComboBoxProtocols.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxProtocols.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxProtocols.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxProtocols.Location = new Point(6, 7);
            ComboBoxProtocols.Name = "ComboBoxProtocols";
            ComboBoxProtocols.Size = new Size(224, 21);
            ComboBoxProtocols.TabIndex = 9;
            // 
            // TabPageControl10
            // 
            TabPageControl10.Location = new Point(-10000, -10000);
            TabPageControl10.Name = "TabPageControl10";
            TabPageControl10.Size = new Size(360, 96);
            // 
            // TabPageControl1
            // 
            TabPageControl1.Controls.Add(LabelTcpNetworkInterface);
            TabPageControl1.Controls.Add(CheckBoxEstablishTcpServer);
            TabPageControl1.Controls.Add(LabelTcpHostIP);
            TabPageControl1.Controls.Add(TextBoxTcpHostIP);
            TabPageControl1.Controls.Add(LabelTcpPort);
            TabPageControl1.Controls.Add(TextBoxTcpPort);
            TabPageControl1.Location = new Point(-10000, -10000);
            TabPageControl1.Name = "TabPageControl1";
            TabPageControl1.Size = new Size(304, 96);
            // 
            // LabelTcpNetworkInterface
            // 
            LabelTcpNetworkInterface.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance36.ForeColor = SystemColors.HotTrack;
            Appearance36.TextHAlignAsString = "Center";
            Appearance36.TextVAlignAsString = "Middle";
            LabelTcpNetworkInterface.Appearance = Appearance36;
            LabelTcpNetworkInterface.Cursor = Cursors.Hand;
            LabelTcpNetworkInterface.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            Appearance37.FontData.UnderlineAsString = "True";
            LabelTcpNetworkInterface.HotTrackAppearance = Appearance37;
            LabelTcpNetworkInterface.Location = new Point(63, 72);
            LabelTcpNetworkInterface.Name = "LabelTcpNetworkInterface";
            LabelTcpNetworkInterface.Size = new Size(94, 21);
            LabelTcpNetworkInterface.TabIndex = 5;
            LabelTcpNetworkInterface.Text = "Network Interface";
            LabelTcpNetworkInterface.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 
            // CheckBoxEstablishTcpServer
            // 
            CheckBoxEstablishTcpServer.Location = new Point(175, 19);
            CheckBoxEstablishTcpServer.Name = "CheckBoxEstablishTcpServer";
            CheckBoxEstablishTcpServer.Size = new Size(128, 26);
            CheckBoxEstablishTcpServer.TabIndex = 4;
            CheckBoxEstablishTcpServer.Text = "Establish Tcp Server";
            // 
            // LabelTcpHostIP
            // 
            Appearance38.TextHAlignAsString = "Right";
            LabelTcpHostIP.Appearance = Appearance38;
            LabelTcpHostIP.Location = new Point(15, 25);
            LabelTcpHostIP.Name = "LabelTcpHostIP";
            LabelTcpHostIP.Size = new Size(45, 23);
            LabelTcpHostIP.TabIndex = 0;
            LabelTcpHostIP.Text = "Host &IP:";
            UltraToolTipInfo3.ToolTipText = "Enter host DNS name, IPv4 or IPv6 address";
            ToolTipManager.SetUltraToolTip(LabelTcpHostIP, UltraToolTipInfo3);
            // 
            // TextBoxTcpHostIP
            // 
            TextBoxTcpHostIP.AutoSize = false;
            TextBoxTcpHostIP.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxTcpHostIP.InputMask = @"nnn\.nnn\.nnn\.nnn";
            TextBoxTcpHostIP.Location = new Point(66, 22);
            TextBoxTcpHostIP.Name = "TextBoxTcpHostIP";
            TextBoxTcpHostIP.PromptChar = ' ';
            TextBoxTcpHostIP.Size = new Size(92, 20);
            TextBoxTcpHostIP.TabIndex = 1;
            TextBoxTcpHostIP.Text = "127.0.0.1";
            // 
            // LabelTcpPort
            // 
            Appearance39.TextHAlignAsString = "Right";
            LabelTcpPort.Appearance = Appearance39;
            LabelTcpPort.Location = new Point(15, 51);
            LabelTcpPort.Name = "LabelTcpPort";
            LabelTcpPort.Size = new Size(45, 23);
            LabelTcpPort.TabIndex = 2;
            LabelTcpPort.Text = "P&ort:";
            // 
            // TextBoxTcpPort
            // 
            TextBoxTcpPort.AutoSize = false;
            TextBoxTcpPort.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxTcpPort.InputMask = "-nnnnn";
            TextBoxTcpPort.Location = new Point(66, 48);
            TextBoxTcpPort.Name = "TextBoxTcpPort";
            TextBoxTcpPort.PromptChar = ' ';
            TextBoxTcpPort.Size = new Size(45, 20);
            TextBoxTcpPort.TabIndex = 3;
            TextBoxTcpPort.Text = "4712";
            // 
            // TabPageControl2
            // 
            TabPageControl2.Controls.Add(LabelReceiveFrom);
            TabPageControl2.Controls.Add(LabelUdpNetworkInterface);
            TabPageControl2.Controls.Add(GroupBoxRemoteUDPServer);
            TabPageControl2.Controls.Add(LabelUdpLocalPort);
            TabPageControl2.Controls.Add(TextBoxUdpLocalPort);
            TabPageControl2.Location = new Point(1, 20);
            TabPageControl2.Name = "TabPageControl2";
            TabPageControl2.Size = new Size(304, 96);
            // LabelReceiveFrom
            // 
            LabelReceiveFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance42.ForeColor = SystemColors.HotTrack;
            Appearance42.TextHAlignAsString = "Center";
            Appearance42.TextVAlignAsString = "Middle";
            LabelReceiveFrom.Appearance = Appearance42;
            LabelReceiveFrom.Cursor = Cursors.Hand;
            LabelReceiveFrom.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            Appearance43.FontData.UnderlineAsString = "True";
            LabelReceiveFrom.HotTrackAppearance = Appearance43;
            LabelReceiveFrom.Location = new Point(5, 62);
            LabelReceiveFrom.Name = "LabelReceiveFrom";
            LabelReceiveFrom.Size = new Size(105, 21);
            LabelReceiveFrom.TabIndex = 3;
            LabelReceiveFrom.Text = "Receive From";
            LabelReceiveFrom.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 

            // 
            // LabelUdpNetworkInterface
            // 
            LabelUdpNetworkInterface.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance42.ForeColor = SystemColors.HotTrack;
            Appearance42.TextHAlignAsString = "Center";
            Appearance42.TextVAlignAsString = "Middle";
            LabelUdpNetworkInterface.Appearance = Appearance42;
            LabelUdpNetworkInterface.Cursor = Cursors.Hand;
            LabelUdpNetworkInterface.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            Appearance43.FontData.UnderlineAsString = "True";
            LabelUdpNetworkInterface.HotTrackAppearance = Appearance43;
            LabelUdpNetworkInterface.Location = new Point(8, 41);
            LabelUdpNetworkInterface.Name = "LabelUdpNetworkInterface";
            LabelUdpNetworkInterface.Size = new Size(94, 21);
            LabelUdpNetworkInterface.TabIndex = 2;
            LabelUdpNetworkInterface.Text = "Network Interface";
            LabelUdpNetworkInterface.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 
            // GroupBoxRemoteUDPServer
            // 
            GroupBoxRemoteUDPServer.Controls.Add(LabelMulticastSource);
            GroupBoxRemoteUDPServer.Controls.Add(LabelUdpHostIP);
            GroupBoxRemoteUDPServer.Controls.Add(TextBoxUdpHostIP);
            GroupBoxRemoteUDPServer.Controls.Add(LabelUdpRemotePort);
            GroupBoxRemoteUDPServer.Controls.Add(TextBoxUdpRemotePort);
            GroupBoxRemoteUDPServer.Controls.Add(CheckBoxRemoteUdpServer);
            GroupBoxRemoteUDPServer.Location = new Point(112, 3);
            GroupBoxRemoteUDPServer.Name = "GroupBoxRemoteUDPServer";
            GroupBoxRemoteUDPServer.Size = new Size(189, 91);
            GroupBoxRemoteUDPServer.TabIndex = 4;
            // 
            // LabelMulticastSource
            // 
            LabelMulticastSource.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance44.ForeColor = SystemColors.HotTrack;
            Appearance44.TextHAlignAsString = "Center";
            Appearance44.TextVAlignAsString = "Middle";
            LabelMulticastSource.Appearance = Appearance44;
            LabelMulticastSource.Cursor = Cursors.Hand;
            LabelMulticastSource.Enabled = false;
            LabelMulticastSource.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            Appearance45.FontData.UnderlineAsString = "True";
            LabelMulticastSource.HotTrackAppearance = Appearance45;
            LabelMulticastSource.Location = new Point(128, 58);
            LabelMulticastSource.Name = "LabelMulticastSource";
            LabelMulticastSource.Size = new Size(58, 27);
            LabelMulticastSource.TabIndex = 5;
            LabelMulticastSource.Text = "Multicast Source";
            LabelMulticastSource.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 
            // LabelUdpHostIP
            // 
            Appearance46.TextHAlignAsString = "Right";
            LabelUdpHostIP.Appearance = Appearance46;
            LabelUdpHostIP.Enabled = false;
            LabelUdpHostIP.Location = new Point(30, 36);
            LabelUdpHostIP.Name = "LabelUdpHostIP";
            LabelUdpHostIP.Size = new Size(45, 23);
            LabelUdpHostIP.TabIndex = 1;
            LabelUdpHostIP.Text = "Host IP:";
            UltraToolTipInfo4.ToolTipText = "Enter host DNS name, IPv4 or IPv6 address";
            ToolTipManager.SetUltraToolTip(LabelUdpHostIP, UltraToolTipInfo4);
            // 
            // TextBoxUdpHostIP
            // 
            TextBoxUdpHostIP.AutoSize = false;
            TextBoxUdpHostIP.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxUdpHostIP.Enabled = false;
            TextBoxUdpHostIP.InputMask = @"nnn\.nnn\.nnn\.nnn";
            TextBoxUdpHostIP.Location = new Point(81, 33);
            TextBoxUdpHostIP.Name = "TextBoxUdpHostIP";
            TextBoxUdpHostIP.PromptChar = ' ';
            TextBoxUdpHostIP.Size = new Size(92, 20);
            TextBoxUdpHostIP.TabIndex = 2;
            TextBoxUdpHostIP.Text = "127.0.0.1";
            // 
            // LabelUdpRemotePort
            // 
            Appearance47.TextHAlignAsString = "Right";
            LabelUdpRemotePort.Appearance = Appearance47;
            LabelUdpRemotePort.Enabled = false;
            LabelUdpRemotePort.Location = new Point(4, 62);
            LabelUdpRemotePort.Name = "LabelUdpRemotePort";
            LabelUdpRemotePort.Size = new Size(71, 23);
            LabelUdpRemotePort.TabIndex = 3;
            LabelUdpRemotePort.Text = "Remote Port:";
            // 
            // TextBoxUdpRemotePort
            // 
            TextBoxUdpRemotePort.AutoSize = false;
            TextBoxUdpRemotePort.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxUdpRemotePort.Enabled = false;
            TextBoxUdpRemotePort.InputMask = "-nnnnn";
            TextBoxUdpRemotePort.Location = new Point(81, 59);
            TextBoxUdpRemotePort.Name = "TextBoxUdpRemotePort";
            TextBoxUdpRemotePort.PromptChar = ' ';
            TextBoxUdpRemotePort.Size = new Size(45, 20);
            TextBoxUdpRemotePort.TabIndex = 4;
            TextBoxUdpRemotePort.Text = "5000";
            // 
            // CheckBoxRemoteUdpServer
            // 
            CheckBoxRemoteUdpServer.Location = new Point(8, 4);
            CheckBoxRemoteUdpServer.Name = "CheckBoxRemoteUdpServer";
            CheckBoxRemoteUdpServer.Size = new Size(178, 26);
            CheckBoxRemoteUdpServer.TabIndex = 0;
            CheckBoxRemoteUdpServer.Text = "Enable Multicast / Remote Udp";
            // 
            // LabelUdpLocalPort
            // 
            Appearance48.TextHAlignAsString = "Right";
            LabelUdpLocalPort.Appearance = Appearance48;
            LabelUdpLocalPort.Location = new Point(-4, 21);
            LabelUdpLocalPort.Name = "LabelUdpLocalPort";
            LabelUdpLocalPort.Size = new Size(63, 23);
            LabelUdpLocalPort.TabIndex = 0;
            LabelUdpLocalPort.Text = "Local P&ort:";
            // 
            // TextBoxUdpLocalPort
            // 
            TextBoxUdpLocalPort.AutoSize = false;
            TextBoxUdpLocalPort.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxUdpLocalPort.InputMask = "-nnnnn";
            TextBoxUdpLocalPort.Location = new Point(61, 18);
            TextBoxUdpLocalPort.Name = "TextBoxUdpLocalPort";
            TextBoxUdpLocalPort.PromptChar = ' ';
            TextBoxUdpLocalPort.Size = new Size(45, 20);
            TextBoxUdpLocalPort.TabIndex = 1;
            TextBoxUdpLocalPort.Text = "4712";
            // 
            // TabPageControl3
            // 
            TabPageControl3.Controls.Add(CheckBoxSerialRTS);
            TabPageControl3.Controls.Add(CheckBoxSerialDTR);
            TabPageControl3.Controls.Add(TextBoxSerialDataBits);
            TabPageControl3.Controls.Add(ComboBoxSerialStopBits);
            TabPageControl3.Controls.Add(ComboBoxSerialParities);
            TabPageControl3.Controls.Add(LabelSerialParity);
            TabPageControl3.Controls.Add(ComboBoxSerialBaudRates);
            TabPageControl3.Controls.Add(LabelSerialBaudRate);
            TabPageControl3.Controls.Add(ComboBoxSerialPorts);
            TabPageControl3.Controls.Add(LabelSerialPort);
            TabPageControl3.Controls.Add(LabelSerialStopBits);
            TabPageControl3.Controls.Add(LabelSerialDataBits);
            TabPageControl3.Location = new Point(-10000, -10000);
            TabPageControl3.Name = "TabPageControl3";
            TabPageControl3.Size = new Size(304, 96);
            // 
            // CheckBoxSerialRTS
            // 
            CheckBoxSerialRTS.Location = new Point(225, 62);
            CheckBoxSerialRTS.Name = "CheckBoxSerialRTS";
            CheckBoxSerialRTS.Size = new Size(50, 26);
            CheckBoxSerialRTS.TabIndex = 11;
            CheckBoxSerialRTS.Text = "RTS";
            UltraToolTipInfo5.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            UltraToolTipInfo5.ToolTipText = "This enables the Request to Send (RTS) signal during serial communication.";
            UltraToolTipInfo5.ToolTipTitle = "Note";
            ToolTipManager.SetUltraToolTip(CheckBoxSerialRTS, UltraToolTipInfo5);
            // 
            // CheckBoxSerialDTR
            // 
            CheckBoxSerialDTR.Location = new Point(169, 62);
            CheckBoxSerialDTR.Name = "CheckBoxSerialDTR";
            CheckBoxSerialDTR.Size = new Size(50, 26);
            CheckBoxSerialDTR.TabIndex = 10;
            CheckBoxSerialDTR.Text = "DTR";
            UltraToolTipInfo6.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            UltraToolTipInfo6.ToolTipText = "This enables the Data Terminal Ready (DTR) signal during serial communication.";
            UltraToolTipInfo6.ToolTipTitle = "Note";
            ToolTipManager.SetUltraToolTip(CheckBoxSerialDTR, UltraToolTipInfo6);
            // 
            // TextBoxSerialDataBits
            // 
            TextBoxSerialDataBits.AutoSize = false;
            TextBoxSerialDataBits.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxSerialDataBits.InputMask = "nnn";
            TextBoxSerialDataBits.Location = new Point(211, 37);
            TextBoxSerialDataBits.Name = "TextBoxSerialDataBits";
            TextBoxSerialDataBits.PromptChar = ' ';
            TextBoxSerialDataBits.Size = new Size(27, 20);
            TextBoxSerialDataBits.TabIndex = 9;
            TextBoxSerialDataBits.Text = "8";
            // 
            // ComboBoxSerialStopBits
            // 
            ComboBoxSerialStopBits.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxSerialStopBits.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxSerialStopBits.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxSerialStopBits.Location = new Point(211, 10);
            ComboBoxSerialStopBits.Name = "ComboBoxSerialStopBits";
            ComboBoxSerialStopBits.Size = new Size(85, 21);
            ComboBoxSerialStopBits.TabIndex = 7;
            // 
            // ComboBoxSerialParities
            // 
            ComboBoxSerialParities.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxSerialParities.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxSerialParities.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxSerialParities.Location = new Point(66, 65);
            ComboBoxSerialParities.Name = "ComboBoxSerialParities";
            ComboBoxSerialParities.Size = new Size(85, 21);
            ComboBoxSerialParities.TabIndex = 5;
            // 
            // LabelSerialParity
            // 
            Appearance49.TextHAlignAsString = "Right";
            LabelSerialParity.Appearance = Appearance49;
            LabelSerialParity.Location = new Point(-4, 68);
            LabelSerialParity.Name = "LabelSerialParity";
            LabelSerialParity.Size = new Size(64, 23);
            LabelSerialParity.TabIndex = 4;
            LabelSerialParity.Text = "Par&ity:";
            // 
            // ComboBoxSerialBaudRates
            // 
            ComboBoxSerialBaudRates.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxSerialBaudRates.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxSerialBaudRates.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxSerialBaudRates.Items.AddRange(new object[] { "115200", "57600", "38400", "19200", "9600", "4800", "2400", "1200" });
            ComboBoxSerialBaudRates.Location = new Point(66, 37);
            ComboBoxSerialBaudRates.Name = "ComboBoxSerialBaudRates";
            ComboBoxSerialBaudRates.Size = new Size(85, 21);
            ComboBoxSerialBaudRates.TabIndex = 3;
            // 
            // LabelSerialBaudRate
            // 
            Appearance50.TextHAlignAsString = "Right";
            LabelSerialBaudRate.Appearance = Appearance50;
            LabelSerialBaudRate.Location = new Point(-4, 40);
            LabelSerialBaudRate.Name = "LabelSerialBaudRate";
            LabelSerialBaudRate.Size = new Size(64, 23);
            LabelSerialBaudRate.TabIndex = 2;
            LabelSerialBaudRate.Text = "&Baud Rate:";
            // 
            // ComboBoxSerialPorts
            // 
            ComboBoxSerialPorts.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxSerialPorts.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxSerialPorts.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxSerialPorts.Location = new Point(66, 10);
            ComboBoxSerialPorts.Name = "ComboBoxSerialPorts";
            ComboBoxSerialPorts.Size = new Size(85, 21);
            ComboBoxSerialPorts.TabIndex = 1;
            // 
            // LabelSerialPort
            // 
            Appearance51.TextHAlignAsString = "Right";
            LabelSerialPort.Appearance = Appearance51;
            LabelSerialPort.Location = new Point(-4, 13);
            LabelSerialPort.Name = "LabelSerialPort";
            LabelSerialPort.Size = new Size(64, 23);
            LabelSerialPort.TabIndex = 0;
            LabelSerialPort.Text = "P&ort:";
            // 
            // LabelSerialStopBits
            // 
            Appearance52.TextHAlignAsString = "Right";
            LabelSerialStopBits.Appearance = Appearance52;
            LabelSerialStopBits.Location = new Point(150, 13);
            LabelSerialStopBits.Name = "LabelSerialStopBits";
            LabelSerialStopBits.Size = new Size(57, 23);
            LabelSerialStopBits.TabIndex = 6;
            LabelSerialStopBits.Text = "Stop Bits:";
            // 
            // LabelSerialDataBits
            // 
            Appearance53.TextHAlignAsString = "Right";
            LabelSerialDataBits.Appearance = Appearance53;
            LabelSerialDataBits.Location = new Point(148, 40);
            LabelSerialDataBits.Name = "LabelSerialDataBits";
            LabelSerialDataBits.Size = new Size(59, 23);
            LabelSerialDataBits.TabIndex = 8;
            LabelSerialDataBits.Text = "Data Bits:";
            // 
            // TabPageControl4
            // 
            TabPageControl4.Controls.Add(CheckBoxAutoRepeatPlayback);
            TabPageControl4.Controls.Add(LabelFrameRate);
            TabPageControl4.Controls.Add(TextBoxFileFrameRate);
            TabPageControl4.Controls.Add(ButtonBrowse);
            TabPageControl4.Controls.Add(TextBoxFileCaptureName);
            TabPageControl4.Controls.Add(LabelCaptureFile);
            TabPageControl4.Controls.Add(LabelFramesPerSecond);
            TabPageControl4.Controls.Add(LabelReplayCapturedFile);
            TabPageControl4.Location = new Point(-10000, -10000);
            TabPageControl4.Name = "TabPageControl4";
            TabPageControl4.Size = new Size(304, 96);
            // 
            // CheckBoxAutoRepeatPlayback
            // 
            CheckBoxAutoRepeatPlayback.Checked = true;
            CheckBoxAutoRepeatPlayback.CheckState = CheckState.Checked;
            CheckBoxAutoRepeatPlayback.Location = new Point(73, 72);
            CheckBoxAutoRepeatPlayback.Name = "CheckBoxAutoRepeatPlayback";
            CheckBoxAutoRepeatPlayback.Size = new Size(217, 26);
            CheckBoxAutoRepeatPlayback.TabIndex = 7;
            CheckBoxAutoRepeatPlayback.Text = "Auto-repeat captured file playback";
            // 
            // LabelFrameRate
            // 
            Appearance54.TextHAlignAsString = "Right";
            LabelFrameRate.Appearance = Appearance54;
            LabelFrameRate.Location = new Point(-2, 53);
            LabelFrameRate.Name = "LabelFrameRate";
            LabelFrameRate.Size = new Size(69, 23);
            LabelFrameRate.TabIndex = 3;
            LabelFrameRate.Text = "Frame Rate:";
            // 
            // TextBoxFileFrameRate
            // 
            TextBoxFileFrameRate.AutoSize = false;
            TextBoxFileFrameRate.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxFileFrameRate.InputMask = "nnn";
            TextBoxFileFrameRate.Location = new Point(73, 50);
            TextBoxFileFrameRate.Name = "TextBoxFileFrameRate";
            TextBoxFileFrameRate.PromptChar = ' ';
            TextBoxFileFrameRate.Size = new Size(27, 20);
            TextBoxFileFrameRate.TabIndex = 4;
            TextBoxFileFrameRate.Text = "30";
            // 
            // ButtonBrowse
            // 
            ButtonBrowse.Location = new Point(215, 19);
            ButtonBrowse.Name = "ButtonBrowse";
            ButtonBrowse.Size = new Size(75, 23);
            ButtonBrowse.TabIndex = 2;
            ButtonBrowse.Text = "&Browse...";
            ButtonBrowse.UseVisualStyleBackColor = true;
            // 
            // TextBoxFileCaptureName
            // 
            TextBoxFileCaptureName.BackColor = Color.White;
            TextBoxFileCaptureName.Location = new Point(73, 21);
            TextBoxFileCaptureName.Name = "TextBoxFileCaptureName";
            TextBoxFileCaptureName.Size = new Size(136, 20);
            TextBoxFileCaptureName.TabIndex = 1;
            // 
            // LabelCaptureFile
            // 
            Appearance55.TextHAlignAsString = "Right";
            LabelCaptureFile.Appearance = Appearance55;
            LabelCaptureFile.Location = new Point(13, 24);
            LabelCaptureFile.Name = "LabelCaptureFile";
            LabelCaptureFile.Size = new Size(54, 23);
            LabelCaptureFile.TabIndex = 0;
            LabelCaptureFile.Text = "F&ilename:";
            // 
            // LabelFramesPerSecond
            // 
            Appearance56.TextHAlignAsString = "Right";
            LabelFramesPerSecond.Appearance = Appearance56;
            LabelFramesPerSecond.Location = new Point(95, 53);
            LabelFramesPerSecond.Name = "LabelFramesPerSecond";
            LabelFramesPerSecond.Size = new Size(82, 17);
            LabelFramesPerSecond.TabIndex = 5;
            LabelFramesPerSecond.Text = "frames/second";
            // 
            // LabelReplayCapturedFile
            // 
            Appearance57.FontData.ItalicAsString = "True";
            LabelReplayCapturedFile.Appearance = Appearance57;
            LabelReplayCapturedFile.Location = new Point(193, 4);
            LabelReplayCapturedFile.Name = "LabelReplayCapturedFile";
            LabelReplayCapturedFile.Size = new Size(120, 17);
            LabelReplayCapturedFile.TabIndex = 6;
            LabelReplayCapturedFile.Text = "Replay captured file...";
            // 
            // TabPageControl5
            // 
            TabPageControl5.Controls.Add(ChartDataDisplay);
            TabPageControl5.Location = new Point(1, 1);
            TabPageControl5.Name = "TabPageControl5";
            TabPageControl5.Size = new Size(312, 223);
            // 
            // 'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
            // 'ChartType' must be persisted ahead of any Axes change made in design time.
            // 
            ChartDataDisplay.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.Composite;
            // 
            // ChartDataDisplay
            // 
            ChartDataDisplay.Axis.BackColor = Color.FromArgb(255, 248, 220);
            PaintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            PaintElement1.Fill = Color.FromArgb(255, 248, 220);
            ChartDataDisplay.Axis.PE = PaintElement1;
            ChartDataDisplay.Axis.X.Labels.HorizontalAlign = StringAlignment.Near;
            ChartDataDisplay.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
            ChartDataDisplay.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            ChartDataDisplay.Axis.X.Labels.SeriesLabels.FormatString = "";
            ChartDataDisplay.Axis.X.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Near;
            ChartDataDisplay.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            ChartDataDisplay.Axis.X.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.X.Labels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.X.MajorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.X.MajorGridLines.Color = Color.Gainsboro;
            ChartDataDisplay.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.X.MajorGridLines.Visible = true;
            ChartDataDisplay.Axis.X.MinorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.X.MinorGridLines.Color = Color.LightGray;
            ChartDataDisplay.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.X.MinorGridLines.Visible = false;
            ChartDataDisplay.Axis.X.Visible = true;
            ChartDataDisplay.Axis.X2.Labels.HorizontalAlign = StringAlignment.Far;
            ChartDataDisplay.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            ChartDataDisplay.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            ChartDataDisplay.Axis.X2.Labels.SeriesLabels.FormatString = "";
            ChartDataDisplay.Axis.X2.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Far;
            ChartDataDisplay.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            ChartDataDisplay.Axis.X2.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.X2.Labels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.X2.MajorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.X2.MajorGridLines.Color = Color.Gainsboro;
            ChartDataDisplay.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.X2.MajorGridLines.Visible = true;
            ChartDataDisplay.Axis.X2.MinorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.X2.MinorGridLines.Color = Color.LightGray;
            ChartDataDisplay.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.X2.MinorGridLines.Visible = false;
            ChartDataDisplay.Axis.X2.Visible = false;
            ChartDataDisplay.Axis.Y.Labels.HorizontalAlign = StringAlignment.Far;
            ChartDataDisplay.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            ChartDataDisplay.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            ChartDataDisplay.Axis.Y.Labels.SeriesLabels.FormatString = "";
            ChartDataDisplay.Axis.Y.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Far;
            ChartDataDisplay.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            ChartDataDisplay.Axis.Y.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.Y.Labels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.Y.MajorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.Y.MajorGridLines.Color = Color.Gainsboro;
            ChartDataDisplay.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.Y.MajorGridLines.Visible = true;
            ChartDataDisplay.Axis.Y.MinorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.Y.MinorGridLines.Color = Color.LightGray;
            ChartDataDisplay.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.Y.MinorGridLines.Visible = false;
            ChartDataDisplay.Axis.Y.Visible = true;
            ChartDataDisplay.Axis.Y2.Labels.HorizontalAlign = StringAlignment.Near;
            ChartDataDisplay.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            ChartDataDisplay.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            ChartDataDisplay.Axis.Y2.Labels.SeriesLabels.FormatString = "";
            ChartDataDisplay.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Near;
            ChartDataDisplay.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            ChartDataDisplay.Axis.Y2.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.Y2.Labels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.Y2.MajorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.Y2.MajorGridLines.Color = Color.Gainsboro;
            ChartDataDisplay.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.Y2.MajorGridLines.Visible = true;
            ChartDataDisplay.Axis.Y2.MinorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.Y2.MinorGridLines.Color = Color.LightGray;
            ChartDataDisplay.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.Y2.MinorGridLines.Visible = false;
            ChartDataDisplay.Axis.Y2.Visible = false;
            ChartDataDisplay.Axis.Z.Labels.HorizontalAlign = StringAlignment.Near;
            ChartDataDisplay.Axis.Z.Labels.ItemFormatString = "";
            ChartDataDisplay.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            ChartDataDisplay.Axis.Z.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Near;
            ChartDataDisplay.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            ChartDataDisplay.Axis.Z.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.Z.Labels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.Z.MajorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.Z.MajorGridLines.Color = Color.Gainsboro;
            ChartDataDisplay.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.Z.MajorGridLines.Visible = true;
            ChartDataDisplay.Axis.Z.MinorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.Z.MinorGridLines.Color = Color.LightGray;
            ChartDataDisplay.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.Z.MinorGridLines.Visible = false;
            ChartDataDisplay.Axis.Z.Visible = false;
            ChartDataDisplay.Axis.Z2.Labels.HorizontalAlign = StringAlignment.Near;
            ChartDataDisplay.Axis.Z2.Labels.ItemFormatString = "";
            ChartDataDisplay.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            ChartDataDisplay.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Near;
            ChartDataDisplay.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            ChartDataDisplay.Axis.Z2.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.Z2.Labels.VerticalAlign = StringAlignment.Center;
            ChartDataDisplay.Axis.Z2.MajorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.Z2.MajorGridLines.Color = Color.Gainsboro;
            ChartDataDisplay.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.Z2.MajorGridLines.Visible = true;
            ChartDataDisplay.Axis.Z2.MinorGridLines.AlphaLevel = (byte)255;
            ChartDataDisplay.Axis.Z2.MinorGridLines.Color = Color.LightGray;
            ChartDataDisplay.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            ChartDataDisplay.Axis.Z2.MinorGridLines.Visible = false;
            ChartDataDisplay.Axis.Z2.Visible = false;
            ChartDataDisplay.BackgroundImageLayout = ImageLayout.Center;
            ChartDataDisplay.Border.Color = Color.Transparent;
            ChartDataDisplay.Border.CornerRadius = 2;
            ChartDataDisplay.ColorModel.AlphaLevel = (byte)150;
            ChartDataDisplay.Dock = DockStyle.Fill;
            ChartDataDisplay.EmptyChartText = "Intializing...";
            ChartDataDisplay.ForeColor = SystemColors.ControlText;
            ChartDataDisplay.Location = new Point(0, 0);
            ChartDataDisplay.Name = "ChartDataDisplay";
            ChartDataDisplay.Size = new Size(312, 223);
            ChartDataDisplay.TabIndex = 8;
            ChartDataDisplay.TitleTop.Extent = 30;
            ChartDataDisplay.TitleTop.HorizontalAlign = StringAlignment.Center;
            ChartDataDisplay.TitleTop.Margins.Bottom = 1;
            ChartDataDisplay.TitleTop.Margins.Top = 1;
            ChartDataDisplay.Tooltips.Font = new Font("Microsoft Sans Serif", 7.8f);
            // 
            // TabPageControl6
            // 
            TabPageControl6.Controls.Add(ButtonRestoreDefaultSettings);
            TabPageControl6.Controls.Add(GroupBoxPowerVarCalculations);
            TabPageControl6.Controls.Add(PropertyGridApplicationSettings);
            TabPageControl6.Controls.Add(ButtonGetStatus);
            TabPageControl6.Location = new Point(-10000, -10000);
            TabPageControl6.Name = "TabPageControl6";
            TabPageControl6.Size = new Size(312, 223);
            // 
            // ButtonRestoreDefaultSettings
            // 
            ButtonRestoreDefaultSettings.Location = new Point(11, 166);
            ButtonRestoreDefaultSettings.Name = "ButtonRestoreDefaultSettings";
            ButtonRestoreDefaultSettings.Size = new Size(135, 23);
            ButtonRestoreDefaultSettings.TabIndex = 2;
            ButtonRestoreDefaultSettings.Text = "Restore Defaults";
            // 
            // GroupBoxPowerVarCalculations
            // 
            GroupBoxPowerVarCalculations.Controls.Add(ComboBoxCurrentPhasors);
            GroupBoxPowerVarCalculations.Controls.Add(LabelCurrentPhasor);
            GroupBoxPowerVarCalculations.Controls.Add(ComboBoxVoltagePhasors);
            GroupBoxPowerVarCalculations.Controls.Add(LabelVoltagePhasor);
            GroupBoxPowerVarCalculations.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            GroupBoxPowerVarCalculations.Location = new Point(5, 6);
            GroupBoxPowerVarCalculations.Name = "GroupBoxPowerVarCalculations";
            GroupBoxPowerVarCalculations.Size = new Size(147, 121);
            GroupBoxPowerVarCalculations.TabIndex = 0;
            GroupBoxPowerVarCalculations.Text = "Power/Var Calculations";
            // 
            // ComboBoxCurrentPhasors
            // 
            ComboBoxCurrentPhasors.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxCurrentPhasors.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxCurrentPhasors.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxCurrentPhasors.FormattingEnabled = true;
            ComboBoxCurrentPhasors.Location = new Point(6, 84);
            ComboBoxCurrentPhasors.Name = "ComboBoxCurrentPhasors";
            ComboBoxCurrentPhasors.Size = new Size(135, 21);
            ComboBoxCurrentPhasors.TabIndex = 3;
            // 
            // LabelCurrentPhasor
            // 
            LabelCurrentPhasor.Location = new Point(6, 67);
            LabelCurrentPhasor.Name = "LabelCurrentPhasor";
            LabelCurrentPhasor.Size = new Size(131, 23);
            LabelCurrentPhasor.TabIndex = 2;
            LabelCurrentPhasor.Text = "Cu&rrent Phasor: ";
            // 
            // ComboBoxVoltagePhasors
            // 
            ComboBoxVoltagePhasors.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxVoltagePhasors.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxVoltagePhasors.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxVoltagePhasors.FormattingEnabled = true;
            ComboBoxVoltagePhasors.Location = new Point(6, 40);
            ComboBoxVoltagePhasors.Name = "ComboBoxVoltagePhasors";
            ComboBoxVoltagePhasors.Size = new Size(135, 21);
            ComboBoxVoltagePhasors.TabIndex = 1;
            // 
            // LabelVoltagePhasor
            // 
            LabelVoltagePhasor.Location = new Point(6, 23);
            LabelVoltagePhasor.Name = "LabelVoltagePhasor";
            LabelVoltagePhasor.Size = new Size(131, 23);
            LabelVoltagePhasor.TabIndex = 0;
            LabelVoltagePhasor.Text = "&Voltage Phasor: ";
            // 
            // PropertyGridApplicationSettings
            // 
            PropertyGridApplicationSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            PropertyGridApplicationSettings.Location = new Point(158, 6);
            PropertyGridApplicationSettings.Name = "PropertyGridApplicationSettings";
            PropertyGridApplicationSettings.Size = new Size(151, 214);
            PropertyGridApplicationSettings.TabIndex = 3;
            // 
            // ButtonGetStatus
            // 
            ButtonGetStatus.Location = new Point(11, 137);
            ButtonGetStatus.Name = "ButtonGetStatus";
            ButtonGetStatus.Size = new Size(135, 23);
            ButtonGetStatus.TabIndex = 1;
            ButtonGetStatus.Text = "Get Parsing Status";
            // 
            // TabPageControl7
            // 
            TabPageControl7.Controls.Add(TextBoxMessages);
            TabPageControl7.Location = new Point(-10000, -10000);
            TabPageControl7.Name = "TabPageControl7";
            TabPageControl7.Size = new Size(312, 223);
            // 
            // TextBoxMessages
            // 
            TextBoxMessages.Dock = DockStyle.Fill;
            TextBoxMessages.Font = new Font("Courier New", 9.0f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            TextBoxMessages.Location = new Point(0, 0);
            TextBoxMessages.MaxLength = 262144;
            TextBoxMessages.Multiline = true;
            TextBoxMessages.Name = "TextBoxMessages";
            TextBoxMessages.ReadOnly = true;
            TextBoxMessages.ScrollBars = ScrollBars.Both;
            TextBoxMessages.Size = new Size(312, 223);
            TextBoxMessages.TabIndex = 0;
            // 
            // TabPageControl8
            // 
            TabPageControl8.Controls.Add(TreeFrameAttributes);
            TabPageControl8.Location = new Point(-10000, -10000);
            TabPageControl8.Name = "TabPageControl8";
            TabPageControl8.Size = new Size(312, 223);
            // 
            // TreeFrameAttributes
            // 
            TreeFrameAttributes.ColumnSettings.AllowCellEdit = Infragistics.Win.UltraWinTree.AllowCellEdit.ReadOnly;
            TreeFrameAttributes.ColumnSettings.AllowSorting = Infragistics.Win.DefaultableBoolean.False;
            TreeFrameAttributes.ContextMenuStrip = ContextMenuForTree;
            TreeFrameAttributes.Dock = DockStyle.Fill;
            TreeFrameAttributes.Location = new Point(0, 0);
            TreeFrameAttributes.Name = "TreeFrameAttributes";
            TreeFrameAttributes.Size = new Size(312, 223);
            TreeFrameAttributes.TabIndex = 0;
            TreeFrameAttributes.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.FreeForm;
            // 
            // ContextMenuForTree
            // 
            ContextMenuForTree.Items.AddRange(new ToolStripItem[] { MenuItemRefresh, ToolStripSeparator5, MenuItemExpandAll, MenuItemCollapseAll });
            ContextMenuForTree.Name = "ContextMenuForTree";
            ContextMenuForTree.RenderMode = ToolStripRenderMode.System;
            ContextMenuForTree.ShowImageMargin = false;
            ContextMenuForTree.ShowItemToolTips = false;
            ContextMenuForTree.Size = new Size(112, 76);
            // 
            // MenuItemRefresh
            // 
            MenuItemRefresh.Name = "MenuItemRefresh";
            MenuItemRefresh.Size = new Size(111, 22);
            MenuItemRefresh.Text = "Refresh";
            // 
            // ToolStripSeparator5
            // 
            ToolStripSeparator5.Name = "ToolStripSeparator5";
            ToolStripSeparator5.Size = new Size(108, 6);
            // 
            // MenuItemExpandAll
            // 
            MenuItemExpandAll.Name = "MenuItemExpandAll";
            MenuItemExpandAll.Size = new Size(111, 22);
            MenuItemExpandAll.Text = "Expand All";
            // 
            // MenuItemCollapseAll
            // 
            MenuItemCollapseAll.Name = "MenuItemCollapseAll";
            MenuItemCollapseAll.Size = new Size(111, 22);
            MenuItemCollapseAll.Text = "Collapse All";
            // 
            // GroupBoxConnection
            // 
            GroupBoxConnection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Appearance33.ForeColor = Color.Black;
            GroupBoxConnection.Appearance = Appearance33;
            GroupBoxConnection.Controls.Add(GroupBoxPanelConnection);
            GroupBoxConnection.ExpandedSize = new Size(686, 142);
            Appearance58.Image = resources.GetObject("Appearance58.Image");
            GroupBoxConnection.HeaderAppearance = Appearance58;
            GroupBoxConnection.Location = new Point(7, 26);
            GroupBoxConnection.Name = "GroupBoxConnection";
            GroupBoxConnection.Size = new Size(686, 142);
            GroupBoxConnection.TabIndex = 1;
            GroupBoxConnection.Text = "Connection Parameters";
            // 
            // GroupBoxPanelConnection
            // 
            GroupBoxPanelConnection.Controls.Add(TabControlProtocolParameters);
            GroupBoxPanelConnection.Controls.Add(TabControlCommunications);
            GroupBoxPanelConnection.Dock = DockStyle.Fill;
            GroupBoxPanelConnection.Location = new Point(3, 19);
            GroupBoxPanelConnection.Name = "GroupBoxPanelConnection";
            GroupBoxPanelConnection.Size = new Size(680, 120);
            GroupBoxPanelConnection.TabIndex = 0;
            // 
            // TabControlProtocolParameters
            // 
            TabControlProtocolParameters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Appearance34.ForeColor = Color.Black;
            TabControlProtocolParameters.Appearance = Appearance34;
            TabControlProtocolParameters.Controls.Add(TabSharedControlsPageProtocolParameters);
            TabControlProtocolParameters.Controls.Add(TabPageControl9);
            TabControlProtocolParameters.Controls.Add(TabPageControl10);
            TabControlProtocolParameters.Location = new Point(315, 0);
            TabControlProtocolParameters.Name = "TabControlProtocolParameters";
            TabControlProtocolParameters.SharedControlsPage = TabSharedControlsPageProtocolParameters;
            TabControlProtocolParameters.Size = new Size(362, 117);
            TabControlProtocolParameters.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            TabControlProtocolParameters.TabIndex = 12;
            UltraTab5.TabPage = TabPageControl9;
            UltraTab5.Text = "&Protocol";
            UltraTab6.TabPage = TabPageControl10;
            UltraTab6.Text = "E&xtra Parameters";
            TabControlProtocolParameters.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] { UltraTab5, UltraTab6 });
            UltraToolTipInfo2.Enabled = Infragistics.Win.DefaultableBoolean.False;
            UltraToolTipInfo2.ToolTipText = "This protocol has additional connection parameters.  Click here to view.";
            ToolTipManagerForExtraParameters.SetUltraToolTip(TabControlProtocolParameters, UltraToolTipInfo2);
            // 
            // TabSharedControlsPageProtocolParameters
            // 
            TabSharedControlsPageProtocolParameters.Location = new Point(-10000, -10000);
            TabSharedControlsPageProtocolParameters.Name = "TabSharedControlsPageProtocolParameters";
            TabSharedControlsPageProtocolParameters.Size = new Size(360, 96);
            // 
            // TabControlCommunications
            // 
            Appearance35.ForeColor = Color.Black;
            TabControlCommunications.Appearance = Appearance35;
            TabControlCommunications.Controls.Add(TabSharedControlsPageCommunications);
            TabControlCommunications.Controls.Add(TabPageControl1);
            TabControlCommunications.Controls.Add(TabPageControl2);
            TabControlCommunications.Controls.Add(TabPageControl3);
            TabControlCommunications.Controls.Add(TabPageControl4);
            TabControlCommunications.Location = new Point(3, 0);
            TabControlCommunications.Name = "TabControlCommunications";
            TabControlCommunications.SharedControlsPage = TabSharedControlsPageCommunications;
            TabControlCommunications.Size = new Size(306, 117);
            TabControlCommunications.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            TabControlCommunications.TabIndex = 0;
            TabControlCommunications.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.SingleRowFixed;
            TabControlCommunications.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.TopLeft;
            UltraTab7.TabPage = TabPageControl1;
            UltraTab7.Text = "&Tcp";
            UltraTab8.TabPage = TabPageControl2;
            UltraTab8.Text = "&Udp";
            UltraTab9.TabPage = TabPageControl3;
            UltraTab9.Text = "&Serial";
            UltraTab10.TabPage = TabPageControl4;
            UltraTab10.Text = "Fi&le";
            TabControlCommunications.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] { UltraTab7, UltraTab8, UltraTab9, UltraTab10 });
            // 
            // TabSharedControlsPageCommunications
            // 
            TabSharedControlsPageCommunications.Location = new Point(-10000, -10000);
            TabSharedControlsPageCommunications.Name = "TabSharedControlsPageCommunications";
            TabSharedControlsPageCommunications.Size = new Size(304, 96);
            // 
            // GroupBoxStatus
            // 
            GroupBoxStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBoxStatus.Controls.Add(GroupBoxPanelStatus);
            GroupBoxStatus.ExpandedSize = new Size(511, 85);
            Appearance19.Image = resources.GetObject("Appearance19.Image");
            GroupBoxStatus.HeaderAppearance = Appearance19;
            GroupBoxStatus.Location = new Point(10, 421);
            GroupBoxStatus.Name = "GroupBoxStatus";
            GroupBoxStatus.Size = new Size(683, 151);
            GroupBoxStatus.TabIndex = 6;
            GroupBoxStatus.Text = "Real-time Frame Detail";
            UltraToolTipInfo1.Enabled = Infragistics.Win.DefaultableBoolean.True;
            UltraToolTipInfo1.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            UltraToolTipInfo1.ToolTipText = "Keeping this data window displayed may cause the graph to fall behind real-time";
            UltraToolTipInfo1.ToolTipTitle = "Note";
            ToolTipManager.SetUltraToolTip(GroupBoxStatus, UltraToolTipInfo1);
            // 
            // GroupBoxPanelStatus
            // 
            GroupBoxPanelStatus.Controls.Add(LabelBinaryFrameImage);
            GroupBoxPanelStatus.Controls.Add(LabelDisplayFormat);
            GroupBoxPanelStatus.Controls.Add(ComboBoxByteEncodingDisplayFormats);
            GroupBoxPanelStatus.Controls.Add(LabelFrameType);
            GroupBoxPanelStatus.Controls.Add(LabelFrameTypeLabel);
            GroupBoxPanelStatus.Controls.Add(LabelFrequency);
            GroupBoxPanelStatus.Controls.Add(LabelFrequencyLabel);
            GroupBoxPanelStatus.Controls.Add(LabelMagnitude);
            GroupBoxPanelStatus.Controls.Add(LabelMagnitudeLabel);
            GroupBoxPanelStatus.Controls.Add(LabelAngle);
            GroupBoxPanelStatus.Controls.Add(LabelAngleLabel);
            GroupBoxPanelStatus.Controls.Add(LabelTime);
            GroupBoxPanelStatus.Controls.Add(LabelTimeLabel);
            GroupBoxPanelStatus.Dock = DockStyle.Fill;
            GroupBoxPanelStatus.Location = new Point(3, 19);
            GroupBoxPanelStatus.Name = "GroupBoxPanelStatus";
            GroupBoxPanelStatus.Size = new Size(677, 129);
            GroupBoxPanelStatus.TabIndex = 0;
            // 
            // LabelBinaryFrameImage
            // 
            LabelBinaryFrameImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Appearance12.TextHAlignAsString = "Left";
            LabelBinaryFrameImage.Appearance = Appearance12;
            LabelBinaryFrameImage.Font = new Font("Courier New", 9.75f, FontStyle.Bold);
            LabelBinaryFrameImage.Location = new Point(206, 0);
            LabelBinaryFrameImage.Name = "LabelBinaryFrameImage";
            LabelBinaryFrameImage.Size = new Size(468, 121);
            LabelBinaryFrameImage.TabIndex = 12;
            LabelBinaryFrameImage.Text = "Binary Frame Image";
            // 
            // LabelDisplayFormat
            // 
            Appearance13.TextHAlignAsString = "Right";
            LabelDisplayFormat.Appearance = Appearance13;
            LabelDisplayFormat.Location = new Point(-8, 105);
            LabelDisplayFormat.Name = "LabelDisplayFormat";
            LabelDisplayFormat.Size = new Size(72, 20);
            LabelDisplayFormat.TabIndex = 10;
            LabelDisplayFormat.Text = "Disp&lay:";
            // 
            // ComboBoxByteEncodingDisplayFormats
            // 
            ComboBoxByteEncodingDisplayFormats.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxByteEncodingDisplayFormats.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxByteEncodingDisplayFormats.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxByteEncodingDisplayFormats.Items.AddRange(new object[] { "Hexadecimal", "Decimal", "Big Endian Binary", "Little Endian Binary", "Base64", "ASCII Extraction" });
            ComboBoxByteEncodingDisplayFormats.Location = new Point(70, 104);
            ComboBoxByteEncodingDisplayFormats.Name = "ComboBoxByteEncodingDisplayFormats";
            ComboBoxByteEncodingDisplayFormats.Size = new Size(130, 21);
            ComboBoxByteEncodingDisplayFormats.TabIndex = 11;
            // 
            // LabelFrameType
            // 
            LabelFrameType.Location = new Point(70, 3);
            LabelFrameType.Name = "LabelFrameType";
            LabelFrameType.Size = new Size(150, 20);
            LabelFrameType.TabIndex = 1;
            LabelFrameType.Text = "undetermined";
            LabelFrameType.WrapText = false;
            // 
            // LabelFrameTypeLabel
            // 
            Appearance14.TextHAlignAsString = "Right";
            LabelFrameTypeLabel.Appearance = Appearance14;
            LabelFrameTypeLabel.Location = new Point(-8, 3);
            LabelFrameTypeLabel.Name = "LabelFrameTypeLabel";
            LabelFrameTypeLabel.Size = new Size(72, 20);
            LabelFrameTypeLabel.TabIndex = 0;
            LabelFrameTypeLabel.Text = "Frame Type:";
            // 
            // LabelFrequency
            // 
            LabelFrequency.Location = new Point(70, 44);
            LabelFrequency.Name = "LabelFrequency";
            LabelFrequency.Size = new Size(150, 20);
            LabelFrequency.TabIndex = 5;
            LabelFrequency.Text = "0.0000 Hz";
            LabelFrequency.WrapText = false;
            // 
            // LabelFrequencyLabel
            // 
            Appearance15.TextHAlignAsString = "Right";
            LabelFrequencyLabel.Appearance = Appearance15;
            LabelFrequencyLabel.Location = new Point(-8, 44);
            LabelFrequencyLabel.Name = "LabelFrequencyLabel";
            LabelFrequencyLabel.Size = new Size(72, 20);
            LabelFrequencyLabel.TabIndex = 4;
            LabelFrequencyLabel.Text = "Frequency:";
            // 
            // LabelMagnitude
            // 
            LabelMagnitude.Location = new Point(70, 84);
            LabelMagnitude.Name = "LabelMagnitude";
            LabelMagnitude.Size = new Size(150, 20);
            LabelMagnitude.TabIndex = 9;
            LabelMagnitude.Text = "0.0000 kV";
            LabelMagnitude.WrapText = false;
            // 
            // LabelMagnitudeLabel
            // 
            Appearance16.TextHAlignAsString = "Right";
            LabelMagnitudeLabel.Appearance = Appearance16;
            LabelMagnitudeLabel.Location = new Point(-8, 84);
            LabelMagnitudeLabel.Name = "LabelMagnitudeLabel";
            LabelMagnitudeLabel.Size = new Size(72, 20);
            LabelMagnitudeLabel.TabIndex = 8;
            LabelMagnitudeLabel.Text = "Magnitude:";
            // 
            // LabelAngle
            // 
            LabelAngle.Location = new Point(70, 64);
            LabelAngle.Name = "LabelAngle";
            LabelAngle.Size = new Size(150, 20);
            LabelAngle.TabIndex = 7;
            LabelAngle.Text = "0.0000°";
            LabelAngle.WrapText = false;
            // 
            // LabelAngleLabel
            // 
            Appearance17.TextHAlignAsString = "Right";
            LabelAngleLabel.Appearance = Appearance17;
            LabelAngleLabel.Location = new Point(-8, 64);
            LabelAngleLabel.Name = "LabelAngleLabel";
            LabelAngleLabel.Size = new Size(72, 20);
            LabelAngleLabel.TabIndex = 6;
            LabelAngleLabel.Text = "Angle:";
            // 
            // LabelTime
            // 
            LabelTime.Location = new Point(70, 24);
            LabelTime.Name = "LabelTime";
            LabelTime.Size = new Size(150, 20);
            LabelTime.TabIndex = 3;
            LabelTime.Text = "undetermined";
            LabelTime.WrapText = false;
            // 
            // LabelTimeLabel
            // 
            Appearance18.TextHAlignAsString = "Right";
            LabelTimeLabel.Appearance = Appearance18;
            LabelTimeLabel.Location = new Point(-8, 24);
            LabelTimeLabel.Name = "LabelTimeLabel";
            LabelTimeLabel.Size = new Size(72, 20);
            LabelTimeLabel.TabIndex = 2;
            LabelTimeLabel.Text = "Time:";
            // 
            // StatusBar
            // 
            StatusBar.InterPanelSpacing = 1;
            StatusBar.Location = new Point(0, 578);
            StatusBar.Name = "StatusBar";
            UltraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel1.Text = "Total frames:";
            UltraStatusPanel1.WrapText = Infragistics.Win.DefaultableBoolean.False;
            Appearance29.TextHAlignAsString = "Left";
            UltraStatusPanel2.Appearance = Appearance29;
            UltraStatusPanel2.MinWidth = 0;
            UltraStatusPanel2.Padding = new Size(2, 0);
            UltraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel2.Text = "0";
            UltraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel3.Text = "Frames/sec:";
            UltraStatusPanel3.WrapText = Infragistics.Win.DefaultableBoolean.False;
            Appearance30.TextHAlignAsString = "Left";
            UltraStatusPanel4.Appearance = Appearance30;
            UltraStatusPanel4.MinWidth = 0;
            UltraStatusPanel4.Padding = new Size(2, 0);
            UltraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel4.Text = "0.0000";
            UltraStatusPanel4.WrapText = Infragistics.Win.DefaultableBoolean.False;
            UltraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel5.Text = "Total bytes:";
            UltraStatusPanel5.WrapText = Infragistics.Win.DefaultableBoolean.False;
            Appearance31.TextHAlignAsString = "Left";
            UltraStatusPanel6.Appearance = Appearance31;
            UltraStatusPanel6.MinWidth = 0;
            UltraStatusPanel6.Padding = new Size(2, 0);
            UltraStatusPanel6.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel6.Text = "0";
            UltraStatusPanel6.WrapText = Infragistics.Win.DefaultableBoolean.False;
            UltraStatusPanel7.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel7.Text = "Bit rate (mbps):";
            UltraStatusPanel8.MinWidth = 0;
            UltraStatusPanel8.Padding = new Size(2, 0);
            UltraStatusPanel8.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel8.Text = "0.0000";
            UltraStatusPanel9.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel9.Text = "Queued buffers:";
            Appearance32.TextHAlignAsString = "Left";
            UltraStatusPanel10.Appearance = Appearance32;
            UltraStatusPanel10.MinWidth = 0;
            UltraStatusPanel10.Padding = new Size(2, 0);
            UltraStatusPanel10.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            UltraStatusPanel10.Text = "0";
            StatusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[] { UltraStatusPanel1, UltraStatusPanel2, UltraStatusPanel3, UltraStatusPanel4, UltraStatusPanel5, UltraStatusPanel6, UltraStatusPanel7, UltraStatusPanel8, UltraStatusPanel9, UltraStatusPanel10 });
            StatusBar.Size = new Size(702, 23);
            StatusBar.TabIndex = 7;
            // 
            // ToolTipManager
            // 
            ToolTipManager.ContainingControl = this;
            ToolTipManager.InitialDelay = 150;
            // 
            // LabelVarsLabel
            // 
            Appearance59.FontData.UnderlineAsString = "False";
            Appearance59.ForeColor = SystemColors.HotTrack;
            Appearance59.TextHAlignAsString = "Right";
            LabelVarsLabel.Appearance = Appearance59;
            LabelVarsLabel.Cursor = Cursors.Hand;
            Appearance60.FontData.UnderlineAsString = "True";
            LabelVarsLabel.HotTrackAppearance = Appearance60;
            LabelVarsLabel.Location = new Point(9, 36);
            LabelVarsLabel.Name = "LabelVarsLabel";
            LabelVarsLabel.Size = new Size(50, 16);
            LabelVarsLabel.TabIndex = 16;
            LabelVarsLabel.Text = "Vars:";
            UltraToolTipInfo7.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            UltraToolTipInfo7.ToolTipText = "Click link to change source voltage and current phasors for power / vars calculat" + "ions...";
            UltraToolTipInfo7.ToolTipTitle = "Note";
            ToolTipManager.SetUltraToolTip(LabelVarsLabel, UltraToolTipInfo7);
            LabelVarsLabel.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 
            // LabelPowerLabel
            // 
            Appearance61.FontData.UnderlineAsString = "False";
            Appearance61.ForeColor = SystemColors.HotTrack;
            Appearance61.TextHAlignAsString = "Right";
            LabelPowerLabel.Appearance = Appearance61;
            LabelPowerLabel.Cursor = Cursors.Hand;
            Appearance62.FontData.UnderlineAsString = "True";
            LabelPowerLabel.HotTrackAppearance = Appearance62;
            LabelPowerLabel.Location = new Point(9, 14);
            LabelPowerLabel.Name = "LabelPowerLabel";
            LabelPowerLabel.Size = new Size(50, 20);
            LabelPowerLabel.TabIndex = 14;
            LabelPowerLabel.Text = "Power:";
            UltraToolTipInfo8.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            UltraToolTipInfo8.ToolTipText = "Click link to change source voltage and current phasors for power / vars calculat" + "ions...";
            UltraToolTipInfo8.ToolTipTitle = "Note";
            ToolTipManager.SetUltraToolTip(LabelPowerLabel, UltraToolTipInfo8);
            LabelPowerLabel.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 
            // OpenFileDialog
            // 
            OpenFileDialog.RestoreDirectory = true;
            // 
            // GroupBoxHeaderFrame
            // 
            GroupBoxHeaderFrame.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            GroupBoxHeaderFrame.Controls.Add(GroupBoxPanelHeaderFrame);
            GroupBoxHeaderFrame.ExpandedSize = new Size(165, 209);
            GroupBoxHeaderFrame.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            Appearance28.Image = resources.GetObject("Appearance28.Image");
            GroupBoxHeaderFrame.HeaderAppearance = Appearance28;
            GroupBoxHeaderFrame.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.RightOnBorder;
            GroupBoxHeaderFrame.Location = new Point(522, 171);
            GroupBoxHeaderFrame.Name = "GroupBoxHeaderFrame";
            GroupBoxHeaderFrame.Size = new Size(179, 244);
            GroupBoxHeaderFrame.TabIndex = 5;
            GroupBoxHeaderFrame.Text = "Header Frame";
            GroupBoxHeaderFrame.VerticalTextOrientation = Infragistics.Win.Misc.GroupBoxVerticalTextOrientation.TopToBottom;
            // 
            // GroupBoxPanelHeaderFrame
            // 
            GroupBoxPanelHeaderFrame.Controls.Add(TextBoxHeaderFrame);
            GroupBoxPanelHeaderFrame.Dock = DockStyle.Fill;
            GroupBoxPanelHeaderFrame.Location = new Point(3, 3);
            GroupBoxPanelHeaderFrame.Name = "GroupBoxPanelHeaderFrame";
            GroupBoxPanelHeaderFrame.Size = new Size(157, 238);
            GroupBoxPanelHeaderFrame.TabIndex = 0;
            // 
            // TextBoxHeaderFrame
            // 
            TextBoxHeaderFrame.AcceptsReturn = true;
            TextBoxHeaderFrame.AlwaysInEditMode = true;
            TextBoxHeaderFrame.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TextBoxHeaderFrame.Location = new Point(3, 3);
            TextBoxHeaderFrame.Multiline = true;
            TextBoxHeaderFrame.Name = "TextBoxHeaderFrame";
            TextBoxHeaderFrame.ReadOnly = true;
            TextBoxHeaderFrame.Scrollbars = ScrollBars.Vertical;
            TextBoxHeaderFrame.Size = new Size(154, 232);
            TextBoxHeaderFrame.TabIndex = 0;
            TextBoxHeaderFrame.Text = "Header Frame";
            // 
            // GroupBoxConfigurationFrame
            // 
            GroupBoxConfigurationFrame.Controls.Add(GroupBoxPanelConfigurationFrame);
            GroupBoxConfigurationFrame.ExpandedSize = new Size(187, 166);
            Appearance27.Image = resources.GetObject("Appearance27.Image");
            GroupBoxConfigurationFrame.HeaderAppearance = Appearance27;
            GroupBoxConfigurationFrame.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            GroupBoxConfigurationFrame.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.RightOnBorder;
            GroupBoxConfigurationFrame.Location = new Point(10, 174);
            GroupBoxConfigurationFrame.Name = "GroupBoxConfigurationFrame";
            GroupBoxConfigurationFrame.Size = new Size(187, 166);
            GroupBoxConfigurationFrame.TabIndex = 2;
            GroupBoxConfigurationFrame.Text = "Configuration Frame";
            GroupBoxConfigurationFrame.VerticalTextOrientation = Infragistics.Win.Misc.GroupBoxVerticalTextOrientation.TopToBottom;
            // 
            // GroupBoxPanelConfigurationFrame
            // 
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelPhasorCount);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelPhasorCountLabel);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelHz);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelNominalFrequency);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelNominalFrequencyLabel);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelDigitalCount);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelDigitalCountLabel);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelAnalogCount);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelAnalogCountLabel);
            GroupBoxPanelConfigurationFrame.Controls.Add(ComboBoxPmus);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelIDCode);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelIDCodeLabel);
            GroupBoxPanelConfigurationFrame.Controls.Add(ComboBoxPhasors);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelPmuList);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelSelectedIsRefAngle);
            GroupBoxPanelConfigurationFrame.Controls.Add(LabelPhasor);
            GroupBoxPanelConfigurationFrame.Dock = DockStyle.Fill;
            GroupBoxPanelConfigurationFrame.Location = new Point(3, 3);
            GroupBoxPanelConfigurationFrame.Name = "GroupBoxPanelConfigurationFrame";
            GroupBoxPanelConfigurationFrame.Size = new Size(165, 160);
            GroupBoxPanelConfigurationFrame.TabIndex = 0;
            // 
            // LabelPhasorCount
            // 
            LabelPhasorCount.Location = new Point(55, 93);
            LabelPhasorCount.Name = "LabelPhasorCount";
            LabelPhasorCount.Size = new Size(40, 20);
            LabelPhasorCount.TabIndex = 5;
            LabelPhasorCount.Text = "0";
            // 
            // LabelPhasorCountLabel
            // 
            Appearance20.TextHAlignAsString = "Right";
            LabelPhasorCountLabel.Appearance = Appearance20;
            LabelPhasorCountLabel.Location = new Point(3, 93);
            LabelPhasorCountLabel.Name = "LabelPhasorCountLabel";
            LabelPhasorCountLabel.Size = new Size(50, 20);
            LabelPhasorCountLabel.TabIndex = 4;
            LabelPhasorCountLabel.Text = "Phasors:";
            // 
            // LabelHz
            // 
            Appearance21.TextHAlignAsString = "Left";
            LabelHz.Appearance = Appearance21;
            LabelHz.Location = new Point(128, 124);
            LabelHz.Name = "LabelHz";
            LabelHz.Size = new Size(18, 16);
            LabelHz.TabIndex = 16;
            LabelHz.Text = "Hz";
            // 
            // LabelNominalFrequency
            // 
            Appearance22.TextHAlignAsString = "Center";
            LabelNominalFrequency.Appearance = Appearance22;
            LabelNominalFrequency.Location = new Point(113, 124);
            LabelNominalFrequency.Name = "LabelNominalFrequency";
            LabelNominalFrequency.Size = new Size(18, 16);
            LabelNominalFrequency.TabIndex = 15;
            LabelNominalFrequency.Text = "0";
            // 
            // LabelNominalFrequencyLabel
            // 
            Appearance23.TextHAlignAsString = "Center";
            LabelNominalFrequencyLabel.Appearance = Appearance23;
            LabelNominalFrequencyLabel.Location = new Point(99, 93);
            LabelNominalFrequencyLabel.Name = "LabelNominalFrequencyLabel";
            LabelNominalFrequencyLabel.Size = new Size(62, 32);
            LabelNominalFrequencyLabel.TabIndex = 14;
            LabelNominalFrequencyLabel.Text = "Nominal Frequency:";
            // 
            // LabelDigitalCount
            // 
            LabelDigitalCount.Location = new Point(55, 137);
            LabelDigitalCount.Name = "LabelDigitalCount";
            LabelDigitalCount.Size = new Size(40, 20);
            LabelDigitalCount.TabIndex = 9;
            LabelDigitalCount.Text = "0";
            // 
            // LabelDigitalCountLabel
            // 
            Appearance24.TextHAlignAsString = "Right";
            LabelDigitalCountLabel.Appearance = Appearance24;
            LabelDigitalCountLabel.Location = new Point(0, 137);
            LabelDigitalCountLabel.Name = "LabelDigitalCountLabel";
            LabelDigitalCountLabel.Size = new Size(50, 20);
            LabelDigitalCountLabel.TabIndex = 8;
            LabelDigitalCountLabel.Text = "Digitals:";
            // 
            // LabelAnalogCount
            // 
            LabelAnalogCount.Location = new Point(55, 115);
            LabelAnalogCount.Name = "LabelAnalogCount";
            LabelAnalogCount.Size = new Size(40, 20);
            LabelAnalogCount.TabIndex = 7;
            LabelAnalogCount.Text = "0";
            // 
            // LabelAnalogCountLabel
            // 
            Appearance25.TextHAlignAsString = "Right";
            LabelAnalogCountLabel.Appearance = Appearance25;
            LabelAnalogCountLabel.Location = new Point(3, 115);
            LabelAnalogCountLabel.Name = "LabelAnalogCountLabel";
            LabelAnalogCountLabel.Size = new Size(50, 20);
            LabelAnalogCountLabel.TabIndex = 6;
            LabelAnalogCountLabel.Text = "Analogs:";
            // 
            // ComboBoxPmus
            // 
            ComboBoxPmus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxPmus.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxPmus.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxPmus.Location = new Point(3, 23);
            ComboBoxPmus.Name = "ComboBoxPmus";
            ComboBoxPmus.Size = new Size(158, 21);
            ComboBoxPmus.TabIndex = 1;
            // 
            // LabelIDCode
            // 
            LabelIDCode.Location = new Point(103, 8);
            LabelIDCode.Name = "LabelIDCode";
            LabelIDCode.Size = new Size(40, 20);
            LabelIDCode.TabIndex = 19;
            LabelIDCode.Text = "0";
            // 
            // LabelIDCodeLabel
            // 
            Appearance26.TextHAlignAsString = "Right";
            LabelIDCodeLabel.Appearance = Appearance26;
            LabelIDCodeLabel.Location = new Point(48, 8);
            LabelIDCodeLabel.Name = "LabelIDCodeLabel";
            LabelIDCodeLabel.Size = new Size(49, 23);
            LabelIDCodeLabel.TabIndex = 18;
            LabelIDCodeLabel.Text = "ID Code:";
            // 
            // ComboBoxPhasors
            // 
            ComboBoxPhasors.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxPhasors.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxPhasors.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxPhasors.Location = new Point(3, 66);
            ComboBoxPhasors.Name = "ComboBoxPhasors";
            ComboBoxPhasors.Size = new Size(158, 21);
            ComboBoxPhasors.TabIndex = 3;
            // 
            // LabelPmuList
            // 
            LabelPmuList.Location = new Point(3, 8);
            LabelPmuList.Name = "LabelPmuList";
            LabelPmuList.Size = new Size(36, 23);
            LabelPmuList.TabIndex = 0;
            LabelPmuList.Text = "PM&U:";
            // 
            // LabelSelectedIsRefAngle
            // 
            LabelSelectedIsRefAngle.Font = new Font("Microsoft Sans Serif", 6.75f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            LabelSelectedIsRefAngle.Location = new Point(43, 52);
            LabelSelectedIsRefAngle.Name = "LabelSelectedIsRefAngle";
            LabelSelectedIsRefAngle.Size = new Size(135, 23);
            LabelSelectedIsRefAngle.TabIndex = 17;
            LabelSelectedIsRefAngle.Text = "(Selected is reference angle)";
            // 
            // LabelPhasor
            // 
            LabelPhasor.Location = new Point(3, 51);
            LabelPhasor.Name = "LabelPhasor";
            LabelPhasor.Size = new Size(47, 23);
            LabelPhasor.TabIndex = 2;
            LabelPhasor.Text = "P&hasor: ";
            // 
            // MenuStripMain
            // 
            MenuStripMain.BackColor = SystemColors.ButtonFace;
            MenuStripMain.Items.AddRange(new ToolStripItem[] { MenuItemFile, MenuItemHelp });
            MenuStripMain.Location = new Point(0, 0);
            MenuStripMain.Name = "MenuStripMain";
            MenuStripMain.RenderMode = ToolStripRenderMode.Professional;
            MenuStripMain.Size = new Size(702, 24);
            MenuStripMain.TabIndex = 0;
            // 
            // MenuItemFile
            // 
            MenuItemFile.DropDownItems.AddRange(new ToolStripItem[] { MenuItemConnection, MenuItemConfigFile, MenuItemCapture, ToolStripSeparator1, MenuItemExit });
            MenuItemFile.Name = "MenuItemFile";
            MenuItemFile.Size = new Size(37, 20);
            MenuItemFile.Text = "&File";
            // 
            // MenuItemConnection
            // 
            MenuItemConnection.DropDownItems.AddRange(new ToolStripItem[] { MenuItemLoadConnection, MenuItemSaveConnection });
            MenuItemConnection.Image = (Image)resources.GetObject("MenuItemConnection.Image");
            MenuItemConnection.Name = "MenuItemConnection";
            MenuItemConnection.Size = new Size(136, 22);
            MenuItemConnection.Text = "&Connection";
            // 
            // MenuItemLoadConnection
            // 
            MenuItemLoadConnection.Name = "MenuItemLoadConnection";
            MenuItemLoadConnection.Size = new Size(109, 22);
            MenuItemLoadConnection.Text = "&Load...";
            // 
            // MenuItemSaveConnection
            // 
            MenuItemSaveConnection.Name = "MenuItemSaveConnection";
            MenuItemSaveConnection.Size = new Size(109, 22);
            MenuItemSaveConnection.Text = "&Save...";
            // 
            // MenuItemConfigFile
            // 
            MenuItemConfigFile.DropDownItems.AddRange(new ToolStripItem[] { MenuItemLoadConfigFile, MenuItemSaveConfigFile });
            MenuItemConfigFile.Image = (Image)resources.GetObject("MenuItemConfigFile.Image");
            MenuItemConfigFile.Name = "MenuItemConfigFile";
            MenuItemConfigFile.Size = new Size(136, 22);
            MenuItemConfigFile.Text = "C&onfig File";
            // 
            // MenuItemLoadConfigFile
            // 
            MenuItemLoadConfigFile.Name = "MenuItemLoadConfigFile";
            MenuItemLoadConfigFile.Size = new Size(109, 22);
            MenuItemLoadConfigFile.Text = "&Load...";
            // 
            // MenuItemSaveConfigFile
            // 
            MenuItemSaveConfigFile.Enabled = false;
            MenuItemSaveConfigFile.Name = "MenuItemSaveConfigFile";
            MenuItemSaveConfigFile.Size = new Size(109, 22);
            MenuItemSaveConfigFile.Text = "&Save...";
            // 
            // MenuItemCapture
            // 
            MenuItemCapture.DropDownItems.AddRange(new ToolStripItem[] { MenuItemStartCapture, MenuItemStopCapture, ToolStripSeparator3, MenuItemStartStreamDebugCapture, MenuItemStopStreamDebugCapture, ToolStripSeparator6, MenuItemCaptureSampleFrames, MenuItemCancelSampleFrameCapture });
            MenuItemCapture.Image = (Image)resources.GetObject("MenuItemCapture.Image");
            MenuItemCapture.Name = "MenuItemCapture";
            MenuItemCapture.Size = new Size(136, 22);
            MenuItemCapture.Text = "C&apture";
            // 
            // MenuItemStartCapture
            // 
            MenuItemStartCapture.Name = "MenuItemStartCapture";
            MenuItemStartCapture.Size = new Size(233, 22);
            MenuItemStartCapture.Text = "S&tart Capture...";
            // 
            // MenuItemStopCapture
            // 
            MenuItemStopCapture.Enabled = false;
            MenuItemStopCapture.Name = "MenuItemStopCapture";
            MenuItemStopCapture.Size = new Size(233, 22);
            MenuItemStopCapture.Text = "Sto&p Capture";
            // 
            // ToolStripSeparator3
            // 
            ToolStripSeparator3.Name = "ToolStripSeparator3";
            ToolStripSeparator3.Size = new Size(230, 6);
            // 
            // MenuItemStartStreamDebugCapture
            // 
            MenuItemStartStreamDebugCapture.Name = "MenuItemStartStreamDebugCapture";
            MenuItemStartStreamDebugCapture.Size = new Size(233, 22);
            MenuItemStartStreamDebugCapture.Text = "Start Stream &Debug Capture...";
            // 
            // MenuItemStopStreamDebugCapture
            // 
            MenuItemStopStreamDebugCapture.Enabled = false;
            MenuItemStopStreamDebugCapture.Name = "MenuItemStopStreamDebugCapture";
            MenuItemStopStreamDebugCapture.Size = new Size(233, 22);
            MenuItemStopStreamDebugCapture.Text = "Stop Stream D&ebug Capture";
            // 
            // ToolStripSeparator6
            // 
            ToolStripSeparator6.Name = "ToolStripSeparator6";
            ToolStripSeparator6.Size = new Size(230, 6);
            // 
            // MenuItemCaptureSampleFrames
            // 
            MenuItemCaptureSampleFrames.Name = "MenuItemCaptureSampleFrames";
            MenuItemCaptureSampleFrames.Size = new Size(233, 22);
            MenuItemCaptureSampleFrames.Text = "&Capture Sample Frames...";
            // 
            // MenuItemCancelSampleFrameCapture
            // 
            MenuItemCancelSampleFrameCapture.Enabled = false;
            MenuItemCancelSampleFrameCapture.Name = "MenuItemCancelSampleFrameCapture";
            MenuItemCancelSampleFrameCapture.Size = new Size(233, 22);
            MenuItemCancelSampleFrameCapture.Text = "C&ancel Sample Frame Capture";
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(133, 6);
            // 
            // MenuItemExit
            // 
            MenuItemExit.Name = "MenuItemExit";
            MenuItemExit.Size = new Size(136, 22);
            MenuItemExit.Text = "E&xit";
            // 
            // MenuItemHelp
            // 
            MenuItemHelp.DropDownItems.AddRange(new ToolStripItem[] { MenuItemLocalHelp, MenuItemOnlineHelp, ToolStripSeparator2, MenuItemAbout });
            MenuItemHelp.Name = "MenuItemHelp";
            MenuItemHelp.Size = new Size(44, 20);
            MenuItemHelp.Text = "&Help";
            // 
            // MenuItemLocalHelp
            // 
            MenuItemLocalHelp.Image = (Image)resources.GetObject("MenuItemLocalHelp.Image");
            MenuItemLocalHelp.Name = "MenuItemLocalHelp";
            MenuItemLocalHelp.Size = new Size(146, 22);
            MenuItemLocalHelp.Text = "&Local Help...";
            // 
            // MenuItemOnlineHelp
            // 
            MenuItemOnlineHelp.Image = (Image)resources.GetObject("MenuItemOnlineHelp.Image");
            MenuItemOnlineHelp.Name = "MenuItemOnlineHelp";
            MenuItemOnlineHelp.Size = new Size(146, 22);
            MenuItemOnlineHelp.Text = "&Online Help...";
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(143, 6);
            // 
            // MenuItemAbout
            // 
            MenuItemAbout.Name = "MenuItemAbout";
            MenuItemAbout.Size = new Size(146, 22);
            MenuItemAbout.Text = "&About...";
            // 
            // TabSharedControlsPageChart
            // 
            TabSharedControlsPageChart.Location = new Point(-10000, -10000);
            TabSharedControlsPageChart.Name = "TabSharedControlsPageChart";
            TabSharedControlsPageChart.Size = new Size(312, 223);
            // 
            // TabControlChart
            // 
            TabControlChart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Appearance7.ForeColor = Color.Black;
            TabControlChart.Appearance = Appearance7;
            TabControlChart.Controls.Add(TabSharedControlsPageChart);
            TabControlChart.Controls.Add(TabPageControl5);
            TabControlChart.Controls.Add(TabPageControl6);
            TabControlChart.Controls.Add(TabPageControl7);
            TabControlChart.Controls.Add(TabPageControl8);
            TabControlChart.Location = new Point(202, 171);
            TabControlChart.Name = "TabControlChart";
            TabControlChart.SharedControlsPage = TabSharedControlsPageChart;
            TabControlChart.Size = new Size(314, 244);
            TabControlChart.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            TabControlChart.TabIndex = 4;
            TabControlChart.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.SingleRowFixed;
            TabControlChart.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft;
            Appearance8.Image = resources.GetObject("Appearance8.Image");
            UltraTab1.Appearance = Appearance8;
            UltraTab1.TabPage = TabPageControl5;
            UltraTab1.Text = "&Graph";
            Appearance9.Image = resources.GetObject("Appearance9.Image");
            UltraTab2.Appearance = Appearance9;
            UltraTab2.TabPage = TabPageControl6;
            UltraTab2.Text = "S&ettings";
            Appearance10.Image = resources.GetObject("Appearance10.Image");
            UltraTab3.Appearance = Appearance10;
            UltraTab3.TabPage = TabPageControl7;
            UltraTab3.Text = "&Messages";
            Appearance11.Image = resources.GetObject("Appearance11.Image");
            UltraTab4.Appearance = Appearance11;
            UltraTab4.TabPage = TabPageControl8;
            UltraTab4.Text = "P&rotocol Specific";
            TabControlChart.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] { UltraTab1, UltraTab2, UltraTab3, UltraTab4 });
            // 
            // GroupBoxRealTimePowerVars
            // 
            GroupBoxRealTimePowerVars.Controls.Add(LabelVars);
            GroupBoxRealTimePowerVars.Controls.Add(LabelVarsLabel);
            GroupBoxRealTimePowerVars.Controls.Add(LabelPower);
            GroupBoxRealTimePowerVars.Controls.Add(LabelPowerLabel);
            GroupBoxRealTimePowerVars.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            GroupBoxRealTimePowerVars.Location = new Point(10, 343);
            GroupBoxRealTimePowerVars.Name = "GroupBoxRealTimePowerVars";
            GroupBoxRealTimePowerVars.Size = new Size(178, 66);
            GroupBoxRealTimePowerVars.TabIndex = 3;
            // 
            // LabelVars
            // 
            LabelVars.Location = new Point(64, 36);
            LabelVars.Name = "LabelVars";
            LabelVars.Size = new Size(110, 20);
            LabelVars.TabIndex = 17;
            LabelVars.Text = "0.0000 MVars";
            // 
            // LabelPower
            // 
            LabelPower.Location = new Point(64, 14);
            LabelPower.Name = "LabelPower";
            LabelPower.Size = new Size(110, 20);
            LabelPower.TabIndex = 15;
            LabelPower.Text = "0.0000 MW";
            // 
            // ToolTipManagerForExtraParameters
            // 
            ToolTipManagerForExtraParameters.ContainingControl = this;
            ToolTipManagerForExtraParameters.InitialDelay = 2147483647;
            ToolTipManagerForExtraParameters.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            ToolTipManagerForExtraParameters.ToolTipTitle = "Extra Parameters Available";
            // 
            // GroupBoxProtocolParameters
            // 
            GroupBoxProtocolParameters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            GroupBoxProtocolParameters.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
            Appearance5.BorderColor = Color.Gray;
            GroupBoxProtocolParameters.ContentAreaAppearance = Appearance5;
            GroupBoxProtocolParameters.Controls.Add(LabelTabMask);
            GroupBoxProtocolParameters.Controls.Add(PropertyGridProtocolParameters);
            GroupBoxProtocolParameters.Location = new Point(555, 455);
            GroupBoxProtocolParameters.Name = "GroupBoxProtocolParameters";
            GroupBoxProtocolParameters.Size = new Size(490, 353);
            GroupBoxProtocolParameters.TabIndex = 11;
            // 
            // LabelTabMask
            // 
            Appearance6.AlphaLevel = (short)255;
            LabelTabMask.Appearance = Appearance6;
            LabelTabMask.Location = new Point(179, 0);
            LabelTabMask.Name = "LabelTabMask";
            LabelTabMask.Size = new Size(96, 12);
            LabelTabMask.TabIndex = 11;
            // 
            // PropertyGridProtocolParameters
            // 
            PropertyGridProtocolParameters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            PropertyGridProtocolParameters.Location = new Point(3, 3);
            PropertyGridProtocolParameters.Name = "PropertyGridProtocolParameters";
            PropertyGridProtocolParameters.Size = new Size(484, 347);
            PropertyGridProtocolParameters.TabIndex = 10;
            // 
            // GlobalExceptionLogger
            // 
            GlobalExceptionLogger.ContactEmail = "jrcarrol@tva.gov";
            GlobalExceptionLogger.ContactName = "James Ritchie Carroll";
            GlobalExceptionLogger.ContactPhone = null;
            // 
            // 
            // 
            GlobalExceptionLogger.ErrorLog.FileName = "ErrorLog.txt";
            GlobalExceptionLogger.LogToEventLog = false;
            GlobalExceptionLogger.LogToUI = true;
            GlobalExceptionLogger.SettingsCategory = "GlobalExceptionLogger";
            // 
            // LabelDefaultIPStack
            // 
            Appearance4.FontData.SizeInPoints = 7.0f;
            Appearance4.TextHAlignAsString = "Right";
            Appearance4.TextVAlignAsString = "Middle";
            LabelDefaultIPStack.Appearance = Appearance4;
            LabelDefaultIPStack.Location = new Point(176, 42);
            LabelDefaultIPStack.Name = "LabelDefaultIPStack";
            LabelDefaultIPStack.Size = new Size(142, 20);
            LabelDefaultIPStack.TabIndex = 16;
            LabelDefaultIPStack.Text = "Default System IP Stack: IPv6";
            LabelDefaultIPStack.Visible = false;
            // 
            // TimerDelay
            // 
            TimerDelay.Interval = 1100;
            // 
            // LabelReceiveFrom
            // 
            LabelReceiveFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance40.ForeColor = SystemColors.HotTrack;
            Appearance40.TextHAlignAsString = "Center";
            Appearance40.TextVAlignAsString = "Middle";
            LabelReceiveFrom.Appearance = Appearance40;
            LabelReceiveFrom.Cursor = Cursors.Hand;
            LabelReceiveFrom.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, default(byte));
            Appearance41.FontData.UnderlineAsString = "True";
            LabelReceiveFrom.HotTrackAppearance = Appearance41;
            LabelReceiveFrom.Location = new Point(5, 62);
            LabelReceiveFrom.Name = "LabelReceiveFrom";
            LabelReceiveFrom.Size = new Size(105, 21);
            LabelReceiveFrom.TabIndex = 3;
            LabelReceiveFrom.Text = "Receive From";
            LabelReceiveFrom.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 
            // PMUConnectionTester
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 601);
            Controls.Add(LabelDefaultIPStack);
            Controls.Add(GroupBoxProtocolParameters);
            Controls.Add(TabControlChart);
            Controls.Add(GroupBoxStatus);
            Controls.Add(GroupBoxConfigurationFrame);
            Controls.Add(GroupBoxHeaderFrame);
            Controls.Add(StatusBar);
            Controls.Add(GroupBoxConnection);
            Controls.Add(MenuStripMain);
            Controls.Add(GroupBoxRealTimePowerVars);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(710, 635);
            Name = "PMUConnectionTester";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PMU Connection Tester";
            TabPageControl9.ResumeLayout(false);
            TabPageControl1.ResumeLayout(false);
            TabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GroupBoxRemoteUDPServer).EndInit();
            GroupBoxRemoteUDPServer.ResumeLayout(false);
            TabPageControl3.ResumeLayout(false);
            TabPageControl4.ResumeLayout(false);
            TabPageControl4.PerformLayout();
            TabPageControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ChartDataDisplay).EndInit();
            TabPageControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GroupBoxPowerVarCalculations).EndInit();
            GroupBoxPowerVarCalculations.ResumeLayout(false);
            TabPageControl7.ResumeLayout(false);
            TabPageControl7.PerformLayout();
            TabPageControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TreeFrameAttributes).EndInit();
            ContextMenuForTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GroupBoxConnection).EndInit();
            GroupBoxConnection.ResumeLayout(false);
            GroupBoxPanelConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TabControlProtocolParameters).EndInit();
            TabControlProtocolParameters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TabControlCommunications).EndInit();
            TabControlCommunications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GroupBoxStatus).EndInit();
            GroupBoxStatus.ResumeLayout(false);
            GroupBoxPanelStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GroupBoxHeaderFrame).EndInit();
            GroupBoxHeaderFrame.ResumeLayout(false);
            GroupBoxPanelHeaderFrame.ResumeLayout(false);
            GroupBoxPanelHeaderFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TextBoxHeaderFrame).EndInit();
            ((System.ComponentModel.ISupportInitialize)GroupBoxConfigurationFrame).EndInit();
            GroupBoxConfigurationFrame.ResumeLayout(false);
            GroupBoxPanelConfigurationFrame.ResumeLayout(false);
            MenuStripMain.ResumeLayout(false);
            MenuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TabControlChart).EndInit();
            TabControlChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GroupBoxRealTimePowerVars).EndInit();
            GroupBoxRealTimePowerVars.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GroupBoxProtocolParameters).EndInit();
            GroupBoxProtocolParameters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GlobalExceptionLogger.ErrorLog).EndInit();
            ((System.ComponentModel.ISupportInitialize)GlobalExceptionLogger).EndInit();
            Load += new EventHandler(PMUConnectionTester_Load);
            FormClosing += new FormClosingEventHandler(PMUConnectionTester_FormClosing);
            Shown += new EventHandler(PMUConnectionTester_Shown);
            DragEnter += new DragEventHandler(PMUConnectionTester_DragEnter);
            DragDrop += new DragEventHandler(PMUConnectionTester_DragDrop);
            Resize += new EventHandler(PMUConnectionTester_Resize);
            ResumeLayout(false);
            PerformLayout();
        }

        internal Infragistics.Win.Misc.UltraExpandableGroupBox GroupBoxConnection;
        internal Infragistics.Win.Misc.UltraExpandableGroupBoxPanel GroupBoxPanelConnection;
        internal Infragistics.Win.Misc.UltraExpandableGroupBox GroupBoxStatus;
        internal Infragistics.Win.Misc.UltraExpandableGroupBoxPanel GroupBoxPanelStatus;
        internal Infragistics.Win.UltraWinStatusBar.UltraStatusBar StatusBar;
        internal Infragistics.Win.Misc.UltraLabel LabelBinaryFrameImage;
        internal Infragistics.Win.UltraWinToolTip.UltraToolTipManager ToolTipManager;
        internal Infragistics.Win.UltraWinTabControl.UltraTabControl TabControlCommunications;
        internal Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage TabSharedControlsPageCommunications;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl1;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl2;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl3;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxUdpLocalPort;
        internal Infragistics.Win.Misc.UltraLabel LabelUdpLocalPort;
        internal Infragistics.Win.Misc.UltraLabel LabelTcpPort;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxTcpPort;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxTcpHostIP;
        internal Infragistics.Win.Misc.UltraLabel LabelTcpHostIP;
        internal OpenFileDialog OpenFileDialog;
        internal Infragistics.Win.Misc.UltraExpandableGroupBox GroupBoxHeaderFrame;
        internal Infragistics.Win.Misc.UltraExpandableGroupBoxPanel GroupBoxPanelHeaderFrame;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor TextBoxHeaderFrame;
        internal SaveFileDialog SaveFileDialog;
        internal Infragistics.Win.Misc.UltraExpandableGroupBox GroupBoxConfigurationFrame;
        internal Infragistics.Win.Misc.UltraExpandableGroupBoxPanel GroupBoxPanelConfigurationFrame;
        internal ComboBox ComboBoxPmus;
        internal Infragistics.Win.Misc.UltraLabel LabelPmuList;
        internal ComboBox ComboBoxPhasors;
        internal Infragistics.Win.Misc.UltraLabel LabelPhasor;
        internal Infragistics.Win.Misc.UltraLabel LabelIDCodeLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelIDCode;
        internal Infragistics.Win.Misc.UltraLabel LabelDigitalCount;
        internal Infragistics.Win.Misc.UltraLabel LabelDigitalCountLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelAnalogCount;
        internal Infragistics.Win.Misc.UltraLabel LabelAnalogCountLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelNominalFrequencyLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelHz;
        internal Infragistics.Win.Misc.UltraLabel LabelNominalFrequency;
        internal Infragistics.Win.Misc.UltraLabel LabelPhasorCount;
        internal Infragistics.Win.Misc.UltraLabel LabelPhasorCountLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelTime;
        internal Infragistics.Win.Misc.UltraLabel LabelTimeLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelFrequency;
        internal Infragistics.Win.Misc.UltraLabel LabelFrequencyLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelMagnitude;
        internal Infragistics.Win.Misc.UltraLabel LabelMagnitudeLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelAngle;
        internal Infragistics.Win.Misc.UltraLabel LabelAngleLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelFrameType;
        internal Infragistics.Win.Misc.UltraLabel LabelFrameTypeLabel;
        internal ComboBox ComboBoxByteEncodingDisplayFormats;
        internal Infragistics.Win.Misc.UltraLabel LabelDisplayFormat;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl4;
        internal Infragistics.Win.Misc.UltraLabel LabelCaptureFile;
        internal MenuStrip MenuStripMain;
        internal ToolStripMenuItem MenuItemFile;
        internal ToolStripMenuItem MenuItemExit;
        internal ToolStripMenuItem MenuItemHelp;
        internal ToolStripMenuItem MenuItemOnlineHelp;
        internal ToolStripSeparator ToolStripSeparator2;
        internal ToolStripMenuItem MenuItemAbout;
        internal ToolStripMenuItem MenuItemConfigFile;
        internal ToolStripMenuItem MenuItemLoadConfigFile;
        internal ToolStripMenuItem MenuItemSaveConfigFile;
        internal ToolStripMenuItem MenuItemConnection;
        internal ToolStripMenuItem MenuItemLoadConnection;
        internal ToolStripMenuItem MenuItemSaveConnection;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripMenuItem MenuItemCapture;
        internal ToolStripMenuItem MenuItemStartCapture;
        internal ToolStripMenuItem MenuItemStopCapture;
        internal Infragistics.Win.Misc.UltraLabel LabelSelectedIsRefAngle;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl6;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl5;
        internal Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage TabSharedControlsPageChart;
        internal Infragistics.Win.UltraWinTabControl.UltraTabControl TabControlChart;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl7;
        internal ToolStripSeparator ToolStripSeparator3;
        internal ToolStripMenuItem MenuItemCaptureSampleFrames;
        internal ToolStripMenuItem MenuItemCancelSampleFrameCapture;
        internal Button ButtonBrowse;
        internal TextBox TextBoxFileCaptureName;
        internal Infragistics.Win.Misc.UltraGroupBox GroupBoxRemoteUDPServer;
        internal Infragistics.Win.Misc.UltraLabel LabelUdpHostIP;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxUdpHostIP;
        internal Infragistics.Win.Misc.UltraLabel LabelUdpRemotePort;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxUdpRemotePort;
        internal Infragistics.Win.UltraWinEditors.UltraCheckEditor CheckBoxRemoteUdpServer;
        internal Infragistics.Win.Misc.UltraLabel LabelFrameRate;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxFileFrameRate;
        internal Infragistics.Win.Misc.UltraLabel LabelFramesPerSecond;
        internal ComboBox ComboBoxSerialPorts;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialPort;
        internal ComboBox ComboBoxSerialParities;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialParity;
        internal ComboBox ComboBoxSerialBaudRates;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialBaudRate;
        internal ComboBox ComboBoxSerialStopBits;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialStopBits;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialDataBits;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxSerialDataBits;
        internal Infragistics.Win.Misc.UltraLabel LabelReplayCapturedFile;
        internal Infragistics.Win.Misc.UltraButton ButtonGetStatus;
        internal TextBox TextBoxMessages;
        internal Infragistics.Win.UltraWinEditors.UltraCheckEditor CheckBoxSerialDTR;
        internal Infragistics.Win.UltraWinEditors.UltraCheckEditor CheckBoxSerialRTS;
        internal ToolStripMenuItem MenuItemLocalHelp;
        internal Infragistics.Win.UltraWinEditors.UltraCheckEditor CheckBoxEstablishTcpServer;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl8;
        internal PropertyGrid PropertyGridApplicationSettings;
        internal Infragistics.Win.Misc.UltraGroupBox GroupBoxPowerVarCalculations;
        internal ComboBox ComboBoxCurrentPhasors;
        internal Infragistics.Win.Misc.UltraLabel LabelCurrentPhasor;
        internal ComboBox ComboBoxVoltagePhasors;
        internal Infragistics.Win.Misc.UltraLabel LabelVoltagePhasor;
        internal Infragistics.Win.Misc.UltraGroupBox GroupBoxRealTimePowerVars;
        internal Infragistics.Win.Misc.UltraLabel LabelVars;
        internal Infragistics.Win.Misc.UltraLabel LabelVarsLabel;
        internal Infragistics.Win.Misc.UltraLabel LabelPower;
        internal Infragistics.Win.Misc.UltraLabel LabelPowerLabel;
        internal Infragistics.Win.UltraWinTree.UltraTree TreeFrameAttributes;
        internal ContextMenuStrip ContextMenuForTree;
        internal ToolStripMenuItem MenuItemRefresh;
        internal ToolStripSeparator ToolStripSeparator5;
        internal ToolStripMenuItem MenuItemExpandAll;
        internal ToolStripMenuItem MenuItemCollapseAll;
        internal Infragistics.Win.UltraWinTabControl.UltraTabControl TabControlProtocolParameters;
        internal Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage TabSharedControlsPageProtocolParameters;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl9;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl10;
        internal Infragistics.Win.Misc.UltraLabel LabelDeviceID;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxDeviceID;
        internal Infragistics.Win.Misc.UltraButton ButtonSendCommand;
        internal ComboBox ComboBoxCommands;
        internal Infragistics.Win.Misc.UltraLabel LabelCommand;
        internal ComboBox ComboBoxProtocols;
        internal Infragistics.Win.Misc.UltraLabel LabelVersion;
        internal Infragistics.Win.Misc.UltraButton ButtonListen;
        internal Infragistics.Win.UltraWinToolTip.UltraToolTipManager ToolTipManagerForExtraParameters;
        internal Infragistics.Win.Misc.UltraGroupBox GroupBoxProtocolParameters;
        internal Infragistics.Win.Misc.UltraLabel LabelTabMask;
        internal PropertyGrid PropertyGridProtocolParameters;
        internal GSF.ErrorManagement.ErrorLogger GlobalExceptionLogger;
        internal Infragistics.Win.UltraWinEditors.UltraCheckEditor CheckBoxAutoRepeatPlayback;
        internal ToolStripSeparator ToolStripSeparator6;
        internal ToolStripMenuItem MenuItemStartStreamDebugCapture;
        internal ToolStripMenuItem MenuItemStopStreamDebugCapture;
        internal Infragistics.Win.Misc.UltraLabel LabelAlternateCommandChannel;
        internal Infragistics.Win.Misc.UltraLabel LabelAlternateCommandChannelState;
        internal Infragistics.Win.UltraWinChart.UltraChart ChartDataDisplay;
        internal Infragistics.Win.Misc.UltraLabel LabelDefaultIPStack;
        internal Timer TimerDelay;
        internal Infragistics.Win.Misc.UltraButton ButtonRestoreDefaultSettings;
        internal Infragistics.Win.Misc.UltraLabel LabelTcpNetworkInterface;
        internal Infragistics.Win.Misc.UltraLabel LabelUdpNetworkInterface;
        internal Infragistics.Win.Misc.UltraLabel LabelMulticastSource;
        internal Infragistics.Win.Misc.UltraLabel LabelReceiveFrom;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxRawCommand;
    }
}