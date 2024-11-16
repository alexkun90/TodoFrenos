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
                    throw new Exception("La cedula ingresada le pertenece a otro employee");
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
