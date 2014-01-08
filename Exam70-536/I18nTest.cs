using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Globalization;

namespace Exam70_536
{
    [TestClass]
    public class I18nTest
    {
        [TestMethod]
        public void ListCulturesTest()
        {
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                if (culture.Name.Length < 5)
                {
                    Assert.Fail();
                }
            }

            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
            {
                if (culture.LCID == 127)
                {
                    // invariant culture
                    Assert.AreEqual("", culture.Name);
                }
            }
        }

        [TestMethod]
        public void CurrentCultureTest()
        {
            Assert.IsTrue(Thread.CurrentThread.CurrentCulture.Name.Length > 0);
            Assert.IsTrue(Thread.CurrentThread.CurrentCulture.DisplayName.Length > 0);
            Assert.IsTrue(Thread.CurrentThread.CurrentCulture.NativeName.Length > 0);
            Assert.AreEqual(2, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Length);
        }

        [TestMethod]
        public void RegionTest()
        {
            RegionInfo regionInfo = new RegionInfo(new CultureInfo("ja-JP").LCID);

            Assert.AreEqual("¥", regionInfo.CurrencySymbol);
            Assert.AreEqual("JPY", regionInfo.ISOCurrencySymbol);
            Assert.AreEqual("Japanese Yen", regionInfo.CurrencyEnglishName);
            Assert.AreEqual("円", regionInfo.CurrencyNativeName);

            Assert.AreEqual("Japan", regionInfo.EnglishName);
            Assert.AreEqual("Japan", regionInfo.DisplayName);

            Assert.AreEqual("JP", regionInfo.Name);
            Assert.AreEqual("日本", regionInfo.NativeName);
            Assert.AreEqual("JP", regionInfo.TwoLetterISORegionName);

            Assert.IsTrue(regionInfo.IsMetric);
        }

        [TestMethod]
        public void DateTimeCultureTest()
        {
            CultureInfo culture = new CultureInfo("ja-JP");

            string[] days = culture.DateTimeFormat.DayNames;

            Assert.AreEqual("日曜日", days[0]); // Sunday
            Assert.AreEqual("月曜日", days[1]); // Monday
            Assert.AreEqual("火曜日", days[2]); // Tuesday
            Assert.AreEqual("水曜日", days[3]); // Wednesday
            Assert.AreEqual("木曜日", days[4]); // Thursday
            Assert.AreEqual("金曜日", days[5]); // Friday
            Assert.AreEqual("土曜日", days[6]); // Saturday 

            Assert.AreEqual("2012年12月21日", new DateTime(2012, 12, 21).ToString("D", culture));
        }

        [TestMethod]
        public void CurrencyTest()
        {
            Assert.AreEqual("$10.03", (10.03).ToString("C", new CultureInfo("en-US")));
            Assert.AreEqual("$10.00", (10).ToString("C", new CultureInfo("en-US")));
            Assert.AreEqual("R$ 10,01", (10.01).ToString("C", new CultureInfo("pt-BR")));
            Assert.AreEqual("¥10", (10.1).ToString("C", new CultureInfo("ja-JP")));
        }

        [TestMethod]
        public void CurrentCultureRetrieveTest()
        {
            Assert.AreEqual(Thread.CurrentThread.CurrentCulture, CultureInfo.CurrentCulture);
        }
    }
}
