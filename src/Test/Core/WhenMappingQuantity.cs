using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenMappingQuantity : GivenSiSystem
    {
        [TestMethod]
        public void ThenCanMapToInfoAndBack()
        {
            var quantity = new Quantity(100, this.J / (this.m ^ 3));

            var info = quantity.ToInfo();
            var result = this.System.FromInfo(info);

            Assert.AreEqual(quantity, result);
        }
            
        [TestMethod]
        public void ThenCanSerializeAndDeserializeUsingDataContractSerializer()
        {
            var quantity = new Quantity(100, this.J / (this.m ^ 3));

            var info = quantity.ToInfo();

            var serializer = new DataContractSerializer(typeof(QuantityInfo));
            
            var builder = new StringBuilder();
            using(var writer = XmlWriter.Create(builder))
            {
                serializer.WriteObject(writer, info);
            }

            QuantityInfo deserializedInfo;
            using (var reader = XmlReader.Create(new StringReader(builder.ToString())))
            {
                deserializedInfo = (QuantityInfo)serializer.ReadObject(reader);
            }

            var result = this.System.FromInfo(deserializedInfo);

            Assert.AreEqual(quantity, result);
        }

        [TestMethod]
        public void ThenCanSerializeAndDeserializeUsingXmlSerializer()
        {
            var quantity = new Quantity(100, this.J / (this.m ^ 3));

            var info = quantity.ToInfo();

            var serializer = new XmlSerializer(typeof(QuantityInfo));

            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder))
            {
                serializer.Serialize(writer, info);
            }

            QuantityInfo deserializedInfo;
            using (var reader = XmlReader.Create(new StringReader(builder.ToString())))
            {
                deserializedInfo = (QuantityInfo)serializer.Deserialize(reader);
            }

            var result = this.System.FromInfo(deserializedInfo);

            Assert.AreEqual(quantity, result);
        }

        [TestMethod]
        public void ThenCanSerializeAndDeserializeUsingJsonSerializer()
        {
            var quantity = new Quantity(100, this.J / (this.m ^ 3));

            var info = quantity.ToInfo();

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(info);
            var deserializedInfo = serializer.Deserialize<QuantityInfo>(json);

            var result = this.System.FromInfo(deserializedInfo);

            Assert.AreEqual(quantity, result);
        }
    }
}
