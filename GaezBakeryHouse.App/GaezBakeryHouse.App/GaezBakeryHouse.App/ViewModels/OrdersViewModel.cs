using Acr.UserDialogs;
using GaezBakeryHouse.App.Helpers;
using GaezBakeryHouse.App.Models;
using GaezBakeryHouse.App.Services;
using Refit;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        #region Attributes
        private StackLayout _stackLayout;
        private IOrderService _orderService;
        #endregion

        #region Properties
        public AwesomeObservableCollection<OrderByUserModel> Orders { get; private set; }
        #endregion

        #region Commands

        #endregion

        #region Constructor
        public OrdersViewModel(StackLayout stackLayout)
        {
            _stackLayout = stackLayout;
            _orderService = RestService.For<IOrderService>(Constants.Url);

            Orders = new AwesomeObservableCollection<OrderByUserModel>();

            OnRefreshCommand = new Command(
                execute: async () => await LoadDataAsync(),
                canExecute: () => true);
        }
        #endregion

        #region Functions
        public async Task LoadDataAsync()
        {
            UserDialogs.Instance.ShowLoading(Constants.LoadingMessage);
            OnLoadingTask();

            try
            {
                await LoadOrders();
                OnSuccessTask();
            }
            catch (Exception ex)
            {
                OnErrorTask();
            }

            UserDialogs.Instance.HideLoading();
        }

        private async Task LoadOrders()
        {
            var orders = await _orderService.GetOrdersByUser(AccessToken, ApplicationUserId);
            _stackLayout.Children.Clear();

            Orders.ClearRange();
            Orders.AddRange(orders);

            DrawProductsInLayouts();
        }

        private void DrawProductsInLayouts()
        {
            foreach (var order in Orders)
                _stackLayout.Children.Add(DrawOrder(order));
        }

        private Frame DrawOrder(OrderByUserModel order)
        {
            var frame = new Frame
            {
                Padding = 10,
                HasShadow = false,
                CornerRadius = 10
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            var stackLayoutOne = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };

            var orderNumberLabel = new Label
            {
                Text = "No.Orden",
                TextColor = Color.Black
            };

            var orderIdLabel = new Label
            {
                Text = order.Id.ToString(),
                TextColor = (Color)Application.Current.Resources["PrimaryColor"]
            };

            var orderTotalLabel = new Label
            {
                Text = "Total",
                TextColor = Color.Black
            };

            stackLayoutOne.Children.Add(orderNumberLabel);
            stackLayoutOne.Children.Add(orderIdLabel);
            stackLayoutOne.Children.Add(orderTotalLabel);

            Grid.SetColumn(stackLayoutOne, 0);

            var stackLayoutTwo = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };

            var madeLabel = new Label
            {
                Text = "Realizada",
                TextColor = Color.Black
            };

            var dateLabel = new Label
            {
                Text = order.OrderPlaced.ToString(),
                TextColor = Color.Black
            };

            var priceLabel = new Label
            {
                TextColor =Color.Black,
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span { Text = "$ " },
                        new Span { Text = order.OrderTotal.ToString() }
                    }
                }
            };

            stackLayoutTwo.Children.Add(madeLabel);
            stackLayoutTwo.Children.Add(dateLabel);
            stackLayoutTwo.Children.Add(priceLabel);

            Grid.SetColumn(stackLayoutTwo, 1);

            grid.Children.Add(stackLayoutOne);
            grid.Children.Add(stackLayoutTwo);

            frame.Content = grid;

            return frame;
        }
        #endregion
    }
}