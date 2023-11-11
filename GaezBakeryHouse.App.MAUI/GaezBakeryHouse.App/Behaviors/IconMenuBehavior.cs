using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class IconMenuBehavior : Behavior<Image>
    {
        private TapGestureRecognizer _tapIconMenu;

        public IconMenuBehavior() =>
            _tapIconMenu = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

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

        private void OnIconMenuLoaded(object sender, EventArgs e) =>
            (sender as Image).GestureRecognizers.Add(_tapIconMenu);

        private async void OnIconMenuTapped(object sender, TappedEventArgs e)
        {
            var iconMenu = sender as Image;
            var contentGrid = (Grid)((ContentView)((Grid) iconMenu.Parent).Parent).Parent;
            var opacityview = (OpacityView)contentGrid.Children[2];
            var flyoutMenu = (FlyoutMenuView)contentGrid.Children[3];

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
    }
}
