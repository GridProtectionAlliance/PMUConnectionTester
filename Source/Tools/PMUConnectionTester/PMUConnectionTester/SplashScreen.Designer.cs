using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ConnectionTester
{
    public partial class SplashScreen : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components is object)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        internal Label ApplicationTitle;
        internal Label Copyright;
        internal TableLayoutPanel MainLayoutPanel;

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            MainLayoutPanel = new TableLayoutPanel();
            DetailsLayoutPanel = new TableLayoutPanel();
            Version = new Label();
            Copyright = new Label();
            ApplicationTitle = new Label();
            MainLayoutPanel.SuspendLayout();
            DetailsLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainLayoutPanel
            // 
            MainLayoutPanel.BackgroundImageLayout = ImageLayout.None;
            MainLayoutPanel.ColumnCount = 2;
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 207f));
            MainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 186f));
            MainLayoutPanel.Controls.Add(DetailsLayoutPanel, 1, 1);
            MainLayoutPanel.Controls.Add(ApplicationTitle, 1, 0);
            MainLayoutPanel.Dock = DockStyle.Fill;
            MainLayoutPanel.Location = new Point(0, 0);
            MainLayoutPanel.Name = "MainLayoutPanel";
            MainLayoutPanel.RowCount = 2;
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 198f));
            MainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 58f));
            MainLayoutPanel.Size = new Size(497, 296);
            MainLayoutPanel.TabIndex = 0;
            // 
            // DetailsLayoutPanel
            // 
            DetailsLayoutPanel.Anchor = AnchorStyles.None;
            DetailsLayoutPanel.BackColor = Color.Transparent;
            DetailsLayoutPanel.ColumnCount = 1;
            DetailsLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            DetailsLayoutPanel.Controls.Add(Version, 0, 0);
            DetailsLayoutPanel.Controls.Add(Copyright, 0, 1);
            DetailsLayoutPanel.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
            DetailsLayoutPanel.Location = new Point(213, 208);
            DetailsLayoutPanel.Name = "DetailsLayoutPanel";
            DetailsLayoutPanel.RowCount = 2;
            DetailsLayoutPanel.RowStyles.Add(new RowStyle());
            DetailsLayoutPanel.RowStyles.Add(new RowStyle());
            DetailsLayoutPanel.Size = new Size(277, 77);
            DetailsLayoutPanel.TabIndex = 1;
            // 
            // Version
            // 
            Version.Anchor = AnchorStyles.None;
            Version.BackColor = Color.Transparent;
            Version.Font = new Font("Tahoma", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Version.ForeColor = Color.LemonChiffon;
            Version.Location = new Point(3, 0);
            Version.Name = "Version";
            Version.Size = new Size(274, 49);
            Version.TabIndex = 1;
            Version.Text = "Version";
            Version.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Copyright
            // 
            Copyright.Anchor = AnchorStyles.Right;
            Copyright.BackColor = Color.Transparent;
            Copyright.Font = new Font("Tahoma", 8.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            Copyright.ForeColor = Color.LemonChiffon;
            Copyright.Location = new Point(112, 49);
            Copyright.Name = "Copyright";
            Copyright.Size = new Size(165, 28);
            Copyright.TabIndex = 2;
            Copyright.Text = "Copyright";
            Copyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ApplicationTitle
            // 
            ApplicationTitle.Anchor = AnchorStyles.None;
            ApplicationTitle.BackColor = Color.Transparent;
            ApplicationTitle.Font = new Font("Tahoma", 22f, FontStyle.Bold, GraphicsUnit.Point, 0);
            ApplicationTitle.ForeColor = Color.LemonChiffon;
            ApplicationTitle.Location = new Point(211, 28);
            ApplicationTitle.Name = "ApplicationTitle";
            ApplicationTitle.Size = new Size(281, 142);
            ApplicationTitle.TabIndex = 0;
            ApplicationTitle.Text = "ApplicationTitle";
            ApplicationTitle.TextAlign = ContentAlignment.BottomCenter;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 296);
            ControlBox = false;
            Controls.Add(MainLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SplashScreen";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            MainLayoutPanel.ResumeLayout(false);
            DetailsLayoutPanel.ResumeLayout(false);
            Load += new EventHandler(SplashScreen_Load);
            ResumeLayout(false);
        }

        internal Label Version;
        internal TableLayoutPanel DetailsLayoutPanel;
    }
}