﻿//******************************************************************************************************
//  Program.cs - Gbtc
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
//  03/26/2022 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

global using static ConnectionTester.Program;

using System;
using System.Windows.Forms;

namespace ConnectionTester;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Forms.SplashScreen.Show();
        Application.DoEvents();

        Application.Run(Forms.PMUConnectionTester);
    }

    internal static SingletonForms Forms => s_singletonForms ??= new SingletonForms();

    [ThreadStatic]
    private static SingletonForms s_singletonForms;
}