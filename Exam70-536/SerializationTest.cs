using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;

namespace Exam70_536
{
    /// <summary>
    /// You should pay attention to:
    /// - Parameterless constructor
    /// - Attributes
    /// - Events
    /// - Interfaces
    /// </summary>
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void XmlSerializationTest()
        {
            // Genkidama inherits from Power, but the serializer doesn't know that
            // to make it work add a attribute [XmlInclude(typeof(type))] on the parent class
            // or instantiate the XmlSerializer with a second paramater Type[]
            //
            // Private members aren't serialized. To ignore a public property add the attribute
            // [XmlIgnore]

            XmlSerializer serializer = new XmlSerializer(typeof(Sayajin));

            Sayajin sayajin = new Sayajin
            {
                Name = "Goku",
                PowerLevel = 9999999999,
                Powers = new List<Power>
                {
                    new Power { Name = "Kamehameha", RequiredPowerLevel = 12500, Type = PowerType.Ki },
                    new Genkidama { Name = "Kamehameha", RequiredPowerLevel = 50000, Type = PowerType.Ki, Radius = 50000 },
                }
            };

            Sayajin deserializedSayajin;

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, sayajin);

                string serializedXmlString = Encoding.UTF8.GetString(ms.ToArray());

                ms.Position = 0;

                deserializedSayajin = serializer.Deserialize(ms) as Sayajin;
            }

            Assert.AreEqual(sayajin.Name, deserializedSayajin.Name);
            Assert.AreEqual(sayajin.PowerLevel, deserializedSayajin.PowerLevel);
            Assert.AreEqual(sayajin.Powers.Count, deserializedSayajin.Powers.Count);
        }

        [TestMethod]
        public void BinarySerializationTest()
        {
            BinaryFormatter serializer = new BinaryFormatter();

            Yokai raizen = new Yokai
            {
                Name = "Raizen",
                PowerClass = PowerClassType.SSSSSSSSSSSS,
                Abilities = new List<Ability>
                {
                    new Ability { RequiredReiki = 99999999999, Type = AbilityType.妖気 }
                }
            };

            Yokai raizenDeserialized;

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, raizen);

                string binaryString = Encoding.UTF8.GetString(ms.ToArray());

                ms.Position = 0;

                raizenDeserialized = serializer.Deserialize(ms) as Yokai;
            }

            Assert.AreEqual(raizen.Name, raizenDeserialized.Name);
            Assert.AreEqual(raizen.PowerClass, raizenDeserialized.PowerClass);
        }
    }

    #region XmlSerialization Classes

    public enum PowerType
    {
        // The attribute XmlEnum changes the value written on the XML file
        [XmlEnum("Ki - Energy from Dragon Ball")]
        Ki,

        [XmlEnum("Reiatsu - Energy from Bleach")]
        Reiatsu,

        [XmlEnum("Reiki - Energy from Yu Yu Hakusho")]
        Reiki,
    }

    [XmlInclude(typeof(Genkidama))]
    public class Power
    {
        public string Name { get; set; }
        public ulong RequiredPowerLevel { get; set; }
        public PowerType Type { get; set; }
    }
    public class Genkidama : Power
    {
        public double Radius { get; set; }
    }

    // Changes the name of the root element
    [XmlRoot(ElementName="SSJ")]
    public class Sayajin
    {
        // XmlAttribute sets the property as an attribute of the root element
        [XmlAttribute]
        public string Name { get; set; }
        public ulong PowerLevel { get; set; }

        // [XmlArray] can change the array container name and
        // [XmlArrayItem] can change the array items elements name
        [XmlArray(ElementName = "Powers")]
        [XmlArrayItem(ElementName = "Power")]
        public List<Power> Powers { get; set; }
    }

    #endregion

    #region BinarySerialization Classes

    public enum PowerClassType
    {
        SSSSSSSSSSSS,
        S,
        A,
        B,
        C,
        D,
    }

    public enum AbilityType
    {
        霊気,
        妖気,
        聖光気,
    }

    // Attribute allows binary serialization
    [Serializable]
    public class Yokai
    {
        public string Name { get; set; }
        public PowerClassType PowerClass { get; set; }
        public List<Ability> Abilities { get; set; }
    }

    [Serializable]
    public class Ability
    {
        public ulong RequiredReiki { get; set; }
        public AbilityType Type { get; set; }
    }

    #endregion
}
