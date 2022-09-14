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
using System.Data.SqlClient;

namespace cw2.Controllers
{
    public class ТренераController : Controller
    {
        private Антидопинговое_агенствоEntities db = new Антидопинговое_агенствоEntities();

        // GET: Тренера
        public async Task<ActionResult> Index(string qualification, bool? certification)
        {
            var тренера = db.Тренера.Include(т => т.Персональные_данные);
            if (!Boolean.Equals(certification, null))
            {
                тренера = тренера.Where(p => p.Сертификация_текущего_года_по_антидопингу == certification);
            }
            if (!String.IsNullOrEmpty(qualification) && !qualification.Equals("null"))
            {
                тренера = тренера.Where(p => p.Квалификация == qualification);
            }
            ViewBag.func = " ";
            return View(await тренера.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult> Index(int? number)
        {
            var тренера = db.Тренера.Include(т => т.Персональные_данные);
            if (number == null)
                number = 0;
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Антидопинговое_агенство;Integrated Security=True";

                // Создание подключения
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    SqlCommand command = new SqlCommand("SELECT dbo.rating(@id_coach)", connection);
                    command.Parameters.AddWithValue("@id_coach", number);

                    // Открываем подключение
                    connection.Open();
                    if (((decimal)command.ExecuteScalar()) != 0)
                        ViewBag.func = ((decimal)command.ExecuteScalar()).ToString();

                    else
                        ViewBag.func = "Вы ввели не существующий номер тренера";
                }


                catch
                {
                    return View("Error");

                }
                finally
                {
                    // закрываем подключение
                    connection.Close();
                }


            }
            catch
            {
                return View("Error");
            }


                return View(await тренера.ToListAsync()); 
        }
        // GET: Тренера/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Тренера тренера = await db.Тренера.FindAsync(id);
            if (тренера == null)
            {
                return HttpNotFound();
            }
            return View(тренера);
        }
        // GET: Тренера/Create
        public ActionResult Create()
        {
            ViewBag.ID_тренера = new SelectList(db.Персональные_данные, "ID", "Фамилия");
            return View();
        }

        // POST: Тренера/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_тренера,Преподаваемый_вид_спорта,Квалификация,Сертификация_текущего_года_по_антидопингу,Наличие_дисквалификаций")] Тренера тренера)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Тренера.Add(тренера);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_тренера = new SelectList(db.Персональные_данные, "ID", "Фамилия", тренера.ID_тренера);
                return View(тренера);
            }
            catch
            {
                return View("ErrorCreates");
            }
        }

        // GET: Тренера/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Тренера тренера = await db.Тренера.FindAsync(id);
            if (тренера == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_тренера = new SelectList(db.Персональные_данные, "ID", "Фамилия", тренера.ID_тренера);
            return View(тренера);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_тренера,Преподаваемый_вид_спорта,Квалификация,Сертификация_текущего_года_по_антидопингу,Наличие_дисквалификаций")] Тренера тренера)
        {
            if (ModelState.IsValid)
            {
                db.Entry(тренера).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_тренера = new SelectList(db.Персональные_данные, "ID", "Фамилия", тренера.ID_тренера);
            return View(тренера);
        }

        // GET: Тренера/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Тренера тренера = await db.Тренера.FindAsync(id);
                if (тренера == null)
                {
                    return HttpNotFound();
                }
                return View(тренера);
            }
            catch(Exception mes)
            {
                ViewBag.Message = mes;
                return View("Error");
              
            }
        }

        // POST: Тренера/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Тренера тренера = await db.Тренера.FindAsync(id);
                db.Тренера.Remove(тренера);
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
