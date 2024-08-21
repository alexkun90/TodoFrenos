using DAL.Models;
using System.Text;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class AppointmentService
    {
        private IConfiguration _config;

        public AppointmentService(IConfiguration config)
        {
            _config = config;
        }

        //GET INDEX
        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value;

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Appointment>>(responseData);

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

        public async Task<IEnumerable<Appointment>> GetMyAppointments(string Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/MyAppointments/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Appointment>>(responseData);

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

        public async Task<IEnumerable<Appointment>> GetMyPaper(string Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/MyPaperAppointments/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Appointment>>(responseData);

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
        public async Task<Appointment> GetAppointment(long? Id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/{Id}";

                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Appointment>(responseData);

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
        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value;

                try
                {
                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(appointment);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Appointment>(responseData);

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
        public async Task<Appointment> EditAppointment(long Id, Appointment appointment)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/{Id}";

                try
                {
                    string body = Newtonsoft.Json.JsonConvert.SerializeObject(appointment);
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Appointment>(responseData);
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

        public async Task<bool> AcceptAppointment(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/Accept/{id}";
                try
                {
                    var response = await client.PutAsync(apiUrl, null);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<bool> RejectAppointment(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/Reject/{id}";
                try
                {
                    var response = await client.PutAsync(apiUrl, null);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<bool> CancelAppointment(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/Cancel/{id}";
                try
                {
                    var response = await client.PutAsync(apiUrl, null);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<bool> PapeleraAppointment(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/Papelera/{id}";
                try
                {
                    var response = await client.PutAsync(apiUrl, null);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<bool> ReturnAppointment(long? id)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = _config.GetSection("UrlServicios").GetSection("Appointment").Value + $"/Return/{id}";
                try
                {
                    var response = await client.PutAsync(apiUrl, null);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }




    }
}


