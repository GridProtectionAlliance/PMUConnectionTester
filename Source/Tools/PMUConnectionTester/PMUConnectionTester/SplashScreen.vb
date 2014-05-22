'******************************************************************************************************
'  SplashScreen.vb - Gbtc
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
'  02/13/2007 - J. Ritchie Carroll
'       Initial version of source generated.
'
'******************************************************************************************************

Imports System.Configuration
Imports System.IO
Imports GSF.Reflection.AssemblyInfo

Public NotInheritable Class SplashScreen

    Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        With EntryAssembly
            ' Load splash screen image
            MainLayoutPanel.BackgroundImage = New System.Drawing.Bitmap(.GetEmbeddedResource(Me.GetType.Namespace & ".SplashScreen.png"))

            ' Set up the dialog text at runtime according to the application's assembly information
            ApplicationTitle.Text = .Title

#If DEBUG Then
            Version.Text = "DEBUG VERSION - DO NOT DEPLOY"
#Else
            With .Version
                Version.Text = "Version " & .Major & "." & .Minor & "." & .Build ' & "." & .Revision
            End With
#End If

            Copyright.Text = .Copyright
        End With

    End Sub

End Class