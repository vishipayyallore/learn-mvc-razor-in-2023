using AutoMapper;
using MediatR;
using TicketsManagement.Application.Contracts.Persistence;
using TicketsManagement.Application.Exceptions;
using TicketsManagement.Domain.Entities;

namespace TicketsManagement.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IMapper mapper, IGenericRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            var eventToUpdate = await _eventRepository.GetById(request.EventId);
            if (eventToUpdate == null)
            {
                throw new NotFoundException(nameof(Event), request.EventId);
            }

            var validator = new UpdateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));

            await _eventRepository.Update(eventToUpdate);

            return Unit.Value;
        }

        Task IRequestHandler<UpdateEventCommand>.Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}