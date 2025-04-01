using Exc2;

IRoomService service = new BasicRoomService();
Console.WriteLine($"{service.GetServiceDetails()} - {service.GetCost()} BYN");

service = new BreakfastDecorator(service);
service = new AirportPickupDecorator(service);
Console.WriteLine($"{service.GetServiceDetails()} - {service.GetCost()} BYN");

service = new BasicRoomService();
service = new BreakfastDecorator(service);
service = new SpaDecorator(service);
service = new AirportPickupDecorator(service);
Console.WriteLine($"{service.GetServiceDetails()} - {service.GetCost()} BYN");

