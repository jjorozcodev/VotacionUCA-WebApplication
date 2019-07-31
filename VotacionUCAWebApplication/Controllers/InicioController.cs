using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VotacionUCAWebApplication.Controllers
{
    public class InicioController : Controller
    {
        public ActionResult Acceso()
        {
            return View();
        }
        public ActionResult Gestion()
        {
            return View();
        }
        public ActionResult Votaciones()
        {
            return View();
        }
    }
}