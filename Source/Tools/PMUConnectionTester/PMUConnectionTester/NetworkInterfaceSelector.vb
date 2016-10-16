'******************************************************************************************************
'  NetworkInterfaceSelector.vb - Gbtc
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

Public Class NetworkInterfaceSelector

    Private Sub ComboBoxNetworkInterfaces_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxNetworkInterfaces.SelectedIndexChanged

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