using GaezBakeryHouse.App.Controls;
using MauiIcons.Material;

namespace GaezBakeryHouse.App.Behaviors
{
    public class IconBackSearchViewBehavior : Behavior<MauiIcon>
    {
        private TapGestureRecognizer _tapIconBackSearch;

        public IconBackSearchViewBehavior() =>
            _tapIconBackSearch = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        protected override void OnAttachedTo(MauiIcon bindable)
        {
            base.OnAttachedTo(bindable);

            _tapIconBackSearch.Tapped += OnIconSearchBackTapped;
            bindable.GestureRecognizers.Add(_tapIconBackSearch);
        }

        private async void OnIconSearchBackTapped(object sender, TappedEventArgs e)
        {
            var iconSearch = sender as MauiIcon;
            var contentGrid = (Grid) ((ContentView) ((Grid)((Grid)iconSearch.Parent).Parent).Parent).Parent;
            var searchView = (SearchView)contentGrid.Children[4];

            await searchView.FadeTo(0, 100, Easing.Linear);
            searchView.IsVisible = false;
        }

        protected override void OnDetachingFrom(MauiIcon bindable)
        {
            base.OnDetachingFrom(bindable);

            _tapIconBackSearch.Tapped -= OnIconSearchBackTapped;
            bindable.GestureRecognizers.Clear();
        }
    }
}
