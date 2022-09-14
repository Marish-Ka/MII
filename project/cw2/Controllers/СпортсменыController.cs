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
    public class СпортсменыController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();

        // GET: Спортсмены
        
        public async Task<ActionResult> Index(string sports_category, string sprots_citizenship)
        {
            var спортсмены = db.Спортсмены.Include(с => с.Персональные_данные).Include(с => с.Тренера);
            if (!String.IsNullOrEmpty(sports_category) && !sports_category.Equals("null"))
            {
                спортсмены = спортсмены.Where(p => p.Спортивное__ый__звание___разряд.
                Substring(0, sports_category.Length) == sports_category);
            }
            if (!String.IsNullOrEmpty(sprots_citizenship) && !sprots_citizenship.Equals("null"))
            {
                string default_val = "Российская федерация";
                if (sprots_citizenship == default_val)
                    спортсмены = спортсмены.Where(p => p.Спортивное_гражданство == sprots_citizenship);
                else
                    спортсмены = спортсмены.Where(p => p.Спортивное_гражданство != default_val);
            }
            return View(await спортсмены.ToListAsync());

        }


        // GET: Спортсмены/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Спортсмены спортсмены = await db.Спортсмены.FindAsync(id);
            if (спортсмены == null)
            {
                return HttpNotFound();
            }
            return View(спортсмены);
        }

        // GET: Спортсмены/Create
        public ActionResult Create()
        {
            ViewBag.ID_спортсмена = new SelectList(db.Персональные_данные, "ID", "Фамилия");
            ViewBag.ID_главного_тренера = new SelectList(db.Тренера, "ID_тренера", "ID_тренера");
            return View();
        }

        // POST: Спортсмены/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_спортсмена,Вид_спорта,Спортивная_дисциплина,Спортивное__ый__звание___разряд,Гражданство,Спортивное_гражданство,Инвалидность,Группа_инвалидности,ID_главного_тренера")] Спортсмены спортсмены)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Спортсмены.Add(спортсмены);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_спортсмена = new SelectList(db.Персональные_данные, "ID", "Фамилия", спортсмены.ID_спортсмена);
                ViewBag.ID_главного_тренера = new SelectList(db.Тренера, "ID_тренера", "ID_тренера", спортсмены.ID_главного_тренера);
                return View(спортсмены);
            }
            catch
            {
                return View("ErrorCreates");
            }
        }

        // GET: Спортсмены/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Спортсмены спортсмены = await db.Спортсмены.FindAsync(id);
            if (спортсмены == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_спортсмена = new SelectList(db.Персональные_данные, "ID", "Фамилия", спортсмены.ID_спортсмена);
            ViewBag.ID_главного_тренера = new SelectList(db.Тренера, "ID_тренера", "ID_тренера", спортсмены.ID_главного_тренера);
            return View(спортсмены);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_спортсмена,Вид_спорта,Спортивная_дисциплина,Спортивное__ый__звание___разряд,Гражданство,Спортивное_гражданство,Инвалидность,Группа_инвалидности,ID_главного_тренера")] Спортсмены спортсмены)
        {
            if (ModelState.IsValid)
            {
                db.Entry(спортсмены).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_спортсмена = new SelectList(db.Персональные_данные, "ID", "Фамилия", спортсмены.ID_спортсмена);
            ViewBag.ID_главного_тренера = new SelectList(db.Тренера, "ID_тренера", "ID_тренера", спортсмены.ID_главного_тренера);
            return View(спортсмены);
        }

        // GET: Спортсмены/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Спортсмены спортсмены = await db.Спортсмены.FindAsync(id);
                if (спортсмены == null)
                {
                    return HttpNotFound();
                }
                return View(спортсмены);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Спортсмены/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Спортсмены спортсмены = await db.Спортсмены.FindAsync(id);
                db.Спортсмены.Remove(спортсмены);
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
