using System.Linq;

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