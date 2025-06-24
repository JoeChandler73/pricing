namespace PriceManager.Messages;

public record PriceSubscribe(string Symbol) : IMessage;