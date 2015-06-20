using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public class ImmutableCollection<TItem> : ReadOnlyCollection<TItem>, IEquatable<ImmutableCollection<TItem>>
    {
        private readonly int hashCode;

        public ImmutableCollection(IList<TItem> items)
            : base(items)
        {
            this.hashCode = this.Items.Hash();
        }

        public bool Equals(ImmutableCollection<TItem> other)
        {
            if (other == null) return false;
            return (this.SequenceEqual(other));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ImmutableCollection<TItem>);
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }
    }
}
