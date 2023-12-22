using Confluent.Kafka;
using System.Text.Json;

var config = new ProducerConfig { BootstrapServers = "localhost:29092" };

using (var p = new ProducerBuilder<Null, string>(config).Build())
{
    try
    {
        var objToSend = new
        {
            name = "adam"
        };

        var message = JsonSerializer.Serialize(objToSend);

        var dr = await p.ProduceAsync("Test", new Message<Null, string> 
        {
            Value = message 
        });

        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
    }
    catch (ProduceException<Null, string> e)
    {
        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
    }
}

Console.ReadKey();