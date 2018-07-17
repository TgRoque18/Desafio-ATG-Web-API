using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [EnableCors("*", "*", "GET, POST")]
    public class FormController : ApiController
    {
        private static string _log = ConfigurationManager.AppSettings["LOGFILE"];

        // POST: api/Form
        public void Post(JObject form)
        {
            try
            {
                LogService.StartProcessLog(_log);

                if (ModelState.IsValid)
                {
                    CommonService service = new CommonService();

                    using (var connection = service.CreateConnection())
                    {
                        LogService.SaveLog(_log, "Conexão com RabbitMQ aberta.");

                        using (var channel = service.CreateModel(connection))
                        {
                            LogService.SaveLog(_log, "Canal com RabbitMQ aberto.");

                            service.QueueDeclare(channel);

                            byte[] body = service.ConvertToJson(form);

                            service.BasicPublish(channel, body);

                            LogService.SaveLog(_log, "Cominicação com RabbitMQ realizada.");
                        }

                        LogService.SaveLog(_log, "Canal com RabbitMQ fechado.");
                    }

                    LogService.SaveLog(_log, "Conexão com RabbitMQ fechada.");
                }
                else
                {
                    LogService.SaveLog(_log, "Modelo de dados inválido - Por favor informe todos os dados!");
                }
            }
            catch (Exception ex)
            {
                LogService.SaveLog(_log, "Erro no processo. Erro: " + ex.Message);
            }

            LogService.FinishProcessLog(_log);

        }
    }
}
