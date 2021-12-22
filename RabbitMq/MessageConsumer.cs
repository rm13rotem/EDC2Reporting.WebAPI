using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq
{
    public class MessageConsumer
    {
        private IModel channel;
        private RabbitMqOptions _options;

        public MessageConsumer(RabbitMqOptions options)
        {
            _options = options;
        }
        public bool ConsumeMessage(string queueName,
            EventHandler<BasicDeliverEventArgs> whatToDoWithStringMessage)
        {
            try
            {
                ChannelFactory factory = new ChannelFactory(_options);

                channel = factory.CreateChannel();
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += whatToDoWithStringMessage;
                consumer.Received += Consumer_Received;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool ConsumeStringMessage(string queueName, Action<string> whatToDoWithStringMessage)
        {
            try
            {
                ChannelFactory factory = new ChannelFactory(_options);

                channel = factory.CreateChannel();

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, eventArguments) =>
                {
                    string response;
                    response = Encoding.UTF8.GetString(eventArguments.Body.ToArray());
                    whatToDoWithStringMessage(response);
                };
                consumer.Received += Consumer_Received;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine("Message = " + message);
            // Log to DB;
        }
    }
}
