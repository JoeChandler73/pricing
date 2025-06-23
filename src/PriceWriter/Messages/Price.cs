using Messaging.Application;

namespace PriceWriter.Messages;

public record Price(string Symbol, decimal Mid, DateTime Timestamp) : IMessage;