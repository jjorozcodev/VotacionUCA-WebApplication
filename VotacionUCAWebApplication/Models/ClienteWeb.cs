using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;

namespace VotacionUCAWebApplication.Models
{
    public class ClienteWeb
    {
        public static readonly string urlBWA = WebConfigurationManager.AppSettings["urlBaseWebApi"];

        public static async Task<List<Usuarios>> ListarUsuarios()
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            HttpClient _clienteHTTP = new HttpClient();
            _clienteHTTP.BaseAddress = new Uri(urlBWA);
            _clienteHTTP.DefaultRequestHeaders.Clear();
            _clienteHTTP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage res = await _clienteHTTP.GetAsync("api/usuarios");
            if (res.IsSuccessStatusCode)
            {
                var listRes = res.Content.ReadAsStringAsync().Result;
                usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(listRes);
            }

            return usuarios;
        }

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
        
        public static bool CrearVotacion(Votaciones votacion)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBWA);
                var postTask = cliente.PostAsJsonAsync<Votaciones>("api/votaciones", votacion);
                postTask.Wait();
                var result = postTask.Result;
                return result.IsSuccessStatusCode;
             }
        }

        public static async Task<List<Candidatos>> ListarCandidatos()
        {
            List<Candidatos> candidatos = new List<Candidatos>();

            HttpClient _clienteHTTP = new HttpClient();
            _clienteHTTP.BaseAddress = new Uri(urlBWA);
            _clienteHTTP.DefaultRequestHeaders.Clear();
            _clienteHTTP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage res = await _clienteHTTP.GetAsync("api/candidatos");
            if (res.IsSuccessStatusCode)
            {
                var listRes = res.Content.ReadAsStringAsync().Result;
                candidatos = JsonConvert.DeserializeObject<List<Candidatos>>(listRes);
            }

            return candidatos;
        }
    }
}