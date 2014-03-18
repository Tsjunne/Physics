using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Presentation
{
    public class Token : ImmutableCollection<string>
    {
        public Token(params string[] representations)
            : base(representations)
        {
        }

        public static implicit operator string[](Token token)
        {
            return token.ToArray();
        }
    }
}
