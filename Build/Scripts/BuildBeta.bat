::*******************************************************************************************************
::  BuildBeta.bat - Gbtc
::
::  Copyright © 2017, Grid Protection Alliance.  All Rights Reserved.
::
::  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
::  the NOTICE file distributed with this work for additional information regarding copyright ownership.
::  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may
::  not use this file except in compliance with the License. You may obtain a copy of the License at:
::
::      http://opensource.org/licenses/MIT
::
::  Unless agreed to in writing, the subject software distributed under the License is distributed on an
::  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
::  License for the specific language governing permissions and limitations.
::
::  Code Modification History:
::  -----------------------------------------------------------------------------------------------------
::  03/07/2017 - Stephen C. Wills
::       Generated original version of source code.
::
::*******************************************************************************************************

@ECHO OFF

SetLocal

IF NOT "%1" == "" SET logflag=/l:FileLogger,Microsoft.Build.Engine;logfile=%1

ECHO BuildBeta: C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe PMUConnectionTester.buildproj /p:ForceBuild=true %logflag%
"C:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin" PMUConnectionTester.buildproj /p:ForceBuild=true %logFlag%