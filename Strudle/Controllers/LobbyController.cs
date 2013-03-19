using System;
using System.Web.Mvc;

namespace Strudle.Controllers
{
    public class LobbyController : Controller
    {
        //
        // GET: /Lobby/

        public ActionResult Index()
        {           
            //för tillfället simulerar en användare
            ViewBag.randomUser = new Random(400).Next(); ;
            return View();
               
        }

    }
}
