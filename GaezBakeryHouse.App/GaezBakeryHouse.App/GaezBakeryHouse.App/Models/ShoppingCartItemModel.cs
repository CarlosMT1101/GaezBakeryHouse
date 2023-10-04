using System.ComponentModel;
using Xamarin.Forms;

namespace GaezBakeryHouse.App.Models
{
    public class ShoppingCartItemModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        public byte[] ProductImage { get; set; }
        public ImageSource ImageSource { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
