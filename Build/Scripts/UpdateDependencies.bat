::*******************************************************************************************************
::  UpdateDependencies.bat - Gbtc
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

SETLOCAL

SET pwd=%CD%
IF "%git%" == "" SET git=%PROGRAMFILES(X86)%\Git\cmd\git.exe
IF "%replace%" == "" SET replace=\\GPAWEB\NightlyBuilds\Tools\ReplaceInFiles\ReplaceInFiles.exe

SET defaulttarget=%LOCALAPPDATA%\Temp\PMUConnectionTester
IF "%remote%" == "" SET remote=git@github.com:GridProtectionAlliance/pmuconnectiontester.git
IF "%gsf%" == "" SET gsf=\\GPAWEB\NightlyBuilds\GridSolutionsFramework\Beta
IF "%target%" == "" SET target=%defaulttarget%

SET gsflibraries=%gsf%\Libraries\*.*
SET gsfdependencies=%target%\Source\Dependencies\GSF
SET sourcemasterbuild=%gsf%\Build Scripts\MasterBuild.buildproj
SET targetmasterbuild=%target%\Build\Scripts

ECHO.
ECHO Entering working directory...
IF EXIST "%target%" IF NOT EXIST "%target%"\.git RMDIR /S /Q "%target%"
IF NOT EXIST "%target%" MKDIR "%target%"
CD /D %target%

IF NOT EXIST .git GOTO CloneRepository
IF NOT "%target%" == "%defaulttarget%" GOTO UpdateDependencies
GOTO UpdateRepository

:CloneRepository
ECHO.
ECHO Getting latest version...
"%git%" clone "%remote%" .
GOTO UpdateDependencies

:UpdateRepository
ECHO.
ECHO Updating to latest version...
"%git%" fetch
"%git%" reset --hard origin/master
"%git%" clean -f -d -x
GOTO UpdateDependencies

:UpdateDependencies
ECHO.
ECHO Updating dependencies...
XCOPY "%gsflibraries%" "%gsfdependencies%\" /E /U /Y
XCOPY "%sourcemasterbuild%" "%targetmasterbuild%\" /Y

:CommitChanges
ECHO.
ECHO Committing updates to local repository...
"%git%" add .
"%git%" commit -m "Updated GSF dependencies."
IF NOT "%donotpush%" == "" GOTO Finish

:PushChanges
ECHO.
ECHO Pushing changes to remote repository...
"%git%" push
CD /D %pwd%

:Finish
ECHO.
ECHO Update complete