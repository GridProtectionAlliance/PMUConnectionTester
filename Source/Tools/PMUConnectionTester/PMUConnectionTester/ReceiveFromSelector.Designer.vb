﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReceiveFromSourceSelector
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxUdpSourceIP = New Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit()
        Me.RadioButtonAnySource = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSpecificSource = New System.Windows.Forms.RadioButton()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBoxUdpSourceIP
        '
        Me.TextBoxUdpSourceIP.AutoSize = False
        Me.TextBoxUdpSourceIP.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask
        Me.TextBoxUdpSourceIP.Enabled = False
        Me.TextBoxUdpSourceIP.InputMask = "nnn\.nnn\.nnn\.nnn"
        Me.TextBoxUdpSourceIP.Location = New System.Drawing.Point(152, 35)
        Me.TextBoxUdpSourceIP.Name = "TextBoxUdpSourceIP"
        Me.TextBoxUdpSourceIP.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.TextBoxUdpSourceIP.Size = New System.Drawing.Size(92, 20)
        Me.TextBoxUdpSourceIP.TabIndex = 2
        Me.TextBoxUdpSourceIP.Text = "127.0.0.1"
        '
        'RadioButtonAnySource
        '
        Me.RadioButtonAnySource.AutoSize = True
        Me.RadioButtonAnySource.Checked = True
        Me.RadioButtonAnySource.Location = New System.Drawing.Point(13, 12)
        Me.RadioButtonAnySource.Name = "RadioButtonAnySource"
        Me.RadioButtonAnySource.Size = New System.Drawing.Size(102, 17)
        Me.RadioButtonAnySource.TabIndex = 0
        Me.RadioButtonAnySource.TabStop = True
        Me.RadioButtonAnySource.Text = "Use &Any Source"
        Me.RadioButtonAnySource.UseVisualStyleBackColor = True
        '
        'RadioButtonSpecificSource
        '
        Me.RadioButtonSpecificSource.AutoSize = True
        Me.RadioButtonSpecificSource.Location = New System.Drawing.Point(13, 36)
        Me.RadioButtonSpecificSource.Name = "RadioButtonSpecificSource"
        Me.RadioButtonSpecificSource.Size = New System.Drawing.Size(138, 17)
        Me.RadioButtonSpecificSource.TabIndex = 1
        Me.RadioButtonSpecificSource.Text = "Use &Specific Source IP:"
        Me.RadioButtonSpecificSource.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ButtonOK.Location = New System.Drawing.Point(169, 73)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 3
        Me.ButtonOK.Text = "&OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ReceiveFromSourceSelector
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(255, 108)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.TextBoxUdpSourceIP)
        Me.Controls.Add(Me.RadioButtonSpecificSource)
        Me.Controls.Add(Me.RadioButtonAnySource)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReceiveFromSourceSelector"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select UDP Receive From Source IP Filter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxUdpSourceIP As Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit
    Friend WithEvents RadioButtonAnySource As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSpecificSource As System.Windows.Forms.RadioButton
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
End Class
