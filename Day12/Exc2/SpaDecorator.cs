namespace Exc2;

public class SpaDecorator(IRoomService service) : RoomServiceDecorator(service)
{
    public override string GetServiceDetails() => $"{base.GetServiceDetails()} + СПА-процедуры";
    public override decimal GetCost() => base.GetCost() + 75.0m;
}