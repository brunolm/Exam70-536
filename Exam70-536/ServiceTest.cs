using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;

namespace Exam70_536
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void ServiceControllerTest()
        {
            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController sc in services)
            {
                // Display properties for the Simple Service sample
                // from the ServiceBase example.
                Debug.WriteLine("DisplayName            = {0}", new object[] { sc.DisplayName });
                Debug.WriteLine("ServiceName            = {0}", new object[] { sc.ServiceName });
                Debug.WriteLine("Status                 = {0}", sc.Status);
                Debug.WriteLine("Can Pause and Continue = {0}", sc.CanPauseAndContinue);
                Debug.WriteLine("Can ShutDown           = {0}", sc.CanShutdown);
                Debug.WriteLine("Can Stop               = {0}", sc.CanStop);
                Debug.WriteLine("");

                #region MSDN Example

                //if (sc.Status == ServiceControllerStatus.Stopped)
                //{
                //    sc.Start();
                //    while (sc.Status == ServiceControllerStatus.Stopped)
                //    {
                //        Thread.Sleep(1000);
                //        sc.Refresh();
                //    }
                //}
                //// Issue custom commands to the service
                //// enum SimpleServiceCustomCommands 
                ////    { StopWorker = 128, RestartWorker, CheckWorker };
                //sc.ExecuteCommand((int)SimpleServiceCustomCommands.StopWorker);
                //sc.ExecuteCommand((int)SimpleServiceCustomCommands.RestartWorker);
                //sc.Pause();
                //while (sc.Status != ServiceControllerStatus.Paused)
                //{
                //    Thread.Sleep(1000);
                //    sc.Refresh();
                //}
                //Console.WriteLine("Status = " + sc.Status);
                //sc.Continue();
                //while (sc.Status == ServiceControllerStatus.Paused)
                //{
                //    Thread.Sleep(1000);
                //    sc.Refresh();
                //}
                //Console.WriteLine("Status = " + sc.Status);
                //sc.Stop();
                //while (sc.Status != ServiceControllerStatus.Stopped)
                //{
                //    Thread.Sleep(1000);
                //    sc.Refresh();
                //}
                //Console.WriteLine("Status = " + sc.Status);
                //String[] argArray = new string[] { "ServiceController arg1", "ServiceController arg2" };
                //sc.Start(argArray);
                //while (sc.Status == ServiceControllerStatus.Stopped)
                //{
                //    Thread.Sleep(1000);
                //    sc.Refresh();
                //}
                //Console.WriteLine("Status = " + sc.Status);
                //// Display the event log entries for the custom commands
                //// and the start arguments.
                //EventLog el = new EventLog("Application");
                //EventLogEntryCollection elec = el.Entries;
                //foreach (EventLogEntry ele in elec)
                //{
                //    if (ele.Source.IndexOf("SimpleService.OnCustomCommand") >= 0 |
                //        ele.Source.IndexOf("SimpleService.Arguments") >= 0)
                //        Console.WriteLine(ele.Message);
                //}

                #endregion
            }

            Assert.IsTrue(services.Count() > 0);
        }
    }
}
