namespace Common.Web.Models.Downloads
{
    using Common.Data.Entities;

    public class DownloadInfo
    {
        public string Filename { get; set; }

        public string Path { get; set; }

        public string Source { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public static DownloadInfo From(Download download)
        {
            return new DownloadInfo { Filename = download.Filename, Path = download.Path, Source = download.Source, Title = download.Title, Url = download.Url };
        }

        public void To(Download download)
        {
            download.Filename = this.Filename;
            download.Path = this.Path;
            download.Source = this.Source;
            download.Title = this.Title;
            download.Url = this.Url;
        }
    }
}