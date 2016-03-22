namespace NativeCode.Core.DotNet.Win32.Exceptions
{
    using System;
    using System.Runtime.CompilerServices;

    public class NativeMethodException : Exception
    {
        public NativeMethodException(int error, [CallerMemberName] string caller = default(string))
            : base(CreateExceptionMessage(caller, error))
        {
        }

        private static string CreateExceptionMessage(string caller, int error)
        {
            return $"A native method call '{caller}' generated an error of '{error}'.";
        }
    }
}