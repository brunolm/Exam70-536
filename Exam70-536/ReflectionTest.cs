using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace Exam70_536
{
    [TestClass]
    public class ReflectionTest
    {
        [TestMethod]
        public void ReflectionClassTest()
        {
            Knight knx = new Knight();

            Type knightType = knx.GetType();

            knightType.GetProperty("Name").SetValue(knx, "Knx", null);

            Assert.AreEqual("Knx", knx.Name);

            var properties = knightType.GetProperties();

            foreach (var prop in properties)
            {
                string displayName = "";

                var attr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).OfType<DisplayNameAttribute>().FirstOrDefault();

                if (attr == null)
                    displayName = prop.Name;
                else
                    displayName = attr.DisplayName;

                switch (prop.Name)
                {
                    case "Name":
                        Assert.AreEqual("Name", displayName);
                        break;
                    case "Strength":
                        Assert.AreEqual("Power level", displayName);
                        break;
                }
            }

            var constructors = knightType.GetConstructors();

            foreach (var constructor in constructors)
            {
                var constructorParametersInfo = constructor.GetParameters();

                object[] constParams = new object[constructorParametersInfo.Length];

                int i = 0;
                foreach (var param in constructorParametersInfo)
                {
                    if (param.ParameterType.IsPrimitive)
                        constParams[i++] = Activator.CreateInstance(param.ParameterType);
                    else
                        constParams[i++] = null;
                }

                Knight knight = Activator.CreateInstance(typeof(Knight), constParams) as Knight;
                Assert.AreEqual(null, knight.Name);

                Assert.AreEqual(20, knight.GetType().GetMethod("CauseDamage").Invoke(knight, new object[] { 10, 2 }));
            }
        }

        class Knight
        {
            public string Name { get; set; }
            public int Age { get; set; }

            [DisplayName("Power level")]
            public int Strength { get; set; }

            public Knight()
            {

            }
            public Knight(string name)
            {
                this.Name = name;
            }

            public int CauseDamage(int kiToConsume, int concetration)
            {
                return kiToConsume * concetration;
            }
        }
    }
}
