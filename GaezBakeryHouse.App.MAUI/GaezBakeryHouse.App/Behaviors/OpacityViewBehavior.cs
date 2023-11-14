using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class OpacityViewBehavior : Behavior<OpacityView>
    {
        private TapGestureRecognizer _tapOpacityView;

        public OpacityViewBehavior() =>
            _tapOpacityView = new TapGestureRecognizer { NumberOfTapsRequired = 1 };


        protected override void OnAttachedTo(OpacityView bindable)
        {
            base.OnAttachedTo(bindable);

            _tapOpacityView.Tapped += OnOpacityViewTapped;
            bindable.GestureRecognizers.Add(_tapOpacityView);
        }

        protected override void OnDetachingFrom(OpacityView bindable)
        {
            base.OnDetachingFrom(bindable);

            _tapOpacityView.Tapped -= OnOpacityViewTapped;
            bindable.GestureRecognizers.Clear();
        }

        private async void OnOpacityViewTapped(object sender, TappedEventArgs e)
        {
            var opacityView = sender as OpacityView;
            var contentGrid = opacityView.Parent as Grid;
            var flyoutMenu = contentGrid.Children[3] as FlyoutMenuView;

            var xPosition = -flyoutMenu.Width;

            await Task.WhenAll
            (
                opacityView.FadeTo(0, 100, Easing.Linear),
                flyoutMenu.TranslateTo(xPosition, 0, 100, Easing.Linear)
            );

            flyoutMenu.IsVisible = false;
            opacityView.IsVisible = false;
        }
    }
}
