namespace ServicesWeb.Models
{
    using System.Collections.Generic;

    public class AssemblyInfo
    {
        public IEnumerable<string> Attributes { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}