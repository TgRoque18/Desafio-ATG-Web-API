using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace WebAPI.Service
{
    public class CommonService
    {
        private static string _hostName = ConfigurationManager.AppSettings["HOSTNAME"];
        private static string _userName = ConfigurationManager.AppSettings["USERNAME"];
        private static string _password = ConfigurationManager.AppSettings["PASSWORD"];
        private static int _port = Int32.Parse(ConfigurationManager.AppSettings["PORT"]);
        private static string _queue = ConfigurationManager.AppSettings["QUEUE"];

        private ConnectionFactory connectionFactory = new ConnectionFactory();

        public CommonService()
        {
            try
            {
                connectionFactory.HostName = _hostName;
                connectionFactory.UserName = _userName;
                connectionFactory.Password = _password;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IConnection CreateConnection()
        {
            try
            {
                return connectionFactory.CreateConnection();
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        public IModel CreateModel(IConnection conn)
        {
            try
            {
                return conn.CreateModel();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void QueueDeclare(IModel channel)
        {
            try
            {
                channel.QueueDeclare(queue: _queue,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void BasicPublish(IModel channel, byte[] body)
        {
            try
            {

                channel.BasicPublish(exchange: "",
                                        routingKey: _queue,
                                        basicProperties: null,
                                        body: body);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public byte[] ConvertToJson(JObject form)
        {
            var obj = JsonConvert.DeserializeObject(form.ToString());

            string message = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(message);
            return body;
        }
    }
}