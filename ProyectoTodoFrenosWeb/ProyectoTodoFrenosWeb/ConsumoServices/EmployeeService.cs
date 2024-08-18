using DAL.Models;
using System.Net;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class EmployeeService
    {
        private IConfiguration _config;
        public EmployeeService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            using (var client = new HttpClient())
            {
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

        }

        public async Task<Employee> GetEmployee(long? Id)
        {
            using (var client = new HttpClient())
            {
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

        }

        //Create
        public async Task<Employee> CreateEmployee(Employee model)
        {
            using (var client = new HttpClient())
            {
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
                        throw new Exception("La cedula ingresada le pertenece a otro empleado");
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
