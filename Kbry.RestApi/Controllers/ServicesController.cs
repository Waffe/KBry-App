using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Kbry.Data;
using System.Device.Location;
using Kbry.Data.Model;

namespace Kbry.RestApi.Controllers
{
    [RoutePrefix("api")]
    public class ServicesController : ApiController
    {
        private KbryDbContext db = new KbryDbContext();
        HttpContext httpContext = HttpContext.Current;

        // GET: api/KbryApi
        [Route("GetAllAttendanceDates/{regcode}")]
        [HttpGet]
        public IQueryable<DateTime> GetAttendances(string regcode)
        {
            AuthorizeApiKey();

            return db.Attendances.Include(x => x.Student).Where(x => x.Student.RegistrationCode == regcode).Select(x => x.Date);
        }

        [Route("GetAttendenceDatesByAmount/{regcode}/{amount:int}")]
        [HttpGet]
        public IQueryable<DateTime> GetLastAttendances(string regcode, int amount)
        {
            AuthorizeApiKey();

            return db.Attendances.Include(x => x.Student).Where(x => x.Student.RegistrationCode == regcode).Take(amount).Select(x => x.Date);
        }


        [ResponseType(typeof(Student))]
        [Route("GetStudentInfo/{regcode}")]
        [HttpGet]
        public IHttpActionResult GetStudent(string regcode)
        {
            AuthorizeApiKey();
            var student = db.Students.Include(x => x.Class).SingleOrDefault(x => x.RegistrationCode == regcode);
            return Ok(student);
        }




        // POST: api/KbryApi
        [ResponseType(typeof(Attendance))]
        [HttpPost]
        public IHttpActionResult PostAttendance(Attendance attendance)
        {
            AuthorizeApiKey();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CheckCoordinates(attendance)) return BadRequest("Coordinates out of range");

            db.Attendances.Add(attendance);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = attendance.Id }, attendance);
        }


        private bool CheckCoordinates(Attendance attendance)
        {
            var acceptedLocationCoordinate = new GeoCoordinate(57.678897, 12.001500);
            var attendanceCords = new GeoCoordinate(attendance.Latitude, attendance.Longitude);

            return !(acceptedLocationCoordinate.GetDistanceTo(attendanceCords) > 500);
        }

        private void AuthorizeApiKey()
        {
            string authHeader = this.httpContext.Request.Headers["Authorization"];

            var apiGuid = Guid.Parse(authHeader);
            if (!db.ApiKeys.Any(x => x.Key == apiGuid))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "API Key is invalid" };
                throw new HttpResponseException(msg);
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