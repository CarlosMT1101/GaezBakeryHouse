using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class FlyoutMenuBehavior : Behavior<FlyoutMenuView>
    {
        private TapGestureRecognizer _tapFlyoutMenu;

        public FlyoutMenuBehavior() =>
            _tapFlyoutMenu = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        protected override void OnAttachedTo(FlyoutMenuView bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Loaded += OnFlyoutMenuLoaded;
            _tapFlyoutMenu.Tapped += OnFlyoutMenuTapped;
        }

        private void OnFlyoutMenuLoaded(object sender, EventArgs e) =>
            (sender as FlyoutMenuView).GestureRecognizers.Add(_tapFlyoutMenu);

        protected override void OnDetachingFrom(FlyoutMenuView bindable)
        {
            base.OnDetachingFrom(bindable);

            _tapFlyoutMenu.Tapped -= OnFlyoutMenuTapped;
            bindable.Loaded -= OnFlyoutMenuLoaded;
        }

        private void OnFlyoutMenuTapped(object sender, TappedEventArgs e)
        {
            // Do something
        }
    }
}
