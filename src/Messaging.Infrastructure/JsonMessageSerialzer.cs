using System.Text;
using System.Text.Json;
using Messaging.Application;

namespace Messaging.Infrastructure;

public class JsonMessageSerialzer : IMessageSerializer
{
    public byte[] Serialize<TMessage>(TMessage message) where TMessage : IMessage
    {
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
    }

    public IMessage? Deserialize(byte[] bytes, Type messageType)
    {
        return JsonSerializer.Deserialize(Encoding.UTF8.GetString(bytes), messageType) as IMessage;
    }
}