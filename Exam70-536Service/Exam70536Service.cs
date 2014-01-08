using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace Exam70_536Service
{
    public partial class Exam70536Service : ServiceBase
    {
        private FileSystemWatcher fsw = new FileSystemWatcher();

        public Exam70536Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            fsw = new FileSystemWatcher();

            fsw.Created += (s, e) =>
            {
                // logger.WriteEntry(String.Format("The file {0} has been created.", e.Name));
            };

            fsw.Deleted += (s, e) =>
            {
                // logger.WriteEntry(String.Format("The file {0} has been deleted.", e.Name));
            };

            fsw.Renamed += (s, e) =>
            {
                // logger.WriteEntry(String.Format("The file {0} has been renamed to {1}.", e.OldName, e.Name));
            };

            fsw.Changed += (s, e) =>
            {
            };

            fsw.EnableRaisingEvents = true;
        }

        protected override void OnContinue()
        {
            fsw.EnableRaisingEvents = true;
        }
        protected override void OnPause()
        {
            fsw.EnableRaisingEvents = false;
        }
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            switch (powerStatus)
            {
                case PowerBroadcastStatus.BatteryLow:
                    break;
                case PowerBroadcastStatus.OemEvent:
                    break;
                case PowerBroadcastStatus.PowerStatusChange:
                    break;
                case PowerBroadcastStatus.QuerySuspend:
                    break;
                case PowerBroadcastStatus.QuerySuspendFailed:
                    break;
                case PowerBroadcastStatus.ResumeAutomatic:
                    break;
                case PowerBroadcastStatus.ResumeCritical:
                    break;
                case PowerBroadcastStatus.ResumeSuspend:
                    break;
                case PowerBroadcastStatus.Suspend:
                    break;
                default:
                    break;
            }

            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnStop()
        {
            fsw.EnableRaisingEvents = false;
            fsw.Dispose();
        }
    }
}
