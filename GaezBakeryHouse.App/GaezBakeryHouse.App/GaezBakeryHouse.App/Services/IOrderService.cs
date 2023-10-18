using GaezBakeryHouse.App.Models;
using Refit;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GaezBakeryHouse.App.Services
{
    public interface IOrderService
    {
        [Post("/orders/PostOrder")]
        Task<HttpResponseMessage> PostOrder(
             [Header("Authorization")] string authorization,
             [Body] OrderModel orderModel);

        [Get("/orders/GetOrdersByUser/{userId}")]
        Task<IEnumerable<OrderByUserModel>> GetOrdersByUser(
             [Header("Authorization")] string authorization,
             [AliasAs("userId")] string userId);
    }
}
