namespace Cms.Endpoints.Site.Proxy.Asnad
{
    public class Attachment
    {
        public int Id { get; set; }
        public string AttachmentGroup { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        public string Extension { get; set; }
        public string AttachedOn { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string FilePath { get; set; }
        public string MainFilePath { get; set; }
        public string ThumnailPath { get; set; }
        public int DownloadsCount { get; set; }
        public int AttachmentCount { get; set; }
    }

}
