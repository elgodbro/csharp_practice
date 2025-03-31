namespace Exc2;

public abstract class RoomServiceDecorator(IRoomService roomService) : IRoomService
{
    public virtual string GetServiceDetails() => roomService.GetServiceDetails();
    public virtual decimal GetCost() => roomService.GetCost();
}