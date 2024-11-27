using DAL.Models;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class EmployeeService
    {
        private readonly IConfiguration _config;
        private readonly HttpClientService clientService;

        public EmployeeService(IConfiguration config, HttpClientService clientService)
        {
            _config = config;
            this.clientService = clientService;
        }

        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Employee").Value;

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Employee>>(responseData);

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

        public async Task<Employee> GetEmployee(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Employee").Value + $"/{Id}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(responseData);

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
        public async Task<Employee> CreateEmployee(Employee model)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Employee").Value;

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(responseData);
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    // Manejo de mensajes detallados de conflicto
                    var errorMessage = await response.Content.ReadAsStringAsync();

                    if (errorMessage.Contains("cédula", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("La cédula ingresada ya pertenece a otro empleado.");
                    }
                    else if (errorMessage.Contains("contacto", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("El contacto de emergencia ingresado ya está registrado por otro empleado.");
                    }
                    else
                    {
                        throw new Exception("Conflicto detectado: el empleado ya existe.");
                    }
                }
                else
                {
                    throw new Exception("Error al procesar la solicitud. Intente nuevamente.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el empleado: {ex.Message}");
            }
        }

        //Editar
        public async Task<Employee> EditEmployee(long Id, Employee employee)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Employee").Value + $"/{Id}";

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(employee);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(responseData);
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
        //Delete
        public async Task<bool> DeleteEmployee(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Employee").Value + $"/{Id}";
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
        public async Task<bool> ActivateEmployee(long? Id)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Employee").Value + $"/Activate/{Id}";
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
