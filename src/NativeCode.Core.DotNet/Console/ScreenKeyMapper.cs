namespace NativeCode.Core.DotNet.Console
{
    using System;
    using System.Collections.Generic;

    public class ScreenKeyMapper
    {
        private readonly Dictionary<ConsoleKeyInfo, KeyMapping> mappings = new Dictionary<ConsoleKeyInfo, KeyMapping>();

        public void Register(string name, ScreenMode mode, ConsoleKeyInfo key, Action handler)
        {
            var mapping = new KeyMapping(name, mode, handler);

            if (this.mappings.ContainsKey(key))
            {
                this.mappings[key] = mapping;
            }
            else
            {
                this.mappings.Add(key, mapping);
            }
        }

        public Action GetMapping(ConsoleKeyInfo key, ScreenMode mode)
        {
            if (this.mappings.ContainsKey(key))
            {
                var mapping = this.mappings[key];

                if (mapping.Mode == mode)
                {
                    return this.mappings[key].Handler;
                }
            }

            return null;
        }

        private class KeyMapping
        {
            public KeyMapping(string name, ScreenMode mode, Action handler)
            {
                this.Handler = handler;
                this.Mode = mode;
                this.Name = name;
            }

            public Action Handler { get; }

            public ScreenMode Mode { get; }

            public string Name { get; set; }
        }
    }
}