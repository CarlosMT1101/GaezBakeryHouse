using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class CustomMenuBarBehavior : Behavior<ContentView>
    {
        #region Atributes
        private Grid _contentGrid;
        private Image _menuIcon;
        private Image _searchIcon;
        private Image _shoppingCarIcon;
        private OpacityView _opacityView;
        private FlyoutMenu _flyoutMenu;
        private TapGestureRecognizer _tapMenuIcon;
        private TapGestureRecognizer _tapSearchIcon;
        private TapGestureRecognizer _tapShoppingCarIcon;
        private TapGestureRecognizer _tapOpacityView;
        #endregion

        #region Constructor
        public CustomMenuBarBehavior()
        {
            _tapMenuIcon = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            _tapSearchIcon = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            _tapShoppingCarIcon = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            _tapOpacityView = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

            _menuIcon = new Image();
            _searchIcon = new Image();
            _opacityView = new OpacityView();
            _flyoutMenu = new FlyoutMenu();
            _shoppingCarIcon = new Image();

            _contentGrid = new Grid();
        }

        #endregion

        #region Functions
        protected override void OnAttachedTo(ContentView bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Loaded += OnCustomMenuBarLoaded;

            _tapMenuIcon.Tapped += OnTapMenuIconTapped;
            _tapSearchIcon.Tapped += OnSearchIconTapped;
            _tapShoppingCarIcon.Tapped += OnShoppingCarIconTapped;
            _tapOpacityView.Tapped += OnOpacityViewTapped;
        }

        protected override void OnDetachingFrom(ContentView bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Loaded -= OnCustomMenuBarLoaded;

            _tapMenuIcon.Tapped -= OnTapMenuIconTapped;
            _tapSearchIcon.Tapped -= OnSearchIconTapped;
            _tapShoppingCarIcon.Tapped -= OnShoppingCarIconTapped;
            _tapOpacityView.Tapped -= OnOpacityViewTapped;
        }

        private void OnCustomMenuBarLoaded(object sender, EventArgs e)
        {
            // Parent layout
            _contentGrid = (Grid)(sender as CustomMenuBar).Parent;
            _contentGrid.Children.Add(_opacityView);
            _contentGrid.Children.Add(_flyoutMenu);

            // Menu icon
            _menuIcon = (Image)((Grid)(sender as CustomMenuBar).Content).Children[0];
            _menuIcon.GestureRecognizers.Add(_tapMenuIcon);

            // Search icon
            _searchIcon = (Image)((HorizontalStackLayout)((Grid)(sender as CustomMenuBar).Content).Children[1]).Children[0];
            _searchIcon.GestureRecognizers.Add(_tapSearchIcon);

            // ShoppingCar icon
            _shoppingCarIcon = (Image)((HorizontalStackLayout)((Grid)(sender as CustomMenuBar).Content).Children[1]).Children[1];
            _shoppingCarIcon.GestureRecognizers.Add(_tapShoppingCarIcon);

            //OpacityView tap gesture
            _opacityView.GestureRecognizers.Add(_tapOpacityView);
        }
        #endregion

        #region TapEvents
        private async void OnTapMenuIconTapped(object sender, TappedEventArgs e)
        {
            var widthPercentage = _contentGrid.Width * 0.65;

            _flyoutMenu.WidthRequest = widthPercentage;
            _flyoutMenu.TranslationX = -_flyoutMenu.WidthRequest;
            _flyoutMenu.IsVisible = true;

            _opacityView.IsVisible = true;

            await Task.WhenAll
            (
                _opacityView.FadeTo(0.5, 200, Easing.Linear),
                _flyoutMenu.TranslateTo(0, 0, 200, Easing.Linear)
            );
        }

        private void OnSearchIconTapped(object sender, TappedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void OnShoppingCarIconTapped(object sender, TappedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private async void OnOpacityViewTapped(object sender, TappedEventArgs e)
        {
            var xPosition = - _flyoutMenu.Width;

            await Task.WhenAll
            (
                _opacityView.FadeTo(0, 200, Easing.Linear),
                _flyoutMenu.TranslateTo(xPosition, 0, 200, Easing.Linear)
            );

            _flyoutMenu.IsVisible = false;
            _opacityView.IsVisible = false;
        }
        #endregion
    }
}
