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
using Kbry.MVC.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Kbry.MVC.Controllers
{
    public class ClassesController : Controller
    {
        private readonly KbryDbContext _db = new KbryDbContext();

        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> GetSchoolClasses([DataSourceRequest]DataSourceRequest request)
        {
            var allClasses = await _db.Classes.Include(c => c.Students).ToListAsync();
            var schoolClassViewModels = new List<SchoolClassViewModel>();
            foreach (var schoolClass in allClasses)
            {
                schoolClassViewModels.Add(ConvertToViewModel(schoolClass));
            }
            return Json(schoolClassViewModels.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, SchoolClassViewModel schoolClass)
        {
            if (schoolClass != null && ModelState.IsValid)
            {
                var classToAdd = ConvertToModel(schoolClass);
                _db.Classes.Add(classToAdd);
                _db.SaveChanges();
            }

            return Json(new[] { schoolClass }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, SchoolClassViewModel schoolClass)
        {
            if (schoolClass != null && ModelState.IsValid)
            {
                var edited = _db.Classes.FirstOrDefault(s => s.Id == schoolClass.Id);
                edited.Id = schoolClass.Id;
                edited.Name = schoolClass.Name;
                _db.Classes.AddOrUpdate(edited);
                _db.SaveChanges();
            }

            return Json(new[] { schoolClass }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, SchoolClassViewModel schoolClass)
        {
            if (schoolClass != null)
            {
                var schoolClassToRemove = _db.Classes.FirstOrDefault(s => s.Id == schoolClass.Id);
                if (schoolClassToRemove != null)
                {
                    _db.Classes.Remove(schoolClassToRemove);
                    _db.SaveChanges();
                }
            }

            return Json(new[] { schoolClass }.ToDataSourceResult(request, ModelState));
        }
        private SchoolClass ConvertToModel(SchoolClassViewModel schoolClass)
        {
            return new SchoolClass()
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name
            };
        }

        private IEnumerable<SchoolClassViewModel> ConvertManyToViewModel(IEnumerable<SchoolClass> schoolClasses)
        {
            var schoolClassViewModels = new List<SchoolClassViewModel>();

            foreach (var schoolClass in schoolClasses)
            {
                schoolClassViewModels.Add(ConvertToViewModel(schoolClass));
            }

            return schoolClassViewModels;
        }

        private SchoolClassViewModel ConvertToViewModel(SchoolClass model)
        {
            return new SchoolClassViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                NumberOfStudents = model.Students.ToList().Count
            };
        }
    }

}
