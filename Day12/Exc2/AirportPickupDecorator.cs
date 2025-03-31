namespace Exc2;

public class AirportPickupDecorator(IRoomService service) : RoomServiceDecorator(service)
{
    public override string GetServiceDetails() => $"{base.GetServiceDetails()} + Трансфер из аэропорта";

    public override decimal GetCost() => base.GetCost() + 40.0m;
}