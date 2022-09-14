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
    
    public class Антидопинговые_тестыController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();

        // GET: Антидопинговые_тесты
        public async Task<ActionResult> Index()
        {
            var антидопинговые_тесты = db.Антидопинговые_тесты.Include(а => а.Лаборанты).Include(а => а.Пробы);
            return View(await антидопинговые_тесты.ToListAsync());
        }
        [HttpPost]
        public ActionResult Index(int? number)
        {
            var антидопинговые_тесты = db.Антидопинговые_тесты.Include(а => а.Лаборанты).Include(а => а.Пробы);

            try
            {
                db.generate_storage_time(number);
                db.SaveChanges();
            }
            catch
            {
                return View();
            }

            return View(антидопинговые_тесты.ToList());
        }
        // GET: Антидопинговые_тесты/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Антидопинговые_тесты антидопинговые_тесты = await db.Антидопинговые_тесты.FindAsync(id);
            if (антидопинговые_тесты == null)
            {
                return HttpNotFound();
            }
            return View(антидопинговые_тесты);
        }

        // GET: Антидопинговые_тесты/Create
        public ActionResult Create()
        {
            ViewBag.ID_лаборанта = new SelectList(db.Лаборанты, "ID_лаборанта", "ID_лаборанта");
            ViewBag.ID_пробы = new SelectList(db.Пробы, "ID_пробы", "ID_пробы");
            return View(new Антидопинговые_тесты());
        }

        // POST: Антидопинговые_тесты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Номер_теста,Дата_проведения,Время_хранения_пробы,Вскрытие_пробы_B,Наличие_запрещенных_препаратов,Выявленные_запрещенные_препараты,Согласие_спортсмена_с_результатом,ID_пробы,ID_лаборанта")] Антидопинговые_тесты антидопинговые_тесты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Антидопинговые_тесты.Add(антидопинговые_тесты);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_лаборанта = new SelectList(db.Лаборанты, "ID_лаборанта", "ID_лаборанта", антидопинговые_тесты.ID_лаборанта);
                ViewBag.ID_пробы = new SelectList(db.Пробы, "ID_пробы", "ID_пробы", антидопинговые_тесты.ID_пробы);
                return View(антидопинговые_тесты);
            }
            catch
            {
                return View("ErrorCreate");
            }
        }

        // GET: Антидопинговые_тесты/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Антидопинговые_тесты антидопинговые_тесты = await db.Антидопинговые_тесты.FindAsync(id);
            if (антидопинговые_тесты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_лаборанта = new SelectList(db.Лаборанты, "ID_лаборанта", "ID_лаборанта", антидопинговые_тесты.ID_лаборанта);
            ViewBag.ID_пробы = new SelectList(db.Пробы, "ID_пробы", "ID_пробы", антидопинговые_тесты.ID_пробы);
            return View(антидопинговые_тесты);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Номер_теста,Дата_проведения,Время_хранения_пробы,Вскрытие_пробы_B,Наличие_запрещенных_препаратов,Выявленные_запрещенные_препараты,Согласие_спортсмена_с_результатом,ID_пробы,ID_лаборанта")] Антидопинговые_тесты антидопинговые_тесты)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(антидопинговые_тесты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.ID_лаборанта = new SelectList(db.Лаборанты, "ID_лаборанта", "ID_лаборанта", антидопинговые_тесты.ID_лаборанта);
                ViewBag.ID_пробы = new SelectList(db.Пробы, "ID_пробы", "ID_пробы", антидопинговые_тесты.ID_пробы);
                return View(антидопинговые_тесты);
            }
            catch
            {
                return View("Error");
            }

        }

        // GET: Антидопинговые_тесты/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Антидопинговые_тесты антидопинговые_тесты = await db.Антидопинговые_тесты.FindAsync(id);
            if (антидопинговые_тесты == null)
            {
                return HttpNotFound();
            }
            return View(антидопинговые_тесты);
        }

        // POST: Антидопинговые_тесты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Антидопинговые_тесты антидопинговые_тесты = await db.Антидопинговые_тесты.FindAsync(id);
            db.Антидопинговые_тесты.Remove(антидопинговые_тесты);
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
  