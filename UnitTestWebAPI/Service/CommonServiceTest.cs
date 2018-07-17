using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Service;

namespace UnitTestWebAPI.Service
{
    [TestClass]
    public class CommonServiceTest
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();

        public CommonServiceTest()
        {
            connectionFactory.HostName = "35.199.98.99";
            connectionFactory.UserName = "guest";
            connectionFactory.Password = "guest";
        }

        [TestMethod]
        public void CreateConnectionTest()
        {
            IConnection conn = connectionFactory.CreateConnection();
            Assert.IsNotNull(conn);
        }

        [TestMethod]
        public void CreateModelTest()
        {
            IConnection conn = connectionFactory.CreateConnection();
            IModel channel = conn.CreateModel();
            Assert.IsNotNull(channel);
        }

        [TestMethod]
        public void QueueDeclareTest()
        {
            IConnection conn = connectionFactory.CreateConnection();
            IModel channel = conn.CreateModel();
            channel.QueueDeclare(queue: "rpc_queue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
            Assert.IsNotNull(channel.QueueDeclare());
        }        
    }
}
