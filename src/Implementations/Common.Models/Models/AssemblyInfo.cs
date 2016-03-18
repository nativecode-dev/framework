namespace Common.Models.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class AssemblyInfo
    {
        internal AssemblyInfo(Assembly assembly)
        {
            this.Attributes = assembly.GetCustomAttributes().Select(x => x.GetType().Name);
            this.FullName = assembly.FullName;
            this.Name = assembly.GetName().Name;
            this.Path = assembly.FullName;
        }

        public IEnumerable<string> Attributes { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}