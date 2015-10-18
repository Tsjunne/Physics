using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        {
        }
    }
}