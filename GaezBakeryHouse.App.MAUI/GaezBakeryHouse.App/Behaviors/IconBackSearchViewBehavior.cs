using GaezBakeryHouse.App.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Behaviors
{
    public class IconBackSearchViewBehavior : Behavior<Image>
    {
        private TapGestureRecognizer _tapIconSearchBack;

        public IconBackSearchViewBehavior() =>
            _tapIconSearchBack = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        protected override void OnAttachedTo(Image bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Loaded += OnIconSearchBackLoaded;
            _tapIconSearchBack.Tapped += OnIconSearchBackTapped;
        }

        private async void OnIconSearchBackTapped(object sender, TappedEventArgs e)
        {
            var iconSearch = sender as Image;
            var contentGrid = (Grid)((ContentView)((Grid)iconSearch.Parent).Parent).Parent;
            var searchView = (SearchView)contentGrid.Children[4];

            await searchView.FadeTo(0, 100, Easing.Linear);
            searchView.IsVisible = false;
        }

        protected override void OnDetachingFrom(Image bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Loaded -= OnIconSearchBackLoaded;
            _tapIconSearchBack.Tapped -= OnIconSearchBackTapped;
        }

        private void OnIconSearchBackLoaded(object sender, EventArgs e) =>
            (sender as Image).GestureRecognizers.Add(_tapIconSearchBack);
    }
}
