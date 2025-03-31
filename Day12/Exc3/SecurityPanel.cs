namespace Exc3;

public class SecurityPanel
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void ExecuteSecurityCommand()
    {
        _command?.Execute();
    }
}