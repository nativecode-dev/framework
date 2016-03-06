namespace NativeCode.Core.DotNet.Console
{
    using NativeCode.Core.DotNet.Console.Win32;

    public class StdErrorConsoleHandle : StdConsoleHandle
    {
        public StdErrorConsoleHandle() : base(StdHandleHandle.StdErrorHandle)
        {
        }
    }
}