using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ConnectionTester
{
    public partial class MulticastSourceSelector : Form
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
            TextBoxMulticastSourceIP = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            RadioButtonAnySource = new RadioButton();
            RadioButtonSpecificSource = new RadioButton();
            RadioButtonSpecificSource.CheckedChanged += new EventHandler(RadioButtonSpecificSource_CheckedChanged);
            ButtonOK = new Button();
            SuspendLayout();
            // 
            // TextBoxMulticastSourceIP
            // 
            TextBoxMulticastSourceIP.AutoSize = false;
            TextBoxMulticastSourceIP.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            TextBoxMulticastSourceIP.Enabled = false;
            TextBoxMulticastSourceIP.InputMask = @"nnn\.nnn\.nnn\.nnn";
            TextBoxMulticastSourceIP.Location = new Point(152, 35);
            TextBoxMulticastSourceIP.Name = "TextBoxMulticastSourceIP";
            TextBoxMulticastSourceIP.PromptChar = ' ';
            TextBoxMulticastSourceIP.Size = new Size(92, 20);
            TextBoxMulticastSourceIP.TabIndex = 2;
            TextBoxMulticastSourceIP.Text = "127.0.0.1";
            // 
            // RadioButtonAnySource
            // 
            RadioButtonAnySource.AutoSize = true;
            RadioButtonAnySource.Checked = true;
            RadioButtonAnySource.Location = new Point(13, 12);
            RadioButtonAnySource.Name = "RadioButtonAnySource";
            RadioButtonAnySource.Size = new Size(102, 17);
            RadioButtonAnySource.TabIndex = 0;
            RadioButtonAnySource.TabStop = true;
            RadioButtonAnySource.Text = "Use &Any Source";
            RadioButtonAnySource.UseVisualStyleBackColor = true;
            // 
            // RadioButtonSpecificSource
            // 
            RadioButtonSpecificSource.AutoSize = true;
            RadioButtonSpecificSource.Location = new Point(13, 36);
            RadioButtonSpecificSource.Name = "RadioButtonSpecificSource";
            RadioButtonSpecificSource.Size = new Size(138, 17);
            RadioButtonSpecificSource.TabIndex = 1;
            RadioButtonSpecificSource.Text = "Use &Specific Source IP:";
            RadioButtonSpecificSource.UseVisualStyleBackColor = true;
            // 
            // ButtonOK
            // 
            ButtonOK.DialogResult = DialogResult.OK;
            ButtonOK.Location = new Point(169, 73);
            ButtonOK.Name = "ButtonOK";
            ButtonOK.Size = new Size(75, 23);
            ButtonOK.TabIndex = 3;
            ButtonOK.Text = "&OK";
            ButtonOK.UseVisualStyleBackColor = true;
            // 
            // MulticastSourceSelector
            // 
            AcceptButton = ButtonOK;
            AutoScaleDimensions = new SizeF(6.0f, 13.0f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(255, 108);
            ControlBox = false;
            Controls.Add(ButtonOK);
            Controls.Add(TextBoxMulticastSourceIP);
            Controls.Add(RadioButtonSpecificSource);
            Controls.Add(RadioButtonAnySource);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MulticastSourceSelector";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Multicast Source";
            ResumeLayout(false);
            PerformLayout();
        }

        internal Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit TextBoxMulticastSourceIP;
        internal RadioButton RadioButtonAnySource;
        internal RadioButton RadioButtonSpecificSource;
        internal Button ButtonOK;
    }
}