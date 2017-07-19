namespace NativeCode.Core.DotNet.Win32.Console
{
    using System;
    using System.Collections.Generic;
    using Settings;

    public class RenderContext : JsonSettings
    {
        private readonly List<Exception> exceptions = new List<Exception>();

        public RenderContext()
        {
        }

        public RenderContext(JsonSettings context)
            : base(context)
        {
        }

        public bool DisableFlip
        {
            get => this.GetMemberValue(false);

            set => this.SetMemberValue(value);
        }

        public List<Exception> Exceptions => this.GetMemberValue(this.exceptions);

        public bool IsDirty
        {
            get => this.GetMemberValue(false);

            set => this.SetMemberValue(value);
        }

        public DateTimeOffset LastRenderStart
        {
            get => this.GetMemberValue(DateTimeOffset.UtcNow);

            set => this.SetMemberValue(value);
        }

        public DateTimeOffset LastRenderStop
        {
            get => this.GetMemberValue(DateTimeOffset.UtcNow);

            set => this.SetMemberValue(value);
        }
    }
}