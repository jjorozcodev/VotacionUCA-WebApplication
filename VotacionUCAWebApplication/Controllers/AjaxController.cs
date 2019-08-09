using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using VotacionUCAWebApplication.Models;

namespace VotacionUCAWebApplication.Controllers
{
    public class AjaxController : Controller
    {
        [HttpGet]
        public JsonResult ObtenerUsuarioActual()
        {
            string usuarioActual = Session["usuarioActual"].ToString();
            return Json(usuarioActual, JsonRequestBehavior.AllowGet);
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
        public async Task<JsonResult> ListarCandidatosVotacion()
        {
            List<Votaciones> votaciones = await ClienteWeb.ListarVotaciones();
            return Json(votaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Acceso(string Usuario)
        {
             Session["usuarioActual"] = Usuario.ToLower();
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
        public void AbrirCerrarVotacion()
        {
            //return Json("Correcto", JsonRequestBehavior.AllowGet);
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