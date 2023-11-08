using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class ContentPageBehavior : Behavior<ContentPage>
    {
        #region Atributes
        private Grid _contentGrid;
        private FlyoutMenu _flyoutMenu;
        private OpacityView _opacityView;
        private Image _menuIcon;
        private Image _searchIcon;
        private Image _shoppingCartIcon;
        #endregion

        #region Constructor
        public ContentPageBehavior()
        {
            _contentGrid = new Grid();
            _flyoutMenu = new FlyoutMenu();
            _opacityView = new OpacityView();
            _menuIcon = new Image();
            _searchIcon = new Image();
        }
        #endregion

        #region Functions
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Appearing += OnPageAppearing;
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Appearing -= OnPageAppearing;

            _menuIcon.GestureRecognizers.Clear();
            _searchIcon.GestureRecognizers.Clear();
            _shoppingCartIcon.GestureRecognizers.Clear();
        }

        private void OnPageAppearing(object sender, EventArgs e)
        {
            _contentGrid = (Grid)(sender as ContentPage).Content;
            _contentGrid.Children.Add(_opacityView);
            _contentGrid.Children.Add(_flyoutMenu);

            var customMenuBar = (CustomMenuBar)_contentGrid.Children[0];
            var customMenuBarContent = (Grid)customMenuBar.Content;

            _menuIcon = (Image)customMenuBarContent.Children[0];
            _searchIcon = (Image) ((HorizontalStackLayout)customMenuBarContent.Children[1]).Children[0];
            _shoppingCartIcon = (Image)((HorizontalStackLayout)customMenuBarContent.Children[1]).Children[1];

            AddGestureRecognizers();
        }

        private void AddGestureRecognizers()
        {
            _menuIcon.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command(OpenFlyoutMenu)
            });

            _opacityView.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
                Command = new Command(CloseFlyoutMenu)
            });

            _flyoutMenu.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
                Command = new Command(() => { })
            });

            _searchIcon.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
                Command = new Command(() => { })
            });
        }

        #endregion

        #region FlyoutMenuBehaviors
        private async void CloseFlyoutMenu()
        {
            _opacityView.IsVisible = false;

            await _flyoutMenu.TranslateTo(-_flyoutMenu.WidthRequest, 0, 200, Easing.CubicOut);

            _flyoutMenu.IsVisible = false;
        }

        private async void OpenFlyoutMenu()
        {
            _opacityView.IsVisible = true;

            _flyoutMenu.WidthRequest = _contentGrid.Width * 0.65;
            _flyoutMenu.TranslationX = -_flyoutMenu.WidthRequest;
            _flyoutMenu.IsVisible = true;

            await _flyoutMenu.TranslateTo(0, 0, 200, Easing.CubicOut);
        }
        #endregion
    }
}
