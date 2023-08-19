using Coffee.eShop.ApplicationCore.Interfaces;
using Coffee.eShop.Data;
using Coffee.eShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Coffee.eShop.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly CoffeeShopDbContext _coffeeShopDbContext;

    public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

    public string? ShoppingCartId { get; set; }

    public ShoppingCartRepository(CoffeeShopDbContext coffeeShopDbContext)
    {
        _coffeeShopDbContext = coffeeShopDbContext ?? throw new ArgumentNullException(nameof(coffeeShopDbContext));
    }

    public static ShoppingCartRepository GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        CoffeeShopDbContext context = services.GetService<CoffeeShopDbContext>() ?? throw new Exception("Error initializing coffeeshopdbcontext");

        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session?.SetString("CartId", cartId);

        return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
    }

    public void AddToCart(Product product)
    {
        var shoppingCartItem = _coffeeShopDbContext.ShoppingCartItems
            .FirstOrDefault(s => s.Product!.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem is null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Product = product,
                Qty = 1
            };

            _coffeeShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Qty++;
        }

        _coffeeShopDbContext.SaveChanges();
    }

    public void ClearCart()
    {
        var cartItems = _coffeeShopDbContext.ShoppingCartItems
                        .Where(s => s.ShoppingCartId == ShoppingCartId);

        _coffeeShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

        _coffeeShopDbContext.SaveChanges();
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??= _coffeeShopDbContext.ShoppingCartItems
            .Where(s => s.ShoppingCartId == ShoppingCartId).Include(p => p.Product).ToList();
    }

    public decimal GetShoppingCartTotal()
    {
        var totalCost = _coffeeShopDbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(s => s.Product!.Price * s.Qty).Sum();

        return totalCost;
    }

    public int RemoveFromCart(Product product)
    {
        var quantity = 0;

        var shoppingCartItem = _coffeeShopDbContext.ShoppingCartItems
                .FirstOrDefault(s => s.Product!.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem is not null)
        {
            if (shoppingCartItem.Qty > 1)
            {
                shoppingCartItem.Qty--;
                quantity = shoppingCartItem.Qty;
            }
            else
            {
                _coffeeShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        _coffeeShopDbContext.SaveChanges();

        return quantity;
    }

}