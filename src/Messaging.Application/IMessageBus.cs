namespace Messaging.Application;

public interface IMessageBus : IAsyncDisposable
{
    Task SendAsync<TMessage>(TMessage message) where TMessage : IMessage;
    
    void Subscribe<TMessage>(Func<TMessage, Task> handler) where TMessage : IMessage;
    
    Task InitialiseAsync();
}