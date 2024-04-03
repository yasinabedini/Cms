using Cmd.Application.Common.Commands;
using Cmd.Application.Convertors;
using Cmd.Application.Tools;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.Update
{
    public class UpdateNewsCommandHandler : ICommandHandler<UpdateNewsCommand>
    {
        private readonly INewsRepository _repository;

        public UpdateNewsCommandHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var newsImageNames = _repository.GetById(request.Id);

            List<string> imageNames = new List<string>
            {
                newsImageNames.MainImageName.Value,
                newsImageNames.SecondImage.Value ?? "",
                newsImageNames.ThirdImage.Value ?? ""
            };


            if (request.MainImage is not null) { FileTools.DeleteFile("News", imageNames[0]); FileTools.SaveImage(request.MainImage, imageNames[0], "News", false); }

            if (request.SecondImage is not null)
            {
                if (!string.IsNullOrEmpty(imageNames[1]))
                {
                    FileTools.DeleteFile("News", imageNames[1]);
                }
                else
                {
                    imageNames[1] = Guid.NewGuid().ToString() + Path.GetExtension(request.SecondImage.FileName);
                }
                FileTools.SaveImage(request.SecondImage, imageNames[1], "News", false);
            }

            if (request.ThirdImage is not null)
            {
                if (!string.IsNullOrEmpty(imageNames[2]))
                {
                    FileTools.DeleteFile("News", imageNames[2]);
                }
                else
                {
                    imageNames[2] = Guid.NewGuid().ToString() + Path.GetExtension(request.ThirdImage.FileName);
                }
                FileTools.SaveImage(request.ThirdImage, imageNames[2], "News", false);
            }


            var news = Cms.Domain.Models.News.Entities.News.Create(request.Title, request.Introduction, request.LanguageId, request.NewsTypeId, request.PublishDate.ToShamsi(), request.FirstParagraph, request.SeconodParagraph, request.ThirdParagraph, imageNames[0], imageNames[1], imageNames[2]);
            news.SetId(request.Id);

            _repository.Update(news);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
