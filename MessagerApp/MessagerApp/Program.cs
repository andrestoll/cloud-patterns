using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MessagerApp;

using IHost host = Host.CreateDefaultBuilder(args).Build();


// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();


// Get values from the config given their key and their target type.
var queueName = config.GetRequiredSection("ServiceBus:QueueName").Get<string>();
var connectionString = config.GetRequiredSection("ServiceBus:ConnectionString").Get<string>();

var serviceBusMessenger = new ServiceBusMessager(connectionString, queueName);

await serviceBusMessenger.SendMessage(50);

await host.RunAsync();
