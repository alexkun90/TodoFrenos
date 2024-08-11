using DAL.Models;
using System.Net;
using System.Text;

namespace FrontEnd.ConsumoServices
{
    public class NominaService
    {
        private IConfiguration _config;

        public NominaService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<Nomina> CalcularTotal(int nominaId)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Nomina").Value + $"/CalcularTotal/{nominaId}";

                try
                {
                    var response = await client.PostAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Nomina>(responseData);

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
        public async Task<IEnumerable<Nomina>> GetNomina()
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Nomina").Value;

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Nomina>>(responseData);

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

        public async Task<IEnumerable<Nomina>> GetMyNominas(string? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Nomina").Value + $"/MyNominas/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Nomina>>(responseData);

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


        //Details
        public async Task<Nomina> GetNomina(int? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Nomina").Value + $"/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Nomina>(responseData);

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
        public async Task<Nomina> CreateNomina(Nomina nomina)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Nomina").Value;

                try
                {
                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(nomina);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Nomina>(responseData);

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

        //Editar
        public async Task<Nomina> EditNomina(int Id, Nomina nomina)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Nomina").Value + $"/{Id}";

                try
                {
                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(nomina);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Nomina>(responseData);
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
        //Delete
        public async Task<bool> DeleteNomina(int? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Nomina").Value + $"/Delete/{Id}";
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

        //Active
        public async Task<bool> ActivateNomina(int? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Nomina").Value + $"/Activate/{Id}";
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
}

