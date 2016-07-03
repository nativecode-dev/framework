namespace Console.Engine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Speech.Synthesis;
    using System.Threading;
    using System.Threading.Tasks;

    using Console.Engine.Mapping;

    using Humanizer;

    using NativeCode.Core.DotNet.Console;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Types;

    public class Engine : Disposable
    {
        public Engine(int width, int height)
        {
            this.DisplayHeight = height;
            this.DisplayWidth = width;
            this.SpeechSynthesizer = new SpeechSynthesizer();
            this.SpeechSynthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);

            this.Renderer = new EngineRenderer(this.Context, this.DisplayWidth, this.DisplayHeight);
            this.Renderer.KeyMapper.Register("Any.Quit", ConsoleKey.Q, RenderMode.Any, this.Stop, control: true);
            this.Renderer.SetMode(RenderMode.Rendering);
        }

        public EngineContext Context { get; } = new EngineContext();

        public int DisplayHeight { get; }

        public int DisplayWidth { get; }

        public EngineRenderer Renderer { get; private set; }

        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        protected SpeechSynthesizer SpeechSynthesizer { get; private set; }

        public void Load(string filename)
        {
            var stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Write);

            this.SetMap(new MapFile(stream, progress: this.HandleProgress));
        }

        private void HandleProgress(long current, long max)
        {
            //var percent = (double)current / max * 100;

            //if ((int)percent % 10 == 0)
            //{
            //    Console.CursorLeft = 0;
            //    Console.CursorTop = 0;
            //    this.Renderer.ActiveBuffer.Write($"{percent:P}", color: Color.ForegroundBlue | Color.ForegroundGreen);
            //}
        }

        public void NewMap(string filename, int width, int height)
        {
            var name = Path.GetFileNameWithoutExtension(filename).Humanize(LetterCasing.Title);
            var stream = File.Open(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            var map = new MapFile(stream, name, width, height, progress: this.HandleProgress);

            this.SetMap(map);
        }

        public Task StartAsync()
        {
            this.CancellationTokenSource = new CancellationTokenSource();

            try
            {
                var token = this.CancellationTokenSource.Token;
                var tasks = new List<Task> { Task.Run(() => this.RunGameLoopAsync(token), token), Task.Run(() => this.RunSoundLoopAsync(token), token) };

                return Task.WhenAll(tasks);
            }
            catch
            {
                this.CancellationTokenSource.Dispose();
                this.CancellationTokenSource = null;
                throw;
            }
        }

        public void Stop()
        {
            this.CancellationTokenSource?.Cancel(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                if (this.SpeechSynthesizer != null)
                {
                    this.SpeechSynthesizer.Dispose();
                    this.SpeechSynthesizer = null;
                }

                if (this.Renderer != null)
                {
                    this.Renderer.Dispose();
                    this.Renderer = null;
                }
            }

            base.Dispose(disposing);
        }

        private void SetMap(MapFile map)
        {
            this.Renderer.ChangeMap(map);
        }

        private Task RunGameLoopAsync(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                this.Renderer.Render();
            }

            return Task.FromResult(0);
        }

        private Task RunSoundLoopAsync(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                // await PlayTetrisAsync(cancellationToken).ConfigureAwait(false);
            }

            return Task.FromResult(0);
        }

        private static async Task PlayTetrisAsync(CancellationToken cancellationToken)
        {
            Console.Beep(658, 125);
            Console.Beep(1320, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 250);
            Console.Beep(1320, 125);
            Console.Beep(1188, 125);
            Console.Beep(1056, 250);
            Console.Beep(990, 250);
            Console.Beep(880, 500);
            Console.Beep(880, 250);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 750);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            await Task.Delay(250, cancellationToken).ConfigureAwait(false);
            Console.Beep(1188, 500);
            Console.Beep(1408, 250);
            Console.Beep(1760, 500);
            Console.Beep(1584, 250);
            Console.Beep(1408, 250);
            Console.Beep(1320, 750);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            await Task.Delay(500, cancellationToken).ConfigureAwait(false);
            Console.Beep(1320, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 250);
            Console.Beep(1320, 125);
            Console.Beep(1188, 125);
            Console.Beep(1056, 250);
            Console.Beep(990, 250);
            Console.Beep(880, 500);
            Console.Beep(880, 250);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 750);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            await Task.Delay(250, cancellationToken).ConfigureAwait(false);
            Console.Beep(1188, 500);
            Console.Beep(1408, 250);
            Console.Beep(1760, 500);
            Console.Beep(1584, 250);
            Console.Beep(1408, 250);
            Console.Beep(1320, 750);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            await Task.Delay(500, cancellationToken).ConfigureAwait(false);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 1000);
            Console.Beep(440, 1000);
            Console.Beep(419, 1000);
            Console.Beep(495, 1000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 500);
            Console.Beep(660, 500);
            Console.Beep(880, 1000);
            Console.Beep(838, 2000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 1000);
            Console.Beep(440, 1000);
            Console.Beep(419, 1000);
            Console.Beep(495, 1000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 500);
            Console.Beep(660, 500);
            Console.Beep(880, 1000);
            Console.Beep(838, 2000);
        }
    }
}