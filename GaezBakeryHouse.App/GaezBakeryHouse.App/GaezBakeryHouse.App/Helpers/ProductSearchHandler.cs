using GaezBakeryHouse.App.Data;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.ViewModels;
using GaezBakeryHouse.App.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.Helpers
{
    public class ProductSearchHandler : SearchHandler
    {
        IList<ProductModel> searchResults;

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                searchResults = ProductData.Products
                    .Where(p => p.Name
                    .ToLower()
                    .Contains(newValue.ToLower()))
                    .ToList();

                ItemsSource = searchResults;
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            var product = (ProductModel)item;
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={product.Id}");
        }

    }
}
