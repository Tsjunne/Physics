using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Xunit;
using Newtonsoft.Json;
using Physics.Serialization;

namespace Physics.Test.Core
{
    public class WhenMappingQuantity : GivenSiSystem
    {
        [Fact]
        public void ThenCanMapToInfoAndBack()
        {
            var quantity = new Quantity(100, J/(m ^ 3));

            var info = quantity.ToInfo();
            var result = System.FromInfo(info);

            Assert.Equal(quantity, result);
        }

        [Fact]
        public void ThenCanSerializeAndDeserializeUsingDataContractSerializer()
        {
            var quantity = new Quantity(100, J/(m ^ 3));

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

            var result = System.FromInfo(deserializedInfo);

            Assert.Equal(quantity, result);
        }
        
        [Fact]
        public void ThenCanSerializeAndDeserializeUsingJsonNet()
        {
            var quantity = new Quantity(100, J/(m ^ 3));

            var info = quantity.ToInfo();

            var json = JsonConvert.SerializeObject(info);
            var deserializedInfo = JsonConvert.DeserializeObject<QuantityInfo>(json);

            var result = System.FromInfo(deserializedInfo);

            Assert.Equal(quantity, result);
        }
    }
}