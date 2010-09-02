'******************************************************************************************************
'  ConnectionSettings.vb - Gbtc
'
'  Copyright � 2010, Grid Protection Alliance.  All Rights Reserved.
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
'  03/16/2006 - J. Ritchie Carroll
'       Initial version of source generated.
'
'******************************************************************************************************

Imports System.Runtime.Serialization
Imports TVA.Communication
Imports TVA.PhasorProtocols

<Serializable()> _
Public Class ConnectionSettings

    Public PhasorProtocol As PhasorProtocol
    Public TransportProtocol As TransportProtocol
    Public ConnectionString As String
    Public PmuID As Integer
    Public FrameRate As Integer
    Public AutoRepeatPlayback As Boolean
    Public ByteEncodingDisplayFormat As Integer
    Public ConnectionParameters As IConnectionParameters

    Public ReadOnly Property This() As ConnectionSettings
        Get
            Return Me
        End Get
    End Property

End Class
