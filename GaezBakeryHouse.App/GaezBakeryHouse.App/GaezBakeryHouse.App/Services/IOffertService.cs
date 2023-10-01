using GaezBakeryHouse.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaezBakeryHouse.App.Services
{
    public interface IOffertService
    {
        IEnumerable<OffertModel> GetBanners();
    }

    public class OffertService : IOffertService
    {
        public IEnumerable<OffertModel> GetBanners() =>
            new List<OffertModel>
            {
                new OffertModel{ ImageSource = "banner2.jpg" },
                new OffertModel{ ImageSource = "banner3.jpg" },
                new OffertModel{ ImageSource = "banner1.jpg" },
            };
    }
}
