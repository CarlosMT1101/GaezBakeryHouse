using GaezBakeryHouse.App.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.Services
{
    public interface IShoppingCartItemService
    {
        [Post("/shoppingCartItem/PostShoppingCartItem")]
        Task<HttpResponseMessage> PostShoppingCartItem(
            [Header("Authorization")] string authorization, 
            [Body] ShoppingCartItemModel shoppingCartItem);

        [Get("/shoppingCartItem/GetShoppingCartItemsByUserIdQuery/{userId}")]
        Task<HttpResponseMessage> GetShoppingCartItemsByUserId(
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

        public async Task<IEnumerable<ShoppingCartItemModel>> GetShoppingCartItemsByUserId()
        {
            try
            {
                var accessToken = SecureStorage.GetAsync("AccessToken").Result;
                var applicationUserId = SecureStorage.GetAsync("ApplicationUserId").Result;

                var response = await _service.GetShoppingCartItemsByUserId(accessToken, applicationUserId);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var shoppingItems = JsonConvert.DeserializeObject<IEnumerable<ShoppingCartItemModel>>(responseContent);


                    foreach (var item in shoppingItems)
                    {
                        item.ImageSource = ImageSource.FromStream(() => new MemoryStream(item.ProductImage));
                    }

                    return shoppingItems;
                }
                else
                {
                    return new List<ShoppingCartItemModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<ShoppingCartItemModel>();
            }
        }

        public async Task<bool> DeleteShoppingCartItem(int id, int productId)
        {
            try
            {
                var accessToken = SecureStorage.GetAsync("AccessToken").Result;
                var applicationUserId = SecureStorage.GetAsync("ApplicationUserId").Result;

                var response = await _service.DeleteShoppingCartItem(accessToken, id, productId, applicationUserId);

                if(response.IsSuccessStatusCode)
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

        public async Task<bool> DeleteAllShoppingCartItemsByUserId()
        {
            try
            {
                var accessToken = SecureStorage.GetAsync("AccessToken").Result;
                var applicationUserId = SecureStorage.GetAsync("ApplicationUserId").Result;

                var response = await _service.DeleteAllShoppingCartItemsByUserId(accessToken, applicationUserId);

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
