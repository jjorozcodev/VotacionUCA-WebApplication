using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VotacionUCAWebApplication.Controllers
{
    public class VotacionesController : Controller
    {
        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Editar()
        {
            return View();
        }

        public ActionResult Candidatos()
        {
            return View();
        }
        
        public ActionResult Votar()
        {
            return View();
        }

        public ActionResult Resultados()
        {
            return View();
        }
    }
}