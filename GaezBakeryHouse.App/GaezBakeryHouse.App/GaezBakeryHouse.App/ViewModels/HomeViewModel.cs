using Acr.UserDialogs;
using FFImageLoading.Forms;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Interfaces;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using GaezBakeryHouse.App.Views.CategorySelectedPageFolder;
using GaezBakeryHouse.App.Views.ProductDetailPageFolder;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class HomeViewModel : BaseViewModel, IRefresh
    {
        #region ATRIBUTES
        readonly CategoryService _categoryService;
        readonly OffertService _offertService;
        readonly ProductService _productService;
        StackLayout _leftStackLayout; // StackLayout Izquierdo
        StackLayout _rightStackLayout; // StackLayout Derecho
        #endregion
        #region PROPERTIES
        public ICommand OnCategoryClicked { get; private set; }
        public ICommand OnProductClicked { get; private set; }
        public AwesomeObservableCollection<CategoryModel> CategoriesList { get; private set; }
        public AwesomeObservableCollection<ProductModel> TrendingProductsList { get; private set; }
        public AwesomeObservableCollection<OffertModel> Banners { get; private set; }
        #endregion
        #region CONSTRUCTOR
        public HomeViewModel(StackLayout leftStackLayout, StackLayout rightStackLayout)
        {
            _leftStackLayout = leftStackLayout;
            _rightStackLayout = rightStackLayout;

            _categoryService = new CategoryService();
            _offertService = new OffertService();
            _productService = new ProductService();

            CategoriesList = new AwesomeObservableCollection<CategoryModel>();
            TrendingProductsList = new AwesomeObservableCollection<ProductModel>();
            Banners = new AwesomeObservableCollection<OffertModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);

            OnCategoryClicked = new Command<CategoryModel>(
                execute: async (e) => await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}/{nameof(CategorySelectedPage)}?id={e.Id}&name={e.Name}"));

            OnProductClicked = new Command<ProductModel>(
                execute: (e) =>
                {

                });
        }
        #endregion
        #region FUNCTIONS
        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategories();

            CategoriesList.ClearRange();
            CategoriesList.AddRange(categories);
        }
        private void LoadBanners()
        {
            if(Banners.Count == 0)
            {
                Banners.AddRange(_offertService.GetBanners());
            }
        }
        private async Task LoadTrendingProducts()
        {
            var products = await _productService.GetTrendingProducts();

            _leftStackLayout.Children.Clear();
            _rightStackLayout.Children.Clear();

            TrendingProductsList.ClearRange();
            TrendingProductsList.AddRange(products);

            for(int i = 0; i < TrendingProductsList.Count; i++)
            {
                if((i + 1) % 2 == 0)
                    _rightStackLayout.Children.Add(DrawTrendingProductCart(TrendingProductsList[i]));
                else
                    _leftStackLayout.Children.Add(DrawTrendingProductCart(TrendingProductsList[i]));
            }
        }
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;

            await LoadCategoriesAsync();
            await LoadTrendingProducts();
            LoadBanners();

            CurrentState = LayoutState.Success;
            UserDialogs.Instance.HideLoading();
        }
        Frame DrawTrendingProductCart(ProductModel productModel)
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
                await Shell.Current.GoToAsync($"//Start/{nameof(HomePage)}/{nameof(ProductDetailPage)}?id={productModel.Id}");

            frame.GestureRecognizers.Add(tapGestureRecognizer);

            frame.Content = grid;

            return frame;
        }
        #endregion
    }
}
