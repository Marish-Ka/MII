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
   
    public class Принимаемые_препаратыController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();

        // GET: Принимаемые_препараты
        public async Task<ActionResult> Index()
        {
            var принимаемые_препараты = db.Принимаемые_препараты.Include(п => п.Спортсмены);
            return View(await принимаемые_препараты.ToListAsync());
        }
        [HttpPost]
        public ActionResult Index(string medication)
        {
            var принимаемые_препараты = db.Принимаемые_препараты.AsEnumerable();

            if (!String.IsNullOrEmpty(medication))
            {
                принимаемые_препараты = db.Принимаемые_препараты.Where(n => n.Медикаменты.Contains(medication));
            }


            return View(принимаемые_препараты.ToList());
        }
        // GET: Принимаемые_препараты/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Принимаемые_препараты принимаемые_препараты = await db.Принимаемые_препараты.FindAsync(id);
            if (принимаемые_препараты == null)
            {
                return HttpNotFound();
            }
            return View(принимаемые_препараты);
        }

        // GET: Принимаемые_препараты/Create
        public ActionResult Create()
        {
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена");
            return View();
        }

        // POST: Принимаемые_препараты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Номер_назначения_врачом,Медикаменты,Пищевые_добавки,Витамины,Минералы,Длительность_приема,Действие_назначения_врача,ID_спортсмена")] Принимаемые_препараты принимаемые_препараты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Принимаемые_препараты.Add(принимаемые_препараты);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", принимаемые_препараты.ID_спортсмена);
                return View(принимаемые_препараты);
            }
            catch
            {
                return View("ErrorCreate");
            }

        }

        // GET: Принимаемые_препараты/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Принимаемые_препараты принимаемые_препараты = await db.Принимаемые_препараты.FindAsync(id);
            if (принимаемые_препараты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", принимаемые_препараты.ID_спортсмена);
            return View(принимаемые_препараты);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Номер_назначения_врачом,Медикаменты,Пищевые_добавки,Витамины,Минералы,Длительность_приема,Действие_назначения_врача,ID_спортсмена")] Принимаемые_препараты принимаемые_препараты)
        {
            if (ModelState.IsValid)
            {
                db.Entry(принимаемые_препараты).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", принимаемые_препараты.ID_спортсмена);
            return View(принимаемые_препараты);
        }

        // GET: Принимаемые_препараты/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Принимаемые_препараты принимаемые_препараты = await db.Принимаемые_препараты.FindAsync(id);
            if (принимаемые_препараты == null)
            {
                return HttpNotFound();
            }
            return View(принимаемые_препараты);
        }

        // POST: Принимаемые_препараты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Принимаемые_препараты принимаемые_препараты = await db.Принимаемые_препараты.FindAsync(id);
            db.Принимаемые_препараты.Remove(принимаемые_препараты);
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
