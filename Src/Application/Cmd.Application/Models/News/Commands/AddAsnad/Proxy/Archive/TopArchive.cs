
public class FileContent
{
    public int PkFileContent { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string ContentName { get; set; }
    public string FileTypeFa { get; set; }
    public string NameInSystem { get; set; }
    public double FileSizeByte { get; set; }
    public string FileSize { get; set; }
    public string CreateDateTime { get; set; }
    public int Visit { get; set; }
    public int Download { get; set; }
    public string PosterPath { get; set; }
    public string PhotoSize { get; set; }
    public string ThumbnailLink { get; set; }
    public string LowResLink { get; set; }
    public string DownloadLink { get; set; }
}

public class TopArchive
{
    public List<FileContent> VideoFile { get; set; }
    public List<object> AudioFile { get; set; }
}

