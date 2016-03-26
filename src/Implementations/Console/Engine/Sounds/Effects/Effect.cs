namespace Console.Engine.Sounds.Effects
{
    using System.Runtime.InteropServices;

    public struct Effect
    {
        public EffectIdentifier Id;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public string Name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8192)]
        public string Text;

        public EffectType Type;
    }
}