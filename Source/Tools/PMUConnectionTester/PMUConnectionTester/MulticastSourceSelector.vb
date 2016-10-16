'******************************************************************************************************
'  MulticastSourceSelector.vb - Gbtc
'
'  Copyright © 2016, Grid Protection Alliance.  All Rights Reserved.
'
'  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
'  the NOTICE file distributed with this work for additional information regarding copyright ownership.
'  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may
'  not use this file except in compliance with the License. You may obtain a copy of the License at:
'
'      http://opensource.org/licenses/MIT
'
'  Unless agreed to in writing, the subject software distributed under the License is distributed on an
'  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
'  License for the specific language governing permissions and limitations.
'
'  Code Modification History:
'  ----------------------------------------------------------------------------------------------------
'  10/16/2014 - J. Ritchie Carroll
'       Generated original version of source code.
'
'******************************************************************************************************

Imports GSF

Public Class MulticastSourceSelector

    Private Sub RadioButtonSpecificSource_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSpecificSource.CheckedChanged

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