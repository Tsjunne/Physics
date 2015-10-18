using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Physics.Serialization;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenMappingQuantity : GivenSiSystem
    {
        [TestMethod]
        public void ThenCanMapToInfoAndBack()
        {
            var quantity = new Quantity(100, this.J/(this.m ^ 3));

            var info = quantity.ToInfo();
            var result = this.System.FromInfo(info);

            Assert.AreEqual(quantity, result);
        }

        [TestMethod]
        public void ThenCanSerializeAndDeserializeUsingDataContractSerializer()
        {
            var quantity = new Quantity(100, this.J/(this.m ^ 3));

            var info = quantity.ToInfo();

            var serializer = new DataContractSerializer(typeof (QuantityInfo));

            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder))
            {
                serializer.WriteObject(writer, info);
            }

            QuantityInfo deserializedInfo;
            using (var reader = XmlReader.Create(new StringReader(builder.ToString())))
            {
                deserializedInfo = (QuantityInfo) serializer.ReadObject(reader);
            }

            var result = this.System.FromInfo(deserializedInfo);

            Assert.AreEqual(quantity, result);
        }

        [TestMethod]
        public void ThenCanSerializeAndDeserializeUsingJavaScriptSerializer()
        {
            var quantity = new Quantity(100, this.J/(this.m ^ 3));

            var info = quantity.ToInfo();

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(info);
            var deserializedInfo = serializer.Deserialize<QuantityInfo>(json);

            var result = this.System.FromInfo(deserializedInfo);

            Assert.AreEqual(quantity, result);
        }

        [TestMethod]
        public void ThenCanSerializeAndDeserializeUsingJsonNet()
        {
            var quantity = new Quantity(100, this.J/(this.m ^ 3));

            var info = quantity.ToInfo();

            var json = JsonConvert.SerializeObject(info);
            var deserializedInfo = JsonConvert.DeserializeObject<QuantityInfo>(json);

            var result = this.System.FromInfo(deserializedInfo);

            Assert.AreEqual(quantity, result);
        }
    }
}