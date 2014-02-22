using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using concierge.Models;

namespace concierge.Controllers
{
    public class AppTimeZoneController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/AppTimeZone
        public IEnumerable<AppTimeZone> GetAppTimeZones()
        {
            return db.AppTimeZones.AsEnumerable();
        }

        // GET api/AppTimeZone/5
        public AppTimeZone GetAppTimeZone(int id)
        {
            AppTimeZone apptimezone = db.AppTimeZones.Find(id);
            if (apptimezone == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return apptimezone;
        }

        // PUT api/AppTimeZone/5
        public HttpResponseMessage PutAppTimeZone(int id, AppTimeZone apptimezone)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != apptimezone.TimeZoneId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(apptimezone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/AppTimeZone
        public HttpResponseMessage PostAppTimeZone(AppTimeZone apptimezone)
        {
            if (ModelState.IsValid)
            {
                db.AppTimeZones.Add(apptimezone);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, apptimezone);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = apptimezone.TimeZoneId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AppTimeZone/5
        public HttpResponseMessage DeleteAppTimeZone(int id)
        {
            AppTimeZone apptimezone = db.AppTimeZones.Find(id);
            if (apptimezone == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AppTimeZones.Remove(apptimezone);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, apptimezone);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}