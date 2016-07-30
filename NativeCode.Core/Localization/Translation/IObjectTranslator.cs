namespace NativeCode.Core.Localization.Translation
{
    public interface IObjectTranslator
    {
        void Translate(object instance);

        void Translate<T>(T instance);
    }
}