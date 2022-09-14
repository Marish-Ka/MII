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
   
    public class Персональные_данныеController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();

        // GET: Персональные_данные
        public async Task<ActionResult> Index()
        {
            var персональные_данные = db.Персональные_данные.Include(п => п.Лаборанты).Include(п => п.Спортсмены).Include(п => п.Тренера);
            return View(await персональные_данные.ToListAsync());
        }

        // GET: Персональные_данные/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Персональные_данные персональные_данные = await db.Персональные_данные.FindAsync(id);
            if (персональные_данные == null)
            {
                return HttpNotFound();
            }
            return View(персональные_данные);
        }

        // GET: Персональные_данные/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Лаборанты, "ID_лаборанта", "Категория");
            ViewBag.ID = new SelectList(db.Спортсмены, "ID_спортсмена", "Вид_спорта");
            ViewBag.ID = new SelectList(db.Тренера, "ID_тренера", "Преподаваемый_вид_спорта");
            return View();
        }

        // POST: Персональные_данные/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Фамилия,Имя,Отчество,Пол,Дата_рождения,Номер_телефона,Статус")] Персональные_данные персональные_данные)
        {
            if (ModelState.IsValid)
            {
                db.Персональные_данные.Add(персональные_данные);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Лаборанты, "ID_лаборанта", "Категория", персональные_данные.ID);
            ViewBag.ID = new SelectList(db.Спортсмены, "ID_спортсмена", "Вид_спорта", персональные_данные.ID);
            ViewBag.ID = new SelectList(db.Тренера, "ID_тренера", "Преподаваемый_вид_спорта", персональные_данные.ID);
            return View(персональные_данные);
        }

        // GET: Персональные_данные/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Персональные_данные персональные_данные = await db.Персональные_данные.FindAsync(id);
            if (персональные_данные == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Лаборанты, "ID_лаборанта", "Категория", персональные_данные.ID);
            ViewBag.ID = new SelectList(db.Спортсмены, "ID_спортсмена", "Вид_спорта", персональные_данные.ID);
            ViewBag.ID = new SelectList(db.Тренера, "ID_тренера", "Преподаваемый_вид_спорта", персональные_данные.ID);
            return View(персональные_данные);
        }

        // POST: Персональные_данные/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Фамилия,Имя,Отчество,Пол,Дата_рождения,Номер_телефона,Статус")] Персональные_данные персональные_данные)
        {
            if (ModelState.IsValid)
            {
                db.Entry(персональные_данные).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Лаборанты, "ID_лаборанта", "Категория", персональные_данные.ID);
            ViewBag.ID = new SelectList(db.Спортсмены, "ID_спортсмена", "Вид_спорта", персональные_данные.ID);
            ViewBag.ID = new SelectList(db.Тренера, "ID_тренера", "Преподаваемый_вид_спорта", персональные_данные.ID);
            return View(персональные_данные);
        }

        // GET: Персональные_данные/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Персональные_данные персональные_данные = await db.Персональные_данные.FindAsync(id);
                if (персональные_данные == null)
                {
                    return HttpNotFound();
                }
                return View(персональные_данные);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Персональные_данные/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Персональные_данные персональные_данные = await db.Персональные_данные.FindAsync(id);
                db.Персональные_данные.Remove(персональные_данные);
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
