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
    public class AppUserLoyaltyController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/AppUserLoyalty
        public IEnumerable<AppUserLoyalty> GetAppUserLoyalties()
        {
            var appuserloyalties = db.AppUserLoyalties.Include(a => a.AppUser).Include(a => a.Loyalty);
            return appuserloyalties.AsEnumerable();
        }

        // GET api/AppUserLoyalty/5
        public AppUserLoyalty GetAppUserLoyalty(Guid id)
        {
            AppUserLoyalty appuserloyalty = db.AppUserLoyalties.Find(id);
            if (appuserloyalty == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return appuserloyalty;
        }

        // PUT api/AppUserLoyalty/5
        public HttpResponseMessage PutAppUserLoyalty(Guid id, AppUserLoyalty appuserloyalty)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != appuserloyalty.AppUserLoyaltyId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(appuserloyalty).State = EntityState.Modified;

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

        // POST api/AppUserLoyalty
        public HttpResponseMessage PostAppUserLoyalty(AppUserLoyalty appuserloyalty)
        {
            if (ModelState.IsValid)
            {
                db.AppUserLoyalties.Add(appuserloyalty);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, appuserloyalty);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = appuserloyalty.AppUserLoyaltyId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AppUserLoyalty/5
        public HttpResponseMessage DeleteAppUserLoyalty(Guid id)
        {
            AppUserLoyalty appuserloyalty = db.AppUserLoyalties.Find(id);
            if (appuserloyalty == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AppUserLoyalties.Remove(appuserloyalty);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, appuserloyalty);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}