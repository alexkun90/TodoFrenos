using DAL.Models;
using Newtonsoft.Json;
using ProyectoTodoFrenosWeb.ViewModels;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class ShoppingCartService
    {
        private readonly IConfiguration _config;
        public ShoppingCartService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<CartItemViewModel>> GetCartItems(string userId)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("ShoppingCart").Value + $"/GetCartItems/{userId}";

                try
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var cartItems = JsonConvert.DeserializeObject<IEnumerable<CartItemViewModel>>(responseContent);

                        return cartItems;
                    }
                    else
                    {
                        // Manejo de error para códigos de respuesta no exitosos
                        return Enumerable.Empty<CartItemViewModel>();
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores en la llamada HTTP o deserialización
                    // Puedes registrar el error o manejarlo de acuerdo a tus necesidades
                    return Enumerable.Empty<CartItemViewModel>();
                }
            }
        }

        // Agregar un ítem al carrito
        public async Task<string> AddToCart(string userId, long productId, int quantity)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("ShoppingCart").Value;
                var cartItemDto = new CartItemDTO
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                var body = JsonConvert.SerializeObject(cartItemDto);
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
                    return "Error al añadir el producto al carrito.";
                }
            }
        }

        // Actualizar la cantidad de un ítem en el carrito
        public async Task<bool> UpdateQuantity(long cartItemId, int quantity)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("ShoppingCart").Value + "/UpdateQuantity";

                var updateQuantityDto = new UpdateQuantityDTO
                {
                    CartItemId = cartItemId,
                    Quantity = quantity
                };

                var body = JsonConvert.SerializeObject(updateQuantityDto);
                var content = new StringContent(body, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(apiUrl, content);

                return response.IsSuccessStatusCode;
            }
        }
    }
}

