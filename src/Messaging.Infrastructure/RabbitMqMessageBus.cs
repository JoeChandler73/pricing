using System.Text;
using System.Text.Json;
using Messaging.Application;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messaging.Infrastructure;

public class RabbitMqMessageBus(IOptions<RabbitMqOptions> _options) : IMessageBus
{
    private IConnection _connection;
    private IChannel _channel;
    private string _queueName;
    private readonly List<Type> _messageTypes = new();
    private readonly Dictionary<string, List<Func<object, Task>>> _handlers = new();
    
    public async Task SendAsync<TMessage>(TMessage message) where TMessage : IMessage
    {
        var exchangeName = typeof(TMessage).Name;
        
        await _channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout, true);
        
        var messageBody = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageBody);
        var props = new BasicProperties
        {
            Type = typeof(TMessage).Name,
        };

        await _channel.BasicPublishAsync(exchangeName, "", true, props, body);
    }

    public void Subscribe<TMessage>(Func<TMessage, Task> handler) where TMessage : IMessage
    {
        var messageType = typeof(TMessage);
        var messageTypeName = messageType.Name;
        
        if(!_messageTypes.Contains(messageType))
            _messageTypes.Add(messageType);
        
        if(!_handlers.ContainsKey(messageTypeName))
            _handlers.Add(messageTypeName, new List<Func<object, Task>>());
        
        _handlers[messageTypeName].Add(async (message) => await handler((TMessage)message));
    }

    public async Task InitialiseAsync()
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_options.Value.ConnectionString),
            ClientProvidedName = _options.Value.serviceName
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
        _queueName = $"{_options.Value.serviceName}.queue";
        
        await _channel.QueueDeclareAsync(_queueName, true, false, false);
        
        foreach (var typeName in _handlers.Keys)
        {
            var exchangeName = typeName;
            await _channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout, true);
            await _channel.QueueBindAsync(_queueName, exchangeName, "");
        }
        
        await SetupConsumerAsync();
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.CloseAsync();
            await _channel.DisposeAsync();
        }
        
        if (_connection != null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
    }
    
    private async Task SetupConsumerAsync()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (sender, args) =>
        {
            try
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var messageTypeName = args.BasicProperties.Type;

                if (!string.IsNullOrEmpty(messageTypeName))
                {
                    var messageType = _messageTypes.SingleOrDefault(t => t.Name == messageTypeName);
                    
                    if (messageType != null && _handlers.TryGetValue(messageTypeName, out var handlerList))
                    {
                        var deserializedMessage = JsonSerializer.Deserialize(message, messageType);
                        if (deserializedMessage != null)
                        {
                            var tasks = handlerList.Select(handler => handler(deserializedMessage));
                            await Task.WhenAll(tasks);
                        }
                    }
                }

                await _channel.BasicAckAsync(args.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                await _channel.BasicNackAsync(args.DeliveryTag, false, false);
            }
        };
        
        await _channel.BasicConsumeAsync(_queueName, false, consumer);
    }
}