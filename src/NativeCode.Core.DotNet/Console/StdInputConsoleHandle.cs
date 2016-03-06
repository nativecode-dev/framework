namespace NativeCode.Core.DotNet.Console
{
    using NativeCode.Core.DotNet.Console.Win32;

    public class StdInputConsoleHandle : StdConsoleHandle
    {
        public StdInputConsoleHandle() : base(StdHandleHandle.StdInputHandle)
        {
        }
    }
}