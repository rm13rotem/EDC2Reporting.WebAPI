using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMq
{
    public class TopicPublisher
    {
        private ChannelFactory channelFactory;
        private ILogger<TopicPublisher> _logger;
        private IModel channel;
        private RabbitMqOptions options;
        public TopicPublisher(IOptionsSnapshot<RabbitMqOptions> optionsRabbitMq, ILogger<TopicPublisher> logger)
        {
            options = optionsRabbitMq.Value;
            channelFactory = new ChannelFactory(options);
            _logger = logger;

            // create channel immediately
            channel = channelFactory.CreateChannel();
        }

        public bool TryDeclareExchange(string exchangeName)
        {
            try
            {
                channel.ExchangeDeclare(exchangeName,
                               "topic",
                               true,
                               false,
                               null);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return false;
            }

        }

        public bool TryLinkExchangeToQueue(string exchangeName, string queueName, string BindingKey)
        {
            try
            {
                channel.QueueDeclare(queueName,
                                    true,
                                    false,
                                    false,
                                    null);
                channel.QueueBind(queueName, exchangeName, BindingKey);


                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return false;
            }
        }
        public bool TrySendMessage(string exchangeName, string queueName, string message)
        {
            try
            {
                if (!TryDeclareExchange(exchangeName))
                    return false;

                channel.BasicPublish(exchangeName,
                    "",
                    null,
                    Encoding.UTF8.GetBytes(message));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
