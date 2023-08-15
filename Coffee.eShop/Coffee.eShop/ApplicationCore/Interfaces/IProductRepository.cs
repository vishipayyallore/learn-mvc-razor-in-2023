﻿using Coffee.eShop.Models;

namespace Coffee.eShop.ApplicationCore.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();

    IEnumerable<Product> GetTrendingProducts();

    Product? GetProductDetail(int id);
}
