using Exc3;

var alarm = new AlarmSystem();
var panel = new SecurityPanel();

var activateCommand = new ActivateAlarmCommand(alarm);
var deactivateCommand = new DeactivateAlarmCommand(alarm);

panel.SetCommand(activateCommand);
panel.ExecuteSecurityCommand();

panel.SetCommand(deactivateCommand);
panel.ExecuteSecurityCommand();