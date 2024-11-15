using System.Net;
using System.Text;
using DAL.Models;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class VehicleService
    {
        private IConfiguration _config;
        private readonly HttpClientService clientService;

        public VehicleService(IConfiguration config, HttpClientService clientService)
        {
            _config = config;
            this.clientService = clientService;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Vehicle").Value;

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(responseData);

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

        public async Task<IEnumerable<Vehicle>> GetMyVehicles(string? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Vehicle").Value + $"/MyVehicles/{Id}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(responseData);

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
        public async Task<Vehicle> GetVehicle(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Vehicle").Value + $"/{Id}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Vehicle>(responseData);

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
        public async Task<Vehicle> CreateVehicle(Vehicle vehicle)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Vehicle").Value;

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Vehicle>(responseData);

                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("La placa del vehículo ya se encuentra registrada.");
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
        public async Task<Vehicle> EditVehicle(long Id, Vehicle vehicle)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Vehicle").Value + $"/{Id}";

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Vehicle>(responseData);
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("La placa del vehículo ya se encuentra registrada.");
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
        //Delete
        public async Task<bool> DeleteVehicle(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Vehicle").Value + $"/{Id}";
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

        //Active
        public async Task<bool> ActivateVehicle(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Vehicle").Value + $"/Activate/{Id}";
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

