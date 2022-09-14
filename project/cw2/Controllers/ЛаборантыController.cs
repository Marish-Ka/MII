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
    
    public class ЛаборантыController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();

        // GET: Лаборанты
        public async Task<ActionResult> Index()
        {
            var лаборанты = db.Лаборанты.Include(л => л.Персональные_данные);
            return View(await лаборанты.ToListAsync());
        }

        // GET: Лаборанты/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Лаборанты лаборанты = await db.Лаборанты.FindAsync(id);
            if (лаборанты == null)
            {
                return HttpNotFound();
            }
            return View(лаборанты);
        }

        // GET: Лаборанты/Create
        public ActionResult Create()
        {
            ViewBag.ID_лаборанта = new SelectList(db.Персональные_данные, "ID", "Фамилия");
            return View();
        }

        // POST: Лаборанты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_лаборанта,Категория,Место_работы,Стаж__в_годах_")] Лаборанты лаборанты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Лаборанты.Add(лаборанты);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_лаборанта = new SelectList(db.Персональные_данные, "ID", "Фамилия", лаборанты.ID_лаборанта);
                return View(лаборанты);
            }
            catch
            {
                return View("ErrorCreates");
            }
        }

        // GET: Лаборанты/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Лаборанты лаборанты = await db.Лаборанты.FindAsync(id);
            if (лаборанты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_лаборанта = new SelectList(db.Персональные_данные, "ID", "Фамилия", лаборанты.ID_лаборанта);
            return View(лаборанты);
        }

        // POST: Лаборанты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_лаборанта,Категория,Место_работы,Стаж__в_годах_")] Лаборанты лаборанты)
        {
            if (ModelState.IsValid)
            {
                db.Entry(лаборанты).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_лаборанта = new SelectList(db.Персональные_данные, "ID", "Фамилия", лаборанты.ID_лаборанта);
            return View(лаборанты);
        }

        // GET: Лаборанты/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Лаборанты лаборанты = await db.Лаборанты.FindAsync(id);
                if (лаборанты == null)
                {
                    return HttpNotFound();
                }
                return View(лаборанты);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Лаборанты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Лаборанты лаборанты = await db.Лаборанты.FindAsync(id);
                db.Лаборанты.Remove(лаборанты);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
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
