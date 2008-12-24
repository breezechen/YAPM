set objShell = CreateObject("Wscript.Shell")
objShell.Run("sc delete KernelMemory")
Set objShell = Nothing