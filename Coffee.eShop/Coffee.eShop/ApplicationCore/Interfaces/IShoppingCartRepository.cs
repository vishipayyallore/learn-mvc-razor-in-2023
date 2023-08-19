using Coffee.eShop.Models;

namespace Coffee.eShop.ApplicationCore.Interfaces;

public interface IShoppingCartRepository
{
    public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

    void AddToCart(Product product);

    int RemoveFromCart(Product product);

    List<ShoppingCartItem> GetShoppingCartItems();

    void ClearCart();

    decimal GetShoppingCartTotal();
}
