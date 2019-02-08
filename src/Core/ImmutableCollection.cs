using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Physics
{
    public class ImmutableCollection<TItem> : ReadOnlyCollection<TItem>, IEquatable<ImmutableCollection<TItem>>
    {
        private readonly int _hashCode;

        public ImmutableCollection(IList<TItem> items)
            : base(items)
        {
            _hashCode = Items.Hash();
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
            return _hashCode;
        }
    }
}