using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class OpacityViewBehavior : Behavior<OpacityView>
    {
        #region Atributes
        private OpacityView _opacityView;
        private TapGestureRecognizer _tapOpacityView;
        private Grid _contentGrid;
        #endregion

        #region Constructor
        public OpacityViewBehavior()
        {
            _contentGrid = new Grid();
            _tapOpacityView = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
        }
        #endregion

        #region Functions
        protected override void OnAttachedTo(OpacityView bindable)
        {
            base.OnAttachedTo(bindable);

            if (_opacityView is null)
                _opacityView = bindable;

            _opacityView.Loaded += OnOpacityViewLoaded;
            _tapOpacityView.Tapped += OnOpacityViewTapped;
        }

        protected override void OnDetachingFrom(OpacityView bindable)
        {
            base.OnDetachingFrom(bindable);

            _opacityView.Loaded -= OnOpacityViewLoaded;
            _tapOpacityView.Tapped -= OnOpacityViewTapped;
        }
        #endregion

        private async void OnOpacityViewTapped(object sender, TappedEventArgs e)
        {
            var flyoutMenu = (FlyoutMenu) _contentGrid.Children.FirstOrDefault(view => view is FlyoutMenu);
            var xPosition = -_contentGrid.Width * 0.65;


            await Task.WhenAll
            (
                _opacityView.FadeTo(0, 200, Easing.Linear),
                flyoutMenu.TranslateTo(xPosition, 0, 200, Easing.Linear)
            );

            _opacityView.IsVisible = false;
            flyoutMenu.IsVisible = false;
        }

        private void OnOpacityViewLoaded(object sender, EventArgs e)
        {
            _opacityView = sender as OpacityView;
            _opacityView.GestureRecognizers.Add(_tapOpacityView);

            _contentGrid = _opacityView.Parent as Grid;
        }
    }
}
