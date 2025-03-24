namespace Exc4;

public class SecuritySystem
{
    public void VerifyMovementAuthorization(object sender, WarehouseEventArgs e)
    {
        Console.WriteLine($"[БЕЗОПАСНОСТЬ] Проверка работника {e.EmployeeId}");
        Console.WriteLine(HasPermission(e.EmployeeId)
            ? $"[БЕЗОПАСНОСТЬ] Перемещение для {e.EmployeeId} авторизовано"
            : $"[БЕЗОПАСНОСТЬ] ВНИМАНИЕ: Неавторизованное перемещение для {e.EmployeeId}");
    }

    private bool HasPermission(string employeeId)
    {
        return employeeId.StartsWith("E") && employeeId.Length == 5;
    }
}