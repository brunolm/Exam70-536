using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;

namespace Exam70_536
{
    /// <remarks>Requires administrator rights</remarks>
    [TestClass]
    public class EventLogTest
    {
        private readonly string eventLogSource = Assembly.GetExecutingAssembly().GetName().Name;

        [TestMethod]
        public void LogTest()
        {
            bool customAppEventLogExists = EventLog.SourceExists(eventLogSource);

            if (!customAppEventLogExists)
            {
                EventLog.CreateEventSource(eventLogSource, "Application");
            }

            EventLog.WriteEntry(eventLogSource, "Yo-hohoho, Yo-hohoho", EventLogEntryType.SuccessAudit);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ListLogsBySource()
        {
            EventLog log = new EventLog("Application", ".", eventLogSource);

            log.Source = eventLogSource;

            string uniqueSource = null;

            foreach (EventLogEntry item in log.Entries)
            {
                if (uniqueSource == null)
                {
                    uniqueSource = item.Source;
                    continue;
                }

                if (uniqueSource != item.Source)
                {
                    Assert.IsTrue(true, "Entries does not filter logs by source");
                }
            }
        }
    }
}
