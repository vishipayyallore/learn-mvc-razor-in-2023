﻿using Coffee.eShop.ApplicationCore.Interfaces;
using Coffee.eShop.Data;
using Coffee.eShop.Models;

namespace Coffee.eShop.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly CoffeeShopDbContext _coffeeShopDbContext;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public OrderRepository(CoffeeShopDbContext coffeeShopDbContext, IShoppingCartRepository shoppingCartRepository)
    {
        _coffeeShopDbContext = coffeeShopDbContext ?? throw new ArgumentNullException(nameof(coffeeShopDbContext));

        _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
    }

    public void PlaceOrder(Order order)
    {
        order.OrderDetails = new();

        var shoppingCartItems = _shoppingCartRepository.GetShoppingCartItems();

        foreach (var item in shoppingCartItems)
        {
            var orderDetail = new OrderDetail
            {
                Quantity = item.Qty,
                ProductId = item.Product is not null ? item.Product.Id : 0,
                Price = item.Product!.Price
            };
            order.OrderDetails.Add(orderDetail);
        }

        order.OrderPlaced = DateTime.Now;
        order.OrderTotal = _shoppingCartRepository.GetShoppingCartTotal();

        _coffeeShopDbContext.Orders.Add(order);
        _coffeeShopDbContext.SaveChanges();
    }

}
