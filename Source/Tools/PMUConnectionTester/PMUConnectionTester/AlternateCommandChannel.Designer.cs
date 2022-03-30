using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ConnectionTester
{
    public partial class AlternateCommandChannel : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
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
            var UltraToolTipInfo1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Enter host DNS name, IPv4 or IPv6 address", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            var Appearance12 = new Infragistics.Win.Appearance();
            var Appearance5 = new Infragistics.Win.Appearance();
            var Appearance6 = new Infragistics.Win.Appearance();
            var Appearance7 = new Infragistics.Win.Appearance();
            var Appearance8 = new Infragistics.Win.Appearance();
            var Appearance9 = new Infragistics.Win.Appearance();
            var Appearance10 = new Infragistics.Win.Appearance();
            var Appearance11 = new Infragistics.Win.Appearance();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(AlternateCommandChannel));
            var Appearance4 = new Infragistics.Win.Appearance();
            var UltraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var UltraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            var UltraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            TabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            LabelTcpNetworkInterface = new Infragistics.Win.Misc.UltraLabel();
            LabelTcpNetworkInterface.Click += new EventHandler(LabelTcpNetworkInterface_Click);
            LabelTcpHostIP = new Infragistics.Win.Misc.UltraLabel();
            TextBoxTcpHostIP = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxTcpHostIP.GotFocus += new EventHandler(TextBox_GotFocus);
            LabelTcpPort = new Infragistics.Win.Misc.UltraLabel();
            TextBoxTcpPort = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            TextBoxTcpPort.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxTcpPort.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            TabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
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
            TabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            ButtonBrowse = new Button();
            ButtonBrowse.Click += new EventHandler(ButtonBrowse_Click);
            TextBoxFileCaptureName = new TextBox();
            TextBoxFileCaptureName.GotFocus += new EventHandler(TextBox_GotFocus);
            TextBoxFileCaptureName.MouseClick += new MouseEventHandler(TextBox_MouseClick);
            LabelCaptureFile = new Infragistics.Win.Misc.UltraLabel();
            LabelReplayCapturedFile = new Infragistics.Win.Misc.UltraLabel();
            ButtonSave = new Button();
            ButtonCancel = new Button();
            PictureBoxIcon = new PictureBox();
            TabControlCommunications = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            TabSharedControlsPageCommunications = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            CheckBoxUndefined = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            CheckBoxUndefined.CheckedChanged += new EventHandler(CheckBoxUndefined_CheckedChanged);
            ToolTipManager = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
            TabPageControl1.SuspendLayout();
            TabPageControl2.SuspendLayout();
            TabPageControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBoxIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TabControlCommunications).BeginInit();
            TabControlCommunications.SuspendLayout();
            SuspendLayout();
            // 
            // TabPageControl1
            // 
            TabPageControl1.Controls.Add(LabelTcpNetworkInterface);
            TabPageControl1.Controls.Add(LabelTcpHostIP);
            TabPageControl1.Controls.Add(TextBoxTcpHostIP);
            TabPageControl1.Controls.Add(LabelTcpPort);
            TabPageControl1.Controls.Add(TextBoxTcpPort);
            TabPageControl1.Location = new Point(1, 20);
            TabPageControl1.Name = "TabPageControl1";
            TabPageControl1.Size = new Size(304, 96);
            // 
            // LabelTcpNetworkInterface
            // 
            LabelTcpNetworkInterface.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Appearance1.ForeColor = SystemColors.HotTrack;
            Appearance1.TextHAlignAsString = "Center";
            Appearance1.TextVAlignAsString = "Middle";
            LabelTcpNetworkInterface.Appearance = Appearance1;
            LabelTcpNetworkInterface.Cursor = Cursors.Hand;
            LabelTcpNetworkInterface.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            Appearance2.FontData.UnderlineAsString = "True";
            LabelTcpNetworkInterface.HotTrackAppearance = Appearance2;
            LabelTcpNetworkInterface.Location = new Point(63, 72);
            LabelTcpNetworkInterface.Name = "LabelTcpNetworkInterface";
            LabelTcpNetworkInterface.Size = new Size(94, 21);
            LabelTcpNetworkInterface.TabIndex = 20;
            LabelTcpNetworkInterface.Text = "Network Interface";
            LabelTcpNetworkInterface.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
            // 
            // LabelTcpHostIP
            // 
            Appearance3.TextHAlignAsString = "Right";
            LabelTcpHostIP.Appearance = Appearance3;
            LabelTcpHostIP.Location = new Point(15, 25);
            LabelTcpHostIP.Name = "LabelTcpHostIP";
            LabelTcpHostIP.Size = new Size(45, 23);
            LabelTcpHostIP.TabIndex = 0;
            LabelTcpHostIP.Text = "Host &IP:";
            UltraToolTipInfo1.ToolTipText = "Enter host DNS name, IPv4 or IPv6 address";
            ToolTipManager.SetUltraToolTip(LabelTcpHostIP, UltraToolTipInfo1);
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
            Appearance12.TextHAlignAsString = "Right";
            LabelTcpPort.Appearance = Appearance12;
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
            TabPageControl2.Controls.Add(CheckBoxSerialRTS);
            TabPageControl2.Controls.Add(CheckBoxSerialDTR);
            TabPageControl2.Controls.Add(TextBoxSerialDataBits);
            TabPageControl2.Controls.Add(ComboBoxSerialStopBits);
            TabPageControl2.Controls.Add(ComboBoxSerialParities);
            TabPageControl2.Controls.Add(LabelSerialParity);
            TabPageControl2.Controls.Add(ComboBoxSerialBaudRates);
            TabPageControl2.Controls.Add(LabelSerialBaudRate);
            TabPageControl2.Controls.Add(ComboBoxSerialPorts);
            TabPageControl2.Controls.Add(LabelSerialPort);
            TabPageControl2.Controls.Add(LabelSerialStopBits);
            TabPageControl2.Controls.Add(LabelSerialDataBits);
            TabPageControl2.Location = new Point(-10000, -10000);
            TabPageControl2.Name = "TabPageControl2";
            TabPageControl2.Size = new Size(304, 96);
            // 
            // CheckBoxSerialRTS
            // 
            CheckBoxSerialRTS.Location = new Point(225, 62);
            CheckBoxSerialRTS.Name = "CheckBoxSerialRTS";
            CheckBoxSerialRTS.Size = new Size(50, 26);
            CheckBoxSerialRTS.TabIndex = 11;
            CheckBoxSerialRTS.Text = "RTS";
            // 
            // CheckBoxSerialDTR
            // 
            CheckBoxSerialDTR.Location = new Point(169, 62);
            CheckBoxSerialDTR.Name = "CheckBoxSerialDTR";
            CheckBoxSerialDTR.Size = new Size(50, 26);
            CheckBoxSerialDTR.TabIndex = 10;
            CheckBoxSerialDTR.Text = "DTR";
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
            Appearance5.TextHAlignAsString = "Right";
            LabelSerialParity.Appearance = Appearance5;
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
            Appearance6.TextHAlignAsString = "Right";
            LabelSerialBaudRate.Appearance = Appearance6;
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
            Appearance7.TextHAlignAsString = "Right";
            LabelSerialPort.Appearance = Appearance7;
            LabelSerialPort.Location = new Point(-4, 13);
            LabelSerialPort.Name = "LabelSerialPort";
            LabelSerialPort.Size = new Size(64, 23);
            LabelSerialPort.TabIndex = 0;
            LabelSerialPort.Text = "P&ort:";
            // 
            // LabelSerialStopBits
            // 
            Appearance8.TextHAlignAsString = "Right";
            LabelSerialStopBits.Appearance = Appearance8;
            LabelSerialStopBits.Location = new Point(150, 13);
            LabelSerialStopBits.Name = "LabelSerialStopBits";
            LabelSerialStopBits.Size = new Size(57, 23);
            LabelSerialStopBits.TabIndex = 6;
            LabelSerialStopBits.Text = "Stop Bits:";
            // 
            // LabelSerialDataBits
            // 
            Appearance9.TextHAlignAsString = "Right";
            LabelSerialDataBits.Appearance = Appearance9;
            LabelSerialDataBits.Location = new Point(148, 40);
            LabelSerialDataBits.Name = "LabelSerialDataBits";
            LabelSerialDataBits.Size = new Size(59, 23);
            LabelSerialDataBits.TabIndex = 8;
            LabelSerialDataBits.Text = "Data Bits:";
            // 
            // TabPageControl3
            // 
            TabPageControl3.Controls.Add(ButtonBrowse);
            TabPageControl3.Controls.Add(TextBoxFileCaptureName);
            TabPageControl3.Controls.Add(LabelCaptureFile);
            TabPageControl3.Controls.Add(LabelReplayCapturedFile);
            TabPageControl3.Location = new Point(-10000, -10000);
            TabPageControl3.Name = "TabPageControl3";
            TabPageControl3.Size = new Size(304, 96);
            // 
            // ButtonBrowse
            // 
            ButtonBrowse.Location = new Point(213, 37);
            ButtonBrowse.Name = "ButtonBrowse";
            ButtonBrowse.Size = new Size(75, 23);
            ButtonBrowse.TabIndex = 2;
            ButtonBrowse.Text = "&Browse...";
            ButtonBrowse.UseVisualStyleBackColor = true;
            // 
            // TextBoxFileCaptureName
            // 
            TextBoxFileCaptureName.BackColor = Color.White;
            TextBoxFileCaptureName.Location = new Point(71, 39);
            TextBoxFileCaptureName.Name = "TextBoxFileCaptureName";
            TextBoxFileCaptureName.Size = new Size(136, 20);
            TextBoxFileCaptureName.TabIndex = 1;
            // 
            // LabelCaptureFile
            // 
            Appearance10.TextHAlignAsString = "Right";
            LabelCaptureFile.Appearance = Appearance10;
            LabelCaptureFile.Location = new Point(11, 42);
            LabelCaptureFile.Name = "LabelCaptureFile";
            LabelCaptureFile.Size = new Size(54, 23);
            LabelCaptureFile.TabIndex = 0;
            LabelCaptureFile.Text = "F&ilename:";
            // 
            // LabelReplayCapturedFile
            // 
            Appearance11.FontData.ItalicAsString = "True";
            LabelReplayCapturedFile.Appearance = Appearance11;
            LabelReplayCapturedFile.Location = new Point(191, 22);
            LabelReplayCapturedFile.Name = "LabelReplayCapturedFile";
            LabelReplayCapturedFile.Size = new Size(120, 17);
            LabelReplayCapturedFile.TabIndex = 6;
            LabelReplayCapturedFile.Text = "Replay captured file...";
            // 
            // ButtonSave
            // 
            ButtonSave.DialogResult = DialogResult.OK;
            ButtonSave.Location = new Point(324, 12);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(75, 23);
            ButtonSave.TabIndex = 0;
            ButtonSave.Text = "S&ave";
            ButtonSave.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            ButtonCancel.DialogResult = DialogResult.Cancel;
            ButtonCancel.Location = new Point(324, 41);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new Size(75, 23);
            ButtonCancel.TabIndex = 1;
            ButtonCancel.Text = "&Cancel";
            ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // PictureBoxIcon
            // 
            PictureBoxIcon.Image = (Image)resources.GetObject("PictureBoxIcon.Image");
            PictureBoxIcon.Location = new Point(339, 89);
            PictureBoxIcon.Name = "PictureBoxIcon";
            PictureBoxIcon.Size = new Size(48, 48);
            PictureBoxIcon.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBoxIcon.TabIndex = 2;
            PictureBoxIcon.TabStop = false;
            // 
            // TabControlCommunications
            // 
            Appearance4.ForeColor = Color.Black;
            TabControlCommunications.Appearance = Appearance4;
            TabControlCommunications.Controls.Add(TabSharedControlsPageCommunications);
            TabControlCommunications.Controls.Add(TabPageControl1);
            TabControlCommunications.Controls.Add(TabPageControl2);
            TabControlCommunications.Controls.Add(TabPageControl3);
            TabControlCommunications.Enabled = false;
            TabControlCommunications.Location = new Point(12, 12);
            TabControlCommunications.Name = "TabControlCommunications";
            TabControlCommunications.SharedControlsPage = TabSharedControlsPageCommunications;
            TabControlCommunications.Size = new Size(306, 117);
            TabControlCommunications.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            TabControlCommunications.TabIndex = 3;
            TabControlCommunications.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.SingleRowFixed;
            TabControlCommunications.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.TopLeft;
            UltraTab1.TabPage = TabPageControl1;
            UltraTab1.Text = "&Tcp";
            UltraTab2.TabPage = TabPageControl2;
            UltraTab2.Text = "&Serial";
            UltraTab3.TabPage = TabPageControl3;
            UltraTab3.Text = "Fi&le";
            TabControlCommunications.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] { UltraTab1, UltraTab2, UltraTab3 });
            // 
            // TabSharedControlsPageCommunications
            // 
            TabSharedControlsPageCommunications.Location = new Point(-10000, -10000);
            TabSharedControlsPageCommunications.Name = "TabSharedControlsPageCommunications";
            TabSharedControlsPageCommunications.Size = new Size(304, 96);
            // 
            // CheckBoxUndefined
            // 
            CheckBoxUndefined.Checked = true;
            CheckBoxUndefined.CheckState = CheckState.Checked;
            CheckBoxUndefined.Location = new Point(324, 64);
            CheckBoxUndefined.Name = "CheckBoxUndefined";
            CheckBoxUndefined.Size = new Size(82, 26);
            CheckBoxUndefined.TabIndex = 11;
            CheckBoxUndefined.Text = "Not defined";
            // 
            // ToolTipManager
            // 
            ToolTipManager.ContainingControl = this;
            ToolTipManager.InitialDelay = 150;
            // 
            // AlternateCommandChannel
            // 
            AcceptButton = ButtonSave;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = ButtonCancel;
            ClientSize = new Size(406, 142);
            ControlBox = false;
            Controls.Add(PictureBoxIcon);
            Controls.Add(CheckBoxUndefined);
            Controls.Add(TabControlCommunications);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonSave);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AlternateCommandChannel";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Alternate Command Channel Configuration";
            TabPageControl1.ResumeLayout(false);
            TabPageControl2.ResumeLayout(false);
            TabPageControl3.ResumeLayout(false);
            TabPageControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBoxIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)TabControlCommunications).EndInit();
            TabControlCommunications.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        internal Button ButtonSave;
        internal Button ButtonCancel;
        internal PictureBox PictureBoxIcon;
        internal Infragistics.Win.UltraWinTabControl.UltraTabControl TabControlCommunications;
        internal Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage TabSharedControlsPageCommunications;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl1;
        internal Infragistics.Win.Misc.UltraLabel LabelTcpHostIP;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxTcpHostIP;
        internal Infragistics.Win.Misc.UltraLabel LabelTcpPort;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxTcpPort;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl2;
        internal Infragistics.Win.UltraWinEditors.UltraCheckEditor CheckBoxSerialRTS;
        internal Infragistics.Win.UltraWinEditors.UltraCheckEditor CheckBoxSerialDTR;
        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxSerialDataBits;
        internal ComboBox ComboBoxSerialStopBits;
        internal ComboBox ComboBoxSerialParities;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialParity;
        internal ComboBox ComboBoxSerialBaudRates;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialBaudRate;
        internal ComboBox ComboBoxSerialPorts;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialPort;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialStopBits;
        internal Infragistics.Win.Misc.UltraLabel LabelSerialDataBits;
        internal Infragistics.Win.UltraWinTabControl.UltraTabPageControl TabPageControl3;
        internal Button ButtonBrowse;
        internal TextBox TextBoxFileCaptureName;
        internal Infragistics.Win.Misc.UltraLabel LabelCaptureFile;
        internal Infragistics.Win.Misc.UltraLabel LabelReplayCapturedFile;
        internal Infragistics.Win.UltraWinEditors.UltraCheckEditor CheckBoxUndefined;
        internal Infragistics.Win.UltraWinToolTip.UltraToolTipManager ToolTipManager;
        internal Infragistics.Win.Misc.UltraLabel LabelTcpNetworkInterface;
    }
}