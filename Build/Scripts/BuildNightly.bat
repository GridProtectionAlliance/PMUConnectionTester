::*******************************************************************************************************
::  BuildNightly.bat - Gbtc
::
::  Tennessee Valley Authority, 2009
::  No copyright is claimed pursuant to 17 USC � 105.  All Other Rights Reserved.
::
::  This software is made freely available under the TVA Open Source Agreement (see below).
::
::  Code Modification History:
::  -----------------------------------------------------------------------------------------------------
::  10/20/2009 - Pinal C. Patel
::       Generated original version of source code.
::  09/14/2010 - Mihir Brahmbhatt
::		 Change Framework path from v3.5 to v4.0
::  10/03/2010 - Pinal C. Patel
::       Updated to use MSBuild 4.0.
::
::*******************************************************************************************************

@ECHO OFF
C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild.exe PMUConnectionTester.buildproj /l:FileLogger,Microsoft.Build.Engine;logfile=PMUConnectionTester.output