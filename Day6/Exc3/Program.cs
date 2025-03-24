using Exc3;

SmartDevice[] devices = [
    new SmartSpeaker("Amazon Echo"),
    new Smartphone("iPhone"),
    new SmartSpeaker("Google Nest"),
    new Smartphone("Samsung Galaxy")
];

Console.WriteLine("Устройства, которые могут звонить:");
foreach (var device in devices)
{
    if (device is ICanMakeCalls phone)
    {
        phone.MakeCall("+123456789");
    }
}