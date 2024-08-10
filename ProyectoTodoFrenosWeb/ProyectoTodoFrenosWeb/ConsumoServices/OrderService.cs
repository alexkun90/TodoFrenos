using DAL.Models;
using Newtonsoft.Json;
using ProyectoTodoFrenosWeb.ViewModels;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class OrderService
    {
        private readonly IConfiguration _config;

        public OrderService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> CreateOrder(Order order)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Order").Value;
                var body = JsonConvert.SerializeObject(order);
                var content = new StringContent(body, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseData);
                    return result.message;
                }
                else
                {
                    return "Error al crear la orden.";
                }
            }
        }

        public async Task<IEnumerable<Order>> GetOrderList()
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Order").Value + $"/GetOrders";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Order>>(responseData);

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }


        public async Task<IEnumerable<Order>> GetMyOrderList(string userId)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Order").Value + $"/MyOrders/{userId}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Order>>(responseData);

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetMyOrderDetailList(long? orderId)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Order").Value + $"/GetMyDetailsOrders/{orderId}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<OrderDetailViewModel>>(responseData);

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

    }
}
