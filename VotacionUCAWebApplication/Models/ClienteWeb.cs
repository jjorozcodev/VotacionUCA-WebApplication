using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace VotacionUCAWebApplication.Models
{
    public class ClienteWeb
    {
        private static readonly string urlBWA = WebConfigurationManager.AppSettings["urlBaseWebApi"];

        public static async Task<List<Estudiantes>> ListarEstudiantes()
        {
            List<Estudiantes> estudiantes = new List<Estudiantes>();

            HttpClient _clienteHTTP = new HttpClient();
            _clienteHTTP.BaseAddress = new Uri(urlBWA);
            _clienteHTTP.DefaultRequestHeaders.Clear();
            _clienteHTTP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage res = await _clienteHTTP.GetAsync("api/estudiantes");
            if (res.IsSuccessStatusCode)
            {
                var listRes = res.Content.ReadAsStringAsync().Result;
                estudiantes = JsonConvert.DeserializeObject<List<Estudiantes>>(listRes);
            }

            return estudiantes;
        }

        public static async Task<List<Votaciones>> ListarVotaciones()
        {
            List<Votaciones> votaciones = new List<Votaciones>();

            HttpClient _clienteHTTP = new HttpClient();
            _clienteHTTP.BaseAddress = new Uri(urlBWA);
            _clienteHTTP.DefaultRequestHeaders.Clear();
            _clienteHTTP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage res = await _clienteHTTP.GetAsync("api/votaciones");
            if (res.IsSuccessStatusCode)
            {
                var listRes = res.Content.ReadAsStringAsync().Result;
                votaciones = JsonConvert.DeserializeObject<List<Votaciones>>(listRes);
            }

            return votaciones;
        }
    }
}