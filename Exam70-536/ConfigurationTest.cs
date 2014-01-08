using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Exam70_536
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void ReadConfigTest()
        {
            Type dataProviderType = Type.GetType(ConfigurationManager.AppSettings["DataProvider"]);

            IDataProvider provider = Activator.CreateInstance(dataProviderType) as IDataProvider;

            bool writeNewLine = bool.Parse(ConfigurationManager.AppSettings["WriteTraceDebug"]);

            if (writeNewLine)
            {
                // newline
            }

            Assert.IsTrue(provider.GetList().Count() > 0);
        }

        [TestMethod]
        public void ConnectionStringTest()
        {
            Type dataProviderType = Type.GetType(ConfigurationManager.ConnectionStrings["ComeGetSome"].ProviderName);

            IDataProvider provider = Activator.CreateInstance(dataProviderType) as IDataProvider;

            Assert.IsTrue(provider.GetList().Count() > 0);
            Assert.AreEqual("ShakeItBaby", ConfigurationManager.ConnectionStrings["ComeGetSome"].ConnectionString);
        }

        [TestMethod]
        public void ConfigurationClassTest()
        {
            // var cfg1 = ConfigurationManager.OpenExeConfiguration("file.exe");
            // var cfg2 = ConfigurationManager.OpenMachineConfiguration();
            // var cfg3 = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap(), ConfigurationUserLevel.PerUserRoaming);
            // var cfg4 = ConfigurationManager.OpenMappedMachineConfiguration(new ConfigurationFileMap());

            Assert.Inconclusive();
        }
    }

    public class CustomDataProvider : IDataProvider
    {
        public IEnumerable<string> GetList()
        {
            var list = new List<string>();
            list.Add("BrunoLM");
            list.Add("RafaelKS");

            return list;
        }
    }
    public interface IDataProvider
    {
        IEnumerable<string> GetList();
    }

    class CustomSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            throw new NotImplementedException();
        }
    }

}
