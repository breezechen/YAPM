Yet Another (remote) Process Monitor (YAPM) v2.0 Alpha


SUMMARY
	* Application description
	* How to use
	* Features
	* Requirements
	* Description of files
	* Known bugs
	* Licenses
	* Contact & links


	
APPLICATION DESCRIPTION

Yet Another (remote) Process Monitor (YAPM) is a powerful aplication for Windows NT that monitors the processes running on your local system, or on a remote system. You can manage their threads, windows, handles, modules, network connections, memory regions, privileges... etc. YAPM also monitors all services running.



HOW TO USE

Help is available here : http://yaprocmon.sourceforge.net/help.html.
But don't worry, YAPM is easy and intuitive ! It just looks like Process Explorer or other task manager.
No installation is needed, just double-click on YAPM to run it. For now, you should run YAPM with an administrator account.

If you want to monitor remote systems, there are two methods :

1) Client-server architecture (best method). You'll have to run a server on the remote server, but it's powerful than WMI method.
- Run YAPM as a server on the remote system you want to monitor (use -server option, or launch the *.bat file)
- Clic 'connect' on the main form of the server
- Run YAPM normaly on the local system
- On the local system : Menu->Change connection type->Disconnect->Server tab->Configure IP of remote system->Connect
- You can refresh views with F5 key (there is no auto refresh)

2) WMI method. You do not have to run anything on the remote system, but it's limited : you can not do all what you (you can only enumerate services, processes, modules and threads, start/stop them, change priority... and other basic actions).
- Run YAPM normaly on the local system
- Menu->Change connection type->Disconnect->WMI tab->Configure remote system name and user/password->Connect (you MUST specify a password, it won't work with an account without password)
- You can refresh views with F5 key (there is no auto refresh)



FEATURES

All commons features of a process/service manager are availables in YAPM.
All these features are available for a remote system (with client-server architecture).
But there are some unique useful features for process/service monitoring.

YAPM supports :
- viewing and manipulating processes (kill, pause, resume, affinity...)
- viewing and manipulating services (start, stop...)
- viewing and manipulating modules, windows, tasks, threads, handles, network connections, memory regions...
- viewing and manipulating privileges, strings in file/memory, environment variables...
- viewing and searching in process memory (with a build-in hex editor)
- viewing complete history of processes statistics
- saving complete and custom reports
- monitoring activities of running processes (handles created, connections opened...)
- viewing complete statistics and graphs about processes and system (memory/cpu usage...)
- Emergency Hotkeys feature (assign a custom command to a set of keyboard keys. Example : Ctrl+Shift+Suppr to kill foreground application)
- State Based Action feature (allows to assign a custom command which is launched when the state of a process changes. Example : reduce priority when cpu usage is up to 50%)
- Find Window's Process feature (allows to find the process which owns a specific window with a simple drag & drop over the screen)
- Find hidden processes feature (find hidden basic rootkits)
- Dependencies Viewer
- etc.



REQUIREMENTS

YAPM runs on Windows XP, Windows Vista or even Windows Seven systems.

For now, to fully control your processes (including system processes) you MUST have the administrator rights.
In the future, the major part of the informations about a system process will be availables even if you are not administrator. Of course, if you want to kill system processes, you still will need to have the required rights...

YAPM take ~60 MB of memory, and less than one percent of cpu usage when minimized (depending of the CPU you have ;-) and depending of the interval of refreshment you specify).



DESCRIPTION OF FILES

Here is a description of the files which are distributed with YAPM :

- hotkeys.xml : contains description of custom actions for Emergency Hotkeys features. Not required, and not present if no custom action is specified.
- statebasedactions.xml : contains description of custom actions for State Based Actions features. Not required, and not present if no custom action is specified.
- MemoryHexEditor.dll : needed to display build-in hex editor. You can run YAPM without this file, but it'll crash if you try to show the hex editor !
- System.Windows.Forms.Ribbon.dll : ribbon control. Required to launch YAPM.
- CoreFunc.dll : core functions for processes management. Required.
- Providers.dll : Custom controls (listviews). Required.
- Taskdialog.dll : used to display custom dialog messages on Vista.
- YAPM.exe : main executable file (of course required ^^).
- KernelMemory.sys : driver needed to retrieve handle informations for system processes. Required.
- DependenciesViewer.dll : the dependencies viewer
- README.txt : the file you are reading :-)
- license.rtf : license file.
- launch server.bat : launch YAPM as a server.



BUGS

YAPM is still an alpha version. That's why there are some bugs which are still unsolved and known.
If you find one, please send me an email with a description of the bug or use the sourceforge bug tracker system (see end of this file for details).



LICENSES

YAPM is under the GNU GPL 3.0 license. But it uses third parties pieces which are under other licenses :
- Fugue Icons which are under a Creative Commons Attribution 3.0 license
- Ribbon Control which is under The Microsoft Public License (Ms-PL)

Some pieces of code are inspired by work under GNU GPL 3.0. It includes :
cSystemInfo.vb file : some structures defined come from Process Hacker by wj32
cProcess.vb file : some pieces of code are inspired by wj32 work (from Process Hacker) :
- GetAccountName function (conversion from SID to username as a string)
- GetImageFile function, especially DeviceDriveNameToDosDriveName and
	RefreshLogicalDrives which are inspired by functions from Process Hacker

Some other pieces of code come from the public domain :
- KernelMemory driver by ShareVB
- All the code used by the Dependecies Viewer from ShareVB too
- Some other tiny pieces of code from other people.

See the license.rtf file for details about the license.



CONTACT & LINKS

See http://sourceforge.net/projects/yaprocmon/ for details about the project (bug tracker, downloads, news, forum... etc.)
Please send feedback to me : YetAnotherProcessMonitor [at] gmail [dot] com

Please visit the website : http://yaprocmon.sourceforge.net/