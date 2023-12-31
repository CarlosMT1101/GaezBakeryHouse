﻿using GaezBakeryHouse.App.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Services
{
    public interface IProductService
    {
        [Get("/products/GetTrendingProducts")]
        Task<IEnumerable<ProductModel>> GetTrendingProducts([Header("Authorization")] string authorization);

        [Get("/products/GetProductsByCategory/{id}")]
        Task<IEnumerable<ProductModel>> GetProductsByCategory(
            [Header("Authorization")] string authorization, 
            [AliasAs("id")] int id);

        [Get("/products/GetProductById/{id}")]
        Task<ProductModel> GetProductById(
            [Header("Authorization")] string authorization, 
            [AliasAs("id")] int id);

        [Get("/products/GetAllProducts")]
        Task<IEnumerable<ProductModel>> GetAllProducts([Header("Authorization")] string authorization);
    }
}
