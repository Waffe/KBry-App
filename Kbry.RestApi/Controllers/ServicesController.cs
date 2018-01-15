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
using System.Data.Entity;
using Kbry.Data.Model;

namespace Kbry.RestApi.Controllers
{
    public class ServicesController : ApiController
    {
        private KbryDbContext db = new KbryDbContext();
        HttpContext httpContext = HttpContext.Current;

        // GET: api/Services
        [Route("customers")]
        [HttpGet]
        public IQueryable<Attendance> GetAttendances()
        {
            return db.Attendances;
        }

        //[Route("test")]
        //[HttpGet]
        //public IQueryable<Student> GetStudents()
        //{           
        //    return db.Students;
        //}

        [Route("test")]
        [HttpGet]
        public IHttpActionResult GetThingy()
        {
            //AuthorizeApiKey();
            //var student = db.Students.Include(x => x.Class).SingleOrDefault(x => x.RegistrationCode == regcode);
            //return Ok(student);
            return Ok(new Student() {FirstName = "Name", LastName = "Last"});
        }

        public IQueryable<DateTime> GetAttendanceDates(int id)
        {
            return db.Attendances.Include(x => x.Student).Where(x => x.Student.Id == id).Select(x => x.Date);
        }

        public IQueryable<DateTime> GetLastAttendances(string regcode, int amount)
        {
            AuthorizeApiKey();

            return db.Attendances.Include(x => x.Student).Where(x => x.Student.RegistrationCode == regcode).Take(amount).Select(x => x.Date);
        }

        // GET: api/Services/5
        [ResponseType(typeof(Attendance))]
        public async Task<IHttpActionResult> GetAttendance(int id)
        {
            Attendance attendance = await db.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }



        // POST: api/Services
        [ResponseType(typeof(Attendance))]
        public async Task<IHttpActionResult> PostAttendance(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Attendances.Add(attendance);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = attendance.Id }, attendance);
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