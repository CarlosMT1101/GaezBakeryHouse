using System.ComponentModel;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.Models
{
    public class ProductModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public byte[] Image { get; set; }

        public int CategoryId { get; set; }

        public ImageSource ImageSource { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
