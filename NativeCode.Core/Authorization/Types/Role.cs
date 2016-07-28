namespace NativeCode.Core.Authorization.Types
{
    public struct Role
    {
        public static readonly Role Administrator = new Role("Administrator");

        public static readonly Role Editor = new Role("Editor");

        public static readonly Role Manager = new Role("Manager");

        public static readonly Role ReadOnly = new Role("ReadOnly");

        public static readonly Role User = new Role("User");

        public Role(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}