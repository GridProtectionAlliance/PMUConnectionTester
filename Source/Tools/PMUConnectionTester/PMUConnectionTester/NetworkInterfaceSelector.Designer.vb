<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NetworkInterfaceSelector
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
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ComboBoxNetworkInterfaces = New System.Windows.Forms.ComboBox()
        Me.LabelNetworkInterfaces = New Infragistics.Win.Misc.UltraLabel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.LabelInterfaceIP = New Infragistics.Win.Misc.UltraLabel()
        Me.SuspendLayout()
        '
        'ComboBoxNetworkInterfaces
        '
        Me.ComboBoxNetworkInterfaces.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ComboBoxNetworkInterfaces.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBoxNetworkInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxNetworkInterfaces.Location = New System.Drawing.Point(12, 30)
        Me.ComboBoxNetworkInterfaces.Name = "ComboBoxNetworkInterfaces"
        Me.ComboBoxNetworkInterfaces.Size = New System.Drawing.Size(375, 21)
        Me.ComboBoxNetworkInterfaces.TabIndex = 1
        '
        'LabelNetworkInterfaces
        '
        Me.LabelNetworkInterfaces.Location = New System.Drawing.Point(12, 12)
        Me.LabelNetworkInterfaces.Name = "LabelNetworkInterfaces"
        Me.LabelNetworkInterfaces.Size = New System.Drawing.Size(105, 23)
        Me.LabelNetworkInterfaces.TabIndex = 0
        Me.LabelNetworkInterfaces.Text = "&Network Interfaces:"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(312, 63)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "&Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ButtonOK.Location = New System.Drawing.Point(231, 63)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 2
        Me.ButtonOK.Text = "&OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'LabelInterfaceIP
        '
        Appearance2.TextHAlignAsString = "Right"
        Appearance2.TextVAlignAsString = "Top"
        Me.LabelInterfaceIP.Appearance = Appearance2
        Me.LabelInterfaceIP.Location = New System.Drawing.Point(112, 12)
        Me.LabelInterfaceIP.Name = "LabelInterfaceIP"
        Me.LabelInterfaceIP.Size = New System.Drawing.Size(275, 20)
        Me.LabelInterfaceIP.TabIndex = 4
        Me.LabelInterfaceIP.Text = "0.0.0.0"
        '
        'NetworkInterfaceSelector
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(399, 98)
        Me.ControlBox = False
        Me.Controls.Add(Me.ComboBoxNetworkInterfaces)
        Me.Controls.Add(Me.LabelInterfaceIP)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.LabelNetworkInterfaces)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NetworkInterfaceSelector"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Desired Network Interface"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBoxNetworkInterfaces As System.Windows.Forms.ComboBox
    Friend WithEvents LabelNetworkInterfaces As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents LabelInterfaceIP As Infragistics.Win.Misc.UltraLabel
End Class
