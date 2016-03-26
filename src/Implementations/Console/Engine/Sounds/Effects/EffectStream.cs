namespace Console.Engine.Sounds.Effects
{
    using System.IO;

    using NativeCode.Core.Extensions;

    public class EffectStream : GameFileCollectionStream<Effect>
    {
        public static readonly int EffectSize = StructExtensions.GetSize<Effect>();

        public EffectStream(Stream stream, bool owner = true)
            : base(stream, owner)
        {
        }
    }
}