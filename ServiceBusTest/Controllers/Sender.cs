using System.Text;
using System.Text.Json;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace ServiceBusTest.Controllers;

public class MessagesViewModel
{
    public List<MessageViewModel> Messages { get; set; }
}

public class MessageViewModel
{
    public string Id { get; set; }
    public string Value { get; set; }
}

[Route("Sender")]
public class Sender : Controller
{
    private ServiceBusSender _sender;
    private ServiceBusClient _client;

    public Sender()
    {
        var clientOptions = new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
       
        var topicName = AppConfiguration.TopicName;
        var nsConnString = AppConfiguration.ConnectionString;
            _client = new ServiceBusClient(nsConnString);
        
        _sender = _client.CreateSender(topicName);
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromBody] MessagesViewModel vm)
    {
        // create a batch 
        // create a batch 
        using ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync();
        //var sessionId = Guid.NewGuid().ToString();

        try
        {
            for (int i = 0; i <= vm.Messages.Count-1; i++)
            {
                var text = JsonSerializer.Serialize(vm.Messages[i]);
                // try adding a message to the batch
                if (!messageBatch.TryAddMessage(new ServiceBusMessage(text)))
                {
                    // if it is too large for the batch
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }  
            await _sender.SendMessagesAsync(messageBatch);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await _sender.DisposeAsync();
            await _client.DisposeAsync();
        }


        return Ok();
    }
}