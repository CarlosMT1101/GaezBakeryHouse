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

            _tapFlyoutMenu.Tapped += OnFlyoutMenuTapped;
            bindable.GestureRecognizers.Add(_tapFlyoutMenu);
        }

        protected override void OnDetachingFrom(FlyoutMenuView bindable)
        {
            base.OnDetachingFrom(bindable);

            _tapFlyoutMenu.Tapped -= OnFlyoutMenuTapped;
            bindable.GestureRecognizers.Clear();
        }

        private void OnFlyoutMenuTapped(object sender, TappedEventArgs e)
        {
            // Do something
        }
    }
}
