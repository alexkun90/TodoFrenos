using DAL.Models;
using System.Net;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class PlayrollService
    {
        IConfiguration _config;
        public PlayrollService(IConfiguration config)
        {
            this._config = config;
        }

        public async Task<PlayrollDetail> CreatePlayroll(PlayrollDetail model)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Playroll").Value;

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
}
