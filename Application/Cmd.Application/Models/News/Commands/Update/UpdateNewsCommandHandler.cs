using Cmd.Application.Common.Commands;
using Cmd.Application.Convertors;
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
            var news = Cms.Domain.Models.News.Entities.News.Create(request.Title,request.Introduction, request.LanguageId, request.NewsTypeId, request.PublishDate.ToShamsi(), request.FirstParagraph, request.SeconodParagraph, request.ThirdParagraph, request.MainImageName, request.SeconodParagraph, request.ThirdParagraph);
            news.SetId(request.Id);

            _repository.Update(news);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
