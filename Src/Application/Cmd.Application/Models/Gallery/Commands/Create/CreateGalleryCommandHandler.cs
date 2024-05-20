using Cmd.Application.Common.Commands;
using Cms.Domain.Models.Gallery.Repositories;

namespace Cmd.Application.Models.Gallery.Commands.Create
{
    public class CreateGalleryCommandHandler : ICommandHandler<CreateGalleryCommand>
    {
        private readonly IGalleryRepository _repository;

        public CreateGalleryCommandHandler(IGalleryRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(CreateGalleryCommand request, CancellationToken cancellationToken)
        {
            _repository.Add(new Cms.Domain.Models.Gallery.Entities.Gallery(request?.Title, request.Type, request?.NewsId));
            _repository.Save();

            return Task.CompletedTask;
        }
    }
}
