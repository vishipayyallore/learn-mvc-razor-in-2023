using AutoMapper;
using MediatR;
using TicketsManagement.Application.Contracts.Persistence;
using TicketsManagement.Application.Exceptions;
using TicketsManagement.Domain.Entities;

namespace TicketsManagement.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVm>
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(IMapper mapper, IGenericRepository<Event> eventRepository, IGenericRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetById(request.Id);
            var eventDetailDto = _mapper.Map<EventDetailVm>(@event);

            var category = await _categoryRepository.GetById(@event!.CategoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }
            eventDetailDto.Category = _mapper.Map<CategoryDto>(category);

            return eventDetailDto;
        }
    }
}
