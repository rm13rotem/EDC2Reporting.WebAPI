namespace ConsoleApp1
{
    public interface IFanoutPublisher
    {
        bool TryDeclareExchange(string exchangeName);
        bool TryLinkExchangeToQueue(string exchangeName, string queueName);
        bool TrySendMessage(string exchangeName, string queueName, string message);
    }
}