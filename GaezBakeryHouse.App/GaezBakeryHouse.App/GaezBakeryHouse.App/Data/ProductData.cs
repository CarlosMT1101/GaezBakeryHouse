using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.ViewModels;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GaezBakeryHouse.App.Data
{
    public static class ProductData
    {
        public static IList<ProductModel> Products { get; private set; }
        static readonly IProductService _productService;

        static ProductData()
        {
            Products = new List<ProductModel>();
            _productService = RestService.For<IProductService>(Constants.Url);
        }

        public static async Task LoadData()
        {
            var accessToken = SecureStorage.GetAsync(Constants.AccessToken).Result;

            if(Products.Count == 0)
            {
                Products.Clear();
                ((List<ProductModel>)Products).AddRange(await _productService.GetAllProducts(accessToken));
            }
        }
    }
}