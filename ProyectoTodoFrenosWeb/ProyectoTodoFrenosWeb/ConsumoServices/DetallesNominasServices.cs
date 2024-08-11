using DAL.Models;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FrontEnd.ConsumoServices
{
    public class DetallesNominasServices
    {
        private readonly IConfiguration _config;

        public DetallesNominasServices(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<DetallesNomina>> GetDetallesNomina()
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("DetallesNomina").Value;

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<DetallesNomina>>(responseData);

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

        public async Task<IEnumerable<DetallesNomina>> GetMyDetallesNominas(string? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("DetallesNomina").Value + $"/MyDetallesNominas/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<DetallesNomina>>(responseData);

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
        public async Task<DetallesNomina> GetDetallesNomina(int? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("DetallesNomina").Value + $"/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DetallesNomina>(responseData);

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
        public async Task<DetallesNomina> CreateDetallesNomina(DetallesNomina detallesNomina)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("DetallesNomina").Value;

                try
                {
                    // Realizar cálculos antes de enviar
                    RealizarCalculos(detallesNomina);

                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(detallesNomina);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DetallesNomina>(responseData);

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
        public async Task<DetallesNomina> EditDetallesNomina(int Id, DetallesNomina detallesNomina)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("DetallesNomina").Value + $"/{Id}";

                try
                {
                    // Realizar cálculos antes de enviar
                    RealizarCalculos(detallesNomina);

                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(detallesNomina);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DetallesNomina>(responseData);
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
        public async Task<bool> DeleteDetallesNomina(int? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("DetallesNomina").Value + $"/{Id}";
                try
                {
                    var response = await client.DeleteAsync(apiUrl);

                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //Active
        public async Task<bool> ActivateDetallesNomina(int? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("DetallesNomina").Value + $"/Activate/{Id}";
                try
                {
                    var response = await client.DeleteAsync(apiUrl);

                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void RealizarCalculos(DetallesNomina detallesNomina)
        {
            if (detallesNomina.CantidadHoras.HasValue && detallesNomina.Hora.HasValue)
            {
                detallesNomina.Diario = detallesNomina.CantidadHoras.Value * detallesNomina.Hora.Value;
                detallesNomina.Semanal = detallesNomina.Diario * 7;
                detallesNomina.Mensual = detallesNomina.Semanal * 4;
                detallesNomina.Pago = detallesNomina.Semanal;

            }

            if (detallesNomina.CantidadHorasExtra.HasValue && detallesNomina.HorasExtra.HasValue)
            {
                detallesNomina.Pago += detallesNomina.CantidadHorasExtra.Value * detallesNomina.HorasExtra.Value;
            }

            if (detallesNomina.Ccss.HasValue)
            {
                detallesNomina.Pago -= detallesNomina.Ccss.Value;
            }

            if (detallesNomina.Vales.HasValue)
            {
                detallesNomina.Pago -= detallesNomina.Vales.Value;
            }
        }
    }
}
