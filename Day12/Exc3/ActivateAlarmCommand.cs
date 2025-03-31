namespace Exc3;

public class ActivateAlarmCommand(AlarmSystem alarmSystem) : ICommand
{
    public void Execute()
    {
        alarmSystem.Activate();
    }
}