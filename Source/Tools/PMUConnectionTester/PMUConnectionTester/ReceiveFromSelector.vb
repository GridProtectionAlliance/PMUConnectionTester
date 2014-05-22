Imports GSF

Public Class ReceiveFromSourceSelector

    Private Sub RadioButtonSpecificSource_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadioButtonSpecificSource.CheckedChanged

        TextBoxUdpSourceIP.Enabled = RadioButtonSpecificSource.Checked

    End Sub

    Public Property ConnectionString() As String
        Get
            If RadioButtonAnySource.Checked Then
                Return ""
            Else
                Return "; receiveFrom=" & TextBoxUdpSourceIP.Text
            End If
        End Get
        Set(ByVal value As String)
            Dim connectionData As Dictionary(Of String, String) = value.ParseKeyValuePairs()
            Dim udpSourceIP As String

            If connectionData.TryGetValue("receiveFrom", udpSourceIP) Then
                RadioButtonSpecificSource.Checked = True
                PMUConnectionTester.AssignHostIP(TextBoxUdpSourceIP, udpSourceIP)
            Else
                RadioButtonAnySource.Checked = True
            End If
        End Set
    End Property

End Class