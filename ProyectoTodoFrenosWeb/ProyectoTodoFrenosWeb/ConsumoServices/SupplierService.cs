using DAL.Models;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class SupplierService
    {
        private IConfiguration _config;

        public SupplierService(IConfiguration config)
        {
            _config = config;
        }

        // GET INDEX
        public async Task<IEnumerable<Suppliers>> GetSuppliers()
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Supplier").Value;

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Suppliers>>(responseData);

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

        // Details
        public async Task<Suppliers> GetSupplier(long? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Supplier").Value + $"/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Suppliers>(responseData);

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

        // Create
        public async Task<Suppliers> CreateSupplier(Suppliers supplier)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Supplier").Value;

                try
                {
                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(supplier);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Suppliers>(responseData);

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

        // Edit
        public async Task<Suppliers> EditSupplier(long Id, Suppliers supplier)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Supplier").Value + $"/{Id}";

                try
                {
                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(supplier);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Suppliers>(responseData);
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

        // Delete
        public async Task<bool> DeleteSupplier(long Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Supplier").Value + $"/{Id}";

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
        }

        

        public async Task<bool> AcceptSupplier(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Supplier").Value + $"/Accept/{id}";
                try
                {
                    var response = await client.PutAsync(apiUrl, null);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<bool> RejectSupplier(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Supplier").Value + $"/Reject/{id}";
                try
                {
                    var response = await client.PutAsync(apiUrl, null);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }



    }
}
