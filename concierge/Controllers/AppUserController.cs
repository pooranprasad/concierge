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
    public class AppUserController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/AppUser
        public IEnumerable<AppUser> GetAppUsers()
        {
            return db.AppUsers.AsEnumerable();
        }

        // GET api/AppUser/5
        public AppUser GetAppUser(Guid id)
        {
            AppUser appuser = db.AppUsers.Find(id);
            if (appuser == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return appuser;
        }

        // PUT api/AppUser/5
        public HttpResponseMessage PutAppUser(Guid id, AppUser appuser)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != appuser.AppUserId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(appuser).State = EntityState.Modified;

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

        // POST api/AppUser
        public HttpResponseMessage PostAppUser(AppUser appuser)
        {
            if (ModelState.IsValid)
            {
                db.AppUsers.Add(appuser);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, appuser);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = appuser.AppUserId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AppUser/5
        public HttpResponseMessage DeleteAppUser(Guid id)
        {
            AppUser appuser = db.AppUsers.Find(id);
            if (appuser == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AppUsers.Remove(appuser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, appuser);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}