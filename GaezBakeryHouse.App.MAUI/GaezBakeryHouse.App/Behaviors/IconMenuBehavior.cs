using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class IconMenuBehavior : Behavior<Image>
    {
        #region Atributes
        private Image _iconMenu;
        private TapGestureRecognizer _tapIconMenu;
        #endregion

        #region Constructor
        public IconMenuBehavior()
        {
            _iconMenu = new Image();
            _tapIconMenu = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
        }
        #endregion

        #region Functions
        protected override void OnAttachedTo(Image bindable)
        {
            base.OnAttachedTo(bindable);

            _tapIconMenu.Tapped += OnIconMenuTapped;
            bindable.Loaded += OnIconMenuLoaded;
        }

        protected override void OnDetachingFrom(Image bindable)
        {
            base.OnDetachingFrom(bindable);

            _tapIconMenu.Tapped -= OnIconMenuTapped;
            bindable.Loaded -= OnIconMenuLoaded;
        }

        private void OnIconMenuLoaded(object sender, EventArgs e)
        {
            _iconMenu = sender as Image;
            _iconMenu.GestureRecognizers.Add(_tapIconMenu);
        }

        private async void OnIconMenuTapped(object sender, TappedEventArgs e)
        {
            var contentGrid = (Grid) ((ContentView) ((Grid) _iconMenu.Parent).Parent).Parent;
            var opacityview = (OpacityView) contentGrid.Children[2];
            var flyoutMenu = (FlyoutMenuView) contentGrid.Children[3];
           
            var widthPercentage = contentGrid.Width * 0.65;

            flyoutMenu.WidthRequest = widthPercentage;
            flyoutMenu.TranslationX = -flyoutMenu.WidthRequest;
            flyoutMenu.IsVisible = true;

            opacityview.IsVisible = true;

            await Task.WhenAll
            (
                opacityview.FadeTo(0.5, 100, Easing.Linear),
                flyoutMenu.TranslateTo(0, 0, 100, Easing.Linear)
            );
        }
        #endregion
    }
}
