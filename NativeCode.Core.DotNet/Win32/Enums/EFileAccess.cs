namespace NativeCode.Core.DotNet.Win32.Enums
{
    using System;

    [Flags]
    public enum EFileAccess : uint
    {
        AccessSystemSecurity = 0x1000000, // AccessSystemAcl access type

        MaximumAllowed = 0x2000000, // MaximumAllowed access type

        Delete = 0x10000,

        ReadControl = 0x20000,

        WriteDac = 0x40000,

        WriteOwner = 0x80000,

        Synchronize = 0x100000,

        StandardRightsRequired = 0xF0000,

        StandardRightsRead = ReadControl,

        StandardRightsWrite = ReadControl,

        StandardRightsExecute = ReadControl,

        StandardRightsAll = 0x1F0000,

        SpecificRightsAll = 0xFFFF,

        FileReadData = 0x0001, // file & pipe

        FileListDirectory = 0x0001, // directory

        FileWriteData = 0x0002, // file & pipe

        FileAddFile = 0x0002, // directory

        FileAppendData = 0x0004, // file

        FileAddSubdirectory = 0x0004, // directory

        FileCreatePipeInstance = 0x0004, // named pipe

        FileReadEa = 0x0008, // file & directory

        FileWriteEa = 0x0010, // file & directory

        FileExecute = 0x0020, // file

        FileTraverse = 0x0020, // directory

        FileDeleteChild = 0x0040, // directory

        FileReadAttributes = 0x0080, // all

        FileWriteAttributes = 0x0100, // all

        GenericRead = 0x80000000,

        GenericWrite = 0x40000000,

        GenericExecute = 0x20000000,

        GenericAll = 0x10000000,

        FileRightsAll = 0x00FFFF,

        FileAllAccess = StandardRightsRequired | Synchronize | 0x1FF,

        FileGenericRead = StandardRightsRead | FileReadData | FileReadAttributes | FileReadEa | Synchronize,

        FileGenericWrite = StandardRightsWrite | FileWriteData | FileWriteAttributes | FileWriteEa | FileAppendData | Synchronize,

        FileGenericExecute = StandardRightsExecute | FileReadAttributes | FileExecute | Synchronize
    }
}
