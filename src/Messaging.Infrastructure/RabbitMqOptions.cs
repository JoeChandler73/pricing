namespace Messaging.Infrastructure;

public record RabbitMqOptions
{
    public string serviceName { get; init; }
    
    public string ConnectionString { get; init; }
}