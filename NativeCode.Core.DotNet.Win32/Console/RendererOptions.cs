namespace NativeCode.Core.DotNet.Win32.Console
{
    using NativeCode.Core.Types.Structs;

    public class RendererOptions
    {
        public int CursorSize { get; set; } = 1;

        public bool CursorVisible { get; set; } = true;

        public string FontName { get; set; } = "Sunkure Font";

        public Size FontSize { get; set; } = new Size(0, 0);

        public int FontWidth { get; set; } = 0;

        public int ScreenHeight { get; set; } = 25;

        public int ScreenWidth { get; set; } = 80;
    }
}