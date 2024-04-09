using Cmd.Application.Common.Commands;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.DeleteNewsType
{
    public class DeleteNewsTypeCommandHandler:ICommandHandler<DeleteNewsTypeCommand>
    {
        private readonly INewsTypeRepository _repository;

        public DeleteNewsTypeCommandHandler(INewsTypeRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DeleteNewsTypeCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
