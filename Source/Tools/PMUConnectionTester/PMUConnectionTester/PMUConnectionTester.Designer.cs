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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo9 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo3 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Enter host DNS name, IPv4 or IPv6 address", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo4 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Enter host DNS name, IPv4 or IPv6 address", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo5 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("This enables the Request to Send (RTS) signal during serial communication.", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo6 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("This enables the Data Terminal Ready (DTR) signal during serial communication.", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement1 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.ColumnLineChartAppearance columnLineChartAppearance1 = new Infragistics.UltraChart.Resources.Appearance.ColumnLineChartAppearance();
            Infragistics.UltraChart.Resources.Appearance.DoughnutChartAppearance doughnutChartAppearance1 = new Infragistics.UltraChart.Resources.Appearance.DoughnutChartAppearance();
            Infragistics.UltraChart.Resources.Appearance.GanttChartAppearance ganttChartAppearance1 = new Infragistics.UltraChart.Resources.Appearance.GanttChartAppearance();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement2 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement3 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.RadarChartAppearance radarChartAppearance1 = new Infragistics.UltraChart.Resources.Appearance.RadarChartAppearance();
            Infragistics.UltraChart.Resources.Appearance.SplineAreaChart3DAppearance splineAreaChart3DAppearance1 = new Infragistics.UltraChart.Resources.Appearance.SplineAreaChart3DAppearance();
            Infragistics.UltraChart.Resources.Appearance.SplineChart3DAppearance splineChart3DAppearance1 = new Infragistics.UltraChart.Resources.Appearance.SplineChart3DAppearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo2 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("This protocol has additional connection parameters.  Click here to view.", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.False);
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Keeping this data window displayed may cause the graph to fall behind real-time", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.True);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel7 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel8 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel9 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel10 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo7 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Click link to change source voltage and current phasors for power / vars calculat" +
        "ions...", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo8 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Click link to change source voltage and current phasors for power / vars calculat" +
        "ions...", Infragistics.Win.ToolTipImage.Info, "Note", Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PMUConnectionTester));
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.TabPageControl9 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.TextBoxRawCommand = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.LabelAlternateCommandChannelState = new Infragistics.Win.Misc.UltraLabel();
            this.LabelAlternateCommandChannel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelVersion = new Infragistics.Win.Misc.UltraLabel();
            this.ButtonListen = new Infragistics.Win.Misc.UltraButton();
            this.ComboBoxCommands = new System.Windows.Forms.ComboBox();
            this.LabelCommand = new Infragistics.Win.Misc.UltraLabel();
            this.LabelDeviceID = new Infragistics.Win.Misc.UltraLabel();
            this.TextBoxDeviceID = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.ButtonSendCommand = new Infragistics.Win.Misc.UltraButton();
            this.ComboBoxProtocols = new System.Windows.Forms.ComboBox();
            this.TabPageControl10 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.TabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.LabelTcpNetworkInterface = new Infragistics.Win.Misc.UltraLabel();
            this.CheckBoxEstablishTcpServer = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.LabelTcpHostIP = new Infragistics.Win.Misc.UltraLabel();
            this.TextBoxTcpHostIP = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.LabelTcpPort = new Infragistics.Win.Misc.UltraLabel();
            this.TextBoxTcpPort = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.TabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.LabelReceiveFrom = new Infragistics.Win.Misc.UltraLabel();
            this.LabelUdpNetworkInterface = new Infragistics.Win.Misc.UltraLabel();
            this.GroupBoxRemoteUDPServer = new Infragistics.Win.Misc.UltraGroupBox();
            this.LabelMulticastSource = new Infragistics.Win.Misc.UltraLabel();
            this.LabelUdpHostIP = new Infragistics.Win.Misc.UltraLabel();
            this.TextBoxUdpHostIP = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.LabelUdpRemotePort = new Infragistics.Win.Misc.UltraLabel();
            this.TextBoxUdpRemotePort = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.CheckBoxRemoteUdpServer = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.LabelUdpLocalPort = new Infragistics.Win.Misc.UltraLabel();
            this.TextBoxUdpLocalPort = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.TabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.CheckBoxSerialRTS = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.CheckBoxSerialDTR = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.TextBoxSerialDataBits = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.ComboBoxSerialStopBits = new System.Windows.Forms.ComboBox();
            this.ComboBoxSerialParities = new System.Windows.Forms.ComboBox();
            this.LabelSerialParity = new Infragistics.Win.Misc.UltraLabel();
            this.ComboBoxSerialBaudRates = new System.Windows.Forms.ComboBox();
            this.LabelSerialBaudRate = new Infragistics.Win.Misc.UltraLabel();
            this.ComboBoxSerialPorts = new System.Windows.Forms.ComboBox();
            this.LabelSerialPort = new Infragistics.Win.Misc.UltraLabel();
            this.LabelSerialStopBits = new Infragistics.Win.Misc.UltraLabel();
            this.LabelSerialDataBits = new Infragistics.Win.Misc.UltraLabel();
            this.TabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.CheckBoxAutoRepeatPlayback = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.LabelFrameRate = new Infragistics.Win.Misc.UltraLabel();
            this.TextBoxFileFrameRate = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.ButtonBrowse = new System.Windows.Forms.Button();
            this.TextBoxFileCaptureName = new System.Windows.Forms.TextBox();
            this.LabelCaptureFile = new Infragistics.Win.Misc.UltraLabel();
            this.LabelFramesPerSecond = new Infragistics.Win.Misc.UltraLabel();
            this.LabelReplayCapturedFile = new Infragistics.Win.Misc.UltraLabel();
            this.TabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ChartDataDisplay = new Infragistics.Win.UltraWinChart.UltraChart();
            this.TabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ButtonRestoreDefaultSettings = new Infragistics.Win.Misc.UltraButton();
            this.GroupBoxPowerVarCalculations = new Infragistics.Win.Misc.UltraGroupBox();
            this.ComboBoxCurrentPhasors = new System.Windows.Forms.ComboBox();
            this.LabelCurrentPhasor = new Infragistics.Win.Misc.UltraLabel();
            this.ComboBoxVoltagePhasors = new System.Windows.Forms.ComboBox();
            this.LabelVoltagePhasor = new Infragistics.Win.Misc.UltraLabel();
            this.PropertyGridApplicationSettings = new System.Windows.Forms.PropertyGrid();
            this.ButtonGetStatus = new Infragistics.Win.Misc.UltraButton();
            this.TabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.TextBoxMessages = new System.Windows.Forms.TextBox();
            this.TabPageControl8 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.TreeFrameAttributes = new Infragistics.Win.UltraWinTree.UltraTree();
            this.ContextMenuForTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBoxConnection = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            this.GroupBoxPanelConnection = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            this.TabControlProtocolParameters = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.TabSharedControlsPageProtocolParameters = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.TabControlCommunications = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.TabSharedControlsPageCommunications = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.GroupBoxStatus = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            this.GroupBoxPanelStatus = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            this.LabelBinaryFrameImage = new Infragistics.Win.Misc.UltraLabel();
            this.LabelDisplayFormat = new Infragistics.Win.Misc.UltraLabel();
            this.ComboBoxByteEncodingDisplayFormats = new System.Windows.Forms.ComboBox();
            this.LabelFrameType = new Infragistics.Win.Misc.UltraLabel();
            this.LabelFrameTypeLabel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelFrequency = new Infragistics.Win.Misc.UltraLabel();
            this.LabelFrequencyLabel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelMagnitude = new Infragistics.Win.Misc.UltraLabel();
            this.LabelMagnitudeLabel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelAngle = new Infragistics.Win.Misc.UltraLabel();
            this.LabelAngleLabel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelTime = new Infragistics.Win.Misc.UltraLabel();
            this.LabelTimeLabel = new Infragistics.Win.Misc.UltraLabel();
            this.StatusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            this.ToolTipManager = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.LabelVarsLabel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelPowerLabel = new Infragistics.Win.Misc.UltraLabel();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.GroupBoxHeaderFrame = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            this.GroupBoxPanelHeaderFrame = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            this.TextBoxHeaderFrame = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.GroupBoxConfigurationFrame = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            this.GroupBoxPanelConfigurationFrame = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            this.LabelPhasorCount = new Infragistics.Win.Misc.UltraLabel();
            this.LabelPhasorCountLabel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelHz = new Infragistics.Win.Misc.UltraLabel();
            this.LabelNominalFrequency = new Infragistics.Win.Misc.UltraLabel();
            this.LabelNominalFrequencyLabel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelDigitalCount = new Infragistics.Win.Misc.UltraLabel();
            this.LabelDigitalCountLabel = new Infragistics.Win.Misc.UltraLabel();
            this.LabelAnalogCount = new Infragistics.Win.Misc.UltraLabel();
            this.LabelAnalogCountLabel = new Infragistics.Win.Misc.UltraLabel();
            this.ComboBoxPmus = new System.Windows.Forms.ComboBox();
            this.LabelIDCode = new Infragistics.Win.Misc.UltraLabel();
            this.LabelIDCodeLabel = new Infragistics.Win.Misc.UltraLabel();
            this.ComboBoxPhasors = new System.Windows.Forms.ComboBox();
            this.LabelPmuList = new Infragistics.Win.Misc.UltraLabel();
            this.LabelSelectedIsRefAngle = new Infragistics.Win.Misc.UltraLabel();
            this.LabelPhasor = new Infragistics.Win.Misc.UltraLabel();
            this.MenuStripMain = new System.Windows.Forms.MenuStrip();
            this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLoadConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLoadConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStartCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStopCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemStartStreamDebugCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStopStreamDebugCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemCaptureSampleFrames = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCancelSampleFrameCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLocalHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOnlineHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.TabSharedControlsPageChart = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.TabControlChart = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.GroupBoxRealTimePowerVars = new Infragistics.Win.Misc.UltraGroupBox();
            this.LabelVars = new Infragistics.Win.Misc.UltraLabel();
            this.LabelPower = new Infragistics.Win.Misc.UltraLabel();
            this.ToolTipManagerForExtraParameters = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.GroupBoxProtocolParameters = new Infragistics.Win.Misc.UltraGroupBox();
            this.LabelTabMask = new Infragistics.Win.Misc.UltraLabel();
            this.PropertyGridProtocolParameters = new System.Windows.Forms.PropertyGrid();
            this.GlobalExceptionLogger = new GSF.ErrorManagement.ErrorLogger(this.components);
            this.LabelDefaultIPStack = new Infragistics.Win.Misc.UltraLabel();
            this.TimerDelay = new System.Windows.Forms.Timer(this.components);
            this.TabPageControl9.SuspendLayout();
            this.TabPageControl1.SuspendLayout();
            this.TabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxRemoteUDPServer)).BeginInit();
            this.GroupBoxRemoteUDPServer.SuspendLayout();
            this.TabPageControl3.SuspendLayout();
            this.TabPageControl4.SuspendLayout();
            this.TabPageControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartDataDisplay)).BeginInit();
            this.TabPageControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxPowerVarCalculations)).BeginInit();
            this.GroupBoxPowerVarCalculations.SuspendLayout();
            this.TabPageControl7.SuspendLayout();
            this.TabPageControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreeFrameAttributes)).BeginInit();
            this.ContextMenuForTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxConnection)).BeginInit();
            this.GroupBoxConnection.SuspendLayout();
            this.GroupBoxPanelConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabControlProtocolParameters)).BeginInit();
            this.TabControlProtocolParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabControlCommunications)).BeginInit();
            this.TabControlCommunications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxStatus)).BeginInit();
            this.GroupBoxStatus.SuspendLayout();
            this.GroupBoxPanelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxHeaderFrame)).BeginInit();
            this.GroupBoxHeaderFrame.SuspendLayout();
            this.GroupBoxPanelHeaderFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextBoxHeaderFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxConfigurationFrame)).BeginInit();
            this.GroupBoxConfigurationFrame.SuspendLayout();
            this.GroupBoxPanelConfigurationFrame.SuspendLayout();
            this.MenuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabControlChart)).BeginInit();
            this.TabControlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxRealTimePowerVars)).BeginInit();
            this.GroupBoxRealTimePowerVars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxProtocolParameters)).BeginInit();
            this.GroupBoxProtocolParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalExceptionLogger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalExceptionLogger.ErrorLog)).BeginInit();
            this.SuspendLayout();
            // 
            // TabPageControl9
            // 
            this.TabPageControl9.Controls.Add(this.TextBoxRawCommand);
            this.TabPageControl9.Controls.Add(this.LabelAlternateCommandChannelState);
            this.TabPageControl9.Controls.Add(this.LabelAlternateCommandChannel);
            this.TabPageControl9.Controls.Add(this.LabelVersion);
            this.TabPageControl9.Controls.Add(this.ButtonListen);
            this.TabPageControl9.Controls.Add(this.ComboBoxCommands);
            this.TabPageControl9.Controls.Add(this.LabelCommand);
            this.TabPageControl9.Controls.Add(this.LabelDeviceID);
            this.TabPageControl9.Controls.Add(this.TextBoxDeviceID);
            this.TabPageControl9.Controls.Add(this.ButtonSendCommand);
            this.TabPageControl9.Controls.Add(this.ComboBoxProtocols);
            this.TabPageControl9.Location = new System.Drawing.Point(1, 20);
            this.TabPageControl9.Name = "TabPageControl9";
            this.TabPageControl9.Size = new System.Drawing.Size(360, 96);
            // 
            // TextBoxRawCommand
            // 
            this.TextBoxRawCommand.AutoSize = false;
            this.TextBoxRawCommand.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxRawCommand.InputMask = "aaaaaa";
            this.TextBoxRawCommand.Location = new System.Drawing.Point(166, 44);
            this.TextBoxRawCommand.Name = "TextBoxRawCommand";
            this.TextBoxRawCommand.PromptChar = ' ';
            this.TextBoxRawCommand.Size = new System.Drawing.Size(64, 20);
            this.TextBoxRawCommand.TabIndex = 20;
            this.TextBoxRawCommand.Text = "513";
            // 
            // LabelAlternateCommandChannelState
            // 
            this.LabelAlternateCommandChannelState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Control;
            appearance1.BackColor2 = System.Drawing.Color.LightGray;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.TextHAlignAsString = "Center";
            appearance1.TextVAlignAsString = "Middle";
            this.LabelAlternateCommandChannelState.Appearance = appearance1;
            this.LabelAlternateCommandChannelState.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
            this.LabelAlternateCommandChannelState.Location = new System.Drawing.Point(248, 79);
            this.LabelAlternateCommandChannelState.Name = "LabelAlternateCommandChannelState";
            this.LabelAlternateCommandChannelState.Size = new System.Drawing.Size(108, 14);
            this.LabelAlternateCommandChannelState.TabIndex = 19;
            this.LabelAlternateCommandChannelState.Text = "Not Defined";
            this.LabelAlternateCommandChannelState.Click += new System.EventHandler(this.LabelAlternateCommandChannelState_Click);
            // 
            // LabelAlternateCommandChannel
            // 
            this.LabelAlternateCommandChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.ForeColor = System.Drawing.SystemColors.HotTrack;
            appearance2.TextHAlignAsString = "Center";
            appearance2.TextVAlignAsString = "Middle";
            this.LabelAlternateCommandChannel.Appearance = appearance2;
            this.LabelAlternateCommandChannel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelAlternateCommandChannel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            appearance3.FontData.UnderlineAsString = "True";
            this.LabelAlternateCommandChannel.HotTrackAppearance = appearance3;
            this.LabelAlternateCommandChannel.Location = new System.Drawing.Point(246, 49);
            this.LabelAlternateCommandChannel.Name = "LabelAlternateCommandChannel";
            this.LabelAlternateCommandChannel.Size = new System.Drawing.Size(109, 27);
            this.LabelAlternateCommandChannel.TabIndex = 18;
            this.LabelAlternateCommandChannel.Text = "Configure Alternate Command Channel";
            ultraToolTipInfo9.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            ultraToolTipInfo9.ToolTipTextFormatted = "This link allows configuration of an alternate communications channel for sending" +
    " commands.<br/>For example, this is used for implementing the UDP_T or UDP_U fea" +
    "ture for SEL relays.";
            ultraToolTipInfo9.ToolTipTitle = "Note";
            this.ToolTipManager.SetUltraToolTip(this.LabelAlternateCommandChannel, ultraToolTipInfo9);
            this.LabelAlternateCommandChannel.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            this.LabelAlternateCommandChannel.Click += new System.EventHandler(this.LabelAlternateCommandChannel_Click);
            // 
            // LabelVersion
            // 
            this.LabelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance63.FontData.SizeInPoints = 7.25F;
            appearance63.TextHAlignAsString = "Right";
            this.LabelVersion.Appearance = appearance63;
            this.LabelVersion.Location = new System.Drawing.Point(228, 30);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(126, 15);
            this.LabelVersion.TabIndex = 16;
            this.LabelVersion.Text = "Version: 0.0.0.0";
            // 
            // ButtonListen
            // 
            this.ButtonListen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonListen.Location = new System.Drawing.Point(246, 7);
            this.ButtonListen.Name = "ButtonListen";
            this.ButtonListen.Size = new System.Drawing.Size(110, 23);
            this.ButtonListen.TabIndex = 15;
            this.ButtonListen.Text = "&Connect";
            this.ButtonListen.Click += new System.EventHandler(this.ButtonListen_Click);
            // 
            // ComboBoxCommands
            // 
            this.ComboBoxCommands.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxCommands.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxCommands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCommands.Enabled = false;
            this.ComboBoxCommands.Items.AddRange(new object[] {
            "Disable Real-time Data",
            "Enable Real-time Data",
            "Send Header Frame",
            "Send Config Frame 1",
            "Send Config Frame 2",
            "Send Config Frame 3",
            "Send Raw Command"});
            this.ComboBoxCommands.Location = new System.Drawing.Point(6, 72);
            this.ComboBoxCommands.Name = "ComboBoxCommands";
            this.ComboBoxCommands.Size = new System.Drawing.Size(154, 21);
            this.ComboBoxCommands.TabIndex = 13;
            this.ComboBoxCommands.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCommands_SelectedIndexChanged);
            // 
            // LabelCommand
            // 
            this.LabelCommand.Enabled = false;
            this.LabelCommand.Location = new System.Drawing.Point(6, 57);
            this.LabelCommand.Name = "LabelCommand";
            this.LabelCommand.Size = new System.Drawing.Size(100, 23);
            this.LabelCommand.TabIndex = 12;
            this.LabelCommand.Text = "Comm&and:";
            // 
            // LabelDeviceID
            // 
            appearance64.TextHAlignAsString = "Right";
            this.LabelDeviceID.Appearance = appearance64;
            this.LabelDeviceID.Location = new System.Drawing.Point(3, 37);
            this.LabelDeviceID.Name = "LabelDeviceID";
            this.LabelDeviceID.Size = new System.Drawing.Size(87, 23);
            this.LabelDeviceID.TabIndex = 10;
            this.LabelDeviceID.Text = "&Device ID Code:";
            // 
            // TextBoxDeviceID
            // 
            this.TextBoxDeviceID.AutoSize = false;
            this.TextBoxDeviceID.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxDeviceID.InputMask = "-nnnnn";
            this.TextBoxDeviceID.Location = new System.Drawing.Point(96, 34);
            this.TextBoxDeviceID.Name = "TextBoxDeviceID";
            this.TextBoxDeviceID.PromptChar = ' ';
            this.TextBoxDeviceID.Size = new System.Drawing.Size(45, 20);
            this.TextBoxDeviceID.TabIndex = 11;
            this.TextBoxDeviceID.Text = "1";
            this.TextBoxDeviceID.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            this.TextBoxDeviceID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            // 
            // ButtonSendCommand
            // 
            this.ButtonSendCommand.Enabled = false;
            this.ButtonSendCommand.Location = new System.Drawing.Point(166, 70);
            this.ButtonSendCommand.Name = "ButtonSendCommand";
            this.ButtonSendCommand.Size = new System.Drawing.Size(65, 23);
            this.ButtonSendCommand.TabIndex = 14;
            this.ButtonSendCommand.Text = "Se&nd";
            this.ButtonSendCommand.Click += new System.EventHandler(this.ButtonSendCommand_Click);
            // 
            // ComboBoxProtocols
            // 
            this.ComboBoxProtocols.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxProtocols.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxProtocols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxProtocols.Location = new System.Drawing.Point(6, 7);
            this.ComboBoxProtocols.Name = "ComboBoxProtocols";
            this.ComboBoxProtocols.Size = new System.Drawing.Size(224, 21);
            this.ComboBoxProtocols.TabIndex = 9;
            this.ComboBoxProtocols.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProtocols_SelectedIndexChanged);
            // 
            // TabPageControl10
            // 
            this.TabPageControl10.Location = new System.Drawing.Point(-10000, -10000);
            this.TabPageControl10.Name = "TabPageControl10";
            this.TabPageControl10.Size = new System.Drawing.Size(360, 96);
            // 
            // TabPageControl1
            // 
            this.TabPageControl1.Controls.Add(this.LabelTcpNetworkInterface);
            this.TabPageControl1.Controls.Add(this.CheckBoxEstablishTcpServer);
            this.TabPageControl1.Controls.Add(this.LabelTcpHostIP);
            this.TabPageControl1.Controls.Add(this.TextBoxTcpHostIP);
            this.TabPageControl1.Controls.Add(this.LabelTcpPort);
            this.TabPageControl1.Controls.Add(this.TextBoxTcpPort);
            this.TabPageControl1.Location = new System.Drawing.Point(1, 20);
            this.TabPageControl1.Name = "TabPageControl1";
            this.TabPageControl1.Size = new System.Drawing.Size(304, 96);
            // 
            // LabelTcpNetworkInterface
            // 
            this.LabelTcpNetworkInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance36.ForeColor = System.Drawing.SystemColors.HotTrack;
            appearance36.TextHAlignAsString = "Center";
            appearance36.TextVAlignAsString = "Middle";
            this.LabelTcpNetworkInterface.Appearance = appearance36;
            this.LabelTcpNetworkInterface.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelTcpNetworkInterface.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            appearance37.FontData.UnderlineAsString = "True";
            this.LabelTcpNetworkInterface.HotTrackAppearance = appearance37;
            this.LabelTcpNetworkInterface.Location = new System.Drawing.Point(63, 72);
            this.LabelTcpNetworkInterface.Name = "LabelTcpNetworkInterface";
            this.LabelTcpNetworkInterface.Size = new System.Drawing.Size(94, 21);
            this.LabelTcpNetworkInterface.TabIndex = 5;
            this.LabelTcpNetworkInterface.Text = "Network Interface";
            this.LabelTcpNetworkInterface.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            this.LabelTcpNetworkInterface.Click += new System.EventHandler(this.LabelTcpNetworkInterface_Click);
            // 
            // CheckBoxEstablishTcpServer
            // 
            this.CheckBoxEstablishTcpServer.Location = new System.Drawing.Point(175, 19);
            this.CheckBoxEstablishTcpServer.Name = "CheckBoxEstablishTcpServer";
            this.CheckBoxEstablishTcpServer.Size = new System.Drawing.Size(128, 26);
            this.CheckBoxEstablishTcpServer.TabIndex = 4;
            this.CheckBoxEstablishTcpServer.Text = "Establish Tcp Server";
            this.CheckBoxEstablishTcpServer.CheckedChanged += new System.EventHandler(this.CheckBoxEstablishTcpServer_CheckedChanged);
            // 
            // LabelTcpHostIP
            // 
            appearance38.TextHAlignAsString = "Right";
            this.LabelTcpHostIP.Appearance = appearance38;
            this.LabelTcpHostIP.Location = new System.Drawing.Point(15, 25);
            this.LabelTcpHostIP.Name = "LabelTcpHostIP";
            this.LabelTcpHostIP.Size = new System.Drawing.Size(45, 23);
            this.LabelTcpHostIP.TabIndex = 0;
            this.LabelTcpHostIP.Text = "Host &IP:";
            ultraToolTipInfo3.ToolTipText = "Enter host DNS name, IPv4 or IPv6 address";
            this.ToolTipManager.SetUltraToolTip(this.LabelTcpHostIP, ultraToolTipInfo3);
            // 
            // TextBoxTcpHostIP
            // 
            this.TextBoxTcpHostIP.AutoSize = false;
            this.TextBoxTcpHostIP.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxTcpHostIP.InputMask = "nnn\\.nnn\\.nnn\\.nnn";
            this.TextBoxTcpHostIP.Location = new System.Drawing.Point(66, 22);
            this.TextBoxTcpHostIP.Name = "TextBoxTcpHostIP";
            this.TextBoxTcpHostIP.PromptChar = ' ';
            this.TextBoxTcpHostIP.Size = new System.Drawing.Size(92, 20);
            this.TextBoxTcpHostIP.TabIndex = 1;
            this.TextBoxTcpHostIP.Text = "127.0.0.1";
            this.TextBoxTcpHostIP.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            // 
            // LabelTcpPort
            // 
            appearance39.TextHAlignAsString = "Right";
            this.LabelTcpPort.Appearance = appearance39;
            this.LabelTcpPort.Location = new System.Drawing.Point(15, 51);
            this.LabelTcpPort.Name = "LabelTcpPort";
            this.LabelTcpPort.Size = new System.Drawing.Size(45, 23);
            this.LabelTcpPort.TabIndex = 2;
            this.LabelTcpPort.Text = "P&ort:";
            // 
            // TextBoxTcpPort
            // 
            this.TextBoxTcpPort.AutoSize = false;
            this.TextBoxTcpPort.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxTcpPort.InputMask = "-nnnnn";
            this.TextBoxTcpPort.Location = new System.Drawing.Point(66, 48);
            this.TextBoxTcpPort.Name = "TextBoxTcpPort";
            this.TextBoxTcpPort.PromptChar = ' ';
            this.TextBoxTcpPort.Size = new System.Drawing.Size(45, 20);
            this.TextBoxTcpPort.TabIndex = 3;
            this.TextBoxTcpPort.Text = "4712";
            this.TextBoxTcpPort.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            this.TextBoxTcpPort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            // 
            // TabPageControl2
            // 
            this.TabPageControl2.Controls.Add(this.LabelReceiveFrom);
            this.TabPageControl2.Controls.Add(this.LabelUdpNetworkInterface);
            this.TabPageControl2.Controls.Add(this.GroupBoxRemoteUDPServer);
            this.TabPageControl2.Controls.Add(this.LabelUdpLocalPort);
            this.TabPageControl2.Controls.Add(this.TextBoxUdpLocalPort);
            this.TabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
            this.TabPageControl2.Name = "TabPageControl2";
            this.TabPageControl2.Size = new System.Drawing.Size(304, 96);
            // 
            // LabelReceiveFrom
            // 
            this.LabelReceiveFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance40.ForeColor = System.Drawing.SystemColors.HotTrack;
            appearance40.TextHAlignAsString = "Center";
            appearance40.TextVAlignAsString = "Middle";
            this.LabelReceiveFrom.Appearance = appearance40;
            this.LabelReceiveFrom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelReceiveFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            appearance41.FontData.UnderlineAsString = "True";
            this.LabelReceiveFrom.HotTrackAppearance = appearance41;
            this.LabelReceiveFrom.Location = new System.Drawing.Point(5, 62);
            this.LabelReceiveFrom.Name = "LabelReceiveFrom";
            this.LabelReceiveFrom.Size = new System.Drawing.Size(105, 21);
            this.LabelReceiveFrom.TabIndex = 3;
            this.LabelReceiveFrom.Text = "Receive From";
            this.LabelReceiveFrom.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            this.LabelReceiveFrom.Click += new System.EventHandler(this.LabelReceiveFrom_Click);
            // 
            // LabelUdpNetworkInterface
            // 
            this.LabelUdpNetworkInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance42.ForeColor = System.Drawing.SystemColors.HotTrack;
            appearance42.TextHAlignAsString = "Center";
            appearance42.TextVAlignAsString = "Middle";
            this.LabelUdpNetworkInterface.Appearance = appearance42;
            this.LabelUdpNetworkInterface.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelUdpNetworkInterface.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            appearance43.FontData.UnderlineAsString = "True";
            this.LabelUdpNetworkInterface.HotTrackAppearance = appearance43;
            this.LabelUdpNetworkInterface.Location = new System.Drawing.Point(8, 41);
            this.LabelUdpNetworkInterface.Name = "LabelUdpNetworkInterface";
            this.LabelUdpNetworkInterface.Size = new System.Drawing.Size(94, 21);
            this.LabelUdpNetworkInterface.TabIndex = 2;
            this.LabelUdpNetworkInterface.Text = "Network Interface";
            this.LabelUdpNetworkInterface.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            this.LabelUdpNetworkInterface.Click += new System.EventHandler(this.LabelUdpNetworkInterface_Click);
            // 
            // GroupBoxRemoteUDPServer
            // 
            this.GroupBoxRemoteUDPServer.Controls.Add(this.LabelMulticastSource);
            this.GroupBoxRemoteUDPServer.Controls.Add(this.LabelUdpHostIP);
            this.GroupBoxRemoteUDPServer.Controls.Add(this.TextBoxUdpHostIP);
            this.GroupBoxRemoteUDPServer.Controls.Add(this.LabelUdpRemotePort);
            this.GroupBoxRemoteUDPServer.Controls.Add(this.TextBoxUdpRemotePort);
            this.GroupBoxRemoteUDPServer.Controls.Add(this.CheckBoxRemoteUdpServer);
            this.GroupBoxRemoteUDPServer.Location = new System.Drawing.Point(112, 3);
            this.GroupBoxRemoteUDPServer.Name = "GroupBoxRemoteUDPServer";
            this.GroupBoxRemoteUDPServer.Size = new System.Drawing.Size(189, 91);
            this.GroupBoxRemoteUDPServer.TabIndex = 4;
            // 
            // LabelMulticastSource
            // 
            this.LabelMulticastSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance44.ForeColor = System.Drawing.SystemColors.HotTrack;
            appearance44.TextHAlignAsString = "Center";
            appearance44.TextVAlignAsString = "Middle";
            this.LabelMulticastSource.Appearance = appearance44;
            this.LabelMulticastSource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelMulticastSource.Enabled = false;
            this.LabelMulticastSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            appearance45.FontData.UnderlineAsString = "True";
            this.LabelMulticastSource.HotTrackAppearance = appearance45;
            this.LabelMulticastSource.Location = new System.Drawing.Point(128, 58);
            this.LabelMulticastSource.Name = "LabelMulticastSource";
            this.LabelMulticastSource.Size = new System.Drawing.Size(58, 27);
            this.LabelMulticastSource.TabIndex = 5;
            this.LabelMulticastSource.Text = "Multicast Source";
            this.LabelMulticastSource.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            this.LabelMulticastSource.Click += new System.EventHandler(this.LabelMulticastSource_Click);
            // 
            // LabelUdpHostIP
            // 
            appearance46.TextHAlignAsString = "Right";
            this.LabelUdpHostIP.Appearance = appearance46;
            this.LabelUdpHostIP.Enabled = false;
            this.LabelUdpHostIP.Location = new System.Drawing.Point(30, 36);
            this.LabelUdpHostIP.Name = "LabelUdpHostIP";
            this.LabelUdpHostIP.Size = new System.Drawing.Size(45, 23);
            this.LabelUdpHostIP.TabIndex = 1;
            this.LabelUdpHostIP.Text = "Host IP:";
            ultraToolTipInfo4.ToolTipText = "Enter host DNS name, IPv4 or IPv6 address";
            this.ToolTipManager.SetUltraToolTip(this.LabelUdpHostIP, ultraToolTipInfo4);
            // 
            // TextBoxUdpHostIP
            // 
            this.TextBoxUdpHostIP.AutoSize = false;
            this.TextBoxUdpHostIP.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxUdpHostIP.Enabled = false;
            this.TextBoxUdpHostIP.InputMask = "nnn\\.nnn\\.nnn\\.nnn";
            this.TextBoxUdpHostIP.Location = new System.Drawing.Point(81, 33);
            this.TextBoxUdpHostIP.Name = "TextBoxUdpHostIP";
            this.TextBoxUdpHostIP.PromptChar = ' ';
            this.TextBoxUdpHostIP.Size = new System.Drawing.Size(92, 20);
            this.TextBoxUdpHostIP.TabIndex = 2;
            this.TextBoxUdpHostIP.Text = "127.0.0.1";
            this.TextBoxUdpHostIP.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            // 
            // LabelUdpRemotePort
            // 
            appearance47.TextHAlignAsString = "Right";
            this.LabelUdpRemotePort.Appearance = appearance47;
            this.LabelUdpRemotePort.Enabled = false;
            this.LabelUdpRemotePort.Location = new System.Drawing.Point(4, 62);
            this.LabelUdpRemotePort.Name = "LabelUdpRemotePort";
            this.LabelUdpRemotePort.Size = new System.Drawing.Size(71, 23);
            this.LabelUdpRemotePort.TabIndex = 3;
            this.LabelUdpRemotePort.Text = "Remote Port:";
            // 
            // TextBoxUdpRemotePort
            // 
            this.TextBoxUdpRemotePort.AutoSize = false;
            this.TextBoxUdpRemotePort.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxUdpRemotePort.Enabled = false;
            this.TextBoxUdpRemotePort.InputMask = "-nnnnn";
            this.TextBoxUdpRemotePort.Location = new System.Drawing.Point(81, 59);
            this.TextBoxUdpRemotePort.Name = "TextBoxUdpRemotePort";
            this.TextBoxUdpRemotePort.PromptChar = ' ';
            this.TextBoxUdpRemotePort.Size = new System.Drawing.Size(45, 20);
            this.TextBoxUdpRemotePort.TabIndex = 4;
            this.TextBoxUdpRemotePort.Text = "5000";
            this.TextBoxUdpRemotePort.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            this.TextBoxUdpRemotePort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            // 
            // CheckBoxRemoteUdpServer
            // 
            this.CheckBoxRemoteUdpServer.Location = new System.Drawing.Point(8, 4);
            this.CheckBoxRemoteUdpServer.Name = "CheckBoxRemoteUdpServer";
            this.CheckBoxRemoteUdpServer.Size = new System.Drawing.Size(178, 26);
            this.CheckBoxRemoteUdpServer.TabIndex = 0;
            this.CheckBoxRemoteUdpServer.Text = "Enable Multicast / Remote Udp";
            this.CheckBoxRemoteUdpServer.CheckedChanged += new System.EventHandler(this.CheckBoxRemoteUDPServer_CheckedChanged);
            // 
            // LabelUdpLocalPort
            // 
            appearance48.TextHAlignAsString = "Right";
            this.LabelUdpLocalPort.Appearance = appearance48;
            this.LabelUdpLocalPort.Location = new System.Drawing.Point(-4, 21);
            this.LabelUdpLocalPort.Name = "LabelUdpLocalPort";
            this.LabelUdpLocalPort.Size = new System.Drawing.Size(63, 23);
            this.LabelUdpLocalPort.TabIndex = 0;
            this.LabelUdpLocalPort.Text = "Local P&ort:";
            // 
            // TextBoxUdpLocalPort
            // 
            this.TextBoxUdpLocalPort.AutoSize = false;
            this.TextBoxUdpLocalPort.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxUdpLocalPort.InputMask = "-nnnnn";
            this.TextBoxUdpLocalPort.Location = new System.Drawing.Point(61, 18);
            this.TextBoxUdpLocalPort.Name = "TextBoxUdpLocalPort";
            this.TextBoxUdpLocalPort.PromptChar = ' ';
            this.TextBoxUdpLocalPort.Size = new System.Drawing.Size(45, 20);
            this.TextBoxUdpLocalPort.TabIndex = 1;
            this.TextBoxUdpLocalPort.Text = "4712";
            this.TextBoxUdpLocalPort.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            this.TextBoxUdpLocalPort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            // 
            // TabPageControl3
            // 
            this.TabPageControl3.Controls.Add(this.CheckBoxSerialRTS);
            this.TabPageControl3.Controls.Add(this.CheckBoxSerialDTR);
            this.TabPageControl3.Controls.Add(this.TextBoxSerialDataBits);
            this.TabPageControl3.Controls.Add(this.ComboBoxSerialStopBits);
            this.TabPageControl3.Controls.Add(this.ComboBoxSerialParities);
            this.TabPageControl3.Controls.Add(this.LabelSerialParity);
            this.TabPageControl3.Controls.Add(this.ComboBoxSerialBaudRates);
            this.TabPageControl3.Controls.Add(this.LabelSerialBaudRate);
            this.TabPageControl3.Controls.Add(this.ComboBoxSerialPorts);
            this.TabPageControl3.Controls.Add(this.LabelSerialPort);
            this.TabPageControl3.Controls.Add(this.LabelSerialStopBits);
            this.TabPageControl3.Controls.Add(this.LabelSerialDataBits);
            this.TabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
            this.TabPageControl3.Name = "TabPageControl3";
            this.TabPageControl3.Size = new System.Drawing.Size(304, 96);
            // 
            // CheckBoxSerialRTS
            // 
            this.CheckBoxSerialRTS.Location = new System.Drawing.Point(225, 62);
            this.CheckBoxSerialRTS.Name = "CheckBoxSerialRTS";
            this.CheckBoxSerialRTS.Size = new System.Drawing.Size(50, 26);
            this.CheckBoxSerialRTS.TabIndex = 11;
            this.CheckBoxSerialRTS.Text = "RTS";
            ultraToolTipInfo5.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            ultraToolTipInfo5.ToolTipText = "This enables the Request to Send (RTS) signal during serial communication.";
            ultraToolTipInfo5.ToolTipTitle = "Note";
            this.ToolTipManager.SetUltraToolTip(this.CheckBoxSerialRTS, ultraToolTipInfo5);
            // 
            // CheckBoxSerialDTR
            // 
            this.CheckBoxSerialDTR.Location = new System.Drawing.Point(169, 62);
            this.CheckBoxSerialDTR.Name = "CheckBoxSerialDTR";
            this.CheckBoxSerialDTR.Size = new System.Drawing.Size(50, 26);
            this.CheckBoxSerialDTR.TabIndex = 10;
            this.CheckBoxSerialDTR.Text = "DTR";
            ultraToolTipInfo6.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            ultraToolTipInfo6.ToolTipText = "This enables the Data Terminal Ready (DTR) signal during serial communication.";
            ultraToolTipInfo6.ToolTipTitle = "Note";
            this.ToolTipManager.SetUltraToolTip(this.CheckBoxSerialDTR, ultraToolTipInfo6);
            // 
            // TextBoxSerialDataBits
            // 
            this.TextBoxSerialDataBits.AutoSize = false;
            this.TextBoxSerialDataBits.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxSerialDataBits.InputMask = "nnn";
            this.TextBoxSerialDataBits.Location = new System.Drawing.Point(211, 37);
            this.TextBoxSerialDataBits.Name = "TextBoxSerialDataBits";
            this.TextBoxSerialDataBits.PromptChar = ' ';
            this.TextBoxSerialDataBits.Size = new System.Drawing.Size(27, 20);
            this.TextBoxSerialDataBits.TabIndex = 9;
            this.TextBoxSerialDataBits.Text = "8";
            this.TextBoxSerialDataBits.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            this.TextBoxSerialDataBits.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            // 
            // ComboBoxSerialStopBits
            // 
            this.ComboBoxSerialStopBits.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxSerialStopBits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxSerialStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxSerialStopBits.Location = new System.Drawing.Point(211, 10);
            this.ComboBoxSerialStopBits.Name = "ComboBoxSerialStopBits";
            this.ComboBoxSerialStopBits.Size = new System.Drawing.Size(85, 21);
            this.ComboBoxSerialStopBits.TabIndex = 7;
            // 
            // ComboBoxSerialParities
            // 
            this.ComboBoxSerialParities.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxSerialParities.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxSerialParities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxSerialParities.Location = new System.Drawing.Point(66, 65);
            this.ComboBoxSerialParities.Name = "ComboBoxSerialParities";
            this.ComboBoxSerialParities.Size = new System.Drawing.Size(85, 21);
            this.ComboBoxSerialParities.TabIndex = 5;
            // 
            // LabelSerialParity
            // 
            appearance49.TextHAlignAsString = "Right";
            this.LabelSerialParity.Appearance = appearance49;
            this.LabelSerialParity.Location = new System.Drawing.Point(-4, 68);
            this.LabelSerialParity.Name = "LabelSerialParity";
            this.LabelSerialParity.Size = new System.Drawing.Size(64, 23);
            this.LabelSerialParity.TabIndex = 4;
            this.LabelSerialParity.Text = "Par&ity:";
            // 
            // ComboBoxSerialBaudRates
            // 
            this.ComboBoxSerialBaudRates.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxSerialBaudRates.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxSerialBaudRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxSerialBaudRates.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "19200",
            "9600",
            "4800",
            "2400",
            "1200"});
            this.ComboBoxSerialBaudRates.Location = new System.Drawing.Point(66, 37);
            this.ComboBoxSerialBaudRates.Name = "ComboBoxSerialBaudRates";
            this.ComboBoxSerialBaudRates.Size = new System.Drawing.Size(85, 21);
            this.ComboBoxSerialBaudRates.TabIndex = 3;
            // 
            // LabelSerialBaudRate
            // 
            appearance50.TextHAlignAsString = "Right";
            this.LabelSerialBaudRate.Appearance = appearance50;
            this.LabelSerialBaudRate.Location = new System.Drawing.Point(-4, 40);
            this.LabelSerialBaudRate.Name = "LabelSerialBaudRate";
            this.LabelSerialBaudRate.Size = new System.Drawing.Size(64, 23);
            this.LabelSerialBaudRate.TabIndex = 2;
            this.LabelSerialBaudRate.Text = "&Baud Rate:";
            // 
            // ComboBoxSerialPorts
            // 
            this.ComboBoxSerialPorts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxSerialPorts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxSerialPorts.Location = new System.Drawing.Point(66, 10);
            this.ComboBoxSerialPorts.Name = "ComboBoxSerialPorts";
            this.ComboBoxSerialPorts.Size = new System.Drawing.Size(85, 21);
            this.ComboBoxSerialPorts.TabIndex = 1;
            // 
            // LabelSerialPort
            // 
            appearance51.TextHAlignAsString = "Right";
            this.LabelSerialPort.Appearance = appearance51;
            this.LabelSerialPort.Location = new System.Drawing.Point(-4, 13);
            this.LabelSerialPort.Name = "LabelSerialPort";
            this.LabelSerialPort.Size = new System.Drawing.Size(64, 23);
            this.LabelSerialPort.TabIndex = 0;
            this.LabelSerialPort.Text = "P&ort:";
            // 
            // LabelSerialStopBits
            // 
            appearance52.TextHAlignAsString = "Right";
            this.LabelSerialStopBits.Appearance = appearance52;
            this.LabelSerialStopBits.Location = new System.Drawing.Point(150, 13);
            this.LabelSerialStopBits.Name = "LabelSerialStopBits";
            this.LabelSerialStopBits.Size = new System.Drawing.Size(57, 23);
            this.LabelSerialStopBits.TabIndex = 6;
            this.LabelSerialStopBits.Text = "Stop Bits:";
            // 
            // LabelSerialDataBits
            // 
            appearance53.TextHAlignAsString = "Right";
            this.LabelSerialDataBits.Appearance = appearance53;
            this.LabelSerialDataBits.Location = new System.Drawing.Point(148, 40);
            this.LabelSerialDataBits.Name = "LabelSerialDataBits";
            this.LabelSerialDataBits.Size = new System.Drawing.Size(59, 23);
            this.LabelSerialDataBits.TabIndex = 8;
            this.LabelSerialDataBits.Text = "Data Bits:";
            // 
            // TabPageControl4
            // 
            this.TabPageControl4.Controls.Add(this.CheckBoxAutoRepeatPlayback);
            this.TabPageControl4.Controls.Add(this.LabelFrameRate);
            this.TabPageControl4.Controls.Add(this.TextBoxFileFrameRate);
            this.TabPageControl4.Controls.Add(this.ButtonBrowse);
            this.TabPageControl4.Controls.Add(this.TextBoxFileCaptureName);
            this.TabPageControl4.Controls.Add(this.LabelCaptureFile);
            this.TabPageControl4.Controls.Add(this.LabelFramesPerSecond);
            this.TabPageControl4.Controls.Add(this.LabelReplayCapturedFile);
            this.TabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
            this.TabPageControl4.Name = "TabPageControl4";
            this.TabPageControl4.Size = new System.Drawing.Size(304, 96);
            // 
            // CheckBoxAutoRepeatPlayback
            // 
            this.CheckBoxAutoRepeatPlayback.Checked = true;
            this.CheckBoxAutoRepeatPlayback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxAutoRepeatPlayback.Location = new System.Drawing.Point(73, 72);
            this.CheckBoxAutoRepeatPlayback.Name = "CheckBoxAutoRepeatPlayback";
            this.CheckBoxAutoRepeatPlayback.Size = new System.Drawing.Size(217, 26);
            this.CheckBoxAutoRepeatPlayback.TabIndex = 7;
            this.CheckBoxAutoRepeatPlayback.Text = "Auto-repeat captured file playback";
            this.CheckBoxAutoRepeatPlayback.CheckStateChanged += new System.EventHandler(this.CheckBoxAutoRepeatPlayback_CheckChanged);
            // 
            // LabelFrameRate
            // 
            appearance54.TextHAlignAsString = "Right";
            this.LabelFrameRate.Appearance = appearance54;
            this.LabelFrameRate.Location = new System.Drawing.Point(-2, 53);
            this.LabelFrameRate.Name = "LabelFrameRate";
            this.LabelFrameRate.Size = new System.Drawing.Size(69, 23);
            this.LabelFrameRate.TabIndex = 3;
            this.LabelFrameRate.Text = "Frame Rate:";
            // 
            // TextBoxFileFrameRate
            // 
            this.TextBoxFileFrameRate.AutoSize = false;
            this.TextBoxFileFrameRate.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.TextBoxFileFrameRate.InputMask = "nnnnn";
            this.TextBoxFileFrameRate.Location = new System.Drawing.Point(73, 50);
            this.TextBoxFileFrameRate.Name = "TextBoxFileFrameRate";
            this.TextBoxFileFrameRate.PromptChar = ' ';
            this.TextBoxFileFrameRate.Size = new System.Drawing.Size(40, 20);
            this.TextBoxFileFrameRate.TabIndex = 4;
            this.TextBoxFileFrameRate.Text = "30";
            this.TextBoxFileFrameRate.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            this.TextBoxFileFrameRate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            // 
            // ButtonBrowse
            // 
            this.ButtonBrowse.Location = new System.Drawing.Point(215, 19);
            this.ButtonBrowse.Name = "ButtonBrowse";
            this.ButtonBrowse.Size = new System.Drawing.Size(75, 23);
            this.ButtonBrowse.TabIndex = 2;
            this.ButtonBrowse.Text = "&Browse...";
            this.ButtonBrowse.UseVisualStyleBackColor = true;
            this.ButtonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // TextBoxFileCaptureName
            // 
            this.TextBoxFileCaptureName.BackColor = System.Drawing.Color.White;
            this.TextBoxFileCaptureName.Location = new System.Drawing.Point(73, 21);
            this.TextBoxFileCaptureName.Name = "TextBoxFileCaptureName";
            this.TextBoxFileCaptureName.Size = new System.Drawing.Size(136, 20);
            this.TextBoxFileCaptureName.TabIndex = 1;
            this.TextBoxFileCaptureName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseClick);
            this.TextBoxFileCaptureName.TextChanged += new System.EventHandler(this.TextBoxFileCaptureName_TextChanged);
            this.TextBoxFileCaptureName.GotFocus += new System.EventHandler(this.TextBox_GotFocus);
            // 
            // LabelCaptureFile
            // 
            appearance55.TextHAlignAsString = "Right";
            this.LabelCaptureFile.Appearance = appearance55;
            this.LabelCaptureFile.Location = new System.Drawing.Point(13, 24);
            this.LabelCaptureFile.Name = "LabelCaptureFile";
            this.LabelCaptureFile.Size = new System.Drawing.Size(54, 23);
            this.LabelCaptureFile.TabIndex = 0;
            this.LabelCaptureFile.Text = "F&ilename:";
            // 
            // LabelFramesPerSecond
            // 
            appearance56.TextHAlignAsString = "Right";
            this.LabelFramesPerSecond.Appearance = appearance56;
            this.LabelFramesPerSecond.Location = new System.Drawing.Point(110, 53);
            this.LabelFramesPerSecond.Name = "LabelFramesPerSecond";
            this.LabelFramesPerSecond.Size = new System.Drawing.Size(82, 17);
            this.LabelFramesPerSecond.TabIndex = 5;
            this.LabelFramesPerSecond.Text = "frames/second";
            // 
            // LabelReplayCapturedFile
            // 
            appearance57.FontData.ItalicAsString = "True";
            this.LabelReplayCapturedFile.Appearance = appearance57;
            this.LabelReplayCapturedFile.Location = new System.Drawing.Point(193, 4);
            this.LabelReplayCapturedFile.Name = "LabelReplayCapturedFile";
            this.LabelReplayCapturedFile.Size = new System.Drawing.Size(120, 17);
            this.LabelReplayCapturedFile.TabIndex = 6;
            this.LabelReplayCapturedFile.Text = "Replay captured file...";
            // 
            // TabPageControl5
            // 
            this.TabPageControl5.Controls.Add(this.ChartDataDisplay);
            this.TabPageControl5.Location = new System.Drawing.Point(1, 1);
            this.TabPageControl5.Name = "TabPageControl5";
            this.TabPageControl5.Size = new System.Drawing.Size(312, 223);
            // 
            // ChartDataDisplay
            // 
            this.ChartDataDisplay.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            paintElement1.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.ChartDataDisplay.Axis.PE = paintElement1;
            this.ChartDataDisplay.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ChartDataDisplay.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ChartDataDisplay.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ChartDataDisplay.Axis.X.Labels.SeriesLabels.FormatString = "";
            this.ChartDataDisplay.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ChartDataDisplay.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ChartDataDisplay.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.X.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ChartDataDisplay.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.X.MajorGridLines.Visible = true;
            this.ChartDataDisplay.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ChartDataDisplay.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.X.MinorGridLines.Visible = false;
            this.ChartDataDisplay.Axis.X.Visible = true;
            this.ChartDataDisplay.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ChartDataDisplay.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ChartDataDisplay.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ChartDataDisplay.Axis.X2.Labels.SeriesLabels.FormatString = "";
            this.ChartDataDisplay.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ChartDataDisplay.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ChartDataDisplay.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.X2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ChartDataDisplay.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.X2.MajorGridLines.Visible = true;
            this.ChartDataDisplay.Axis.X2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ChartDataDisplay.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.X2.MinorGridLines.Visible = false;
            this.ChartDataDisplay.Axis.X2.Visible = false;
            this.ChartDataDisplay.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ChartDataDisplay.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ChartDataDisplay.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ChartDataDisplay.Axis.Y.Labels.SeriesLabels.FormatString = "";
            this.ChartDataDisplay.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ChartDataDisplay.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ChartDataDisplay.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.Y.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ChartDataDisplay.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.Y.MajorGridLines.Visible = true;
            this.ChartDataDisplay.Axis.Y.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ChartDataDisplay.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.Y.MinorGridLines.Visible = false;
            this.ChartDataDisplay.Axis.Y.Visible = true;
            this.ChartDataDisplay.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ChartDataDisplay.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ChartDataDisplay.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ChartDataDisplay.Axis.Y2.Labels.SeriesLabels.FormatString = "";
            this.ChartDataDisplay.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ChartDataDisplay.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ChartDataDisplay.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.Y2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ChartDataDisplay.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.Y2.MajorGridLines.Visible = true;
            this.ChartDataDisplay.Axis.Y2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ChartDataDisplay.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.Y2.MinorGridLines.Visible = false;
            this.ChartDataDisplay.Axis.Y2.Visible = false;
            this.ChartDataDisplay.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ChartDataDisplay.Axis.Z.Labels.ItemFormatString = "";
            this.ChartDataDisplay.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ChartDataDisplay.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ChartDataDisplay.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ChartDataDisplay.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.Z.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ChartDataDisplay.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.Z.MajorGridLines.Visible = true;
            this.ChartDataDisplay.Axis.Z.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ChartDataDisplay.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.Z.MinorGridLines.Visible = false;
            this.ChartDataDisplay.Axis.Z.Visible = false;
            this.ChartDataDisplay.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ChartDataDisplay.Axis.Z2.Labels.ItemFormatString = "";
            this.ChartDataDisplay.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ChartDataDisplay.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ChartDataDisplay.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ChartDataDisplay.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.Axis.Z2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ChartDataDisplay.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.Z2.MajorGridLines.Visible = true;
            this.ChartDataDisplay.Axis.Z2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ChartDataDisplay.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ChartDataDisplay.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ChartDataDisplay.Axis.Z2.MinorGridLines.Visible = false;
            this.ChartDataDisplay.Axis.Z2.Visible = false;
            this.ChartDataDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ChartDataDisplay.Border.Color = System.Drawing.Color.Transparent;
            this.ChartDataDisplay.Border.CornerRadius = 2;
            this.ChartDataDisplay.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.Composite;
            this.ChartDataDisplay.ColorModel.AlphaLevel = ((byte)(150));
            columnLineChartAppearance1.Column.SeriesSpacing = 0;
            this.ChartDataDisplay.ColumnLineChart = columnLineChartAppearance1;
            this.ChartDataDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartDataDisplay.DoughnutChart3D = doughnutChartAppearance1;
            this.ChartDataDisplay.EmptyChartText = "Intializing...";
            this.ChartDataDisplay.ForeColor = System.Drawing.SystemColors.ControlText;
            paintElement2.Fill = System.Drawing.Color.Yellow;
            ganttChartAppearance1.CompletePercentagesPE = paintElement2;
            paintElement3.Fill = System.Drawing.Color.White;
            ganttChartAppearance1.EmptyPercentagesPE = paintElement3;
            ganttChartAppearance1.LinkLineStyle.EndStyle = Infragistics.UltraChart.Shared.Styles.LineCapStyle.ArrowAnchor;
            ganttChartAppearance1.OwnersLabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.ChartDataDisplay.GanttChart = ganttChartAppearance1;
            this.ChartDataDisplay.Location = new System.Drawing.Point(0, 0);
            this.ChartDataDisplay.Name = "ChartDataDisplay";
            radarChartAppearance1.SplineTension = 0F;
            this.ChartDataDisplay.RadarChart = radarChartAppearance1;
            this.ChartDataDisplay.Size = new System.Drawing.Size(312, 223);
            this.ChartDataDisplay.SplineAreaChart3D = splineAreaChart3DAppearance1;
            this.ChartDataDisplay.SplineChart3D = splineChart3DAppearance1;
            this.ChartDataDisplay.TabIndex = 8;
            this.ChartDataDisplay.TitleTop.Extent = 30;
            this.ChartDataDisplay.TitleTop.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ChartDataDisplay.TitleTop.Margins.Bottom = 1;
            this.ChartDataDisplay.TitleTop.Margins.Top = 1;
            this.ChartDataDisplay.Tooltips.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            // 
            // TabPageControl6
            // 
            this.TabPageControl6.Controls.Add(this.ButtonRestoreDefaultSettings);
            this.TabPageControl6.Controls.Add(this.GroupBoxPowerVarCalculations);
            this.TabPageControl6.Controls.Add(this.PropertyGridApplicationSettings);
            this.TabPageControl6.Controls.Add(this.ButtonGetStatus);
            this.TabPageControl6.Location = new System.Drawing.Point(-10000, -10000);
            this.TabPageControl6.Name = "TabPageControl6";
            this.TabPageControl6.Size = new System.Drawing.Size(312, 223);
            // 
            // ButtonRestoreDefaultSettings
            // 
            this.ButtonRestoreDefaultSettings.Location = new System.Drawing.Point(11, 166);
            this.ButtonRestoreDefaultSettings.Name = "ButtonRestoreDefaultSettings";
            this.ButtonRestoreDefaultSettings.Size = new System.Drawing.Size(135, 23);
            this.ButtonRestoreDefaultSettings.TabIndex = 2;
            this.ButtonRestoreDefaultSettings.Text = "Restore Defaults";
            this.ButtonRestoreDefaultSettings.Click += new System.EventHandler(this.ButtonRestoreDefaultSettings_Click);
            // 
            // GroupBoxPowerVarCalculations
            // 
            this.GroupBoxPowerVarCalculations.Controls.Add(this.ComboBoxCurrentPhasors);
            this.GroupBoxPowerVarCalculations.Controls.Add(this.LabelCurrentPhasor);
            this.GroupBoxPowerVarCalculations.Controls.Add(this.ComboBoxVoltagePhasors);
            this.GroupBoxPowerVarCalculations.Controls.Add(this.LabelVoltagePhasor);
            this.GroupBoxPowerVarCalculations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxPowerVarCalculations.Location = new System.Drawing.Point(5, 6);
            this.GroupBoxPowerVarCalculations.Name = "GroupBoxPowerVarCalculations";
            this.GroupBoxPowerVarCalculations.Size = new System.Drawing.Size(147, 121);
            this.GroupBoxPowerVarCalculations.TabIndex = 0;
            this.GroupBoxPowerVarCalculations.Text = "Power/Var Calculations";
            // 
            // ComboBoxCurrentPhasors
            // 
            this.ComboBoxCurrentPhasors.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxCurrentPhasors.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxCurrentPhasors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCurrentPhasors.FormattingEnabled = true;
            this.ComboBoxCurrentPhasors.Location = new System.Drawing.Point(6, 84);
            this.ComboBoxCurrentPhasors.Name = "ComboBoxCurrentPhasors";
            this.ComboBoxCurrentPhasors.Size = new System.Drawing.Size(135, 21);
            this.ComboBoxCurrentPhasors.TabIndex = 3;
            // 
            // LabelCurrentPhasor
            // 
            this.LabelCurrentPhasor.Location = new System.Drawing.Point(6, 67);
            this.LabelCurrentPhasor.Name = "LabelCurrentPhasor";
            this.LabelCurrentPhasor.Size = new System.Drawing.Size(131, 23);
            this.LabelCurrentPhasor.TabIndex = 2;
            this.LabelCurrentPhasor.Text = "Cu&rrent Phasor: ";
            // 
            // ComboBoxVoltagePhasors
            // 
            this.ComboBoxVoltagePhasors.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxVoltagePhasors.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxVoltagePhasors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVoltagePhasors.FormattingEnabled = true;
            this.ComboBoxVoltagePhasors.Location = new System.Drawing.Point(6, 40);
            this.ComboBoxVoltagePhasors.Name = "ComboBoxVoltagePhasors";
            this.ComboBoxVoltagePhasors.Size = new System.Drawing.Size(135, 21);
            this.ComboBoxVoltagePhasors.TabIndex = 1;
            // 
            // LabelVoltagePhasor
            // 
            this.LabelVoltagePhasor.Location = new System.Drawing.Point(6, 23);
            this.LabelVoltagePhasor.Name = "LabelVoltagePhasor";
            this.LabelVoltagePhasor.Size = new System.Drawing.Size(131, 23);
            this.LabelVoltagePhasor.TabIndex = 0;
            this.LabelVoltagePhasor.Text = "&Voltage Phasor: ";
            // 
            // PropertyGridApplicationSettings
            // 
            this.PropertyGridApplicationSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertyGridApplicationSettings.Location = new System.Drawing.Point(158, 6);
            this.PropertyGridApplicationSettings.Name = "PropertyGridApplicationSettings";
            this.PropertyGridApplicationSettings.Size = new System.Drawing.Size(151, 214);
            this.PropertyGridApplicationSettings.TabIndex = 3;
            this.PropertyGridApplicationSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridApplicationSettings_PropertyValueChanged);
            // 
            // ButtonGetStatus
            // 
            this.ButtonGetStatus.Location = new System.Drawing.Point(11, 137);
            this.ButtonGetStatus.Name = "ButtonGetStatus";
            this.ButtonGetStatus.Size = new System.Drawing.Size(135, 23);
            this.ButtonGetStatus.TabIndex = 1;
            this.ButtonGetStatus.Text = "Get Parsing Status";
            this.ButtonGetStatus.Click += new System.EventHandler(this.ButtonGetStatus_Click);
            // 
            // TabPageControl7
            // 
            this.TabPageControl7.Controls.Add(this.TextBoxMessages);
            this.TabPageControl7.Location = new System.Drawing.Point(-10000, -10000);
            this.TabPageControl7.Name = "TabPageControl7";
            this.TabPageControl7.Size = new System.Drawing.Size(312, 223);
            // 
            // TextBoxMessages
            // 
            this.TextBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxMessages.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxMessages.Location = new System.Drawing.Point(0, 0);
            this.TextBoxMessages.MaxLength = 262144;
            this.TextBoxMessages.Multiline = true;
            this.TextBoxMessages.Name = "TextBoxMessages";
            this.TextBoxMessages.ReadOnly = true;
            this.TextBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBoxMessages.Size = new System.Drawing.Size(312, 223);
            this.TextBoxMessages.TabIndex = 0;
            // 
            // TabPageControl8
            // 
            this.TabPageControl8.Controls.Add(this.TreeFrameAttributes);
            this.TabPageControl8.Location = new System.Drawing.Point(-10000, -10000);
            this.TabPageControl8.Name = "TabPageControl8";
            this.TabPageControl8.Size = new System.Drawing.Size(312, 223);
            // 
            // TreeFrameAttributes
            // 
            this.TreeFrameAttributes.ColumnSettings.AllowCellEdit = Infragistics.Win.UltraWinTree.AllowCellEdit.ReadOnly;
            this.TreeFrameAttributes.ColumnSettings.AllowSorting = Infragistics.Win.DefaultableBoolean.False;
            this.TreeFrameAttributes.ContextMenuStrip = this.ContextMenuForTree;
            this.TreeFrameAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeFrameAttributes.Location = new System.Drawing.Point(0, 0);
            this.TreeFrameAttributes.Name = "TreeFrameAttributes";
            this.TreeFrameAttributes.SettingsKey = "";
            this.TreeFrameAttributes.Size = new System.Drawing.Size(312, 223);
            this.TreeFrameAttributes.TabIndex = 0;
            this.TreeFrameAttributes.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.FreeForm;
            this.TreeFrameAttributes.InitializeDataNode += new Infragistics.Win.UltraWinTree.InitializeDataNodeEventHandler(this.TreeFrameAttributes_InitializeDataNode);
            this.TreeFrameAttributes.BeforeDataNodesCollectionPopulated += new Infragistics.Win.UltraWinTree.BeforeDataNodesCollectionPopulatedEventHandler(this.TreeFrameAttributes_BeforeDataNodesCollectionPopulated);
            this.TreeFrameAttributes.AfterDataNodesCollectionPopulated += new Infragistics.Win.UltraWinTree.AfterDataNodesCollectionPopulatedEventHandler(this.TreeFrameAttributes_AfterDataNodesCollectionPopulated);
            this.TreeFrameAttributes.ColumnSetGenerated += new Infragistics.Win.UltraWinTree.ColumnSetGeneratedEventHandler(this.TreeFrameAttributes_ColumnSetGenerated);
            this.TreeFrameAttributes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TreeFrameAttributes_MouseClick);
            this.TreeFrameAttributes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TreeFrameAttributes_MouseMove);
            // 
            // ContextMenuForTree
            // 
            this.ContextMenuForTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemRefresh,
            this.ToolStripSeparator5,
            this.MenuItemExpandAll,
            this.MenuItemCollapseAll});
            this.ContextMenuForTree.Name = "ContextMenuForTree";
            this.ContextMenuForTree.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ContextMenuForTree.ShowImageMargin = false;
            this.ContextMenuForTree.ShowItemToolTips = false;
            this.ContextMenuForTree.Size = new System.Drawing.Size(112, 76);
            // 
            // MenuItemRefresh
            // 
            this.MenuItemRefresh.Name = "MenuItemRefresh";
            this.MenuItemRefresh.Size = new System.Drawing.Size(111, 22);
            this.MenuItemRefresh.Text = "Refresh";
            this.MenuItemRefresh.Click += new System.EventHandler(this.MenuItemRefresh_Click);
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(108, 6);
            // 
            // MenuItemExpandAll
            // 
            this.MenuItemExpandAll.Name = "MenuItemExpandAll";
            this.MenuItemExpandAll.Size = new System.Drawing.Size(111, 22);
            this.MenuItemExpandAll.Text = "Expand All";
            this.MenuItemExpandAll.Click += new System.EventHandler(this.MenuItemExpandAll_Click);
            // 
            // MenuItemCollapseAll
            // 
            this.MenuItemCollapseAll.Name = "MenuItemCollapseAll";
            this.MenuItemCollapseAll.Size = new System.Drawing.Size(111, 22);
            this.MenuItemCollapseAll.Text = "Collapse All";
            this.MenuItemCollapseAll.Click += new System.EventHandler(this.MenuItemCollapseAll_Click);
            // 
            // GroupBoxConnection
            // 
            this.GroupBoxConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance33.ForeColor = System.Drawing.Color.Black;
            this.GroupBoxConnection.Appearance = appearance33;
            this.GroupBoxConnection.Controls.Add(this.GroupBoxPanelConnection);
            this.GroupBoxConnection.ExpandedSize = new System.Drawing.Size(686, 142);
            appearance58.Image = ((object)(resources.GetObject("appearance58.Image")));
            this.GroupBoxConnection.HeaderAppearance = appearance58;
            this.GroupBoxConnection.Location = new System.Drawing.Point(7, 26);
            this.GroupBoxConnection.Name = "GroupBoxConnection";
            this.GroupBoxConnection.Size = new System.Drawing.Size(686, 142);
            this.GroupBoxConnection.TabIndex = 1;
            this.GroupBoxConnection.Text = "Connection Parameters";
            this.GroupBoxConnection.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(this.GroupBoxConnection_ExpandedStateChanging);
            // 
            // GroupBoxPanelConnection
            // 
            this.GroupBoxPanelConnection.Controls.Add(this.TabControlProtocolParameters);
            this.GroupBoxPanelConnection.Controls.Add(this.TabControlCommunications);
            this.GroupBoxPanelConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxPanelConnection.Location = new System.Drawing.Point(3, 19);
            this.GroupBoxPanelConnection.Name = "GroupBoxPanelConnection";
            this.GroupBoxPanelConnection.Size = new System.Drawing.Size(680, 120);
            this.GroupBoxPanelConnection.TabIndex = 0;
            // 
            // TabControlProtocolParameters
            // 
            this.TabControlProtocolParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance34.ForeColor = System.Drawing.Color.Black;
            this.TabControlProtocolParameters.Appearance = appearance34;
            this.TabControlProtocolParameters.Controls.Add(this.TabSharedControlsPageProtocolParameters);
            this.TabControlProtocolParameters.Controls.Add(this.TabPageControl9);
            this.TabControlProtocolParameters.Controls.Add(this.TabPageControl10);
            this.TabControlProtocolParameters.Location = new System.Drawing.Point(315, 0);
            this.TabControlProtocolParameters.Name = "TabControlProtocolParameters";
            this.TabControlProtocolParameters.SharedControlsPage = this.TabSharedControlsPageProtocolParameters;
            this.TabControlProtocolParameters.Size = new System.Drawing.Size(362, 117);
            this.TabControlProtocolParameters.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            this.TabControlProtocolParameters.TabIndex = 12;
            ultraTab5.TabPage = this.TabPageControl9;
            ultraTab5.Text = "&Protocol";
            ultraTab6.TabPage = this.TabPageControl10;
            ultraTab6.Text = "E&xtra Parameters";
            this.TabControlProtocolParameters.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab5,
            ultraTab6});
            ultraToolTipInfo2.Enabled = Infragistics.Win.DefaultableBoolean.False;
            ultraToolTipInfo2.ToolTipText = "This protocol has additional connection parameters.  Click here to view.";
            this.ToolTipManagerForExtraParameters.SetUltraToolTip(this.TabControlProtocolParameters, ultraToolTipInfo2);
            this.TabControlProtocolParameters.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(this.TabControlProtocolParameters_SelectedTabChanged);
            // 
            // TabSharedControlsPageProtocolParameters
            // 
            this.TabSharedControlsPageProtocolParameters.Location = new System.Drawing.Point(-10000, -10000);
            this.TabSharedControlsPageProtocolParameters.Name = "TabSharedControlsPageProtocolParameters";
            this.TabSharedControlsPageProtocolParameters.Size = new System.Drawing.Size(360, 96);
            // 
            // TabControlCommunications
            // 
            appearance35.ForeColor = System.Drawing.Color.Black;
            this.TabControlCommunications.Appearance = appearance35;
            this.TabControlCommunications.Controls.Add(this.TabSharedControlsPageCommunications);
            this.TabControlCommunications.Controls.Add(this.TabPageControl1);
            this.TabControlCommunications.Controls.Add(this.TabPageControl2);
            this.TabControlCommunications.Controls.Add(this.TabPageControl3);
            this.TabControlCommunications.Controls.Add(this.TabPageControl4);
            this.TabControlCommunications.Location = new System.Drawing.Point(3, 0);
            this.TabControlCommunications.Name = "TabControlCommunications";
            this.TabControlCommunications.SharedControlsPage = this.TabSharedControlsPageCommunications;
            this.TabControlCommunications.Size = new System.Drawing.Size(306, 117);
            this.TabControlCommunications.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            this.TabControlCommunications.TabIndex = 0;
            this.TabControlCommunications.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.SingleRowFixed;
            this.TabControlCommunications.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.TopLeft;
            ultraTab7.TabPage = this.TabPageControl1;
            ultraTab7.Text = "&Tcp";
            ultraTab8.TabPage = this.TabPageControl2;
            ultraTab8.Text = "&Udp";
            ultraTab9.TabPage = this.TabPageControl3;
            ultraTab9.Text = "&Serial";
            ultraTab10.TabPage = this.TabPageControl4;
            ultraTab10.Text = "Fi&le";
            this.TabControlCommunications.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab7,
            ultraTab8,
            ultraTab9,
            ultraTab10});
            // 
            // TabSharedControlsPageCommunications
            // 
            this.TabSharedControlsPageCommunications.Location = new System.Drawing.Point(-10000, -10000);
            this.TabSharedControlsPageCommunications.Name = "TabSharedControlsPageCommunications";
            this.TabSharedControlsPageCommunications.Size = new System.Drawing.Size(304, 96);
            // 
            // GroupBoxStatus
            // 
            this.GroupBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxStatus.Controls.Add(this.GroupBoxPanelStatus);
            this.GroupBoxStatus.ExpandedSize = new System.Drawing.Size(511, 85);
            appearance19.Image = ((object)(resources.GetObject("appearance19.Image")));
            this.GroupBoxStatus.HeaderAppearance = appearance19;
            this.GroupBoxStatus.Location = new System.Drawing.Point(10, 421);
            this.GroupBoxStatus.Name = "GroupBoxStatus";
            this.GroupBoxStatus.Size = new System.Drawing.Size(683, 151);
            this.GroupBoxStatus.TabIndex = 6;
            this.GroupBoxStatus.Text = "Real-time Frame Detail";
            ultraToolTipInfo1.Enabled = Infragistics.Win.DefaultableBoolean.True;
            ultraToolTipInfo1.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            ultraToolTipInfo1.ToolTipText = "Keeping this data window displayed may cause the graph to fall behind real-time";
            ultraToolTipInfo1.ToolTipTitle = "Note";
            this.ToolTipManager.SetUltraToolTip(this.GroupBoxStatus, ultraToolTipInfo1);
            this.GroupBoxStatus.ExpandedStateChanged += new System.EventHandler(this.GroupBoxStatus_ExpandedStateChanged);
            this.GroupBoxStatus.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(this.GroupBoxStatus_ExpandedStateChanging);
            // 
            // GroupBoxPanelStatus
            // 
            this.GroupBoxPanelStatus.Controls.Add(this.LabelBinaryFrameImage);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelDisplayFormat);
            this.GroupBoxPanelStatus.Controls.Add(this.ComboBoxByteEncodingDisplayFormats);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelFrameType);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelFrameTypeLabel);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelFrequency);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelFrequencyLabel);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelMagnitude);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelMagnitudeLabel);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelAngle);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelAngleLabel);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelTime);
            this.GroupBoxPanelStatus.Controls.Add(this.LabelTimeLabel);
            this.GroupBoxPanelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxPanelStatus.Location = new System.Drawing.Point(3, 19);
            this.GroupBoxPanelStatus.Name = "GroupBoxPanelStatus";
            this.GroupBoxPanelStatus.Size = new System.Drawing.Size(677, 129);
            this.GroupBoxPanelStatus.TabIndex = 0;
            // 
            // LabelBinaryFrameImage
            // 
            this.LabelBinaryFrameImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance12.TextHAlignAsString = "Left";
            this.LabelBinaryFrameImage.Appearance = appearance12;
            this.LabelBinaryFrameImage.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.LabelBinaryFrameImage.Location = new System.Drawing.Point(206, 0);
            this.LabelBinaryFrameImage.Name = "LabelBinaryFrameImage";
            this.LabelBinaryFrameImage.Size = new System.Drawing.Size(468, 121);
            this.LabelBinaryFrameImage.TabIndex = 12;
            this.LabelBinaryFrameImage.Text = "Binary Frame Image";
            // 
            // LabelDisplayFormat
            // 
            appearance13.TextHAlignAsString = "Right";
            this.LabelDisplayFormat.Appearance = appearance13;
            this.LabelDisplayFormat.Location = new System.Drawing.Point(-8, 105);
            this.LabelDisplayFormat.Name = "LabelDisplayFormat";
            this.LabelDisplayFormat.Size = new System.Drawing.Size(72, 20);
            this.LabelDisplayFormat.TabIndex = 10;
            this.LabelDisplayFormat.Text = "Disp&lay:";
            // 
            // ComboBoxByteEncodingDisplayFormats
            // 
            this.ComboBoxByteEncodingDisplayFormats.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxByteEncodingDisplayFormats.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxByteEncodingDisplayFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxByteEncodingDisplayFormats.Items.AddRange(new object[] {
            "Hexadecimal",
            "Decimal",
            "Big Endian Binary",
            "Little Endian Binary",
            "Base64",
            "ASCII Extraction"});
            this.ComboBoxByteEncodingDisplayFormats.Location = new System.Drawing.Point(70, 104);
            this.ComboBoxByteEncodingDisplayFormats.Name = "ComboBoxByteEncodingDisplayFormats";
            this.ComboBoxByteEncodingDisplayFormats.Size = new System.Drawing.Size(130, 21);
            this.ComboBoxByteEncodingDisplayFormats.TabIndex = 11;
            this.ComboBoxByteEncodingDisplayFormats.SelectedIndexChanged += new System.EventHandler(this.ComboBoxByteEncodingDisplayFormats_SelectedIndexChanged);
            // 
            // LabelFrameType
            // 
            this.LabelFrameType.Location = new System.Drawing.Point(70, 3);
            this.LabelFrameType.Name = "LabelFrameType";
            this.LabelFrameType.Size = new System.Drawing.Size(150, 20);
            this.LabelFrameType.TabIndex = 1;
            this.LabelFrameType.Text = "undetermined";
            this.LabelFrameType.WrapText = false;
            // 
            // LabelFrameTypeLabel
            // 
            appearance14.TextHAlignAsString = "Right";
            this.LabelFrameTypeLabel.Appearance = appearance14;
            this.LabelFrameTypeLabel.Location = new System.Drawing.Point(-8, 3);
            this.LabelFrameTypeLabel.Name = "LabelFrameTypeLabel";
            this.LabelFrameTypeLabel.Size = new System.Drawing.Size(72, 20);
            this.LabelFrameTypeLabel.TabIndex = 0;
            this.LabelFrameTypeLabel.Text = "Frame Type:";
            // 
            // LabelFrequency
            // 
            this.LabelFrequency.Location = new System.Drawing.Point(70, 44);
            this.LabelFrequency.Name = "LabelFrequency";
            this.LabelFrequency.Size = new System.Drawing.Size(150, 20);
            this.LabelFrequency.TabIndex = 5;
            this.LabelFrequency.Text = "0.0000 Hz";
            this.LabelFrequency.WrapText = false;
            // 
            // LabelFrequencyLabel
            // 
            appearance15.TextHAlignAsString = "Right";
            this.LabelFrequencyLabel.Appearance = appearance15;
            this.LabelFrequencyLabel.Location = new System.Drawing.Point(-8, 44);
            this.LabelFrequencyLabel.Name = "LabelFrequencyLabel";
            this.LabelFrequencyLabel.Size = new System.Drawing.Size(72, 20);
            this.LabelFrequencyLabel.TabIndex = 4;
            this.LabelFrequencyLabel.Text = "Frequency:";
            // 
            // LabelMagnitude
            // 
            this.LabelMagnitude.Location = new System.Drawing.Point(70, 84);
            this.LabelMagnitude.Name = "LabelMagnitude";
            this.LabelMagnitude.Size = new System.Drawing.Size(150, 20);
            this.LabelMagnitude.TabIndex = 9;
            this.LabelMagnitude.Text = "0.0000 kV";
            this.LabelMagnitude.WrapText = false;
            // 
            // LabelMagnitudeLabel
            // 
            appearance16.TextHAlignAsString = "Right";
            this.LabelMagnitudeLabel.Appearance = appearance16;
            this.LabelMagnitudeLabel.Location = new System.Drawing.Point(-8, 84);
            this.LabelMagnitudeLabel.Name = "LabelMagnitudeLabel";
            this.LabelMagnitudeLabel.Size = new System.Drawing.Size(72, 20);
            this.LabelMagnitudeLabel.TabIndex = 8;
            this.LabelMagnitudeLabel.Text = "Magnitude:";
            // 
            // LabelAngle
            // 
            this.LabelAngle.Location = new System.Drawing.Point(70, 64);
            this.LabelAngle.Name = "LabelAngle";
            this.LabelAngle.Size = new System.Drawing.Size(150, 20);
            this.LabelAngle.TabIndex = 7;
            this.LabelAngle.Text = "0.0000°";
            this.LabelAngle.WrapText = false;
            // 
            // LabelAngleLabel
            // 
            appearance17.TextHAlignAsString = "Right";
            this.LabelAngleLabel.Appearance = appearance17;
            this.LabelAngleLabel.Location = new System.Drawing.Point(-8, 64);
            this.LabelAngleLabel.Name = "LabelAngleLabel";
            this.LabelAngleLabel.Size = new System.Drawing.Size(72, 20);
            this.LabelAngleLabel.TabIndex = 6;
            this.LabelAngleLabel.Text = "Angle:";
            // 
            // LabelTime
            // 
            this.LabelTime.Location = new System.Drawing.Point(70, 24);
            this.LabelTime.Name = "LabelTime";
            this.LabelTime.Size = new System.Drawing.Size(150, 20);
            this.LabelTime.TabIndex = 3;
            this.LabelTime.Text = "undetermined";
            this.LabelTime.WrapText = false;
            // 
            // LabelTimeLabel
            // 
            appearance18.TextHAlignAsString = "Right";
            this.LabelTimeLabel.Appearance = appearance18;
            this.LabelTimeLabel.Location = new System.Drawing.Point(-8, 24);
            this.LabelTimeLabel.Name = "LabelTimeLabel";
            this.LabelTimeLabel.Size = new System.Drawing.Size(72, 20);
            this.LabelTimeLabel.TabIndex = 2;
            this.LabelTimeLabel.Text = "Time:";
            // 
            // StatusBar
            // 
            this.StatusBar.InterPanelSpacing = 1;
            this.StatusBar.Location = new System.Drawing.Point(0, 578);
            this.StatusBar.Name = "StatusBar";
            ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel1.Text = "Total frames:";
            ultraStatusPanel1.WrapText = Infragistics.Win.DefaultableBoolean.False;
            appearance29.TextHAlignAsString = "Left";
            ultraStatusPanel2.Appearance = appearance29;
            ultraStatusPanel2.MinWidth = 0;
            ultraStatusPanel2.Padding = new System.Drawing.Size(2, 0);
            ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel2.Text = "0";
            ultraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel3.Text = "Frames/sec:";
            ultraStatusPanel3.WrapText = Infragistics.Win.DefaultableBoolean.False;
            appearance30.TextHAlignAsString = "Left";
            ultraStatusPanel4.Appearance = appearance30;
            ultraStatusPanel4.MinWidth = 0;
            ultraStatusPanel4.Padding = new System.Drawing.Size(2, 0);
            ultraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel4.Text = "0.0000";
            ultraStatusPanel4.WrapText = Infragistics.Win.DefaultableBoolean.False;
            ultraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel5.Text = "Total bytes:";
            ultraStatusPanel5.WrapText = Infragistics.Win.DefaultableBoolean.False;
            appearance31.TextHAlignAsString = "Left";
            ultraStatusPanel6.Appearance = appearance31;
            ultraStatusPanel6.MinWidth = 0;
            ultraStatusPanel6.Padding = new System.Drawing.Size(2, 0);
            ultraStatusPanel6.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel6.Text = "0";
            ultraStatusPanel6.WrapText = Infragistics.Win.DefaultableBoolean.False;
            ultraStatusPanel7.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel7.Text = "Bit rate (mbps):";
            ultraStatusPanel8.MinWidth = 0;
            ultraStatusPanel8.Padding = new System.Drawing.Size(2, 0);
            ultraStatusPanel8.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel8.Text = "0.0000";
            ultraStatusPanel9.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel9.Text = "Queued buffers:";
            appearance32.TextHAlignAsString = "Left";
            ultraStatusPanel10.Appearance = appearance32;
            ultraStatusPanel10.MinWidth = 0;
            ultraStatusPanel10.Padding = new System.Drawing.Size(2, 0);
            ultraStatusPanel10.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel10.Text = "0";
            this.StatusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[] {
            ultraStatusPanel1,
            ultraStatusPanel2,
            ultraStatusPanel3,
            ultraStatusPanel4,
            ultraStatusPanel5,
            ultraStatusPanel6,
            ultraStatusPanel7,
            ultraStatusPanel8,
            ultraStatusPanel9,
            ultraStatusPanel10});
            this.StatusBar.Size = new System.Drawing.Size(702, 23);
            this.StatusBar.TabIndex = 7;
            // 
            // ToolTipManager
            // 
            this.ToolTipManager.ContainingControl = this;
            this.ToolTipManager.InitialDelay = 150;
            // 
            // LabelVarsLabel
            // 
            appearance59.FontData.UnderlineAsString = "False";
            appearance59.ForeColor = System.Drawing.SystemColors.HotTrack;
            appearance59.TextHAlignAsString = "Right";
            this.LabelVarsLabel.Appearance = appearance59;
            this.LabelVarsLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            appearance60.FontData.UnderlineAsString = "True";
            this.LabelVarsLabel.HotTrackAppearance = appearance60;
            this.LabelVarsLabel.Location = new System.Drawing.Point(9, 36);
            this.LabelVarsLabel.Name = "LabelVarsLabel";
            this.LabelVarsLabel.Size = new System.Drawing.Size(50, 16);
            this.LabelVarsLabel.TabIndex = 16;
            this.LabelVarsLabel.Text = "Vars:";
            ultraToolTipInfo7.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            ultraToolTipInfo7.ToolTipText = "Click link to change source voltage and current phasors for power / vars calculat" +
    "ions...";
            ultraToolTipInfo7.ToolTipTitle = "Note";
            this.ToolTipManager.SetUltraToolTip(this.LabelVarsLabel, ultraToolTipInfo7);
            this.LabelVarsLabel.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            this.LabelVarsLabel.Click += new System.EventHandler(this.CalculatedPowerOrVarLabel_Click);
            // 
            // LabelPowerLabel
            // 
            appearance61.FontData.UnderlineAsString = "False";
            appearance61.ForeColor = System.Drawing.SystemColors.HotTrack;
            appearance61.TextHAlignAsString = "Right";
            this.LabelPowerLabel.Appearance = appearance61;
            this.LabelPowerLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            appearance62.FontData.UnderlineAsString = "True";
            this.LabelPowerLabel.HotTrackAppearance = appearance62;
            this.LabelPowerLabel.Location = new System.Drawing.Point(9, 14);
            this.LabelPowerLabel.Name = "LabelPowerLabel";
            this.LabelPowerLabel.Size = new System.Drawing.Size(50, 20);
            this.LabelPowerLabel.TabIndex = 14;
            this.LabelPowerLabel.Text = "Power:";
            ultraToolTipInfo8.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            ultraToolTipInfo8.ToolTipText = "Click link to change source voltage and current phasors for power / vars calculat" +
    "ions...";
            ultraToolTipInfo8.ToolTipTitle = "Note";
            this.ToolTipManager.SetUltraToolTip(this.LabelPowerLabel, ultraToolTipInfo8);
            this.LabelPowerLabel.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            this.LabelPowerLabel.Click += new System.EventHandler(this.CalculatedPowerOrVarLabel_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.RestoreDirectory = true;
            // 
            // GroupBoxHeaderFrame
            // 
            this.GroupBoxHeaderFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxHeaderFrame.Controls.Add(this.GroupBoxPanelHeaderFrame);
            this.GroupBoxHeaderFrame.ExpandedSize = new System.Drawing.Size(165, 209);
            this.GroupBoxHeaderFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            appearance28.Image = ((object)(resources.GetObject("appearance28.Image")));
            this.GroupBoxHeaderFrame.HeaderAppearance = appearance28;
            this.GroupBoxHeaderFrame.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.RightOnBorder;
            this.GroupBoxHeaderFrame.Location = new System.Drawing.Point(522, 171);
            this.GroupBoxHeaderFrame.Name = "GroupBoxHeaderFrame";
            this.GroupBoxHeaderFrame.Size = new System.Drawing.Size(179, 244);
            this.GroupBoxHeaderFrame.TabIndex = 5;
            this.GroupBoxHeaderFrame.Text = "Header Frame";
            this.GroupBoxHeaderFrame.VerticalTextOrientation = Infragistics.Win.Misc.GroupBoxVerticalTextOrientation.TopToBottom;
            this.GroupBoxHeaderFrame.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(this.GroupBoxHeaderFrame_ExpandedStateChanging);
            // 
            // GroupBoxPanelHeaderFrame
            // 
            this.GroupBoxPanelHeaderFrame.Controls.Add(this.TextBoxHeaderFrame);
            this.GroupBoxPanelHeaderFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxPanelHeaderFrame.Location = new System.Drawing.Point(3, 3);
            this.GroupBoxPanelHeaderFrame.Name = "GroupBoxPanelHeaderFrame";
            this.GroupBoxPanelHeaderFrame.Size = new System.Drawing.Size(157, 238);
            this.GroupBoxPanelHeaderFrame.TabIndex = 0;
            // 
            // TextBoxHeaderFrame
            // 
            this.TextBoxHeaderFrame.AcceptsReturn = true;
            this.TextBoxHeaderFrame.AlwaysInEditMode = true;
            this.TextBoxHeaderFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxHeaderFrame.Location = new System.Drawing.Point(3, 3);
            this.TextBoxHeaderFrame.Multiline = true;
            this.TextBoxHeaderFrame.Name = "TextBoxHeaderFrame";
            this.TextBoxHeaderFrame.ReadOnly = true;
            this.TextBoxHeaderFrame.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxHeaderFrame.Size = new System.Drawing.Size(154, 232);
            this.TextBoxHeaderFrame.TabIndex = 0;
            this.TextBoxHeaderFrame.Text = "Header Frame";
            // 
            // GroupBoxConfigurationFrame
            // 
            this.GroupBoxConfigurationFrame.Controls.Add(this.GroupBoxPanelConfigurationFrame);
            this.GroupBoxConfigurationFrame.ExpandedSize = new System.Drawing.Size(187, 166);
            appearance27.Image = ((object)(resources.GetObject("appearance27.Image")));
            this.GroupBoxConfigurationFrame.HeaderAppearance = appearance27;
            this.GroupBoxConfigurationFrame.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.GroupBoxConfigurationFrame.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.RightOnBorder;
            this.GroupBoxConfigurationFrame.Location = new System.Drawing.Point(10, 174);
            this.GroupBoxConfigurationFrame.Name = "GroupBoxConfigurationFrame";
            this.GroupBoxConfigurationFrame.Size = new System.Drawing.Size(187, 166);
            this.GroupBoxConfigurationFrame.TabIndex = 2;
            this.GroupBoxConfigurationFrame.Text = "Configuration Frame";
            this.GroupBoxConfigurationFrame.VerticalTextOrientation = Infragistics.Win.Misc.GroupBoxVerticalTextOrientation.TopToBottom;
            this.GroupBoxConfigurationFrame.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(this.GroupBoxConfigurationFrame_ExpandedStateChanging);
            // 
            // GroupBoxPanelConfigurationFrame
            // 
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelPhasorCount);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelPhasorCountLabel);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelHz);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelNominalFrequency);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelNominalFrequencyLabel);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelDigitalCount);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelDigitalCountLabel);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelAnalogCount);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelAnalogCountLabel);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.ComboBoxPmus);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelIDCode);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelIDCodeLabel);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.ComboBoxPhasors);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelPmuList);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelSelectedIsRefAngle);
            this.GroupBoxPanelConfigurationFrame.Controls.Add(this.LabelPhasor);
            this.GroupBoxPanelConfigurationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxPanelConfigurationFrame.Location = new System.Drawing.Point(3, 3);
            this.GroupBoxPanelConfigurationFrame.Name = "GroupBoxPanelConfigurationFrame";
            this.GroupBoxPanelConfigurationFrame.Size = new System.Drawing.Size(165, 160);
            this.GroupBoxPanelConfigurationFrame.TabIndex = 0;
            // 
            // LabelPhasorCount
            // 
            this.LabelPhasorCount.Location = new System.Drawing.Point(55, 93);
            this.LabelPhasorCount.Name = "LabelPhasorCount";
            this.LabelPhasorCount.Size = new System.Drawing.Size(40, 20);
            this.LabelPhasorCount.TabIndex = 5;
            this.LabelPhasorCount.Text = "0";
            // 
            // LabelPhasorCountLabel
            // 
            appearance20.TextHAlignAsString = "Right";
            this.LabelPhasorCountLabel.Appearance = appearance20;
            this.LabelPhasorCountLabel.Location = new System.Drawing.Point(3, 93);
            this.LabelPhasorCountLabel.Name = "LabelPhasorCountLabel";
            this.LabelPhasorCountLabel.Size = new System.Drawing.Size(50, 20);
            this.LabelPhasorCountLabel.TabIndex = 4;
            this.LabelPhasorCountLabel.Text = "Phasors:";
            // 
            // LabelHz
            // 
            appearance21.TextHAlignAsString = "Left";
            this.LabelHz.Appearance = appearance21;
            this.LabelHz.Location = new System.Drawing.Point(128, 124);
            this.LabelHz.Name = "LabelHz";
            this.LabelHz.Size = new System.Drawing.Size(18, 16);
            this.LabelHz.TabIndex = 16;
            this.LabelHz.Text = "Hz";
            // 
            // LabelNominalFrequency
            // 
            appearance22.TextHAlignAsString = "Center";
            this.LabelNominalFrequency.Appearance = appearance22;
            this.LabelNominalFrequency.Location = new System.Drawing.Point(113, 124);
            this.LabelNominalFrequency.Name = "LabelNominalFrequency";
            this.LabelNominalFrequency.Size = new System.Drawing.Size(18, 16);
            this.LabelNominalFrequency.TabIndex = 15;
            this.LabelNominalFrequency.Text = "0";
            // 
            // LabelNominalFrequencyLabel
            // 
            appearance23.TextHAlignAsString = "Center";
            this.LabelNominalFrequencyLabel.Appearance = appearance23;
            this.LabelNominalFrequencyLabel.Location = new System.Drawing.Point(99, 93);
            this.LabelNominalFrequencyLabel.Name = "LabelNominalFrequencyLabel";
            this.LabelNominalFrequencyLabel.Size = new System.Drawing.Size(62, 32);
            this.LabelNominalFrequencyLabel.TabIndex = 14;
            this.LabelNominalFrequencyLabel.Text = "Nominal Frequency:";
            // 
            // LabelDigitalCount
            // 
            this.LabelDigitalCount.Location = new System.Drawing.Point(55, 137);
            this.LabelDigitalCount.Name = "LabelDigitalCount";
            this.LabelDigitalCount.Size = new System.Drawing.Size(40, 20);
            this.LabelDigitalCount.TabIndex = 9;
            this.LabelDigitalCount.Text = "0";
            // 
            // LabelDigitalCountLabel
            // 
            appearance24.TextHAlignAsString = "Right";
            this.LabelDigitalCountLabel.Appearance = appearance24;
            this.LabelDigitalCountLabel.Location = new System.Drawing.Point(0, 137);
            this.LabelDigitalCountLabel.Name = "LabelDigitalCountLabel";
            this.LabelDigitalCountLabel.Size = new System.Drawing.Size(50, 20);
            this.LabelDigitalCountLabel.TabIndex = 8;
            this.LabelDigitalCountLabel.Text = "Digitals:";
            // 
            // LabelAnalogCount
            // 
            this.LabelAnalogCount.Location = new System.Drawing.Point(55, 115);
            this.LabelAnalogCount.Name = "LabelAnalogCount";
            this.LabelAnalogCount.Size = new System.Drawing.Size(40, 20);
            this.LabelAnalogCount.TabIndex = 7;
            this.LabelAnalogCount.Text = "0";
            // 
            // LabelAnalogCountLabel
            // 
            appearance25.TextHAlignAsString = "Right";
            this.LabelAnalogCountLabel.Appearance = appearance25;
            this.LabelAnalogCountLabel.Location = new System.Drawing.Point(3, 115);
            this.LabelAnalogCountLabel.Name = "LabelAnalogCountLabel";
            this.LabelAnalogCountLabel.Size = new System.Drawing.Size(50, 20);
            this.LabelAnalogCountLabel.TabIndex = 6;
            this.LabelAnalogCountLabel.Text = "Analogs:";
            // 
            // ComboBoxPmus
            // 
            this.ComboBoxPmus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboBoxPmus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxPmus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPmus.Location = new System.Drawing.Point(3, 23);
            this.ComboBoxPmus.Name = "ComboBoxPmus";
            this.ComboBoxPmus.Size = new System.Drawing.Size(158, 21);
            this.ComboBoxPmus.TabIndex = 1;
            this.ComboBoxPmus.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPmus_SelectedIndexChanged);
            // 
            // LabelIDCode
            // 
            this.LabelIDCode.Location = new System.Drawing.Point(103, 8);
            this.LabelIDCode.Name = "LabelIDCode";
            this.LabelIDCode.Size = new System.Drawing.Size(40, 20);
            this.LabelIDCode.TabIndex = 19;
            this.LabelIDCode.Text = "0";
            // 
            // LabelIDCodeLabel
            // 
            appearance26.TextHAlignAsString = "Right";
            this.LabelIDCodeLabel.Appearance = appearance26;
            this.LabelIDCodeLabel.Location = new System.Drawing.Point(48, 8);
            this.LabelIDCodeLabel.Name = "LabelIDCodeLabel";
            this.LabelIDCodeLabel.Size = new System.Drawing.Size(49, 23);
            this.LabelIDCodeLabel.TabIndex = 18;
            this.LabelIDCodeLabel.Text = "ID Code:";
            // 
            // ComboBoxPhasors
            // 
            this.ComboBoxPhasors.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBoxPhasors.Location = new System.Drawing.Point(3, 66);
            this.ComboBoxPhasors.Name = "ComboBoxPhasors";
            this.ComboBoxPhasors.Size = new System.Drawing.Size(158, 21);
            this.ComboBoxPhasors.TabIndex = 3;
            this.ComboBoxPhasors.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPhasors_SelectedIndexChanged);
            this.ComboBoxPhasors.TextUpdate += new System.EventHandler(this.ComboBoxPhasors_TextUpdate);
            this.ComboBoxPhasors.DropDownClosed += new System.EventHandler(this.ComboBoxPhasors_DropDownClosed);
            this.ComboBoxPhasors.Enter += new System.EventHandler(this.ComboBoxPhasors_Enter);
            // 
            // LabelPmuList
            // 
            this.LabelPmuList.Location = new System.Drawing.Point(3, 8);
            this.LabelPmuList.Name = "LabelPmuList";
            this.LabelPmuList.Size = new System.Drawing.Size(36, 23);
            this.LabelPmuList.TabIndex = 0;
            this.LabelPmuList.Text = "PM&U:";
            // 
            // LabelSelectedIsRefAngle
            // 
            this.LabelSelectedIsRefAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSelectedIsRefAngle.Location = new System.Drawing.Point(43, 52);
            this.LabelSelectedIsRefAngle.Name = "LabelSelectedIsRefAngle";
            this.LabelSelectedIsRefAngle.Size = new System.Drawing.Size(135, 23);
            this.LabelSelectedIsRefAngle.TabIndex = 17;
            this.LabelSelectedIsRefAngle.Text = "(Selected is reference angle)";
            // 
            // LabelPhasor
            // 
            this.LabelPhasor.Location = new System.Drawing.Point(3, 51);
            this.LabelPhasor.Name = "LabelPhasor";
            this.LabelPhasor.Size = new System.Drawing.Size(47, 23);
            this.LabelPhasor.TabIndex = 2;
            this.LabelPhasor.Text = "P&hasor: ";
            // 
            // MenuStripMain
            // 
            this.MenuStripMain.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemHelp});
            this.MenuStripMain.Location = new System.Drawing.Point(0, 0);
            this.MenuStripMain.Name = "MenuStripMain";
            this.MenuStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MenuStripMain.Size = new System.Drawing.Size(702, 24);
            this.MenuStripMain.TabIndex = 0;
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemConnection,
            this.MenuItemConfigFile,
            this.MenuItemCapture,
            this.ToolStripSeparator1,
            this.MenuItemExit});
            this.MenuItemFile.Name = "MenuItemFile";
            this.MenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.MenuItemFile.Text = "&File";
            // 
            // MenuItemConnection
            // 
            this.MenuItemConnection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemLoadConnection,
            this.MenuItemSaveConnection});
            this.MenuItemConnection.Image = ((System.Drawing.Image)(resources.GetObject("MenuItemConnection.Image")));
            this.MenuItemConnection.Name = "MenuItemConnection";
            this.MenuItemConnection.Size = new System.Drawing.Size(136, 22);
            this.MenuItemConnection.Text = "&Connection";
            // 
            // MenuItemLoadConnection
            // 
            this.MenuItemLoadConnection.Name = "MenuItemLoadConnection";
            this.MenuItemLoadConnection.Size = new System.Drawing.Size(109, 22);
            this.MenuItemLoadConnection.Text = "&Load...";
            this.MenuItemLoadConnection.Click += new System.EventHandler(this.MenuItemLoadConnection_Click);
            // 
            // MenuItemSaveConnection
            // 
            this.MenuItemSaveConnection.Name = "MenuItemSaveConnection";
            this.MenuItemSaveConnection.Size = new System.Drawing.Size(109, 22);
            this.MenuItemSaveConnection.Text = "&Save...";
            this.MenuItemSaveConnection.Click += new System.EventHandler(this.MenuItemSaveConnection_Click);
            // 
            // MenuItemConfigFile
            // 
            this.MenuItemConfigFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemLoadConfigFile,
            this.MenuItemSaveConfigFile});
            this.MenuItemConfigFile.Image = ((System.Drawing.Image)(resources.GetObject("MenuItemConfigFile.Image")));
            this.MenuItemConfigFile.Name = "MenuItemConfigFile";
            this.MenuItemConfigFile.Size = new System.Drawing.Size(136, 22);
            this.MenuItemConfigFile.Text = "C&onfig File";
            // 
            // MenuItemLoadConfigFile
            // 
            this.MenuItemLoadConfigFile.Name = "MenuItemLoadConfigFile";
            this.MenuItemLoadConfigFile.Size = new System.Drawing.Size(109, 22);
            this.MenuItemLoadConfigFile.Text = "&Load...";
            this.MenuItemLoadConfigFile.Click += new System.EventHandler(this.MenuItemLoadConfigFile_Click);
            // 
            // MenuItemSaveConfigFile
            // 
            this.MenuItemSaveConfigFile.Enabled = false;
            this.MenuItemSaveConfigFile.Name = "MenuItemSaveConfigFile";
            this.MenuItemSaveConfigFile.Size = new System.Drawing.Size(109, 22);
            this.MenuItemSaveConfigFile.Text = "&Save...";
            this.MenuItemSaveConfigFile.Click += new System.EventHandler(this.MenuItemSaveConfigFile_Click);
            // 
            // MenuItemCapture
            // 
            this.MenuItemCapture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemStartCapture,
            this.MenuItemStopCapture,
            this.ToolStripSeparator3,
            this.MenuItemStartStreamDebugCapture,
            this.MenuItemStopStreamDebugCapture,
            this.ToolStripSeparator6,
            this.MenuItemCaptureSampleFrames,
            this.MenuItemCancelSampleFrameCapture});
            this.MenuItemCapture.Image = ((System.Drawing.Image)(resources.GetObject("MenuItemCapture.Image")));
            this.MenuItemCapture.Name = "MenuItemCapture";
            this.MenuItemCapture.Size = new System.Drawing.Size(136, 22);
            this.MenuItemCapture.Text = "C&apture";
            // 
            // MenuItemStartCapture
            // 
            this.MenuItemStartCapture.Name = "MenuItemStartCapture";
            this.MenuItemStartCapture.Size = new System.Drawing.Size(233, 22);
            this.MenuItemStartCapture.Text = "S&tart Capture...";
            this.MenuItemStartCapture.Click += new System.EventHandler(this.MenuItemStartCapture_Click);
            // 
            // MenuItemStopCapture
            // 
            this.MenuItemStopCapture.Enabled = false;
            this.MenuItemStopCapture.Name = "MenuItemStopCapture";
            this.MenuItemStopCapture.Size = new System.Drawing.Size(233, 22);
            this.MenuItemStopCapture.Text = "Sto&p Capture";
            this.MenuItemStopCapture.Click += new System.EventHandler(this.MenuItemStopCapture_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(230, 6);
            // 
            // MenuItemStartStreamDebugCapture
            // 
            this.MenuItemStartStreamDebugCapture.Name = "MenuItemStartStreamDebugCapture";
            this.MenuItemStartStreamDebugCapture.Size = new System.Drawing.Size(233, 22);
            this.MenuItemStartStreamDebugCapture.Text = "Start Stream &Debug Capture...";
            this.MenuItemStartStreamDebugCapture.Click += new System.EventHandler(this.MenuItemStartStreamDebugCapture_Click);
            // 
            // MenuItemStopStreamDebugCapture
            // 
            this.MenuItemStopStreamDebugCapture.Enabled = false;
            this.MenuItemStopStreamDebugCapture.Name = "MenuItemStopStreamDebugCapture";
            this.MenuItemStopStreamDebugCapture.Size = new System.Drawing.Size(233, 22);
            this.MenuItemStopStreamDebugCapture.Text = "Stop Stream D&ebug Capture";
            this.MenuItemStopStreamDebugCapture.Click += new System.EventHandler(this.MenuItemStopStreamDebugCapture_Click);
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(230, 6);
            // 
            // MenuItemCaptureSampleFrames
            // 
            this.MenuItemCaptureSampleFrames.Name = "MenuItemCaptureSampleFrames";
            this.MenuItemCaptureSampleFrames.Size = new System.Drawing.Size(233, 22);
            this.MenuItemCaptureSampleFrames.Text = "&Capture Sample Frames...";
            this.MenuItemCaptureSampleFrames.Click += new System.EventHandler(this.MenuItemCaptureSampleFrames_Click);
            // 
            // MenuItemCancelSampleFrameCapture
            // 
            this.MenuItemCancelSampleFrameCapture.Enabled = false;
            this.MenuItemCancelSampleFrameCapture.Name = "MenuItemCancelSampleFrameCapture";
            this.MenuItemCancelSampleFrameCapture.Size = new System.Drawing.Size(233, 22);
            this.MenuItemCancelSampleFrameCapture.Text = "C&ancel Sample Frame Capture";
            this.MenuItemCancelSampleFrameCapture.Click += new System.EventHandler(this.MenuItemCancelSampleFrameCapture_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // MenuItemExit
            // 
            this.MenuItemExit.Name = "MenuItemExit";
            this.MenuItemExit.Size = new System.Drawing.Size(136, 22);
            this.MenuItemExit.Text = "E&xit";
            this.MenuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);
            // 
            // MenuItemHelp
            // 
            this.MenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemLocalHelp,
            this.MenuItemOnlineHelp,
            this.ToolStripSeparator2,
            this.MenuItemAbout});
            this.MenuItemHelp.Name = "MenuItemHelp";
            this.MenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.MenuItemHelp.Text = "&Help";
            // 
            // MenuItemLocalHelp
            // 
            this.MenuItemLocalHelp.Image = ((System.Drawing.Image)(resources.GetObject("MenuItemLocalHelp.Image")));
            this.MenuItemLocalHelp.Name = "MenuItemLocalHelp";
            this.MenuItemLocalHelp.Size = new System.Drawing.Size(146, 22);
            this.MenuItemLocalHelp.Text = "&Local Help...";
            this.MenuItemLocalHelp.Click += new System.EventHandler(this.MenuItemLocalHelp_Click);
            // 
            // MenuItemOnlineHelp
            // 
            this.MenuItemOnlineHelp.Image = ((System.Drawing.Image)(resources.GetObject("MenuItemOnlineHelp.Image")));
            this.MenuItemOnlineHelp.Name = "MenuItemOnlineHelp";
            this.MenuItemOnlineHelp.Size = new System.Drawing.Size(146, 22);
            this.MenuItemOnlineHelp.Text = "&Online Help...";
            this.MenuItemOnlineHelp.Click += new System.EventHandler(this.MenuItemOnlineHelp_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // MenuItemAbout
            // 
            this.MenuItemAbout.Name = "MenuItemAbout";
            this.MenuItemAbout.Size = new System.Drawing.Size(146, 22);
            this.MenuItemAbout.Text = "&About...";
            this.MenuItemAbout.Click += new System.EventHandler(this.MenuItemAbout_Click);
            // 
            // TabSharedControlsPageChart
            // 
            this.TabSharedControlsPageChart.Location = new System.Drawing.Point(-10000, -10000);
            this.TabSharedControlsPageChart.Name = "TabSharedControlsPageChart";
            this.TabSharedControlsPageChart.Size = new System.Drawing.Size(312, 223);
            // 
            // TabControlChart
            // 
            this.TabControlChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.TabControlChart.Appearance = appearance7;
            this.TabControlChart.Controls.Add(this.TabSharedControlsPageChart);
            this.TabControlChart.Controls.Add(this.TabPageControl5);
            this.TabControlChart.Controls.Add(this.TabPageControl6);
            this.TabControlChart.Controls.Add(this.TabPageControl7);
            this.TabControlChart.Controls.Add(this.TabPageControl8);
            this.TabControlChart.Location = new System.Drawing.Point(202, 171);
            this.TabControlChart.Name = "TabControlChart";
            this.TabControlChart.SharedControlsPage = this.TabSharedControlsPageChart;
            this.TabControlChart.Size = new System.Drawing.Size(314, 244);
            this.TabControlChart.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            this.TabControlChart.TabIndex = 4;
            this.TabControlChart.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.SingleRowFixed;
            this.TabControlChart.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft;
            appearance8.Image = ((object)(resources.GetObject("appearance8.Image")));
            ultraTab1.Appearance = appearance8;
            ultraTab1.TabPage = this.TabPageControl5;
            ultraTab1.Text = "&Graph";
            appearance9.Image = ((object)(resources.GetObject("appearance9.Image")));
            ultraTab2.Appearance = appearance9;
            ultraTab2.TabPage = this.TabPageControl6;
            ultraTab2.Text = "S&ettings";
            appearance10.Image = ((object)(resources.GetObject("appearance10.Image")));
            ultraTab3.Appearance = appearance10;
            ultraTab3.TabPage = this.TabPageControl7;
            ultraTab3.Text = "&Messages";
            appearance11.Image = ((object)(resources.GetObject("appearance11.Image")));
            ultraTab4.Appearance = appearance11;
            ultraTab4.TabPage = this.TabPageControl8;
            ultraTab4.Text = "P&rotocol Specific";
            this.TabControlChart.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3,
            ultraTab4});
            this.TabControlChart.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(this.TabControlChart_SelectedTabChanged);
            // 
            // GroupBoxRealTimePowerVars
            // 
            this.GroupBoxRealTimePowerVars.Controls.Add(this.LabelVars);
            this.GroupBoxRealTimePowerVars.Controls.Add(this.LabelVarsLabel);
            this.GroupBoxRealTimePowerVars.Controls.Add(this.LabelPower);
            this.GroupBoxRealTimePowerVars.Controls.Add(this.LabelPowerLabel);
            this.GroupBoxRealTimePowerVars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxRealTimePowerVars.Location = new System.Drawing.Point(10, 343);
            this.GroupBoxRealTimePowerVars.Name = "GroupBoxRealTimePowerVars";
            this.GroupBoxRealTimePowerVars.Size = new System.Drawing.Size(178, 66);
            this.GroupBoxRealTimePowerVars.TabIndex = 3;
            // 
            // LabelVars
            // 
            this.LabelVars.Location = new System.Drawing.Point(64, 36);
            this.LabelVars.Name = "LabelVars";
            this.LabelVars.Size = new System.Drawing.Size(110, 20);
            this.LabelVars.TabIndex = 17;
            this.LabelVars.Text = "0.0000 MVars";
            // 
            // LabelPower
            // 
            this.LabelPower.Location = new System.Drawing.Point(64, 14);
            this.LabelPower.Name = "LabelPower";
            this.LabelPower.Size = new System.Drawing.Size(110, 20);
            this.LabelPower.TabIndex = 15;
            this.LabelPower.Text = "0.0000 MW";
            // 
            // ToolTipManagerForExtraParameters
            // 
            this.ToolTipManagerForExtraParameters.ContainingControl = this;
            this.ToolTipManagerForExtraParameters.InitialDelay = 2147483647;
            this.ToolTipManagerForExtraParameters.ToolTipImage = Infragistics.Win.ToolTipImage.Info;
            this.ToolTipManagerForExtraParameters.ToolTipTitle = "Extra Parameters Available";
            // 
            // GroupBoxProtocolParameters
            // 
            this.GroupBoxProtocolParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxProtocolParameters.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
            appearance5.BorderColor = System.Drawing.Color.Gray;
            this.GroupBoxProtocolParameters.ContentAreaAppearance = appearance5;
            this.GroupBoxProtocolParameters.Controls.Add(this.LabelTabMask);
            this.GroupBoxProtocolParameters.Controls.Add(this.PropertyGridProtocolParameters);
            this.GroupBoxProtocolParameters.Location = new System.Drawing.Point(555, 455);
            this.GroupBoxProtocolParameters.Name = "GroupBoxProtocolParameters";
            this.GroupBoxProtocolParameters.Size = new System.Drawing.Size(490, 353);
            this.GroupBoxProtocolParameters.TabIndex = 11;
            this.GroupBoxProtocolParameters.Paint += new System.Windows.Forms.PaintEventHandler(this.GroupBoxProtocolParameters_Paint);
            // 
            // LabelTabMask
            // 
            appearance6.AlphaLevel = ((short)(255));
            this.LabelTabMask.Appearance = appearance6;
            this.LabelTabMask.Location = new System.Drawing.Point(179, 0);
            this.LabelTabMask.Name = "LabelTabMask";
            this.LabelTabMask.Size = new System.Drawing.Size(96, 12);
            this.LabelTabMask.TabIndex = 11;
            // 
            // PropertyGridProtocolParameters
            // 
            this.PropertyGridProtocolParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertyGridProtocolParameters.Location = new System.Drawing.Point(3, 3);
            this.PropertyGridProtocolParameters.Name = "PropertyGridProtocolParameters";
            this.PropertyGridProtocolParameters.Size = new System.Drawing.Size(484, 347);
            this.PropertyGridProtocolParameters.TabIndex = 10;
            // 
            // GlobalExceptionLogger
            // 
            this.GlobalExceptionLogger.ContactEmail = "jrcarrol@tva.gov";
            this.GlobalExceptionLogger.ContactName = "James Ritchie Carroll";
            this.GlobalExceptionLogger.ContactPhone = null;
            // 
            // 
            // 
            this.GlobalExceptionLogger.ErrorLog.FileName = "ErrorLog.txt";
            this.GlobalExceptionLogger.LogToEventLog = false;
            this.GlobalExceptionLogger.LogToUI = true;
            this.GlobalExceptionLogger.SettingsCategory = "GlobalExceptionLogger";
            // 
            // LabelDefaultIPStack
            // 
            appearance4.FontData.SizeInPoints = 7F;
            appearance4.TextHAlignAsString = "Right";
            appearance4.TextVAlignAsString = "Middle";
            this.LabelDefaultIPStack.Appearance = appearance4;
            this.LabelDefaultIPStack.Location = new System.Drawing.Point(176, 42);
            this.LabelDefaultIPStack.Name = "LabelDefaultIPStack";
            this.LabelDefaultIPStack.Size = new System.Drawing.Size(142, 20);
            this.LabelDefaultIPStack.TabIndex = 16;
            this.LabelDefaultIPStack.Text = "Default System IP Stack: IPv6";
            this.LabelDefaultIPStack.Visible = false;
            // 
            // TimerDelay
            // 
            this.TimerDelay.Interval = 1100;
            this.TimerDelay.Tick += new System.EventHandler(this.TimerDelay_Tick);
            // 
            // PMUConnectionTester
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 601);
            this.Controls.Add(this.LabelDefaultIPStack);
            this.Controls.Add(this.GroupBoxProtocolParameters);
            this.Controls.Add(this.TabControlChart);
            this.Controls.Add(this.GroupBoxStatus);
            this.Controls.Add(this.GroupBoxConfigurationFrame);
            this.Controls.Add(this.GroupBoxHeaderFrame);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.GroupBoxConnection);
            this.Controls.Add(this.MenuStripMain);
            this.Controls.Add(this.GroupBoxRealTimePowerVars);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(710, 635);
            this.Name = "PMUConnectionTester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PMU Connection Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PMUConnectionTester_FormClosing);
            this.Load += new System.EventHandler(this.PMUConnectionTester_Load);
            this.Shown += new System.EventHandler(this.PMUConnectionTester_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PMUConnectionTester_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PMUConnectionTester_DragEnter);
            this.Resize += new System.EventHandler(this.PMUConnectionTester_Resize);
            this.TabPageControl9.ResumeLayout(false);
            this.TabPageControl1.ResumeLayout(false);
            this.TabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxRemoteUDPServer)).EndInit();
            this.GroupBoxRemoteUDPServer.ResumeLayout(false);
            this.TabPageControl3.ResumeLayout(false);
            this.TabPageControl4.ResumeLayout(false);
            this.TabPageControl4.PerformLayout();
            this.TabPageControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartDataDisplay)).EndInit();
            this.TabPageControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxPowerVarCalculations)).EndInit();
            this.GroupBoxPowerVarCalculations.ResumeLayout(false);
            this.TabPageControl7.ResumeLayout(false);
            this.TabPageControl7.PerformLayout();
            this.TabPageControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TreeFrameAttributes)).EndInit();
            this.ContextMenuForTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxConnection)).EndInit();
            this.GroupBoxConnection.ResumeLayout(false);
            this.GroupBoxPanelConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabControlProtocolParameters)).EndInit();
            this.TabControlProtocolParameters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabControlCommunications)).EndInit();
            this.TabControlCommunications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxStatus)).EndInit();
            this.GroupBoxStatus.ResumeLayout(false);
            this.GroupBoxPanelStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxHeaderFrame)).EndInit();
            this.GroupBoxHeaderFrame.ResumeLayout(false);
            this.GroupBoxPanelHeaderFrame.ResumeLayout(false);
            this.GroupBoxPanelHeaderFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextBoxHeaderFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxConfigurationFrame)).EndInit();
            this.GroupBoxConfigurationFrame.ResumeLayout(false);
            this.GroupBoxPanelConfigurationFrame.ResumeLayout(false);
            this.MenuStripMain.ResumeLayout(false);
            this.MenuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabControlChart)).EndInit();
            this.TabControlChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxRealTimePowerVars)).EndInit();
            this.GroupBoxRealTimePowerVars.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupBoxProtocolParameters)).EndInit();
            this.GroupBoxProtocolParameters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GlobalExceptionLogger.ErrorLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalExceptionLogger)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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