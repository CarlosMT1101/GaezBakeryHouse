using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class MenuIconBehavior : Behavior<Image>
    {
        #region Atributes
        private Grid _contentGrid;
        private Image _menuIcon;
        private TapGestureRecognizer _tapMenuIcon;
        private OpacityView _opacityView;
        private FlyoutMenu _flyoutMenu; 
        #endregion

        #region Constructor
        public MenuIconBehavior()
        {
            _contentGrid = new Grid();
            _menuIcon = new Image();
            _tapMenuIcon = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            _opacityView = new OpacityView();
            _flyoutMenu = new FlyoutMenu();
        }
        #endregion

        #region Functions
        protected override void OnAttachedTo(Image bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Loaded += OnMenuIconLoaded;
            _tapMenuIcon.Tapped += OnTapMenuIcon;
        }

        protected override void OnDetachingFrom(Image bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Loaded -= OnMenuIconLoaded;
            _tapMenuIcon.Tapped -= OnTapMenuIcon;
        }

        private void OnMenuIconLoaded(object sender, EventArgs e)
        {
            // Assign Parent Layout
            _contentGrid = (Grid)((ContentView)((sender as Image).Parent).Parent).Parent;

            // Assign Image
            _menuIcon = sender as Image;

            // Add Tap Gesture Recognizer to MenuIcon
            _menuIcon.GestureRecognizers.Add(_tapMenuIcon);

            // Add views to content grid
            _contentGrid.Children.Add(_opacityView);
            _contentGrid.Children.Add(_flyoutMenu);
        }

        private async void OnTapMenuIcon(object sender, TappedEventArgs e)
        {
            _flyoutMenu.WidthRequest = _contentGrid.Width * 0.65;
            _flyoutMenu.TranslationX = -_flyoutMenu.WidthRequest;
            _opacityView.IsVisible = true;
            _flyoutMenu.IsVisible = true;

            await Task.WhenAll
            (
                _opacityView.FadeTo(0.5, 200, Easing.Linear),
                _flyoutMenu.TranslateTo(0, 0, 200, Easing.Linear)
            );
        }
        #endregion
    }
}
