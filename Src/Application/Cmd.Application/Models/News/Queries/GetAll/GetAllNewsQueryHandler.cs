﻿using Cmd.Application.Common.Queries;
using Cmd.Application.Convertors;
using Cmd.Application.Models.News.Queries.Common;
using Cms.Domain.Models.News.Entities;
using Cms.Domain.Models.News.Repository;

namespace Cmd.Application.Models.News.Queries.GetAll;

public class GetAllNewsQueryHandler : IQueryHandler<GetAllNewsQuery, PagedData<NewsViewModel>>
{
    private readonly INewsRepository _repository;
    private readonly INewsTypeRepository _newsTypeRepository;

    public GetAllNewsQueryHandler(INewsRepository repository, INewsTypeRepository newsTypeRepository)
    {
        _repository = repository;
        _newsTypeRepository = newsTypeRepository;
    }

    public Task<PagedData<NewsViewModel>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
    {
        var newsList = _repository.GetAllWithRelations().OrderByDescending(t => t.CreateAt).ToList();

        if (request.TypeId is not null && request.TypeId is not 0)
        {
            newsList = newsList.Where(t => t.NewsTypeId == request.TypeId).ToList();
        }
        if (request.LanguageId is not null && request.LanguageId is not 0)
        {
            newsList = newsList.Where(t => t.LanguageId == request.LanguageId).ToList();
        }
        newsList = newsList.Where(t => t.NewsType.IsPage == request.IsPage).ToList();
        int totalCount = newsList.Count;
        newsList = newsList.Skip(request.SkipCount).Take(request.PageSize).ToList();

        var page = new PagedData<NewsViewModel>();

        page.QueryResult = newsList.Select(t => new NewsViewModel
        {
            Id = t.Id,
            Title = t.Title.Value,
            Introduction = t.Introduction.Value,
            LanguageId = t.LanguageId,
            NewsTypeId = t.NewsTypeId,
            PublishDate = DateTime.Parse(t.PublishDate).ToShamsi(),
            Text = t.Text,
            MainImageName = t.MainImageName.Value ?? "",
            SecondImage = t.SecondImage?.Value ?? "",
            ThirdImage = t.ThirdImage?.Value ?? "",
            ThumbNailImage = t.ThumbNailImage?.Value ?? "",
            IsEnable = t.IsEnable,
            Author = t.Author,
            Galleries = new List<Gallery.Queries.Common.GalleryViewModel>(),
            NewsType = new NewsTypeViewModel
            {
                Id = t.NewsType.Id,
                IsPage = t.NewsType.IsPage,
                IsEnable = t.NewsType.IsEnable,
                Name = t.NewsType.Name.Value,
                LanguageId = (int)t.NewsType.LanguageId,
                Title = t.NewsType.Title.Value,
            }
        }).ToList();
        page.PageSize = request.PageSize;
        page.PageNumber = request.PageNumber;

        page.TotalCount = totalCount;


        return Task.FromResult(page);
    }
}
