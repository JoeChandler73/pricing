using Messaging.Application;

namespace PriceManager.Messages;

public record PriceSubscribe(string Symbol) : IMessage;