using Cmd.Application.Common.Commands;

namespace Cmd.Application.Models.Gallery.Commands.Create
{
    public class CreateGalleryCommand:ICommand
    {
        public string? Title { get;  set; }
        public bool Type { get;  set; }
        public long? NewsId { get;  set; }

        public CreateGalleryCommand()
        {
            
        }
        public CreateGalleryCommand(string? title, bool type, long? newsId)
        {
            Title = title;
            Type = type;
            NewsId = newsId;
        }
    }
}
