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
    
    public class ПробыController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();

        // GET: Пробы
        public async Task<ActionResult> Index()
        {
            var пробы = db.Пробы.Include(п => п.Лаборанты).Include(п => п.Спортсмены);
            return View(await пробы.ToListAsync());
        }

        // GET: Пробы/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пробы пробы = await db.Пробы.FindAsync(id);
            if (пробы == null)
            {
                return HttpNotFound();
            }
            return View(пробы);
        }

        // GET: Пробы/Create
        public ActionResult Create()
        {
            ViewBag.ID_лаборанта = new SelectList(db.Лаборанты, "ID_лаборанта", "ID_лаборанта");
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена");
            return View();
        }

        // POST: Пробы/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_пробы,Дата_забора,Вид_пробы,Объем_пробы__мл_,Плотность__г_л_,Наличие_проб_A_и_B,ID_спортсмена,ID_лаборанта")] Пробы пробы)
        {
            if (ModelState.IsValid)
            {
                db.Пробы.Add(пробы);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_лаборанта = new SelectList(db.Лаборанты, "ID_лаборанта", "ID_лаборанта", пробы.ID_лаборанта);
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", пробы.ID_спортсмена);
            return View(пробы);
        }

        // GET: Пробы/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пробы пробы = await db.Пробы.FindAsync(id);
            if (пробы == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_лаборанта = new SelectList(db.Лаборанты, "ID_лаборанта", "ID_лаборанта", пробы.ID_лаборанта);
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", пробы.ID_спортсмена);
            return View(пробы);
        }

        // POST: Пробы/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_пробы,Дата_забора,Вид_пробы,Объем_пробы__мл_,Плотность__г_л_,Наличие_проб_A_и_B,ID_спортсмена,ID_лаборанта")] Пробы пробы)
        {
            if (ModelState.IsValid)
            {
                db.Entry(пробы).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_лаборанта = new SelectList(db.Лаборанты, "ID_лаборанта", "ID_лаборанта", пробы.ID_лаборанта);
            ViewBag.ID_спортсмена = new SelectList(db.Спортсмены, "ID_спортсмена", "ID_спортсмена", пробы.ID_спортсмена);
            return View(пробы);
        }

        // GET: Пробы/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пробы пробы = await db.Пробы.FindAsync(id);
            if (пробы == null)
            {
                return HttpNotFound();
            }
            return View(пробы);
        }

        // POST: Пробы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Пробы пробы = await db.Пробы.FindAsync(id);
            db.Пробы.Remove(пробы);
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
