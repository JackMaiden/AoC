namespace Challenges.Util
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ChallengeName(string? name) : Attribute
    {
        public readonly string? Name = name;
    }
}