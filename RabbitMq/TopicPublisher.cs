using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMq
{
    public class TopicPublisher
    {
        private ChannelFactory channelFactory;
        private IModel channel;
        private RabbitMqOptions options;
        public TopicPublisher(IOptionsSnapshot<RabbitMqOptions> optionsRabbitMq)
        {
            options = optionsRabbitMq.Value;
            channelFactory = new ChannelFactory(options);
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
                //Logger.log(e);
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
