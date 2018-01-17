using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kbry.Data;
using Kbry.Data.Model;
using Kbry.Data.Repository;
using Kbry.MVC.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;

namespace Kbry.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly KbryDbContext _db = KbryDbContext.Create();
        //private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
        //private readonly IStudentRepository _repo;

        public StudentsController()
        {
            //var svej = _db.Attendances.FirstOrDefault(a => a.Id == 6);
            //if (svej != null)
            //    _db.Attendances.Remove(svej);
            

            //var hej = _db.Students.FirstOrDefault(s => s.Id == 6);
            //if (hej != null)
            //    _db.Students.Remove(hej);

            //_db.SaveChanges();
        }

        // GET: Students
        public ActionResult Index(string searchString)
        {
            //if (!searchString.IsNullOrWhiteSpace())
            //{
            //    IEnumerable<Student> foundStudents =FindStudents(searchString);

            //    return View(foundStudents.Select(ConvertToViewModel));
            //}

            //var allStudents = _db.Students;
            //var allStudentsVM = new List<StudentViewModel>();

            //foreach (var student in allStudents)
            //{
            //    allStudentsVM.Add(ConvertToViewModel(student));
            //}
            return View();
        }

        public async Task<ActionResult> Customers_Read([DataSourceRequest]DataSourceRequest request)
        {
            var allStudents = await _db.Students.ToListAsync();
            var allStudentsVM = new List<StudentViewModel>();

            foreach (var student in allStudents)
            {
                allStudentsVM.Add(ConvertToViewModel(student));
            }
            return Json(allStudentsVM.ToDataSourceResult(request));
        }

        private IEnumerable<Student> FindStudents(string searchString)
        {
            var formattedSearchString = searchString.ToLower().Trim();
            var foundStudents = _db.Students.Where(
                s => ((s.FirstName.ToLower() + " " + s.LastName.ToLower()).Contains(formattedSearchString)
                      || s.Email.ToLower().Contains(formattedSearchString)
                      || s.SchoolClass.Name.ToLower().Contains(formattedSearchString)
                      || s.RegistrationCode.ToLower().Contains(formattedSearchString)
                      || s.RegistrationCode.ToLower().Contains(formattedSearchString)));
            return foundStudents;
        }


        // GET: Students/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = await _repo.GetByIdAsync(Convert.ToInt32(id));
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        private StudentViewModel ConvertToViewModel(Student s)
        {
            return new StudentViewModel()
            {
                Id = s.Id,
                FullName = $"{s.LastName}, {s.FirstName}",
                Email = s.Email,
                RegistrationCode = s.RegistrationCode,
                Attendances = s.Attendances,
                SchoolClassName = s.SchoolClass?.Name ?? string.Empty
            };
        }

        private IEnumerable<StudentViewModel> ConvertManyToViewModel(IEnumerable<Student> students)
        {
            var studentViewModels = new List<StudentViewModel>();

            foreach (var student in students)
            {
                studentViewModels.Add(ConvertToViewModel(student));
            }

            return studentViewModels;
        }

        // GET: Students/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Students/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,RegistrationCode,FirstName,LastName,Email")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Students.Add(student);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(student);
        //}

        // GET: Students/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = _db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        //// POST: Students/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,RegistrationCode,FirstName,LastName,Email")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _repo.Update(student);
        //        _unitOfWork.Save();
        //        return RedirectToAction("Index");
        //    }
        //    return View(student);
        //}

        // GET: Students/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = await _repo.GetByIdAsync(Convert.ToInt32(id));
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        //// POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Student student = _db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    _repo.Remove(id);
        //    _unitOfWork.Save();
        //    return RedirectToAction("Index");
        //}


        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request, string searchString)
        {
            var students = _db.Students.Include(s => s.SchoolClass).ToList();
            
            var studentViewModels = ConvertManyToViewModel(students).ToList();
            return Json(studentViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, StudentViewModel student)
        {
            if (student != null && ModelState.IsValid)
            {
                var studentToAdd = ConvertToModel(student);
                _db.Students.Add(studentToAdd);
                _db.SaveChanges();
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }

        private Student ConvertToModel(StudentViewModel student)
        {
            return new Student()
            {
                LastName = student.FullName.Split(' ').First().Trim(','),
                FirstName = student.FullName.Split(' ').Last().Trim(','),
                Email = student.Email,
                Attendances = student.Attendances,
                RegistrationCode = student.RegistrationCode
                //SchoolClass = student.SchoolClassName
            };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, StudentViewModel student)
        {
            if (student != null && ModelState.IsValid)
            {
                var edited = _db.Students.FirstOrDefault(s => s.Id == student.Id);
                edited.Id = student.Id;
                edited.FirstName = student.FullName.Split(' ').First().Trim(',');
                edited.LastName = student.FullName.Split(' ').Last().Trim(',');
                edited.Email = student.Email;
                edited.Attendances = student.Attendances;
                //edited.SchoolClass = student.SchoolClass;
                _db.Students.AddOrUpdate(edited);
                _db.SaveChanges();
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, StudentViewModel student)
        {
            if (student != null)
            {
                var studentToRemove = _db.Students.FirstOrDefault(s => s.Id == student.Id);
                _db.Students.Remove(studentToRemove);
                _db.SaveChanges();
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }
    }
}
