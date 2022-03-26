using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ConnectionTester
{
    public partial class NetworkInterfaceSelector : Form
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
            var Appearance2 = new Infragistics.Win.Appearance();
            ComboBoxNetworkInterfaces = new ComboBox();
            ComboBoxNetworkInterfaces.SelectedIndexChanged += new EventHandler(ComboBoxNetworkInterfaces_SelectedIndexChanged);
            LabelNetworkInterfaces = new Infragistics.Win.Misc.UltraLabel();
            ButtonCancel = new Button();
            ButtonOK = new Button();
            LabelInterfaceIP = new Infragistics.Win.Misc.UltraLabel();
            SuspendLayout();
            // 
            // ComboBoxNetworkInterfaces
            // 
            ComboBoxNetworkInterfaces.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBoxNetworkInterfaces.AutoCompleteSource = AutoCompleteSource.ListItems;
            ComboBoxNetworkInterfaces.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxNetworkInterfaces.Location = new Point(12, 30);
            ComboBoxNetworkInterfaces.Name = "ComboBoxNetworkInterfaces";
            ComboBoxNetworkInterfaces.Size = new Size(375, 21);
            ComboBoxNetworkInterfaces.TabIndex = 1;
            // 
            // LabelNetworkInterfaces
            // 
            LabelNetworkInterfaces.Location = new Point(12, 12);
            LabelNetworkInterfaces.Name = "LabelNetworkInterfaces";
            LabelNetworkInterfaces.Size = new Size(105, 23);
            LabelNetworkInterfaces.TabIndex = 0;
            LabelNetworkInterfaces.Text = "&Network Interfaces:";
            // 
            // ButtonCancel
            // 
            ButtonCancel.DialogResult = DialogResult.Cancel;
            ButtonCancel.Location = new Point(312, 63);
            ButtonCancel.Name = "ButtonCancel";
            ButtonCancel.Size = new Size(75, 23);
            ButtonCancel.TabIndex = 3;
            ButtonCancel.Text = "&Cancel";
            ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // ButtonOK
            // 
            ButtonOK.DialogResult = DialogResult.OK;
            ButtonOK.Location = new Point(231, 63);
            ButtonOK.Name = "ButtonOK";
            ButtonOK.Size = new Size(75, 23);
            ButtonOK.TabIndex = 2;
            ButtonOK.Text = "&OK";
            ButtonOK.UseVisualStyleBackColor = true;
            // 
            // LabelInterfaceIP
            // 
            Appearance2.TextHAlignAsString = "Right";
            Appearance2.TextVAlignAsString = "Top";
            LabelInterfaceIP.Appearance = Appearance2;
            LabelInterfaceIP.Location = new Point(112, 12);
            LabelInterfaceIP.Name = "LabelInterfaceIP";
            LabelInterfaceIP.Size = new Size(275, 20);
            LabelInterfaceIP.TabIndex = 4;
            LabelInterfaceIP.Text = "0.0.0.0";
            // 
            // NetworkInterfaceSelector
            // 
            AcceptButton = ButtonOK;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = ButtonCancel;
            ClientSize = new Size(399, 98);
            ControlBox = false;
            Controls.Add(ComboBoxNetworkInterfaces);
            Controls.Add(LabelInterfaceIP);
            Controls.Add(ButtonCancel);
            Controls.Add(ButtonOK);
            Controls.Add(LabelNetworkInterfaces);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NetworkInterfaceSelector";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Desired Network Interface";
            ResumeLayout(false);
        }

        internal ComboBox ComboBoxNetworkInterfaces;
        internal Infragistics.Win.Misc.UltraLabel LabelNetworkInterfaces;
        internal Button ButtonCancel;
        internal Button ButtonOK;
        internal Infragistics.Win.Misc.UltraLabel LabelInterfaceIP;
    }
}