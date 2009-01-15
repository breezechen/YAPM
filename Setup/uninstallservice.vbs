' Delete service (if existing)
set objShell = CreateObject("Wscript.Shell")
objShell.Run("sc delete KernelMemory")
Set objShell = Nothing


' Remove YAPM from windows startup
Const HKEY_CURRENT_USER = &H80000001
strComputer = "."
Set oReg = GetObject("winmgmts:{impersonationLevel=impersonate}!\\" & strComputer & "\root\default:StdRegProv")
strKeyPath = "Software\Microsoft\Windows\CurrentVersion\Run"
strStringValueName = "YAPM"
oReg.DeleteValue HKEY_CURRENT_USER, strKeyPath, strStringValueName
set oReg = Nothing