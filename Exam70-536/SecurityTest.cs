using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32;
using System.Security;
using System.IO;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Security.Policy;
using System.Runtime.Remoting;
using System.Reflection;

namespace Exam70_536
{
    [TestClass]
    public class SecurityTest
    {
        [TestMethod]
        public void WindowsAuthTest()
        {
            // Current user
            WindowsIdentity wi = WindowsIdentity.GetCurrent();

            // User profile
            WindowsPrincipal wp = new WindowsPrincipal(wi);

            if (wp.IsInRole(WindowsBuiltInRole.Administrator))
            {
                Assert.IsTrue(true, "Admin");
            }
            else
            {
                Assert.IsFalse(false, "Not admin");
            }
        }

        [TestMethod]
        public void PermissionTest()
        {
            RegistryPermission rp = new RegistryPermission(RegistryPermissionAccess.Write, "HKEY_LOCAL_MACHINE\\HARDWARE");
            rp.PermitOnly();

            try
            {
                string[] names = Registry.LocalMachine.GetValueNames();
            }
            catch (SecurityException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AccessControlTest()
        {
            DirectoryInfo fi = new DirectoryInfo(".");
            if (!fi.Exists)
                fi.Create();

            FileSecurity fs = new FileSecurity();
            fs.SetAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.ListDirectory, AccessControlType.Deny));

            try
            {
                fi.GetFiles();
            }
            catch (IOException)
            {
                Assert.IsTrue(true);
            }

            fs.SetAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.ListDirectory, AccessControlType.Allow));

            fi.GetFiles();

            Assert.IsTrue(true);
        }

        /// <summary>
        /// Sandboxing is the practice of running code in a restricted security environment,
        /// which limits the access permissions granted to the code. For example, if you have
        /// a managed library from a source you do not completely trust, you should not run it
        /// as fully trusted. Instead, you should place the code in a sandbox that limits its
        /// permissions to those that you expect it to need (for example, Execution permission).
        /// </summary>
        /// <remarks>Requires Strong Name key file.</remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/bb763046.aspx"/>
        [TestMethod]
        public void SandboxTest()
        {
            // Create a PermissionSet only allowing a 3rd party assembly to execute,
            // but not writing to disk
            // (None = Removes all permissions; Unrestricted = Allows everything)
            PermissionSet ps = new PermissionSet(PermissionState.None);
            ps.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            // ps.AddPermission(new FileIOPermission(FileIOPermissionAccess.NoAccess, "."));

            // Creates a setup for the application domain
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = Environment.CurrentDirectory;

            // The hosting class must run as fully trusted to enable the execution of th
            // partial-trust code or to offer services to the partial-trust application. 
            // Strongkey password: 123123
            StrongName strongName = typeof(Sandbox).Assembly.Evidence.GetHostEvidence<StrongName>();

            // Creates an isolated domain to invoke code
            AppDomain sandboxDomain = AppDomain.CreateDomain("Sandbox",
                null,
                setup,
                ps,
                strongName);

            // Creates an instance of the Sandbox at the sandbox domain
            ObjectHandle handle = Activator.CreateInstanceFrom(sandboxDomain, @"..\..\..\Exam70-536\bin\debug\Exam70-536.dll", "Exam70_536.SecurityTest+Sandbox");

            // Retrieve the Sandbox object from the created domain
            Sandbox sandbox = (Sandbox)handle.Unwrap();

            // Invoke methods of the `unsafe code` at the sandbox domain
            try
            {
                sandbox.ExecuteMethod("Other", "Other.UnsafeClass", "PretendToBeANiceMethod", "this method is evil");
                Assert.IsTrue(false);
            }
            catch (SecurityException)
            {
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [Serializable]
        public class Sandbox : MarshalByRefObject
        {
            public void ExecuteMethod(string assemblyName, string className, string methodName, params object[] parameters)
            {
                var assembly = Assembly.Load(assemblyName);

                object unsafeCode = assembly.CreateInstance(className);

                assembly.GetType(className).GetMethod(methodName).Invoke(unsafeCode, parameters);
            }
        }
    }
}
