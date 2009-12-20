Yet Another (remote) Process Monitor (YAPM) v2.4.2 beta


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

Yet Another Process Monitor (YAPM) is a powerful process viewer for Windows that monitors the services & processes, their modules, threads, handles, windows, TCP/UDP connections, heaps... etc. running on local system or on any of the computers on your network.



HOW TO USE

For now there is only a small help file available (http://yaprocmon.sourceforge.net/help.html), so you'll have to learn how to use YAPM by yourself :-) But don't worry, it's easy and intuitive ! It just looks like Process Explorer or other task manager.
No installation is needed, just double-click on YAPM to run it. Running YAPM with an administrator account enable more features (such as job enumeration and control over all processes).
Double click on a process/service to get the detailed view with advanced informations.

If you want to monitor remote systems, there are two methods :

1) Client-server architecture (best method). You'll have to run a server on the remote server, but it's powerful than WMI method.
- Run YAPM as a server on the remote system you want to monitor (use -server option, or launch the *.bat file)
- Run YAPM normaly on the local system
- On the local system : Menu->Change connection type->Disconnect->Server tab->Configure IP/port of remote system->Connect

2) WMI method. You do not have to run anything on the remote system, but it's limited : you can only enumerate services, processes, modules and threads, start/stop them, change priority... and other basic actions.
- Run YAPM normaly on the local system
- Menu->Change connection type->Disconnect->WMI tab->Configure remote system name and user/password->Connect (you MUST specify a password, it won't work with an account without password)



FEATURES

All commons features of a process/service manager are availables in YAPM.
All these features are available for a remote system (with client-server architecture).
But there are some unique useful features for process/service monitoring.

YAPM supports :
- viewing and manipulating processes (kill, pause, resume, affinity...)
- viewing and manipulating services (start, stop...)
- viewing and manipulating modules, windows, tasks, threads, handles, network connections, memory regions...
- viewing and manipulating privileges, strings in file/memory, environment variables...
- viewing and manipulating jobs
- viewing and searching in process memory (with a build-in hex editor)
- viewing complete history of processes statistics
- saving complete and custom reports
- monitoring activities of running processes (handles created, connections opened...)
- viewing complete statistics and graphs about processes and system (memory/cpu usage...)
- Find Window's Process feature (allows to find the process which owns a specific window with a simple drag & drop over the screen)
- Find hidden processes feature (find hidden basic rootkits)
- System Snapshot mode
- etc.



REQUIREMENTS

YAPM runs on Windows XP, Windows Vista or Windows Seven systems. 64 bits support is only experimental for now !

YAPM should be elevated (run as Admin) on Windows Vista/Seven if you want to access to all processes/services.



DESCRIPTION OF FILES

Here is a description of the files which are distributed with YAPM :

- hotkeys.xml : contains description of custom actions for Emergency Hotkeys features. Not required, and not present if no custom action is specified.
- YAPM.exe : main executable file (of course required ^^).
- KernelMemory.sys : driver needed to retrieve handle informations for system processes. Required.
- README.txt : the file you are reading :-)
- license.rtf : license file.
- launch server.bat : launch YAPM as a server.



BUGS

YAPM is still a beta version. That's why there are some bugs which are still unsolved and known.
If you find one, please send me an email with a description of the bug or use the sourceforge bug tracker system (see end of this file for details).



LICENSES

YAPM is under the GNU GPL 3.0 license. But it uses third parties pieces which are under other licenses :
- Fugue Icons which are under a Creative Commons Attribution 3.0 license
- Ribbon Control which is under The Microsoft Public License (Ms-PL)
- VistaMenu which is under the BSD license

Some other pieces of code are based on work under GNU GPL 3.0. See code for details.

Some other pieces of code come from the public domain :
- KernelMemory driver by ShareVB
- Dependency viewer by ShareVB
- Some other tiny pieces of code

See the license.rtf file for details about the license.



CONTACT & LINKS

See http://sourceforge.net/projects/yaprocmon/ for details about the project (bug tracker, downloads, news, forum... etc.)
Please feel free to send feedback/comments : YetAnotherProcessMonitor [at] gmail [dot] com

Please visit the website : http://yaprocmon.sourceforge.net/