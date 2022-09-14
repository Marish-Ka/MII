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

namespace cw2.Controllers
{
    
    public class Активность_спортсменаController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();

        // GET: Активность_спортсмена
        public async Task<ActionResult> Index()
        {
            var активность_спортсмена = db.Активность_спортсмена.Include(а => а.Спортсмены);
            return View(await активность_спортсмена.ToListAsync());
        }
        [HttpPost]
        public ActionResult Index(string location, int? number, string submit)
        {
            var активность_спортсмена = db.Активность_спортсмена.AsEnumerable();

            switch (submit)
            {
                case "Найти":

                    {
                        if (!String.IsNullOrEmpty(location))
                        {
                            активность_спортсмена = db.Активность_спортсмена.Where(n => n.Местонахождение_.Contains(location));
                        }
                        break;
                    }

                case "Ok":

                    {
                        try
                        {
                            db.editing_location(number);
                            db.SaveChanges();
                        }
                        catch
                        {
                            return View("Error");
                        }
                        break;
                    }
                default:
                    break;
            }
            return View(активность_спортсмена.ToList());
        }
        // GET: Активность_спортсмена/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Активность_спортсмена активность_спортсмена = await db.Активность_спортсмена.FindAsync(id);
            if (активность_спортсмена == null)
            {
                return HttpNotFound();
            }
            return View(активность_спортсмена);
        }

        // GET: Активность_спортсмена/Create
        public ActionResult Create()
        {
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена");
            return View();
        }

        // POST: Активность_спортсмена/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Номер_активости_,Дата,Местонахождение_,Деятельность,Свободный_час,ID_спортсмена")] Активность_спортсмена активность_спортсмена)
        {
            if (ModelState.IsValid)
            {
                db.Активность_спортсмена.Add(активность_спортсмена);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", активность_спортсмена.ID_спортсмена);
            return View(активность_спортсмена);
        }

        // GET: Активность_спортсмена/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Активность_спортсмена активность_спортсмена = await db.Активность_спортсмена.FindAsync(id);
            if (активность_спортсмена == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", активность_спортсмена.ID_спортсмена);
            return View(активность_спортсмена);
        }

        // POST: Активность_спортсмена/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Номер_активости_,Дата,Местонахождение_,Деятельность,Свободный_час,ID_спортсмена")] Активность_спортсмена активность_спортсмена)
        {
            if (ModelState.IsValid)
            {
                db.Entry(активность_спортсмена).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", активность_спортсмена.ID_спортсмена);
            return View(активность_спортсмена);
        }

        // GET: Активность_спортсмена/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Активность_спортсмена активность_спортсмена = await db.Активность_спортсмена.FindAsync(id);
            if (активность_спортсмена == null)
            {
                return HttpNotFound();
            }
            return View(активность_спортсмена);
        }

        // POST: Активность_спортсмена/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Активность_спортсмена активность_спортсмена = await db.Активность_спортсмена.FindAsync(id);
            db.Активность_спортсмена.Remove(активность_спортсмена);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
