using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQClientEx
{
    class Program
    {
        private static readonly ConnectionFactory rabbitMqFactory = new ConnectionFactory()
        {
            HostName = "127.0.0.1",
            Port = 5672,
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World RabbitMQClientEx!");
            var exchangeAll = "changeHeader";
            var queueman = "queueHeader";

            using (IConnection conn = rabbitMqFactory.CreateConnection())
            using (IModel channel = conn.CreateModel())
            {
                channel.ExchangeDeclare(exchangeAll, type: "headers", durable: true, autoDelete: false);
                channel.QueueDeclare(queueman, durable: true, exclusive: false, autoDelete: false);
                channel.QueueBind(queueman, exchangeAll, "", new Dictionary<string, object> { { "sex", "man" } });

                channel.BasicQos(prefetchSize: 0, prefetchCount: 50, global: false);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    Byte[] body = ea.Body;
                    String message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(queue: queueman, autoAck: false, consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}
