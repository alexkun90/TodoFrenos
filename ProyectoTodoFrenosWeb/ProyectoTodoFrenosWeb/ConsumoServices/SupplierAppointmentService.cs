using DAL.Models;
using System.Net;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class SupplierAppointmentService
    {
        private IConfiguration _config;
        public SupplierAppointmentService(IConfiguration config)
        {
            this._config = config;
        }

        public async Task<IEnumerable<SupplierAppointment>> GetSupplierAppointments()
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("SupplierAppointment").Value;

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SupplierAppointment>>(responseData);

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

        public async Task<SupplierAppointment> GetSupplierAppointment(long? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("SupplierAppointment").Value + $"/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SupplierAppointment>(responseData);

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

        public async Task<SupplierAppointment> CreateSupplierAppointment(SupplierAppointment supplierAppointment)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("SupplierAppointment").Value;

                try
                {
                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(supplierAppointment);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SupplierAppointment>(responseData);

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

        public async Task<bool> DeleteSupplierAppointment(long? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("SupplierAppointment").Value + $"/{Id}";
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

        public async Task<bool> AcceptSupplierAppointment(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("SupplierAppointment").Value + $"/Accept/{id}";
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

        public async Task<bool> RejectSupplierAppointment(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("SupplierAppointment").Value + $"/Reject/{id}";
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
