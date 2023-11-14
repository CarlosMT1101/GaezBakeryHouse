using GaezBakeryHouse.App.Controls;
using MauiIcons.Material;

namespace GaezBakeryHouse.App.Behaviors
{
    public class IconMenuBehavior : Behavior<MauiIcon>
    {
        private TapGestureRecognizer _tapIconMenu;

        public IconMenuBehavior() =>
            _tapIconMenu = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        protected override void OnAttachedTo(MauiIcon bindable)
        {
            base.OnAttachedTo(bindable);

            _tapIconMenu.Tapped += OnIconMenuTapped;
            bindable.GestureRecognizers.Add(_tapIconMenu);
        }

        protected override void OnDetachingFrom(MauiIcon bindable)
        {
            base.OnDetachingFrom(bindable);

            _tapIconMenu.Tapped -= OnIconMenuTapped;
            bindable.GestureRecognizers.Clear();
        }

        private async void OnIconMenuTapped(object sender, TappedEventArgs e)
        {
            var iconMenu = sender as MauiIcon;
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
