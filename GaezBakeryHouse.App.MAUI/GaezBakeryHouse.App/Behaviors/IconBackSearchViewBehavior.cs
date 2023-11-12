using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class IconBackSearchViewBehavior : Behavior<Image>
    {
        private TapGestureRecognizer _tapIconBackSearch;

        public IconBackSearchViewBehavior() =>
            _tapIconBackSearch = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        protected override void OnAttachedTo(Image bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Loaded += OnIconSearchBackLoaded;
            _tapIconBackSearch.Tapped += OnIconSearchBackTapped;
        }

        private async void OnIconSearchBackTapped(object sender, TappedEventArgs e)
        {
            var iconSearch = sender as Image;
            var contentGrid = (Grid) ((ContentView) ((Grid)((Grid)iconSearch.Parent).Parent).Parent).Parent;
            var searchView = (SearchView)contentGrid.Children[4];

            await searchView.FadeTo(0, 100, Easing.Linear);
            searchView.IsVisible = false;
        }

        protected override void OnDetachingFrom(Image bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Loaded -= OnIconSearchBackLoaded;
            _tapIconBackSearch.Tapped -= OnIconSearchBackTapped;
        }

        private void OnIconSearchBackLoaded(object sender, EventArgs e) =>
            (sender as Image).GestureRecognizers.Add(_tapIconBackSearch);
    }
}
