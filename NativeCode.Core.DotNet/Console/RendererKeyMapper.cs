namespace NativeCode.Core.DotNet.Console
{
    using System;
    using System.Collections.Generic;

    public class RendererKeyMapper
    {
        private readonly Dictionary<ConsoleKey, KeyMapping> mappings = new Dictionary<ConsoleKey, KeyMapping>();

        public Action GetMapping(ConsoleKeyInfo key, RenderMode mode)
        {
            if (this.mappings.ContainsKey(key.Key))
            {
                var mapping = this.mappings[key.Key];

                if (mapping.Mode == mode || mapping.Mode == RenderMode.Any)
                {
                    if (mapping.Alt != key.Modifiers.HasFlag(ConsoleModifiers.Alt))
                    {
                        return null;
                    }

                    if (mapping.Control != key.Modifiers.HasFlag(ConsoleModifiers.Control))
                    {
                        return null;
                    }

                    if (mapping.Shift != key.Modifiers.HasFlag(ConsoleModifiers.Shift))
                    {
                        return null;
                    }

                    return this.mappings[key.Key].Handler;
                }
            }

            return null;
        }

        public void Register(string name, ConsoleKey key, RenderMode mode, Action handler, bool alt = false, bool control = false, bool shift = false)
        {
            var mapping = new KeyMapping(name, mode, handler, alt, control, shift);

            if (this.mappings.ContainsKey(key))
            {
                this.mappings[key] = mapping;
            }
            else
            {
                this.mappings.Add(key, mapping);
            }
        }

        private class KeyMapping
        {
            public KeyMapping(string name, RenderMode mode, Action handler, bool alt = false, bool control = false, bool shift = false)
            {
                this.Alt = alt;
                this.Control = control;
                this.Shift = shift;

                this.Handler = handler;
                this.Mode = mode;
                this.Name = name;
            }

            public bool Alt { get; }

            public bool Control { get; }

            public Action Handler { get; }

            public RenderMode Mode { get; }

            public string Name { get; set; }

            public bool Shift { get; }
        }
    }
}