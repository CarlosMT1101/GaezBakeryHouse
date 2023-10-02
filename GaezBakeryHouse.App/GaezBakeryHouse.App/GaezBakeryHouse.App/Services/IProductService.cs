using GaezBakeryHouse.App.Models;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.Services
{
    public interface IProductService
    {
        [Get("/products/GetTrendingProducts")]
        Task<HttpResponseMessage> GetTrendingProducts([Header("Authorization")] string authorization);

        [Get("/products/GetProductsByCategory/{id}")]
        Task<HttpResponseMessage> GetProductsByCategory([Header("Authorization")] string authorization, [AliasAs("id")] int id);

        [Get("/products/GetProductById/{id}")]
        Task<HttpResponseMessage> GetProductById([Header("Authorization")] string authorization, [AliasAs("id")] int id);
    }

    public class ProductService
    {
        readonly IProductService _productService;

        public ProductService() =>
            _productService = RestService.For<IProductService>(Constants.Url);

        public async Task<IEnumerable<ProductModel>>GetTrendingProducts()
        {
            try
            {
                var accessToken = SecureStorage.GetAsync("AccessToken").Result;
                var response = await _productService.GetTrendingProducts(accessToken);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(responseContent);


                    foreach (var product in products)
                    {
                        product.ImageSource = ImageSource.FromStream(() => new MemoryStream(product.ProductImage));
                    }

                    return products;
                }
                else
                {
                    return new List<ProductModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<ProductModel>();
            }
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByCategory(int id)
        {
            try
            {
                var accessToken = SecureStorage.GetAsync("AccessToken").Result;
                var response = await _productService.GetProductsByCategory(accessToken, id);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(responseContent);


                    foreach (var product in products)
                    {
                        product.ImageSource = ImageSource.FromStream(() => new MemoryStream(product.ProductImage));
                    }

                    return products;
                }
                else
                {
                    return new List<ProductModel>();
                }
            }
            catch (Exception ex)
            {
                return new List<ProductModel>();
            }
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            try
            {
                var accessToken = SecureStorage.GetAsync("AccessToken").Result;
                var response = await _productService.GetProductById(accessToken, id);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductModel>(responseContent);

                    product.ImageSource = ImageSource.FromStream(() => new MemoryStream(product.ProductImage));

                    return product;
                }
                else
                {
                    return new ProductModel();
                }
            }
            catch (Exception ex)
            {
                return new ProductModel();
            }
        }
    }
}
