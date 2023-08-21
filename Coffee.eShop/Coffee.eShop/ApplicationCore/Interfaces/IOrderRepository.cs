using Coffee.eShop.Models;

namespace Coffee.eShop.ApplicationCore.Interfaces;

public interface IOrderRepository
{
    void PlaceOrder(Order order);
}
