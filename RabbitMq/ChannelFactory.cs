using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMq
{
    public class ChannelFactory
    {
        private IConnection conn;
        private ConnectionFactory factory = new ConnectionFactory();
        private RabbitMqOptions options;

        public ChannelFactory(RabbitMqOptions _options)
        {
            options = _options;
        }

        public IModel CreateChannel()
        {
            LoadFactorySettings();

            conn = factory.CreateConnection();
            return conn.CreateModel();
        }

        private void LoadFactorySettings()
        {
            if (options == null)
            {
                factory.HostName = "localhost";
                factory.VirtualHost = "\\";
                factory.Port = 5672;
                factory.UserName = "guest";
                factory.Password = "guest";
            }
            else
            {
                factory.HostName = options.HostName;
                factory.VirtualHost = options.VirtualHost;
                factory.Port = options.Port;
                factory.UserName = options.UserName;
                factory.Password = options.Password;
            }
        }

    }
}
