// =======================================================
// Yet Another (remote) Process Monitor (YAPM)
// Copyright (c) 2008-2009 Alain Descotes (violent_ken)
// https://sourceforge.net/projects/yaprocmon/
// =======================================================
//
//
// YAPM is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.
//
// YAPM is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with YAPM; if not, see http://www.gnu.org/licenses/.
//
// This code comes from here :
// http://asprosys.blogspot.com/2009/03/perils-and-pitfalls-of-launching.html


using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace YAPMLauncherService
{
    class InteractiveProcessRunner
    {
        private string m_ApplicationPath;
        private IntPtr m_SessionTokenHandle;

        //Remember many applications expect the application name to be the first
        //command line argument.
        public string CommandLine { get; set; }
        public string WorkingDirectory { get; set; }
        public bool CreateNoWindow { get; set; }
        public string Desktop { get; set; }

        private const int NORMAL_PRIORITY_CLASS = 0x20;
        private const int CREATE_UNICODE_ENVIRONMENT = 0x400;
        private const int CREATE_NO_WINDOW = 0x08000000;

        public InteractiveProcessRunner(string appPath, IntPtr hSessionToken)
        {
            m_ApplicationPath = appPath;
            m_SessionTokenHandle = hSessionToken;
            
            //Working directory must be set to something. If there is no working directory
            //CreateProcessAsUser will fail with invalid directory error.
            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);

            //Default desktop and window station name. This is valid for an interactive 
            //session but if launching into a non-interactive session the name of the window
            //station will need to be discovered. If there are multiple desktops in the 
            //window staton the name of the correct one will need to be discovered.
            Desktop = "WinSta0\\Default";
        }
        
        public int Run()
        {
            STARTUPINFO si = new STARTUPINFO();
            si.lpDesktop = Desktop;
            PROCESSINFO pi = new PROCESSINFO();

            int creationFlags = NORMAL_PRIORITY_CLASS | CREATE_UNICODE_ENVIRONMENT;
            creationFlags |= CreateNoWindow ? CREATE_NO_WINDOW : 0;

            //This creates the default environment block for this user. If you need 
            //something custom skip te CreateEnvironmentBlock (and DestroyEnvironmentBlock) 
            //calls. You need to handle the allocation of the memory and writing to 
            //it yourself.
            IntPtr envBlock;
            if (!CreateEnvironmentBlock(out envBlock, m_SessionTokenHandle, 0))
            {
                throw new System.ComponentModel.Win32Exception();
            }

            try
            {
                if (!CreateProcessAsUser(m_SessionTokenHandle, m_ApplicationPath, CommandLine, IntPtr.Zero,
                    IntPtr.Zero, 0, creationFlags, envBlock, WorkingDirectory,
                    si, pi))
                {
                    throw new System.ComponentModel.Win32Exception();
                }
            }
            finally
            {
                DestroyEnvironmentBlock(envBlock);
            }

            CloseHandle(pi.hThread);
            CloseHandle(pi.hProcess);

            return pi.dwProcessId;
        }

        [DllImport("AdvApi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CreateProcessAsUser(IntPtr hToken, string lpApplicationName,
            string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
            int bInheritHandles, int dwCreationFlags, IntPtr lpEnvironment,
            string lpCurrentDirectory, STARTUPINFO lpStartupInfo, 
            [Out]PROCESSINFO lpProcessInformation);


        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObj);

        [DllImport("userenv.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CreateEnvironmentBlock(out IntPtr lpEnvironment,
            IntPtr hToken, int bInherit);

        [DllImport("userenv.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DestroyEnvironmentBlock(IntPtr lpEnvironment);

    }
}
