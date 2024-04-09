using Cmd.Application.Common.Commands;
using Cms.Domain.Models.News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Models.News.Commands.Delete
{
    public class DeleteNewsCommandHandler : ICommandHandler<DeleteNewsCommand>
    {
        private readonly INewsRepository _repository;

        public DeleteNewsCommandHandler(INewsRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
