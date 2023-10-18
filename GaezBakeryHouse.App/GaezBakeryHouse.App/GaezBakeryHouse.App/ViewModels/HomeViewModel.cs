using Acr.UserDialogs;
using FFImageLoading.Forms;
using GaezBakeryHouse.App.Data;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using GaezBakeryHouse.App.Views;
using Refit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Attributes
        private readonly OffertService _offertService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private StackLayout _leftStackLayout;
        private StackLayout _rightStackLayout;
        #endregion

        #region Properties
        public AwesomeObservableCollection<CategoryModel> Categories { get; private set; }

        public AwesomeObservableCollection<ProductModel> TrendingProducts { get; private set; }

        public AwesomeObservableCollection<OffertModel> Banners { get; private set; }
        #endregion

        #region Commands
        public ICommand OnCategoryClickedCommand { get; private set; }
        #endregion

        #region Constructor
        public HomeViewModel(
            StackLayout leftStackLayout,
            StackLayout rightStackLayout)
        {
            _leftStackLayout = leftStackLayout;
            _rightStackLayout = rightStackLayout;

            _categoryService = RestService.For<ICategoryService>(Constants.Url);
            _productService = RestService.For<IProductService>(Constants.Url);
            _offertService = new OffertService();

            Categories = new AwesomeObservableCollection<CategoryModel>();
            TrendingProducts = new AwesomeObservableCollection<ProductModel>();
            Banners = new AwesomeObservableCollection<OffertModel>();


            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);

            OnCategoryClickedCommand = new Command<CategoryModel>(
                execute: async (e) => await Shell.Current.GoToAsync($"{nameof(CategorySelectedPage)}?id={e.Id}&name={e.Name}"));
        }
        #endregion

        #region Functions
        private void LoadBanners()
        {
            if (Banners.Count == 0)
            {
                Banners.AddRange(_offertService.GetBanners());
            }
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategories(AccessToken);
            categories = categories.OrderBy(x => x.Name);

            Categories.ClearRange();  
            Categories.AddRange(categories);

            LoadCategoryImages();
        }

        private void LoadCategoryImages()
        {
            foreach (var category in Categories)
            {
                category.ImageSource = ImageSource.FromStream(() => new MemoryStream(category.CategoryImage));
            }

        }

        private void LoadProductImages()
        {
            foreach (var product in TrendingProducts)
            {
                product.ImageSource = ImageSource.FromStream(() => new MemoryStream(product.ProductImage));
            }

        }

        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading(Constants.LoadingMessage);
            OnLoadingTask();

            try
            {
                await LoadCategoriesAsync();
                await LoadTrendingProducts();
                await ProductData.LoadData();

                LoadBanners();

                OnSuccessTask();
            }
            catch (Exception ex)
            {
                OnErrorTask();
            }

            UserDialogs.Instance.HideLoading();
        }

        private async Task LoadTrendingProducts()
        {
            var products = await _productService.GetTrendingProducts(AccessToken);

            _leftStackLayout.Children.Clear();
            _rightStackLayout.Children.Clear();

            TrendingProducts.ClearRange();
            TrendingProducts.AddRange(products);

            LoadProductImages();
            DrawProductsInLayouts();
        }

        private void DrawProductsInLayouts()
        {
            for (int i = 0; i < TrendingProducts.Count; i++)
            {
                if ((i + 1) % 2 == 0)
                    _rightStackLayout.Children.Add(DrawTrendingProductCart(TrendingProducts[i]));
                else
                    _leftStackLayout.Children.Add(DrawTrendingProductCart(TrendingProducts[i]));
            }
        }

        // *** WARNING ***
        // If you modify the HomePageViews, it is very likely
        // that this method will stop working and cause some
        // exception.
        private Frame DrawTrendingProductCart(ProductModel productModel)
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
        #endregion
    }
}
