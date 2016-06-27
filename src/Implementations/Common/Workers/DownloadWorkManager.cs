namespace Common.Workers
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Data.Entities;
    using Common.Data.Entities.Storage;

    using HtmlAgilityPack;

    using NativeCode.Core.Extensions;
    using NativeCode.Core.Logging;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Types;

    using Newtonsoft.Json;

    public class DownloadWorkManager : WorkManager<Download>
    {
        private static readonly Regex RegexFlashvars = new Regex(@"var flashvars = (.*);", RegexOptions.Compiled | RegexOptions.Multiline);

        public DownloadWorkManager(IApplication application, ILogger logger, IWorkProvider<Download> work)
            : base(application, logger, work)
        {
        }

        protected override async Task ExecuteWorkAsync(Download entity, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(entity.Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();

                    var content = response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var length = response.Content.Headers.ContentLength.GetValueOrDefault();

                    using (var stream = (await content).Monitor(length))
                    {
                        stream.StreamRead += HandleStreamRead;

                        try
                        {
                            var filename = response.Content.Headers.ContentDisposition?.FileName;

                            if (string.IsNullOrWhiteSpace(filename))
                            {
                                filename = entity.Filename;
                            }

                            var path = Path.Combine(entity.Storage.Path, filename);
                            this.Logger.Debug(path);

                            using (var filestream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Write))
                            {
                                await stream.CopyToAsync(filestream, 4096, cancellationToken).ConfigureAwait(false);
                            }
                        }
                        finally
                        {
                            stream.StreamRead -= HandleStreamRead;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Logger.Exception(ex);
                    throw;
                }
            }
        }

        protected override Task ResumeWorkAsync(Download entity, CancellationToken cancellationToken)
        {
            return this.RetryWorkAsync(entity, cancellationToken);
        }

        protected override async Task RetryWorkAsync(Download entity, CancellationToken cancellationToken)
        {
            // TODO: Need to abstract this into some of of URL fixer.
            if (!string.IsNullOrWhiteSpace(entity.Source) && entity.Source.Contains("tube8.com"))
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync(entity.Source, cancellationToken).ConfigureAwait(false);

                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        var document = new HtmlDocument();
                        document.LoadHtml(content);

                        var scripts = document.QuerySelectorAll("#playerContainer script");

                        foreach (var script in scripts)
                        {
                            if (RegexFlashvars.IsMatch(script.InnerText))
                            {
                                var json = RegexFlashvars.Match(script.InnerText).Groups[1].Value;
                                var flashvars = JsonConvert.DeserializeObject<Flashvars>(json);

                                entity.Url = GetBestQuality(flashvars);
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Exception(ex);
                    }
                }
            }

            await this.ExecuteWorkAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        private static string GetBestQuality(Flashvars flashvars)
        {
            if (IsValidQuality(flashvars.Quality1080))
            {
                return flashvars.Quality1080;
            }

            if (IsValidQuality(flashvars.Quality720))
            {
                return flashvars.Quality720;
            }

            if (IsValidQuality(flashvars.Quality480))
            {
                return flashvars.Quality480;
            }

            if (IsValidQuality(flashvars.Quality240))
            {
                return flashvars.Quality240;
            }

            return flashvars.Quality180;
        }

        private static bool IsValidQuality(string quality)
        {
            if (string.IsNullOrWhiteSpace(quality))
            {
                return false;
            }

            if (quality.ToUpper() == "FALSE")
            {
                return false;
            }

            return true;
        }

        private static void HandleStreamRead(object sender, StreamMonitorEventArgs e)
        {
        }

        public class Flashvars
        {
            [JsonProperty("quality_148p")]
            public string Quality180 { get; set; }

            [JsonProperty("quality_240p")]
            public string Quality240 { get; set; }

            [JsonProperty("quality_480p")]
            public string Quality480 { get; set; }

            [JsonProperty("quality_720p")]
            public string Quality720 { get; set; }

            [JsonProperty("quality_1080p")]
            public string Quality1080 { get; set; }
        }
    }
}