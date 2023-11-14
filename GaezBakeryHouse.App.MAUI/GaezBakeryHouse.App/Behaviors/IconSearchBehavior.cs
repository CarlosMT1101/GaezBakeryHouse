using GaezBakeryHouse.App.Controls;
using MauiIcons.Material;

namespace GaezBakeryHouse.App.Behaviors
{
    public class IconSearchBehavior : Behavior<MauiIcon>
    {
        private TapGestureRecognizer _tapSearchIcon;

        public IconSearchBehavior() =>
            _tapSearchIcon = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        protected override void OnAttachedTo(MauiIcon bindable)
        {
            base.OnAttachedTo(bindable);

            _tapSearchIcon.Tapped += OnSearchIconTapped;
            bindable.GestureRecognizers.Add(_tapSearchIcon);
        }

        private async void OnSearchIconTapped(object sender, TappedEventArgs e)
        {
            var iconSearch = sender as MauiIcon;
            var contentGrid = (Grid)((ContentView)((Grid)iconSearch.Parent).Parent).Parent;
            var searchView = (SearchView)contentGrid.Children[4];

            searchView.IsVisible = true;
            await searchView.FadeTo(1, 100, Easing.Linear);
        }

        protected override void OnDetachingFrom(MauiIcon bindable)
        {
            base.OnDetachingFrom(bindable);

            _tapSearchIcon.Tapped -= OnSearchIconTapped;
            bindable.GestureRecognizers.Clear();
        }
    }
}
