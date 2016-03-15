namespace Common.DataServices
{
    public class DownloadStats
    {
        public int Claimed { get; set; }

        public int Completed { get; set; }

        public int Downloading { get; set; }

        public int Failed { get; set; }

        public int Queued { get; set; }

        public int Retrying { get; set; }

        public int Total { get; set; }
    }
}