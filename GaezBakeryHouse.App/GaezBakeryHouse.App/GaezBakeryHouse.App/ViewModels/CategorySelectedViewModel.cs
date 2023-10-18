using Acr.UserDialogs;
using FFImageLoading.Forms;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class CategorySelectedViewModel : BaseViewModel, IQueryAttributable
    {
        #region Attributes
        private readonly IProductService _productService;
        private StackLayout _leftStackLayout;
        private StackLayout _rightStackLayout;
        int _categoryId;
        #endregion

        #region Properties
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        public AwesomeObservableCollection<ProductModel> Products { get; private set; }
        #endregion

        #region Commands

        #endregion

        #region Constructor
        public CategorySelectedViewModel(
            StackLayout leftStackLayout, 
            StackLayout rightStackLayout)
        {
            _leftStackLayout = leftStackLayout;
            _rightStackLayout = rightStackLayout;

            _productService = RestService.For<IProductService>(Constants.Url);
            Products = new AwesomeObservableCollection<ProductModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);
        }
        #endregion

        #region Functions
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            CategoryId = int.Parse(HttpUtility.UrlDecode(query["id"]));
            Title = HttpUtility.UrlDecode(query["name"]);
        }

        private async Task LoadProducts()
        {
            var products = await _productService.GetProductsByCategory(AccessToken, CategoryId);

            _leftStackLayout.Children.Clear();
            _rightStackLayout.Children.Clear();

            Products.ClearRange();
            Products.AddRange(products);

            LoadProductImages();
            DrawProductsInLayouts();
        }

        private void LoadProductImages()
        {
            foreach (var product in Products)
            {
                product.ImageSource = ImageSource.FromStream(() => new MemoryStream(product.ProductImage));
            }

        }

        private void DrawProductsInLayouts()
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if ((i + 1) % 2 == 0)
                    _rightStackLayout.Children.Add(DrawProducts(Products[i]));
                else
                    _leftStackLayout.Children.Add(DrawProducts(Products[i]));
            }
        }

        Frame DrawProducts(ProductModel productModel)
        {
            var frame = new Frame
            {
                CornerRadius = 10,
                HasShadow = false,
                Padding = 10,
                HeightRequest = 200,
            };

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            var cachedImage = new CachedImage
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = productModel.ImageSource
            };

            var stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.EndAndExpand
            };

            var nameLabel = new Label
            {
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Text = productModel.Name
            };

            var priceLabel = new Label
            {
                TextColor = (Color)Application.Current.Resources["PrimaryColor"],
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span { Text = "$ " },
                        new Span { Text = productModel.Price.ToString() }
                    }
                }
            };

            stackLayout.Children.Add(nameLabel);
            stackLayout.Children.Add(priceLabel);

            Grid.SetRow(cachedImage, 0);
            Grid.SetRow(stackLayout, 1);

            grid.Children.Add(cachedImage);
            grid.Children.Add(stackLayout);

            TouchEffect.SetNativeAnimation(frame, true);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.NumberOfTapsRequired = 1;

            tapGestureRecognizer.Tapped += async (object sender, EventArgs e) =>
                 await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={productModel.Id}");

            frame.GestureRecognizers.Add(tapGestureRecognizer);

            frame.Content = grid;

            return frame;
        }

        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading(Constants.LoadingMessage);
            OnLoadingTask();

            try
            {
                await LoadProducts();
                OnSuccessTask();
            }
            catch (Exception)
            {
                OnErrorTask();
            }

            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
