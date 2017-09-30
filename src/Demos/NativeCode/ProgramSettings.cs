namespace NativeCode
{
    using Core.Platform.Serialization;
    using Core.Settings;

    public class ProgramSettings : ApplicationSettings
    {
        public ProgramSettings(IStringSerializer serializer) : base(serializer)
        {
        }
    }
}