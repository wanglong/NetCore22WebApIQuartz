using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQServersEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "127.0.0.1";
            factory.Port = 5672;
            factory.VirtualHost = "/";
            factory.UserName = "guest";
            factory.Password = "guest";

            var exchangeAll = "changeHeader";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeAll, type: "headers", durable: true, autoDelete: false);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.Headers = new Dictionary<string, object> {
                        { "sex","man"}
                    };
                    //发布消息
                    channel.BasicPublish(exchange: exchangeAll,
                    routingKey: "",
                    basicProperties: properties,
                    body: Encoding.UTF8.GetBytes("hihihi20190116"));
                }
            }
        }
    }
}
