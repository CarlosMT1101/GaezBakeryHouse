using Acr.UserDialogs;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views.CategorySelectedPageFolder;
using GaezBakeryHouse.App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using GaezBakeryHouse.App.Views.ProductDetailPageFolder;
using GaezBakeryHouse.App.Interfaces;
using FFImageLoading.Forms;
using Xamarin.CommunityToolkit.Effects;

namespace GaezBakeryHouse.App.ViewModels
{
    public class CategorySelectedViewModel : BaseViewModel, IQueryAttributable, IRefresh
    {
        #region ATRIBUTES
        readonly ProductService _productService;
        StackLayout _leftStackLayout;
        StackLayout _rightStackLayout;
        int _categoryId;
        #endregion
        #region PROPERTIES
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }
        public AwesomeObservableCollection<ProductModel> ProductsList { get; private set; }
        #endregion
        #region COMMANDS
        #endregion
        #region CONSTRUCTOR
        public CategorySelectedViewModel(StackLayout leftStackLayout, StackLayout rightStackLayout)
        {
            _leftStackLayout = leftStackLayout;
            _rightStackLayout = rightStackLayout;

            _productService = new ProductService();
            ProductsList = new AwesomeObservableCollection<ProductModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);
        }
        #endregion
        #region FUNCTIONS
        private async Task LoadProducts()
        {
            var products = await _productService.GetProductsByCategory(CategoryId);

            _leftStackLayout.Children.Clear();
            _rightStackLayout.Children.Clear();

            ProductsList.ClearRange();
            ProductsList.AddRange(products);

            for (int i = 0; i < ProductsList.Count; i++)
            {
                if ((i + 1) % 2 == 0)
                    _rightStackLayout.Children.Add(DrawProducts(ProductsList[i]));
                else
                    _leftStackLayout.Children.Add(DrawProducts(ProductsList[i]));
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
                 await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}/{nameof(CategorySelectedPage)}/{nameof(ProductDetailPage)}?id={productModel.Id}");

            frame.GestureRecognizers.Add(tapGestureRecognizer);

            frame.Content = grid;

            return frame;
        }
        #endregion
        #region IQueryAttributable
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            CategoryId = int.Parse(HttpUtility.UrlDecode(query["id"]));
            Title = HttpUtility.UrlDecode(query["name"]);
        }
        #endregion
        #region IRefresh
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;

            await LoadProducts();

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}
