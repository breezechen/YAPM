rem      =======================================================
rem     Yet Another (remote) Process Monitor (YAPM)
rem     Copyright (c) 2008-2009 Alain Descotes (violent_ken)
rem     https://sourceforge.net/projects/yaprocmon/
rem     =======================================================
rem    
rem    
rem     YAPM is free software; you can redistribute it and/or modify
rem     it under the terms of the GNU General Public License as published by
rem     the Free Software Foundation; either version 3 of the License, or
rem     (at your option) any later version.
rem    
rem     YAPM is distributed in the hope that it will be useful,
rem     but WITHOUT ANY WARRANTY; without even the implied warranty of
rem     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
rem     GNU General Public License for more details.
rem    
rem     You should have received a copy of the GNU General Public License
rem     along with YAPM; if not, see http://www.gnu.org/licenses/.
rem    
rem    
rem     WHAT IT DOES :
rem     1) This file generates a single executable by merging
rem     all assemblies compiled with YAPM solution.
rem     This uses ILMerge.exe from Microsoft
rem    
rem     2) It creates an output directory in the root folder and copied
rem     in it all the files needed by YAPM
rem    
rem    
rem     MODIFICATIONS OF THIS SCRIPT
rem     18/08/09 - Initial version (violent_ken)
rem	    29/08/09 - Now it creates the setup
rem	    31/08/09 - Now it create a source package
rem	    16/09/09 - Added SecureChannel.dll to ILMerge
rem	    20/12/09 - Removed 7z generation for now


rem     Remove YAPM.exe
erase .\Bin\YAPM.exe

rem     Make YAPM.exe
..\..\..\Tools\ILMerge\ILMerge.exe YAPM.exe VistaMenu.dll SecurePasswordTextBox.dll System.Windows.Forms.Ribbon.dll TaskDialog.dll SecureChannel.dll -out:.\Bin\YAPM.exe

rem     Remove YAPM.pdb
erase .\Bin\YAPM.pdb

rem     Create zip file
..\..\..\Tools\7za\7za.exe a -tzip -y ..\..\..\RELEASE\YAPM-vx.x.x-binaries.zip -r .\Bin\* -x!.svn

rem     Create 7z file
rem ..\..\..\Tools\7za\7za.exe a -t7z -y ..\..\..\RELEASE\YAPM-vx.x.x-binaries.7z -r .\Bin\* -x!.svn

rem     Build setup
"C:\Program Files\Inno Setup 5\Compil32.exe" /cc ..\..\..\Setup\ISS\main.iss

rem	Build source package
rem "..\..\..\Source Packager\Packager\bin\Release\Packager.exe" -auto ..\..\..\..\RELEASE\YAPM-vx.x.x-source.zip