sc create PumpService binPath= "%~dp0PumpService.Web.exe"
sc failure PumpService actions= restart/60000/restart/60000/""/60000 reset= 86400
sc start PumpService
sc config PumpService start=auto