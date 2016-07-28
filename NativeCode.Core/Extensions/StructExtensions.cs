namespace NativeCode.Core.Extensions
{
    using System.Runtime.InteropServices;

    public static class StructExtensions
    {
        public static T FromBytes<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        public static byte[] GetBytes<T>(this T value) where T : struct
        {
            var length = Marshal.SizeOf(value);
            var pointer = Marshal.AllocHGlobal(length);

            try
            {
                var buffer = new byte[length];
                Marshal.StructureToPtr(value, pointer, true);
                Marshal.Copy(pointer, buffer, 0, length);

                return buffer;
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }

        public static int GetSize<T>() where T : struct
        {
            return Marshal.SizeOf(typeof(T));
        }
    }
}