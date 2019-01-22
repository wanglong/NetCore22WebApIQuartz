using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQClient
{
    class Program
    {
        public static readonly ConnectionFactory rabbitMqFactory = new ConnectionFactory()
        {
            HostName = "127.0.0.1",
            Port = 5672,
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World RabbitMQClient!");
            var exchangeAll = "changeAll";
            var queueman = "queueman";
            var quemankey = "man.#";

            using (IConnection conn = rabbitMqFactory.CreateConnection())
            using (IModel channel = conn.CreateModel())
            {
                channel.ExchangeDeclare(exchangeAll, type: "topic", durable: true, autoDelete: false);
                channel.QueueDeclare(queueman, durable: true, exclusive: false, autoDelete: false);
                channel.QueueBind(queueman, exchangeAll, quemankey);

                channel.BasicQos(prefetchSize:0, prefetchCount:50, global:false);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (mode, ea) =>
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
