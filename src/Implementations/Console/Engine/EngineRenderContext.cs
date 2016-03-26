namespace Console.Engine
{
    using NativeCode.Core.DotNet.Console;

    public class EngineRenderContext : RenderContext
    {
        public int LastViewPositionX
        {
            get
            {
                return this.GetMemberValue(-1);
            }
            set
            {
                this.SetMemberValue(value);
            }
        }

        public int LastViewPositionY
        {
            get
            {
                return this.GetMemberValue(-1);
            }
            set
            {
                this.SetMemberValue(value);
            }
        }

        public bool UpdateScreen
        {
            get
            {
                return this.GetMemberValue(true);
            }
            set
            {
                this.SetMemberValue(value);
            }
        }
    }
}