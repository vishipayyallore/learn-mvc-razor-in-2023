using MediatR;

namespace TicketsManagement.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQuery : IRequest<List<EventListVm>>
    {

    }
}
