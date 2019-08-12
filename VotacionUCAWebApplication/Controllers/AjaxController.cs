using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using VotacionUCAWebApplication.Models;

namespace VotacionUCAWebApplication.Controllers
{
    public class AjaxController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> Acceso(string Usuario, string Clave, bool TipoUsuario)
        {
            Usuarios objU = null;

            List<Usuarios> listaUsuarios = await ClienteWeb.ListarUsuarios();
            List<Estudiantes> listaEstudiantes = await ClienteWeb.ListarEstudiantes();
            Estudiantes est = null;

            foreach (Estudiantes e in listaEstudiantes)
            {
                if (e.NumCarnet == Usuario)
                {
                    est = e;
                    break;
                }
            }

            foreach (Usuarios u in listaUsuarios)
            {
                if(u.Usuario.ToLower() == Usuario.ToLower() && u.Clave == Clave)
                {
                    objU = u;
                    Session["idUsuarioActual"] = objU.Id;

                    if (est != null && TipoUsuario)
                    {
                        Session["usuarioActual"] = est.NombreCompleto;
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
        public async Task<JsonResult> ObtenerNombreVotacion(int id)
        {
            string nombrevotacion = "";
            List<Votaciones> votaciones = await ClienteWeb.ListarVotaciones();

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
        public async Task<JsonResult> ListarEstudiantes()
        {
            List<Estudiantes> estudiantes = await ClienteWeb.ListarEstudiantes();
            return Json(estudiantes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ListarVotaciones()
        {
            List<Votaciones> votaciones = await ClienteWeb.ListarVotaciones();
            return Json(votaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ListarVotacionesDisponibles()
        {
            List<Votaciones> votaciones = await ClienteWeb.ListarVotaciones();
            return Json(votaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ListarCandidatosVotacion(int id)
        {
            List<Candidatos> candidatos = await ClienteWeb.ListarCandidatos();
            List<Estudiantes> estudiantes = await ClienteWeb.ListarEstudiantes();

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
        public void CrearVotacion()
        {
            //return Json("Correcto", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void EditarVotacion()
        {
            //return Json("Correcto", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task AbrirCerrarVotacionAsync(int id)
        {
            List<Votaciones> votaciones = await ClienteWeb.ListarVotaciones();
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
        public void BorrarVotacion()
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