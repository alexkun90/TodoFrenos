using DAL.Models;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    
    public class ProductService
    {
        private IConfiguration _config;
        private readonly HttpClientService clientService;

        public ProductService(IConfiguration config, HttpClientService clientService)
        {
            _config = config;
            this.clientService = clientService;
        }

        //GET INDEX
        public async Task<IEnumerable<Product>> GetProduct()
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Product").Value;

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Product>>(responseData);

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


        //Details
        public async Task<Product> GetProduct(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Product").Value + $"/{Id}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(responseData);

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

        //Create
        public async Task<Product> CreateProduct(Product product)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Product").Value;

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(product);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(responseData);

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
        
        //Editar
        public async Task<Product> EditProduct(long Id, Product product)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Product").Value + $"/{Id}";

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(product);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(responseData);
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
        //Detete
        public async Task<bool> DeleteProduct(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Product").Value + $"/{Id}";

            try
            {
                var response = await client.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> ActivarProduct(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Product").Value + $"/Activate/{Id}";

            try
            {
                var response = await client.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStock(long productId, int quantityToReduce)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Product").Value + $"/UpdateStock/{productId}/{quantityToReduce}";

            try
            {
                var response = await client.PutAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}
