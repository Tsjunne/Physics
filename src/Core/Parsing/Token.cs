using System.Linq;

namespace Physics.Parsing
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