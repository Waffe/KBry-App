using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kbry.Data;
using Kbry.Data.Model;
using Kbry.MVC.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Kbry.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly KbryDbContext _db = KbryDbContext.Create();

        public ActionResult Index(string searchString)
        {
            return View();
        }

        public async Task<ActionResult> GetStudents([DataSourceRequest]DataSourceRequest request)
        {
            var allStudents = await _db.Students.ToListAsync();
            var allStudentsVM = new List<StudentViewModel>();

            foreach (var student in allStudents)
            {
                allStudentsVM.Add(ConvertToViewModel(student));
            }
            return Json(allStudentsVM.ToDataSourceResult(request));
        }


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
                edited.SchoolClass = _db.Classes.FirstOrDefault(c => c.Id == student.SchoolClassId);
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

        private Student ConvertToModel(StudentViewModel student)
        {
            return new Student()
            {
                LastName = student.FullName.Split(' ').Last().Trim(','),
                FirstName = student.FullName.Split(' ').First().Trim(','),
                Email = student.Email,
                Attendances = student.Attendances,
                RegistrationCode = student.RegistrationCode,
                SchoolClass = _db.Classes.FirstOrDefault(c => c.Id == Convert.ToInt32(student.SchoolClassId))
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

        private StudentViewModel ConvertToViewModel(Student s)
        {
            return new StudentViewModel()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                RegistrationCode = s.RegistrationCode,
                Attendances = s.Attendances,
                SchoolClassName = s.SchoolClass?.Name ?? string.Empty,
                SchoolClassId = s.SchoolClass?.Id
            };
        }

        public ActionResult GetClasses()
        {
            var schoolClasses = _db.Classes.ToList();
            return Json(schoolClasses, JsonRequestBehavior.AllowGet);
        }
    }
}
