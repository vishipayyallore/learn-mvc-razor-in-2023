﻿using AutoMapper;
using TicketsManagement.Application.Features.Categories.Commands.CreateCateogry;
using TicketsManagement.Application.Features.Categories.Queries.GetCategoriesList;
using TicketsManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using TicketsManagement.Application.Features.Events.Commands.CreateEvent;
using TicketsManagement.Application.Features.Events.Commands.UpdateEvent;
using TicketsManagement.Application.Features.Events.Queries.GetEventDetail;
using TicketsManagement.Application.Features.Events.Queries.GetEventsList;
using TicketsManagement.Application.Features.Orders.GetOrdersForMonth;
using TicketsManagement.Domain.Entities;

namespace TicketsManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();

            CreateMap<Order, OrdersForMonthDto>();
        }
    }
}
