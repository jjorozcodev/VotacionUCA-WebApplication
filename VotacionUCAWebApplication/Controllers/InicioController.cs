using System.Web.Mvc;

namespace VotacionUCAWebApplication.Controllers
{
    public class InicioController : Controller
    {
        public ActionResult Acceso()
        {
            if (!(Session["idUsuarioActual"] ==  null))
            {
                if (Session["usuarioActual"].ToString().Equals("admin"))
                {
                    RedirectToAction("Gestion");
                }
                else
                {
                    RedirectToAction("Votaciones");
                }
            }
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