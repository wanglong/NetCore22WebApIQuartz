using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQServers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World RabbitMQServers!");
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "127.0.0.1";
            factory.Port = 5672;
            factory.VirtualHost = "/";
            factory.UserName = "guest";
            factory.Password = "guest";

            var exchangeAll = "changeAll";
            //性别.姓氏.头发长度
            var keymanA = "man.chen.long";
            var keymanB = "man.liu.long";
            var keymanC = "woman.liu.long";
            var keymanD = "woman.chen.short";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeAll, type: "topic", durable: true, autoDelete: false);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    //发布消息
                    channel.BasicPublish(exchange: exchangeAll,
                    routingKey: keymanA,
                    basicProperties: properties,
                    body: Encoding.UTF8.GetBytes(keymanA));

                    channel.BasicPublish(exchange: exchangeAll,
                     routingKey: keymanB,
                     basicProperties: properties,
                     body: Encoding.UTF8.GetBytes(keymanB));

                    channel.BasicPublish(exchange: exchangeAll,
                     routingKey: keymanC,
                     basicProperties: properties,
                     body: Encoding.UTF8.GetBytes(keymanC));

                    channel.BasicPublish(exchange: exchangeAll,
                     routingKey: keymanD,
                     basicProperties: properties,
                     body: Encoding.UTF8.GetBytes(keymanD));
                }
            }
        }
    }
}
