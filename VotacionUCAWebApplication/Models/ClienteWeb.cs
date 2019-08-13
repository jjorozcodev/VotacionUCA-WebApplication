using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;

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

        public static bool EditarVotacion(Votaciones votacion)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBWA);
                var putTask = cliente.PutAsJsonAsync($"api/votaciones/{votacion.Id}", votacion);
                putTask.Wait();
                var result = putTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public static bool EliminarVotacion(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBWA);
                var deleteTask = cliente.DeleteAsync($"api/votaciones/{id}");
                deleteTask.Wait();
                var result = deleteTask.Result;
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

        public static bool CrearCandidato(Candidatos candidato)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBWA);
                var postTask = cliente.PostAsJsonAsync<Candidatos>("api/candidatos", candidato);
                postTask.Wait();
                var result = postTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public static bool EditarCandidato(Candidatos candidato)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBWA);
                var putTask = cliente.PutAsJsonAsync($"api/candidatos/{candidato.Id}", candidato);
                putTask.Wait();
                var result = putTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public static bool EliminarCandidato(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBWA);
                var deleteTask = cliente.DeleteAsync($"api/candidatos/{id}");
                deleteTask.Wait();
                var result = deleteTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public static async Task<List<Votos>> ListarVotos()
        {
            List<Votos> votos = new List<Votos>();

            HttpClient _clienteHTTP = new HttpClient();
            _clienteHTTP.BaseAddress = new Uri(urlBWA);
            _clienteHTTP.DefaultRequestHeaders.Clear();
            _clienteHTTP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage res = await _clienteHTTP.GetAsync("api/votos");
            if (res.IsSuccessStatusCode)
            {
                var listRes = res.Content.ReadAsStringAsync().Result;
                votos = JsonConvert.DeserializeObject<List<Votos>>(listRes);
            }

            return votos;
        }

        public static bool CrearVoto(Votos voto)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBWA);
                var postTask = cliente.PostAsJsonAsync<Votos>("api/votos", voto);
                postTask.Wait();
                var result = postTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public static bool EliminarVoto(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(urlBWA);
                var deleteTask = cliente.DeleteAsync($"api/votos/{id}");
                deleteTask.Wait();
                var result = deleteTask.Result;
                return result.IsSuccessStatusCode;
            }
        }
    }
}