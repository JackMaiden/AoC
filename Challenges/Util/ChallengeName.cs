namespace Challenges.Util
{
    internal class ChallengeName : Attribute
    {
        public readonly string? Name;
        public ChallengeName(string? name)
        {
            this.Name = name;
        }
    }
}