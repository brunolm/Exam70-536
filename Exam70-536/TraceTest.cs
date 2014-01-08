using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Exam70_536
{
    /// <see cref="http://msdn.microsoft.com/en-us/library/system.diagnostics.correlationmanager.aspx"/>
    [TestClass]
    public class TraceTest
    {
        [TestMethod]
        public void TracingTest()
        {
            Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            Trace.Listeners.Add(new XmlWriterTraceListener("log.xml"));
            Trace.Listeners.Add(new DelimitedListTraceListener("log.csv"));
            // Trace.Listeners.Add(new EventSchemaTraceListener("x"));            
            // Trace.Listeners.Add(new EventLogTraceListener("Exam70-536"));

            Trace.Listeners.Add(new CustomTraceListener());

            Trace.Write("Test trace");
            Trace.WriteLine("!");
            Trace.Flush();

            // Process.Start(".");

            Assert.IsTrue(true);
        }
    }

    public class CustomTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            Console.Write(message);
        }

        public override void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }

}
