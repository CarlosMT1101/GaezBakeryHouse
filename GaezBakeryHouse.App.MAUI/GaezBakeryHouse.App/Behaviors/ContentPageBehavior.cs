using GaezBakeryHouse.App.Controls;
namespace GaezBakeryHouse.App.Behaviors
{
    public class ContentPageBehavior : Behavior<ContentPage>
    {
        #region Atributes
        private SearchView _searchView;
        private OpacityView _opacityView;
        private FlyoutMenuView _flyoutMenuView;
        #endregion

        #region Constructor
        public ContentPageBehavior()
        {
            _searchView = new SearchView();
            _opacityView = new OpacityView();
            _flyoutMenuView = new FlyoutMenuView();
        }
        #endregion

        #region Functions
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Appearing += OnContentPageAppearing;
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.Appearing -= OnContentPageAppearing;
        }

        private void OnContentPageAppearing(object sender, EventArgs e)
        {
            var contentGrid = (Grid)(sender as ContentPage).Content;

            contentGrid.Children.Add(_opacityView);
            contentGrid.Children.Add(_flyoutMenuView);
            contentGrid.Children.Add(_searchView);
        }
        #endregion
    }
}
