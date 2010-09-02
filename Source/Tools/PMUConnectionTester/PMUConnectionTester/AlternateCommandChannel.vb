'******************************************************************************************************
'  AlternateCommandChannel.vb - Gbtc
'
'  Copyright © 2010, Grid Protection Alliance.  All Rights Reserved.
'
'  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
'  the NOTICE file distributed with this work for additional information regarding copyright ownership.
'  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
'  not use this file except in compliance with the License. You may obtain a copy of the License at:
'
'      http://www.opensource.org/licenses/eclipse-1.0.php
'
'  Unless agreed to in writing, the subject software distributed under the License is distributed on an
'  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
'  License for the specific language governing permissions and limitations.
'
'  Code Modification History:
'  ----------------------------------------------------------------------------------------------------
'  06/15/2009 - James R. Carroll
'       Generated original version of source code.
'
'******************************************************************************************************

Imports System.IO
Imports System.Text
Imports System.Windows.Forms.DialogResult
Imports TVA
Imports TVA.Communication
Imports TVA.Common
Imports Infragistics.Win.UltraWinMaskedEdit

Public Class AlternateCommandChannel

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Initialize serial port selection lists
        For Each port As String In Ports.SerialPort.GetPortNames()
            ComboBoxSerialPorts.Items.Add(port)
        Next

        For Each parity As String In [Enum].GetNames(GetType(Ports.Parity))
            ComboBoxSerialParities.Items.Add(parity)
        Next

        For Each stopbit As String In [Enum].GetNames(GetType(Ports.StopBits))
            ComboBoxSerialStopBits.Items.Add(stopbit)
        Next

        ' Default to TCP tab
        TabControlCommunications.Tabs(TransportProtocol.Tcp).Selected = True

    End Sub

    Private Sub CheckBoxUndefined_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxUndefined.CheckedChanged

        ' Enable or disable command channel settings tab
        TabControlCommunications.Enabled = Not CheckBoxUndefined.Checked

        ' Automatically show change in link label text/format on parent for to cue user of defined state indicator
        If Me.Visible Then PMUConnectionTester.UpdateAlternateCommandChannelLabel()

    End Sub

    Private Sub ButtonBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBrowse.Click

        With PMUConnectionTester.OpenFileDialog
            .Title = "Open Capture File"
            .Filter = "Captured Files (*.PmuCapture)|*.PmuCapture|All Files|*.*"
            .FileName = ""
            If .ShowDialog(Me) = OK Then
                TextBoxFileCaptureName.Text = .FileName()
            End If
        End With

    End Sub

    Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxFileCaptureName.GotFocus, TextBoxSerialDataBits.GotFocus, TextBoxTcpHostIP.GotFocus, TextBoxTcpPort.GotFocus

        ' Select all text box contents upon focus or selection
        Dim maskedEdit As UltraMaskedEdit = TryCast(sender, UltraMaskedEdit)

        If maskedEdit IsNot Nothing Then
            If maskedEdit.EditAs = EditAsType.UseSpecifiedMask Then
                maskedEdit.SelectAll()
            Else
                maskedEdit.SelectionStart = 0
                maskedEdit.SelectionLength = maskedEdit.Text.Length
            End If
        Else
            Dim windowsTextBox As TextBox = TryCast(sender, TextBox)

            If windowsTextBox IsNot Nothing Then
                windowsTextBox.SelectAll()
            End If
        End If

    End Sub

    Private Sub TextBox_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles _
        TextBoxFileCaptureName.MouseClick, TextBoxSerialDataBits.MouseClick, TextBoxTcpHostIP.MouseClick, TextBoxTcpPort.MouseClick

        TextBox_GotFocus(sender, e)

    End Sub

    Public ReadOnly Property IsDefined() As Boolean
        Get
            Return Not CheckBoxUndefined.Checked
        End Get
    End Property

    Public Property ConnectionString() As String
        Get
            If CheckBoxUndefined.Checked Then
                Return ""
            Else
                Select Case TabControlCommunications.SelectedTab.Index
                    Case TransportProtocol.Tcp
                        Return _
                            "; commandchannel={protocol=Tcp" & _
                            "; server=" & TextBoxTcpHostIP.Text & _
                            "; port=" & TextBoxTcpPort.Text & _
                            IIf(PMUConnectionTester.m_applicationSettings.ForceIPv4, "; interface=0.0.0.0", "") & "}"
                    Case TransportProtocol.Serial - 1 ' UDP removed from tab set...
                        Return _
                            "; commandchannel={protocol=Serial" & _
                            "; port=" & ComboBoxSerialPorts.Text & _
                            "; baudrate=" & ComboBoxSerialBaudRates.Text & _
                            "; parity=" & ComboBoxSerialParities.Text & _
                            "; stopbits=" & ComboBoxSerialStopBits.Text & _
                            "; databits=" & TextBoxSerialDataBits.Text & _
                            "; dtrenable=" & CheckBoxSerialDTR.Checked.ToString() & _
                            "; rtsenable=" & CheckBoxSerialRTS.Checked.ToString() & "}"
                    Case TransportProtocol.File - 1 ' UDP removed from tab set...
                        Return _
                            "; commandchannel={protocol=File" & _
                            "; file=" & TextBoxFileCaptureName.Text & "}"
                    Case Else
                        Return ""
                End Select
            End If
        End Get
        Set(ByVal value As String)
            Dim connectionData As Dictionary(Of String, String) = value.ParseKeyValuePairs()

            If connectionData.ContainsKey("commandchannel") Then
                Dim protocol As TransportProtocol

                CheckBoxUndefined.Checked = False
                connectionData = connectionData("commandchannel").ParseKeyValuePairs()
                protocol = DirectCast([Enum].Parse(GetType(TransportProtocol), connectionData("protocol"), True), TransportProtocol)

                ' Load remaining connection settings
                TabControlCommunications.Tabs(IIf(protocol > TransportProtocol.Tcp, protocol - 1, protocol)).Selected = True

                Select Case protocol
                    Case TransportProtocol.Tcp
                        TextBoxTcpPort.Text = connectionData("port")
                        PMUConnectionTester.AssignHostIP(TextBoxTcpHostIP, connectionData("server"))
                    Case TransportProtocol.Serial
                        ComboBoxSerialPorts.Text = connectionData("port")
                        ComboBoxSerialBaudRates.Text = connectionData("baudrate")
                        ComboBoxSerialParities.Text = connectionData("parity")
                        ComboBoxSerialStopBits.Text = connectionData("stopbits")
                        TextBoxSerialDataBits.Text = connectionData("databits")
                        CheckBoxSerialDTR.Checked = connectionData("dtrenable").ParseBoolean()
                        CheckBoxSerialRTS.Checked = connectionData("rtsenable").ParseBoolean()
                    Case TransportProtocol.File
                        TextBoxFileCaptureName.Text = connectionData("file")
                End Select
            Else
                CheckBoxUndefined.Checked = True
            End If
        End Set
    End Property

End Class