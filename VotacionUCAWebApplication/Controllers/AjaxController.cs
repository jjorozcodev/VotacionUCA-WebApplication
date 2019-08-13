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
        public JsonResult CrearVotacion(string Descripcion, string CodGrupo, bool Abierto)
        {
            Votaciones nuevaVotacion = new Votaciones();

            nuevaVotacion.Abierto = Abierto;
            nuevaVotacion.Descripcion = Descripcion;
            nuevaVotacion.CodGrupo = CodGrupo;

            bool resp = ClienteWeb.CrearVotacion(nuevaVotacion);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void EditarVotacion()
        {
            //return Json("Correcto", JsonRequestBehavior.AllowGet);
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
        public void BorrarVotacion(int id)
        {
            //return Json("Correcto", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Votar()
        {
            //return Json("Correcto", JsonRequestBehavior.AllowGet);
        }
    }
}