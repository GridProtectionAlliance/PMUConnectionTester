//******************************************************************************************************
//  NetworkInterfaceSelector.cs - Gbtc
//
//  Copyright © 2022, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
//  file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  10/16/2014 - J. Ritchie Carroll
//       Generated original version of source code.
//  03/26/2022 - J. Ritchie Carroll
//       Generated original version of source code.
//       Migrated code from VB to C# assisted with Code Converter (VB - C#):
//       https://marketplace.visualstudio.com/items?itemName=SharpDevelopTeam.CodeConverter
//
//******************************************************************************************************

using System;

namespace ConnectionTester;

public partial class NetworkInterfaceSelector
{
    public NetworkInterfaceSelector()
    {
        InitializeComponent();
    }

    private void ComboBoxNetworkInterfaces_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ComboBoxNetworkInterfaces.SelectedItem is Tuple<string, string, string> selection)
            LabelInterfaceIP.Text = Forms.PMUConnectionTester.ForceIPv4 ? selection.Item2 : selection.Item3;
    }
}