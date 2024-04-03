using Cmd.Application.Common.Commands;
using Cmd.Application.Convertors;
using Cmd.Application.Tools;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.Create
{
    public class CreateNewsCommandHandler : ICommandHandler<CreateNewsCommand>
    {
        private readonly INewsRepository _repository;

        public CreateNewsCommandHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            List<string> imageNames = new List<string>
            {
                Guid.NewGuid().ToString()+Path.GetExtension(request.MainImage.FileName),
                Guid.NewGuid().ToString()+Path.GetExtension(request.SecondImage.FileName),
                Guid.NewGuid().ToString()+Path.GetExtension(request.ThirdImage.FileName),
            };

            FileTools.SaveImage(request.MainImage, imageNames[0], "News", false);
            FileTools.SaveImage(request.SecondImage, imageNames[1], "News", false);
            FileTools.SaveImage(request.ThirdImage, imageNames[2], "News", false);

            _repository.Add(Cms.Domain.Models.News.Entities.News.Create(request.Title,request.Introduction, request.LanguageId, request.NewsTypeId, request.PublishDate.ToShamsi(), request.FirstParagraph, request.SeconodParagraph, request.ThirdParagraph, imageNames[0], imageNames[1], imageNames[2] ));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
