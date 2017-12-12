using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Kbry.Data;
using Kbry.Data.Model;

namespace Kbry.Web.Controllers
{
    public class AttendancesController : ApiController
    {
        private KbryDbContext db = new KbryDbContext();

        // GET: api/Attendances
        public IQueryable<Attendance> GetAttendances()
        {
            return db.Attendances;
        }

        // GET: api/Attendances/5
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult GetAttendance(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }


        // POST: api/Attendances
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult PostAttendance(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Attendances.Add(attendance);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = attendance.Id }, attendance);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendanceExists(int id)
        {
            return db.Attendances.Count(e => e.Id == id) > 0;
        }
    }
}