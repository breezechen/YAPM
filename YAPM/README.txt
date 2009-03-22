Yet Another Process Monitor (YAPM) Beta 5


SUMMARY
	* Application description
	* Features
	* Requirements
	* Description of files
	* Known bugs
	* Licenses
	* Contact & links


	
APPLICATION DESCRIPTION

Yet Another Process Monitor (YAPM) is a powerful aplication for Windows NT that monitors the processes running on your system. You can manage their threads, windows, handles, modules and network connections. YAPM also monitors all services running.



FEATURES

All commons features of a process manager are availables in YAPM.
But there are some unique useful features for process monitoring.
YAPM supports :
- viewing and manipulating processes (kill, pause, resume, affinity...)
- viewing and manipulating services (start, stop...)
- viewing and manipulating modules, windows, tasks, threads, handles, network connections...
- viewing and manipulating privileges, strings in file/memory, environment variables...
- viewing complete history of processes statistics
- viewing and searching in process memory (with a build-in hex editor)
- saving complete and custom reports
- monitoring activities of running processes (handles created, connections opened...)
- viewing complete statistics and graphs about processes and system (memory/cpu usage...)
- Emergency Hotkeys feature (assign a custom command to a set of keyboard keys. Example : Ctrl+Shift+Suppr to kill foreground application)
- State Based Action feature (allows to assign a custom command which is launched when the state of a process changes. Example : reduce priority when cpu usage is up to 50%)
- Find Window's Process feature (allows to find the process which owns a specific window with a simple drag & drop on the screen)



REQUIREMENTS

YAPM runs under Windows XP and Vista systems.

For now, to fully control your processes (including system processes) you MUST have the administrator rights.
In the future, the major part of the informations about a system process will be availables even if you are not administrator. Of course, if you want to kill system processes, you still will need to have the required rights...

YAPM take ~50 MB of memory, and less than one percent of cpu usage when minimized (depending of the CPU you have ;-) and depending of the interval of refreshment you specify).



DESCRIPTION OF FILES

Here is a description of the files which are distributed with YAPM :

- config.xml : configuration file (for preferences). Not required to launch YAPM, but it will be created if it's missing.
- hotkeys.xml : contains description of custom actions for Emergency Hotkeys features. Not required, and not present if no custom action is specify.
- statebasedactions.xml : contains description of custom actions for State Based Actions features. Not required, and not present if no custom action is specify.
- MemoryHexEditor.dll : needed to display build-in hex editor. You can run YAPM without this file, but it'll crash if you try to show the hex editor.
- System.Windows.Forms.Ribbon.dll : ribbon control. Required to launch YAPM.
- CoreFunc.dll : core functions for processes management. Required.
- Providers.dll : Custom controls (listviews). Required.
- YAPM.exe : main executable file (of course required ^^).
- KernelMemory.sys : driver needed to retrieve handle informations for system processes. Required file.
- README.txt : the file you are reading :-)
- license.rtf : license file.



BUGS

YAPM is still a beta version. That's why there may are some minor bugs which are still unsolved.
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
- Some other tiny pieces of code

See the license.rtf file for details about the license.



CONTACT & LINKS

See http://sourceforge.net/projects/yaprocmon/ for details about the project (bug tracker, downloads, news, forum... etc.)
Please send feedback to me : YetAnotherProcessMonitor [at] gmail [dot] com