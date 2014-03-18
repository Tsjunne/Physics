using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Test
{
    public abstract class GivenWhenThen
    {
        [TestInitialize]
        public virtual void Initialize()
        {
            Given();
            When();
        }

        public virtual void Given()
        {
        }

        public virtual void When()
        {
        }

        [TestCleanup]
        public virtual void Cleanup()
        { }
    }
}
