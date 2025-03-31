namespace Exc3;

public class DeactivateAlarmCommand(AlarmSystem alarmSystem) : ICommand
{
    public void Execute()
    {
        alarmSystem.Deactivate();
    }
}