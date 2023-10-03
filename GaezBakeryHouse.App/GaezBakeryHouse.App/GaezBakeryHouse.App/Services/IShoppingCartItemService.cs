using GaezBakeryHouse.App.Models;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GaezBakeryHouse.App.Services
{
    public interface IShoppingCartItemService
    {
        [Post("/shoppingCartItem/PostShoppingCartItem")]
        Task<HttpResponseMessage> PostShoppingCartItem(
            [Header("Authorization")] string authorization, 
            [Body] ShoppingCartItemModel shoppingCartItem);
    }

    public class ShoppingService
    {
        readonly IShoppingCartItemService _service;

        public ShoppingService() =>
            _service = RestService.For<IShoppingCartItemService>(Constants.Url);

        public async Task<bool> PostShoppingCartItem(ShoppingCartItemModel shoppingCartItem)
        {
            try
            {
                var accessToken = SecureStorage.GetAsync("AccessToken").Result;
                var response = await _service.PostShoppingCartItem(accessToken, shoppingCartItem);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
