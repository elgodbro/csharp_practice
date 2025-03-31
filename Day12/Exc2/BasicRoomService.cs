namespace Exc2;

public class BasicRoomService : IRoomService
{
    public string GetServiceDetails() => "Базовый сервис (уборка номера)";
    public decimal GetCost() => 50.0m;
}