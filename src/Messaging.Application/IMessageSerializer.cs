namespace Messaging.Application;

public interface IMessageSerializer
{
    byte[] Serialize<TMessage>(TMessage message) where TMessage : IMessage;

    IMessage? Deserialize(byte[] bytes, Type messageType);
}