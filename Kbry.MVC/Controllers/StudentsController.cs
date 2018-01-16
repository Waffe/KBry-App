using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kbry.Data;
using Kbry.Data.Model;
using Kbry.Data.Repository;
using Microsoft.Ajax.Utilities;

namespace Kbry.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private KbryDbContext db = KbryDbContext.Create();
        private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IStudentRepository _repo;

        public StudentsController()
        {
            _repo = _unitOfWork.StudentRepository;
        }

        // GET: Students
        public async Task<ActionResult> Index(string searchString)
        {
            if (!searchString.IsNullOrWhiteSpace())
            {
                var formattedSearchString = searchString.ToLower().Trim();
                
                return View(await _repo.GetByPredicate(
                    s => ((s.FirstName.ToLower() + " " + s.LastName.ToLower()).Contains(formattedSearchString)
                                        || s.Email.ToLower().Contains(formattedSearchString)
                                        || s.Class.Name.ToLower().Contains(formattedSearchString)
                                        || s.RegistrationCode.ToLower().Contains(formattedSearchString)
                                        || s.RegistrationCode.ToLower().Contains(formattedSearchString))));
            }
            return View(await _repo.GetAllAsync());
        }


        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _repo.GetByIdAsync(Convert.ToInt32(id));
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegistrationCode,FirstName,LastName,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationCode,FirstName,LastName,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(student);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _repo.GetByIdAsync(Convert.ToInt32(id));
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _repo.Remove(id);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> FindStudent(string searchString)
        {
            var foundStudents = await _repo.GetByPredicate(s => s.FirstName.ToLower().Contains(searchString.ToLower()));
            return RedirectToAction("Index", new { students = foundStudents });
        }
    }
}
