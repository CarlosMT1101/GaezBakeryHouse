using GaezBakeryHouse.App.Controls;

namespace GaezBakeryHouse.App.Behaviors
{
    public class IconSearchBehavior : Behavior<Image>
    {
        private TapGestureRecognizer _tapSearchIcon;

        public IconSearchBehavior() =>
            _tapSearchIcon = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        protected override void OnAttachedTo(Image bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Loaded += OnIconSearchLoaded;
            _tapSearchIcon.Tapped += OnSearchIconTapped;
        }

        private async void OnSearchIconTapped(object sender, TappedEventArgs e)
        {
            var iconSearch = sender as Image;
            var contentGrid = (Grid)((ContentView)((Grid)iconSearch.Parent).Parent).Parent;
            var searchView = (SearchView)contentGrid.Children[4];

            searchView.IsVisible = true;
            await searchView.FadeTo(1, 100, Easing.Linear);
        }

        protected override void OnDetachingFrom(Image bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Loaded -= OnIconSearchLoaded;
            _tapSearchIcon.Tapped -= OnSearchIconTapped;
        }

        private void OnIconSearchLoaded(object sender, EventArgs e) =>
            (sender as Image).GestureRecognizers.Add(_tapSearchIcon);
    }
}
