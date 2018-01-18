using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kbry.Data;
using Kbry.Data.Model;
using Kbry.MVC.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Kbry.MVC.Controllers
{
    [RoutePrefix("Attendances")]
    public class AttendancesController : Controller
    {
        private readonly KbryDbContext _db = new KbryDbContext();

        // GET: Attendances
        [Route("Index")]
        public ActionResult Index()
        {
            ViewBag.Title = "All Attendeces";
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

        public ActionResult GetAttendances([DataSourceRequest]DataSourceRequest request)
        {
            var allAttendances = _db.Attendances.Include(s => s.Student).Include(s => s.Student.SchoolClass).ToList();
            var allAttandenceVM = new List<AttendanceViewModel>();

            foreach (var attendance in allAttendances)
            {
                allAttandenceVM.Add(ConvertToViewModel(attendance));
            }
            return Json(allAttandenceVM.ToDataSourceResult(request));
        }

        private AttendanceViewModel ConvertToViewModel(Attendance model)
        {
            return new AttendanceViewModel()
            {
                Id = model.Id,
                Date = model.Date.ToString("yyyy-MM-dd h:mm"),
                StudentClass = model.Student.SchoolClass.Name,
                StudentLastName = model.Student.LastName,
                StudentFirstName = model.Student.FirstName
            };
        }
    }
}
