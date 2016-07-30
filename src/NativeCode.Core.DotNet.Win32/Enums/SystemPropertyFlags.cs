namespace NativeCode.Core.DotNet.Win32.Enums
{
    using System;

    [Flags]
    public enum SystemPropertyFlags
    {
        None = 0x00, 

        UpdateIniFile = 0x01, 

        SendChange = 0x02, 

        SendWinIniChange = 0x02
    }
}