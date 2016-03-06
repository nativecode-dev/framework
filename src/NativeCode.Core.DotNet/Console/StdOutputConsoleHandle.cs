namespace NativeCode.Core.DotNet.Console
{
    using NativeCode.Core.DotNet.Console.Win32;

    public class StdOutputConsoleHandle : StdConsoleHandle
    {
        public StdOutputConsoleHandle() : base(StdHandleHandle.StdOutputHandle)
        {
        }
    }
}