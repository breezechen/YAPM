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
using System.Configuration.Install;
using System.ComponentModel;
using System.ServiceProcess;

namespace YAPMLauncherService
{
    [RunInstaller(true)]
    public class InteractiveProcessInstaller : Installer
    {
        //Standard service installation.
        public InteractiveProcessInstaller()
        {
            ServiceProcessInstaller pi = new ServiceProcessInstaller();
            ServiceInstaller si = new ServiceInstaller();

            //Launching into another session like this requires the service
            //account to have the SE_TCB_NAME (Act as part of the operating 
            //system) privilege. By default only the loacl system account 
            //has this privilege.
            pi.Account = ServiceAccount.LocalSystem;
            si.ServiceName = "InteractiveProcess";
            si.Description = "Launches processes into interactive sessions.";
            si.DisplayName = "Interactive Process Launcher";

            this.Installers.AddRange(new Installer[] { pi, si });
        }
    }
}
