using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam70_536
{
    [TestClass]
    public class AppDomainTest
    {
        [TestMethod]
        public void AppDomainDataTest()
        {
            AppDomain appdomain = AppDomain.CreateDomain("Exam70536");

            appdomain.SetData("Exam70536", "Sending message...");

            // wraps both (current and invoked) applications in the same AppDomain
            // allowing to transfer data

            //
            //  ////////           Created AppDomain
            //  /  -   /           /////////////////////////////////
            //  /   -  /           /                               /
            //  ////////           /  ////////        ////////     /
            //                     /  /  -   /        /  -   /     /
            //  ////////           /  /   -  /        /   -  /     /
            //  /  -   /           /  ////////        ////////     /
            //  /   -  /           /                               /
            //  ////////           /////////////////////////////////
            //

            appdomain.ExecuteAssembly(@"..\..\..\Other\bin\Debug\Other.exe");

            Assert.AreEqual("OK", appdomain.GetData("Exam70536") as string);

            AppDomain.Unload(appdomain);
        }
    }
}
