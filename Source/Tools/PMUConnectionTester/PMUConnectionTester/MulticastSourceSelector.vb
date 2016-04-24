Imports GSF

Public Class MulticastSourceSelector

    Private Sub RadioButtonSpecificSource_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadioButtonSpecificSource.CheckedChanged

        TextBoxMulticastSourceIP.Enabled = RadioButtonSpecificSource.Checked

    End Sub

    Public Property ConnectionString() As String
        Get
            If RadioButtonAnySource.Checked Then
                Return ""
            Else
                Return "; multicastSource=" & TextBoxMulticastSourceIP.Text
            End If
        End Get
        Set(ByVal value As String)
            Dim connectionData As Dictionary(Of String, String) = value.ParseKeyValuePairs()
            Dim multicastSourceIP As String

            If connectionData.TryGetValue("multicastSource", multicastSourceIP) Then
                RadioButtonSpecificSource.Checked = True
                PMUConnectionTester.AssignHostIP(TextBoxMulticastSourceIP, multicastSourceIP)
            Else
                RadioButtonAnySource.Checked = True
            End If
        End Set
    End Property

End Class