namespace NativeCode.Core.DotNet.Win32.Console.Controls
{
    using System.Collections.Generic;

    public class Panel : Control
    {
        private readonly List<Control> children = new List<Control>();

        public IReadOnlyList<Control> Children => this.children;
    }
}