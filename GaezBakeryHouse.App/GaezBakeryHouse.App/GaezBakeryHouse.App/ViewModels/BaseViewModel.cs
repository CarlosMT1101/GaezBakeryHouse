using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;

namespace GaezBakeryHouse.App.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private bool _isRefreshing;
        private string _title;
        private string _userName;
        private LayoutState _currentState;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => SecureStorage.GetAsync(Constants.UserName).Result;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        //public string FullName
        //{
        //    get => SecureStorage.GetAsync(Constants.FullName).Result;
        //}

        //public string LastName
        //{
        //    get => SecureStorage.GetAsync(Constants.LastName).Result;
        //}

        public LayoutState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string AccessToken
        {
            get => SecureStorage.GetAsync(Constants.AccessToken).Result;
        }

        public string ApplicationUserId
        {
            get => SecureStorage.GetAsync(Constants.ApplicationUserId).Result;
        }
        #endregion

        #region Commands
        public ICommand OnRefreshCommand { get; set; }
        #endregion

        #region Functions
        protected void OnLoadingTask()
        {
            CurrentState = LayoutState.Loading;
            IsRefreshing = false;
        }

        protected void OnSuccessTask()
        {
            CurrentState = LayoutState.Success;
            IsRefreshing = false;
        }

        protected void OnErrorTask()
        {
            CurrentState = LayoutState.Error;
            IsRefreshing = false;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
