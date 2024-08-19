using DAL.Models;
using System.Collections.Generic;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class PlanillaService
    {
        IConfiguration _config;
        public PlanillaService(IConfiguration config)
        {
            this._config = config;
        }

        public async Task<IEnumerable<PlanillaEmpleado>> GetAllPlanilla(long nominaId)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Planilla").Value + $"/List/{nominaId}";
                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<PlanillaEmpleado>>(responseData);

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

        public async Task<PlanillaEmpleado> GetPlanilla(long planillaId)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Planilla").Value + $"/Details/{planillaId}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanillaEmpleado>(responseData);

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

        public async Task<PlanillaEmpleado> CreatePlanilla(long nominaId, PlanillaEmpleado model)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Planilla").Value + $"/{nominaId}";

                try
                {
                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanillaEmpleado>(responseData);

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
