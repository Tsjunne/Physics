using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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