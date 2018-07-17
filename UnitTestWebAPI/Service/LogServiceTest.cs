using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Service;

namespace UnitTestWebAPI.Service
{
    [TestClass]
    public class LogServiceTest
    {
        [TestMethod]
        public void StartProcessLogTest()
        {
            Assert.IsTrue(LogService.TryStartProcessLog("C:\\log.txt"));
        }

        [TestMethod]
        public void SaveLog()
        {
            Assert.IsTrue(LogService.TrySaveLog("C:\\log.txt","Teste"));
        }

        [TestMethod]
        public void FinishProcessLog()
        {
            Assert.IsTrue(LogService.TryFinishProcessLog("C:\\log.txt"));
        }
    }
}
