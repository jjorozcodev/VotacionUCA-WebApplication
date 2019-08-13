using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using VotacionUCAWebApplication.Models;

namespace VotacionUCAWebApplication.Controllers
{
    public class AjaxController : Controller
    {
        private List<Usuarios> usuarios = Task.Run(() => ClienteWeb.ListarUsuarios()).Result;
        private List<Estudiantes> estudiantes = Task.Run(() => ClienteWeb.ListarEstudiantes()).Result;
        private List<Votaciones> votaciones = Task.Run(() => ClienteWeb.ListarVotaciones()).Result;
        private List<Candidatos> candidatos = Task.Run(() => ClienteWeb.ListarCandidatos()).Result;
        private List<Votos> votos = Task.Run(() => ClienteWeb.ListarVotos()).Result;
        
        [HttpPost]
        public JsonResult Acceso(string Usuario, string Clave, bool TipoUsuario)
        {
            Estudiantes est = null;

            foreach (Estudiantes e in estudiantes)
            {
                if (e.NumCarnet == Usuario)
                {
                    est = e;
                    break;
                }
            }

            Usuarios objU = null;

            foreach (Usuarios u in usuarios)
            {
                if(u.Usuario.ToLower() == Usuario.ToLower() && u.Clave == Clave)
                {
                    objU = u;
                    Session["idUsuarioActual"] = objU.Id;

                    if (est != null && TipoUsuario)
                    {
                        Session["usuarioActual"] = est.NombreCompleto;
                        Session["grupoUActual"] = est.CodGrupo;
                        Session["idEstudiante"] = est.Id;
                    }
                    else if (est == null && !TipoUsuario)
                    {
                        Session["usuarioActual"] = objU.Usuario;
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }

                    break;
                }
            }

            if (objU == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(objU, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ObtenerUsuarioActual()
        {
            string usuarioActual = "";
            if (Session["usuarioActual"] != null)
            {
                usuarioActual = Session["usuarioActual"].ToString();
            }
            
            return Json(usuarioActual, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerNombreVotacion(int id)
        {
            string nombrevotacion = "";

            foreach (Votaciones v in votaciones)
            {
                if (v.Id == id)
                {
                    nombrevotacion = v.Descripcion;
                    break;
                }
            }

            return Json(nombrevotacion, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEstudiantes()
        {
            return Json(estudiantes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarVotaciones()
        {
            return Json(votaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarVotacionesDisponibles()
        {
            List<Votaciones> filtrado = new List<Votaciones>();
            string grupo = Session["grupoUActual"].ToString();

            foreach (Votaciones v in votaciones)
            {
                if(v.CodGrupo == grupo)
                {
                    filtrado.Add(v);
                    foreach (Votos vts in votos)
                    {
                        if (vts.IdEstudiante.ToString() == Session["idEstudiante"].ToString() && vts.IdVotacion == v.Id)
                        {
                            filtrado.Remove(v);
                        }
                    }
                }
            }
            
            return Json(filtrado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarCandidatosVotacion(int id)
        {
            List<Candidatos> candidatosFiltrados = new List<Candidatos>();

            foreach (Candidatos c in candidatos)
            {
                if(c.IdVotacion == id)
                {
                    foreach (Estudiantes e in estudiantes)
                    {
                        if (e.Id == c.IdEstudiante)
                        {
                            c.NombreCandidato = e.NombreCompleto;
                            break;
                        }
                    }

                    candidatosFiltrados.Add(c);
                }
            }

            return Json(candidatosFiltrados, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CrearVotacion(string Descripcion, string CodGrupo, bool Abierto, List<int> Seleccion)
        {
            Votaciones nuevaVotacion = new Votaciones();

            nuevaVotacion.Abierto = Abierto;
            nuevaVotacion.Descripcion = Descripcion;
            nuevaVotacion.CodGrupo = CodGrupo;

            bool resp = ClienteWeb.CrearVotacion(nuevaVotacion);

            if (resp)
            {
                votaciones = await ClienteWeb.ListarVotaciones();
                int idVot = votaciones[votaciones.Count - 1].Id;

                foreach(int i in Seleccion)
                {
                    Candidatos c = new Candidatos();
                    c.IdEstudiante = i;
                    c.IdVotacion = idVot;
                    c.VotosObtenidos = 0;

                    ClienteWeb.CrearCandidato(c);
                }
                candidatos = await ClienteWeb.ListarCandidatos();
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> EditarVotacion(int Id, string Descripcion, string CodGrupo, bool Abierto, List<int> Seleccion)
        {
            Votaciones votacionEditada = new Votaciones();

            votacionEditada.Id = Id;
            votacionEditada.Descripcion = Descripcion;
            votacionEditada.CodGrupo = CodGrupo;
            votacionEditada.Abierto = Abierto;

            bool resp = ClienteWeb.EditarVotacion(votacionEditada);

            if (resp)
            {
                votaciones = await ClienteWeb.ListarVotaciones();

                foreach(Candidatos c in candidatos)
                {
                    if(c.IdVotacion == Id)
                    {
                        ClienteWeb.EliminarCandidato(c.Id);
                    }
                }

                foreach (int i in Seleccion)
                {
                    Candidatos c = new Candidatos();
                    c.IdEstudiante = i;
                    c.IdVotacion = Id;
                    c.VotosObtenidos = 0;

                    ClienteWeb.CrearCandidato(c);
                }
                candidatos = await ClienteWeb.ListarCandidatos();
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AbrirCerrarVotacion(int id)
        {
            foreach(Votaciones v in votaciones)
            {
                if(v.Id == id)
                {
                    v.Abierto = !v.Abierto;
                    break;
                }
            }
        }

        [HttpPost]
        public void BorrarVotacion(int IdVotacion)
        {
            foreach (Votaciones v in votaciones)
            {
                if (v.Id == IdVotacion)
                {
                    if (ClienteWeb.EliminarVotacion(IdVotacion))
                    {
                        foreach (Candidatos c in candidatos)
                        {
                            if (c.IdVotacion == IdVotacion)
                            {
                                ClienteWeb.EliminarCandidato(c.Id);
                            }
                        }
                    }
                    break;
                }
            }
        }

        [HttpPost]
        public void Votar(int IdVotacion, int IdCandidato)
        {
            Votos voto = new Votos();
            voto.IdEstudiante = Convert.ToInt32(Session["idEstudiante"]);
            voto.IdVotacion = IdVotacion;

            Candidatos candidatoSeleccionado = null;
            foreach(Candidatos c in candidatos)
            {
                if(c.Id == IdCandidato)
                {
                    candidatoSeleccionado = c;
                    break;
                }
            }

            candidatoSeleccionado.VotosObtenidos++;

            if (ClienteWeb.EditarCandidato(candidatoSeleccionado))
            {
                ClienteWeb.CrearVoto(voto);
            }
        }
    }
}