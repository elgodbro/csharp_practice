using Exc4;

var clients = new BankClient[10];

for (var i = 0; i < clients.Length; i++)
{
    clients[i] = new BankClient();
}

Console.WriteLine("Клиенты:");
foreach (var client in clients)
{
    Console.WriteLine(client);
}

var bank = new Bank(clients);

Console.WriteLine("\nКлиенты с балансом менее 500:");
foreach (var client in bank.GetClientsWithLowBalance(500))
{
    Console.WriteLine(client);
}

Console.WriteLine($"\nСамый богатый клиент: {bank.GetRichestClient()}");