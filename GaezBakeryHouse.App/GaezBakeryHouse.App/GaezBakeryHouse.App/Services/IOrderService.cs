using GaezBakeryHouse.App.Models;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GaezBakeryHouse.App.Services
{
    public interface IOrderService
    {
        #region POST
        [Post("/orders/PostOrder")]
        Task<HttpResponseMessage> PostOrder(
            [Header("Authorization")] string authorization,
            [Body] OrderModel orderModel);
        #endregion
    }

    public class OrderService
    {
        #region ATTRIBUTES
        readonly IOrderService _service;
        #endregion
        #region CONSTRUCTOR
        public OrderService() =>
            _service = RestService.For<IOrderService>(Constants.Url);
        #endregion
        #region FUNCTIONS
        public async Task<bool> PostOrder(OrderModel orderModel)
        {
            try
            {
                var authorization = await SecureStorage.GetAsync("AccessToken");
                var response = await _service.PostOrder(authorization, orderModel);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
