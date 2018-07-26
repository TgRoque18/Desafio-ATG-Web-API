using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class RpcService
    {
        #region Variáveis 

        private static string _hostName = ConfigurationManager.AppSettings["HOSTNAME"];
        private static string _userName = ConfigurationManager.AppSettings["USERNAME"];
        private static string _password = ConfigurationManager.AppSettings["PASSWORD"];
        private static int _port = Int32.Parse(ConfigurationManager.AppSettings["PORT"]);
        private static string _queue = ConfigurationManager.AppSettings["QUEUE"];

        private ConnectionFactory connectionFactory = new ConnectionFactory();

        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;
        
        #endregion Variáveis 

        public RpcService()
        {
            connectionFactory.HostName = _hostName;
            connectionFactory.UserName = _userName;
            connectionFactory.Password = _password;

            connection = CreateConnection();
            channel = CreateModel(connection);
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }

        #region Metodos de Conexão

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

        public string Call(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: _queue,
                basicProperties: props,
                body: body);

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            return respQueue.Take();
        }

        public void Close()
        {
            connection.Close();
        }

        #endregion Metodos de Conexão

        #region Metodos de Conversão

        public FormModelResult ConvertFormModelToResult(FormModel formModel, JObject response)
        {
            FormModelResult formResult = new FormModelResult();

            if (formModel != null)
            {
                formResult.Id = formModel.Id;
                formResult.Side = formModel.Side;
                formResult.Quantity = formModel.Quantity;
                formResult.Price = formModel.Price;
                formResult.Symbol = formModel.Symbol;

                if (response != null)
                {
                    if (formResult.Id == Int32.Parse(response["id"].ToString()))
                    {
                        formResult.Id = Int32.Parse(response["id"].ToString());

                        if(bool.Parse(response["status"].ToString()))
                        {
                            formResult.Status = "Enviado";
                        }
                        else
                        {
                            formResult.Status = "Erro";
                        }
                        if (((Newtonsoft.Json.Linq.JContainer)(response["msgs"])).Count > 0)
                        {
                            formResult.Msgs = "Erro: " + response["msgs"][0].ToString();
                        }
                        else
                        {
                            formResult.Msgs = "Conexão Estabelecida";
                        }
                    }
                }
            }
            return formResult;
        }

        public byte[] ConvertToJson(JObject form)
        {
            var obj = JsonConvert.DeserializeObject(form.ToString());

            string message = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(message);
            return body;
        }

        public string ConvertToJson(FormModel form)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(FormModel));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, form);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        public FormModel ConvertToJsonToFormObj(JObject form)
        {
            var details = JObject.Parse(form.ToString());

            FormModel newFormModel = new FormModel()
            {
                Id = Int32.Parse(details["form"]["id"].ToString()),
                Quantity = Int32.Parse(details["form"]["quantity"].ToString()),
                Side = details["form"]["side"].ToString(),
                Price = double.Parse(details["form"]["price"].ToString()),
                Symbol = details["form"]["symbol"].ToString()
            };

            return newFormModel;
        }

        #endregion Metodos de Conversão

    }

}