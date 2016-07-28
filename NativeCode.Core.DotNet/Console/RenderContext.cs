namespace NativeCode.Core.DotNet.Console
{
    using System;
    using System.Collections.Generic;

    using NativeCode.Core.Settings;

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
            get
            {
                return this.GetMemberValue(false);
            }
            set
            {
                this.SetMemberValue(value);
            }
        }

        public List<Exception> Exceptions => this.GetMemberValue(this.exceptions);

        public bool IsDirty
        {
            get
            {
                return this.GetMemberValue(false);
            }
            set
            {
                this.SetMemberValue(value);
            }
        }

        public DateTimeOffset LastRenderStart
        {
            get
            {
                return this.GetMemberValue(DateTimeOffset.UtcNow);
            }
            set
            {
                this.SetMemberValue(value);
            }
        }

        public DateTimeOffset LastRenderStop
        {
            get
            {
                return this.GetMemberValue(DateTimeOffset.UtcNow);
            }
            set
            {
                this.SetMemberValue(value);
            }
        }
    }
}