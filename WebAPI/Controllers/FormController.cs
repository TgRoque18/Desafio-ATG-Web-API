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
        public FormModelResult Post(JObject form)
        {
            LogService.StartProcessLog(_log);
            FormModelResult formResult = new FormModelResult();

            try
            {
                if (ModelState.IsValid)
                {
                    LogService.SaveLog(_log, "Conexão com RabbitMQ aberta.");

                    LogService.SaveLog(_log, "Iniciando operação RPC.");

                    RpcService rpcService = new RpcService();

                    FormModel formModel = rpcService.ConvertToJsonToFormObj(form);
                    
                    string body = rpcService.ConvertToJson(formModel).ToLower();

                    LogService.SaveLog(_log, "Chamada RPC.");

                    var response = rpcService.Call(body);

                    JObject jsonResult = JObject.Parse(response);

                    formResult = rpcService.ConvertFormModelToResult(formModel, jsonResult);
                    
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

            return formResult;
        }
    }
}
