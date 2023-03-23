using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMq 
{
    public class FanoutPublisher : IFanoutPublisher
    {
        private readonly ChannelFactory channelFactory;
        private readonly IModel channel;
        private readonly ILogger<FanoutPublisher> logger;
        private readonly RabbitMqOptions options;
        public FanoutPublisher(IOptionsSnapshot<RabbitMqOptions> optionsRabbitMq, ILogger<FanoutPublisher> _logger)
        {
            options = optionsRabbitMq.Value;
            channelFactory = new ChannelFactory(options);
            channel = channelFactory.CreateChannel();
            logger = _logger;
        }

        public bool TryDeclareExchange(string exchangeName)
        {
            try
            {
                channel.ExchangeDeclare(exchangeName,
                               "fanout",
                               true,
                               false,
                               null);
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message, null);
                return false;
            }

        }

        public bool TryLinkExchangeToQueue(string exchangeName, string queueName)
        {
            try
            {
                channel.QueueDeclare(queueName,
                                    true,
                                    false,
                                    false,
                                    null);
                channel.QueueBind(queueName, exchangeName, "");


                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message,null);
                return false;
            }
        }
        public bool TrySendMessage(string exchangeName, string queueName, string message)
        {
            try
            {
                if (!TryDeclareExchange(exchangeName))
                    return false;

                if (!TryLinkExchangeToQueue(exchangeName, queueName))
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
