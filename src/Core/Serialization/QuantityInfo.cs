using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Serialization
{
    [Serializable]
    [DataContract]
    public class QuantityInfo
    {
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public Dictionary<string, int> Unit { get; set; }
    }
}
