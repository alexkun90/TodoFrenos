using DAL.Models;
using ProyectoTodoFrenosWeb.ViewModels;
using System.Net;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class PlayrollService
    {
        IConfiguration _config;
        private readonly HttpClientService clientService;
        public PlayrollService(IConfiguration config, HttpClientService clientService)
        {
            this._config = config;
            this.clientService = clientService;
        }

        public async Task<IEnumerable<PlayRollDTO>> GetAllPlayrolls()
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Playroll").Value;

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<PlayRollDTO>>(responseData);

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

        public async Task<PlayRollDTO> GetPlayrollDetails(long nominaId)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Playroll").Value + $"/{nominaId}";

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayRollDTO>(responseData);

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

        public async Task<PlayrollDetail> CreatePlayroll(long employeeId, PlayrollDetail model)
        {
            var client = clientService.CreateClient();
            var apiUrl = _config.GetSection("UrlServicios").GetSection("Playroll").Value + $"/{employeeId}";

            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayrollDetail>(responseData);

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
