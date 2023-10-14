using GaezBakeryHouse.App.Models;
using Refit;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Services
{
    public interface IShoppingCartItemService
    {
        [Post("/shoppingCartItem/PostShoppingCartItem")]
        Task<HttpResponseMessage> PostShoppingCartItem(
            [Header("Authorization")] string authorization,
            [Body] ShoppingCartItemModel shoppingCartItem);

        [Get("/shoppingCartItem/GetShoppingCartItemsByUserIdQuery/{userId}")]
        Task<IEnumerable<ShoppingCartItemModel>> GetShoppingCartItemsByUserId(
            [Header("Authorization")] string authorization,
            [AliasAs("userId")] string userId);

        [Delete("/shoppingCartItem/DeleteShoppingCartItem/{id}/{productId}/{applicationUserId}")]
        Task<HttpResponseMessage> DeleteShoppingCartItem(
            [Header("Authorization")] string authorization,
            [AliasAs("id")] int id,
            [AliasAs("productId")] int productId,
            [AliasAs("applicationUserId")] string applicationUserId);

        [Delete("/shoppingCartItem/DeleteAllShoppingCartItemsByUserId/{applicationUserId}")]
        Task<HttpResponseMessage> DeleteAllShoppingCartItemsByUserId(
            [Header("Authorization")] string authorization,
            [AliasAs("applicationUserId")] string applicationUserId);


        [Get("/shoppingCartItem/GetUserTotalAmount/{applicationUserId}")]
        Task<HttpResponseMessage> GetUserTotalAmount(
            [Header("Authorization")] string authorization,
            [AliasAs("applicationUserId")] string applicationUserId);
    }
}
