using System;
using System.Collections.Generic;

namespace Physics.Serialization
{
    public class QuantityInfo
    {
        public double Amount { get; set; }
        
        public Dictionary<string, int> Unit { get; set; }
    }
}