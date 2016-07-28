namespace NativeCode.Core.Authorization.Types
{
    public struct Feature
    {
        public Feature(string name, string group = "Default")
        {
            this.Group = group;
            this.Name = name;
        }

        public string Group { get; }

        public string Name { get; }
    }
}