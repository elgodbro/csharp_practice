namespace Exc2;

public class BreakfastDecorator(IRoomService service) : RoomServiceDecorator(service)
{
    public override string GetServiceDetails() => $"{base.GetServiceDetails()} + Завтрак";
    public override decimal GetCost() => base.GetCost() + 25.0m;
}