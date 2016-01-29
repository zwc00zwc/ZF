using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.RabbitMQ
{
    public class RabbitMQHelper
    {
        public static void Send(RabbitMQModel model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = model.HostName;
            factory.UserName = model.UserName;
            factory.Password = model.Password;
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                    channel.QueueDeclare(model.QueueName, false, false, false, null);
                    while (true)
                    {
                        string customStr = Console.ReadLine();
                        byte[] bytes = Encoding.UTF8.GetBytes(customStr);

                        //设置消息持久化
                        IBasicProperties properties = channel.CreateBasicProperties();
                        properties.DeliveryMode = 2;
                        channel.BasicPublish("", model.QueueName, properties, bytes);

                        //channel.BasicPublish("", "MyFirstQueue", null, bytes);
                    }
                }
            }
        }

        public static void Send(RabbitMQModel model, Action<string> action)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = model.HostName;
            factory.UserName = model.UserName;
            factory.Password = model.Password;
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                    channel.QueueDeclare(model.QueueName, false, false, false, null);
                    while (true)
                    {
                        string customStr = Console.ReadLine();
                        byte[] bytes = Encoding.UTF8.GetBytes(customStr);

                        //设置消息持久化
                        IBasicProperties properties = channel.CreateBasicProperties();
                        properties.DeliveryMode = 2;
                        channel.BasicPublish("", model.QueueName, properties, bytes);

                        //channel.BasicPublish("", "MyFirstQueue", null, bytes);
                        action.Invoke("消息已发送：" + customStr);
                    }
                }
            }
        }

        public static void Receive(RabbitMQModel model,Action<string> action)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = model.HostName;
            factory.UserName = model.UserName;
            factory.Password = model.Password;
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                    channel.QueueDeclare(model.QueueName, false, false, false, null);

                    //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                    channel.BasicQos(0, 1, false);

                    //在队列上定义一个消费者
                    QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                    //消费队列，并设置应答模式为程序主动应答
                    channel.BasicConsume(model.QueueName, false, consumer);

                    while (true)
                    {
                        //阻塞函数，获取队列中的消息
                        BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        byte[] bytes = ea.Body;
                        string str = Encoding.UTF8.GetString(bytes);
                        action.Invoke(str);
                        //回复确认
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                }
            }
        }
    }
}
