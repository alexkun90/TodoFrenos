using DAL.Models;
using System.Net;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class VehicleInspectionService
    {
        private IConfiguration _config;
        private readonly HttpClientService clientService;

        public VehicleInspectionService(IConfiguration config, HttpClientService clientService)
        {
            _config = config;
            this.clientService = clientService;
        }

        public async Task<IEnumerable<VehicleInspection>> GetList(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Inspection").Value + $"/List/{Id}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<VehicleInspection>>(responseData);

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

        public async Task<VehicleInspection> GetVehicleInspection(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Inspection").Value + $"/{Id}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<VehicleInspection>(responseData);

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

        public async Task<VehicleInspection> CreateVehicleInspection(VehicleInspection model)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Inspection").Value;

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<VehicleInspection>(responseData);

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

        public async Task<VehicleInspection> EditVehicleInspection(long Id, VehicleInspection model)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Inspection").Value + $"/{Id}";

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<VehicleInspection>(responseData);
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

        public async Task<bool> DeleteVehicleInspection(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Inspection").Value + $"/{Id}";
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
}
