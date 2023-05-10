using TicketsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketsManagement.Application.Contracts.Persistence
{
    public interface IOrderRepository: IAsyncRepository<Order>
    {
        
    }
}
