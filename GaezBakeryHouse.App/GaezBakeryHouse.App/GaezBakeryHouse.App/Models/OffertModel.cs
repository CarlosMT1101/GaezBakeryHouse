using System.ComponentModel;

namespace GaezBakeryHouse.App.Models
{
    public class OffertModel : INotifyPropertyChanged
    {
        public string ImageSource { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
