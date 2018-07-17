using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace WebAPI.Service
{
    public class LogService
    {
        public static bool StartProcessLog(string log)
        {
            using (StreamWriter objWriter = new StreamWriter(log, true))
            {
                objWriter.WriteLine();
                objWriter.WriteLine("+============== Iniciando Processo ==============+");
            }

            return true;
        }

        public static void SaveLog(string log, string message)
        {
            using (StreamWriter objWriter = new StreamWriter(log, true))
            {
                objWriter.WriteLine();
                objWriter.WriteLine(message);
                objWriter.WriteLine();
            }
        }
        public static void FinishProcessLog(string log)
        {
            using (StreamWriter objWriter = new StreamWriter(log, true))
            {
                objWriter.WriteLine("+============== Concluindo Processo ==============+");
                objWriter.WriteLine();
            }
        }

        //Para testes 
        public static bool TryStartProcessLog(string log)
        {
            try
            {
                StartProcessLog(log);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TrySaveLog(string log, string message)
        {
            try
            {
                SaveLog(log, message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TryFinishProcessLog(string log)
        {
            try
            {
                FinishProcessLog(log);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}