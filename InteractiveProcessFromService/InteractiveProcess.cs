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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

using System.Threading;
using System.Runtime.InteropServices;

namespace YAPMLauncherService
{
    partial class InteractiveProcess : ServiceBase
    {
        public InteractiveProcess()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ThreadPool.QueueUserWorkItem(LaunchService);
        }

        public void LaunchService(object context)
        {
            IntPtr hSessionToken = IntPtr.Zero;
            try
            {
                SessionFinder sf = new SessionFinder();
                //Get the ineractive console session.
                hSessionToken = sf.GetLocalInteractiveSession();
                
                //Use this instead to get the session of a specific user.
                //hSessionToken = sf.GetSessionByUser(Environment.MachineName, "InteractiveLaunchUser");

                if (hSessionToken != IntPtr.Zero)
                {
                    //Run notepad in the session that we found using the default
                    //values for working directory and desktop.
                    InteractiveProcessRunner runner =
                        new InteractiveProcessRunner("c:\\windows\\system32\\yapm.exe", hSessionToken);
                    runner.CommandLine = " -server -autoconnect -port 8085";
                    runner.Run();
                }
                else
                {
                    EventLog.WriteEntry("Session not found.", EventLogEntryType.Error);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(string.Format("Exception thrown: {0}{0}{1}", Environment.NewLine, ex), EventLogEntryType.Error);
            }
            finally
            {
                if (hSessionToken != IntPtr.Zero)
                {
                    CloseHandle(hSessionToken);
                }
            }
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObj);

    }
}
