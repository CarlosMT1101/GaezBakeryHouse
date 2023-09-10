using System.ComponentModel;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.Models
{
    public class CategoryModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public ImageSource ImageSource { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
