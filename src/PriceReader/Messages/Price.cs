using Messaging.Application;

namespace PriceReader.Messages;

public record Price(string Symbol, decimal Mid, DateTime Timestamp) : IMessage;