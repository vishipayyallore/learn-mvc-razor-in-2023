﻿using MediatR;

namespace TicketsManagement.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<List<CategoryListVm>>
    {
    }
}
