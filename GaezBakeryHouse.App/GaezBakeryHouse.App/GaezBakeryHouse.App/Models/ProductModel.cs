using GaezBakeryHouse.App.ViewModels;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.Models
{
    public class ProductModel : BaseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ProductImage { get; set; }

        public decimal Price { get; set; }

        public bool IsTrendingProduct { get; set; }

        public int CategoryId { get; set; }


        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

    }
}
