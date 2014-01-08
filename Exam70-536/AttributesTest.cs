using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam70_536
{
    /// <summary>
    /// The Attribute class associates predefined system information or user-defined
    /// custom information with a target element.
    /// A target element can be an assembly, class, constructor, delegate, enum, event,
    /// field, interface, method, portable executable file module, parameter, property,
    /// return value, struct, or another attribute.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/system.attribute.aspx"/>
    /// <see cref="http://msdn.microsoft.com/en-us/library/bfz783fz.aspx"/>
    /// <see cref="http://msdn.microsoft.com/en-us/library/84c42s56.aspx"/>
    [TestClass]
    public class AttributesTest
    {
        [TestMethod]
        public void AttributeDisplayTest()
        {
            Gundam gundam = new Gundam
            {
                Name = "Wing 0",
                RequiredLevel = 99,
                Weapons =
                { 
                    new Weapon { Name = "GundamSaber", PowerLevel = 9999 },
                    new PlasmaCannon { Name = "Plasma Cannon 0", PowerLevel = 9999 },
                }
            };

            Weapon scythe = new Weapon { Name = "Death Scythe", PowerLevel = 9020 };
            PlasmaCannon proton = new PlasmaCannon { Name = "Proton Cannon", PowerLevel = 9999 };

            var gundamAttr = gundam.GetType().GetProperty("Name").GetCustomAttributes(typeof(DisplayAttribute), false).First() as DisplayAttribute;
            var weaponAttr = scythe.GetType().GetProperty("Name").GetCustomAttributes(typeof(DisplayAttribute), false).First() as DisplayAttribute;
            var plasmaAttr = proton.GetType().GetProperty("Name").GetCustomAttributes(typeof(DisplayAttribute), false).First() as DisplayAttribute;

            Assert.AreEqual("Gundam Name", gundamAttr.Name);
            Assert.AreEqual("Weapon Name", weaponAttr.Name);
            Assert.AreEqual("Ranged Weapon Name", plasmaAttr.Name);
        }

        public class Gundam
        {
            [Display(Name = "Gundam Name")]
            public string Name { get; set; }
            public int RequiredLevel { get; set; }
            public List<Weapon> Weapons { get; set; }

            public Gundam()
            {
                Weapons = new List<Weapon>();
            }
        }

        public class Weapon
        {
            [Display(Name = "Weapon Name")]
            public virtual string Name { get; set; }
            public int PowerLevel { get; set; }
        }

        public class PlasmaCannon : Weapon
        {
            [Display(Name = "Ranged Weapon Name")]
            public override string Name
            {
                get { return base.Name; }
                set { base.Name = value; }
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class DisplayAttribute : Attribute
        {
            public string Name { get; set; }
        }
    }
}
