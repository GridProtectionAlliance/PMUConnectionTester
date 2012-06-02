Public Class NetworkInterfaceSelector

    Private Sub ComboBoxNetworkInterfaces_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxNetworkInterfaces.SelectedIndexChanged

        Dim selection As Tuple(Of String, String, String) = TryCast(ComboBoxNetworkInterfaces.SelectedItem, Tuple(Of String, String, String))

        If selection IsNot Nothing Then
            If PMUConnectionTester.ForceIPv4 Then
                LabelInterfaceIP.Text = selection.Item2
            Else
                LabelInterfaceIP.Text = selection.Item3
            End If
        End If

    End Sub

End Class