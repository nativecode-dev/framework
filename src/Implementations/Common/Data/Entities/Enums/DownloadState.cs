namespace Common.Data.Entities.Enums
{
    public enum DownloadState
    {
        Default = 0,

        Claimed = 1,

        Completed = 2,

        Downloading = 3,

        Failed = 4,

        Retry = 5,

        Queued = Default
    }
}