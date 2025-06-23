using Messaging.Application;

namespace PriceManager.Messages;

public record Price(string Symbol, decimal Mid, DateTime Timestamp) : IMessage;