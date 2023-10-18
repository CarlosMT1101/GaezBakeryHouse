using System;
using System.ComponentModel;

namespace GaezBakeryHouse.App.Models
{
    public class OrderByUserModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
