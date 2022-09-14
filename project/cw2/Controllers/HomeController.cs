using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cw2.Models;
using System.Web.Security;

namespace cw2.Controllers
{
    public class HomeController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Role()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Role(string role)
        {
            if (ModelState.IsValid)
            {
               
                if (role != "null")
                {
                    ViewBag.mes = " ";
                    Session["role"] = role;
                    if(role=="Тренер")
                        return RedirectToAction("Index", "Тренера");
                    else if(role== "Спортсмен")
                        return RedirectToAction("Index", "Спортсмены");
                    else if(role=="Лаборант")
                        return RedirectToAction("Index", "Лаборанты");
                    else
                        return RedirectToAction("Index", "Персональные_данные");
                    
                }
                else
                {
                    ViewBag.mes = "Вы не выбрали роль";
                }
            }

            return View();
        }


        
        public ActionResult Logoff()
        {
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("role", ""));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       
    }
}