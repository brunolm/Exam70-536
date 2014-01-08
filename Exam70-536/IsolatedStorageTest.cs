using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.IsolatedStorage;
using System.Diagnostics;

namespace Exam70_536
{
    [TestClass]
    public class IsolatedStorageTest
    {
        /// <summary>
        /// The System.IO.IsolatedStorage namespace contains types that allow the creation and use of isolated stores.
        /// With these stores, you can read and write data that less trusted code cannot access and prevent the
        /// exposure of sensitive information that can be saved elsewhere on the file system. Data is stored in
        /// compartments that are isolated by the current user and by the assembly in which the code exists. 
        /// Additionally, data can be isolated by domain. Roaming profiles can be used in conjunction with isolated
        /// storage so isolated stores will travel with the user's profile.
        /// The IsolatedStorageScope enumeration indicates different types of isolation.
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.io.isolatedstorage.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.io.isolatedstorage.isolatedstoragefile.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.io.isolatedstorage.isolatedstoragefilestream.aspx"/>
        [TestMethod]
        public void StorageTest()
        {
            string storageFolder = "Exam70536";
            string storageFile = @"Exam70536\exam.txt";

            // IsolatedStorage (abstract)

            // var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.Machine, null);
            //var storeApp = IsolatedStorageFile.GetUserStoreForApplication(); ??
            var storeAss = IsolatedStorageFile.GetUserStoreForAssembly(); // User\App Data
            var storeDom = IsolatedStorageFile.GetUserStoreForDomain();
            //var storeSit = IsolatedStorageFile.GetUserStoreForSite(); ??

            //var storeAppM = IsolatedStorageFile.GetMachineStoreForApplication(); ??
            var storeAssM = IsolatedStorageFile.GetMachineStoreForAssembly(); // ProgramData
            var storeDomM = IsolatedStorageFile.GetMachineStoreForDomain();

            if (!storeAss.DirectoryExists(storageFolder))
                storeAss.CreateDirectory(storageFolder); // override folder
            Assert.IsTrue(storeAss.DirectoryExists(storageFolder));

            if (!storeAss.FileExists(storageFile))
                storeAss.CreateFile(storageFile);
            Assert.IsTrue(storeAss.FileExists(storageFile));

            storeDom.CreateDirectory(storageFolder);
            Assert.IsTrue(storeDom.DirectoryExists(storageFolder));
            //storeSit.CreateDirectory(storageFolder);

            //storeAppM.CreateDirectory(storageFolder);
            storeAssM.CreateDirectory(storageFolder);
            Assert.IsTrue(storeAssM.DirectoryExists(storageFolder));
            storeDomM.CreateDirectory(storageFolder);
            Assert.IsTrue(storeDomM.DirectoryExists(storageFolder));

            Process.Start(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile));
        }
    }
}
