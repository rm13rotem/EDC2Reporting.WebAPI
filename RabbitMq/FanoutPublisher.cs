using Microsoft.Extensions.Options;
using RabbitMq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class FanoutPublisher : IFanoutPublisher
    {
        private ChannelFactory channelFactory;
        private IModel channel;
        private RabbitMqOptions options;
        public FanoutPublisher(IOptionsSnapshot<RabbitMqOptions> optionsRabbitMq)
        {
            options = optionsRabbitMq.Value;
            channelFactory = new ChannelFactory(options);
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
                //Logger.log(e);
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
                //Logger.Log(e.Message);
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
