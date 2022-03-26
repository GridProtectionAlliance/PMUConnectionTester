//******************************************************************************************************
//  SplashScreen.cs - Gbtc
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
//  02/13/2007 - J. Ritchie Carroll
//       Initial version of source generated.
//  03/26/2022 - J. Ritchie Carroll
//       Generated original version of source code.
//       Migrated code from VB to C# assisted with Code Converter (VB - C#):
//       https://marketplace.visualstudio.com/items?itemName=SharpDevelopTeam.CodeConverter
//
//******************************************************************************************************

using System;
using System.Drawing;
using GSF.Reflection;
using static GSF.Reflection.AssemblyInfo;

namespace ConnectionTester;

public sealed partial class SplashScreen
{
    public SplashScreen()
    {
        InitializeComponent();
    }

    private void SplashScreen_Load(object sender, EventArgs e)
    {
        AssemblyInfo assembly = EntryAssembly;

        // Load splash screen image
        MainLayoutPanel.BackgroundImage = new Bitmap(assembly.GetEmbeddedResource($"{nameof(ConnectionTester)}.SplashScreen.png"));

        // Set up the dialog text at runtime according to the application's assembly information
        ApplicationTitle.Text = assembly.Title;
        {
            Version version = assembly.Version;
            Version.Text = $"Version {version.Major}.{version.Minor}.{version.Build}"; // & "." & .Revision
        }

    #if DEBUG
        Version.Text += $"{Environment.NewLine}DEBUG VERSION - DO NOT DEPLOY";
    #endif
        Copyright.Text = assembly.Copyright;
    }
}