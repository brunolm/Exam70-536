using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Configuration.Install;
using System.Collections;

namespace Exam70_536
{
    [TestClass]
    public class InstallerTest
    {
        [TestMethod]
        public void InstallationTest()
        {
            // To create a custom install, inherit from Installer
            // override methods and subscribe to events
            // at the SetupProject add the custom installer

            // The craeted class must be public and have the attribute
            // RunInstaller set to true

            Assert.IsTrue(true);
        }
    }

    [RunInstaller(true)]
    public class AppInstaller : Installer
    {
        public AppInstaller()
        {
            this.BeforeInstall += (s, e) => { };
            this.AfterInstall += (s, e) => { };

            this.BeforeUninstall += (s, e) => { };
            this.AfterUninstall += (s, e) => { };

            this.Committing += (s, e) => { };
            this.Committed += (s, e) => { };

            this.BeforeRollback += (s, e) => { };
            this.AfterRollback += (s, e) => { };
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }
    }
}
