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
    public class PassportController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/Passport
        public IEnumerable<Passport> GetPassports()
        {
            var passports = db.Passports.Include(p => p.FamilyProfile).Include(p => p.PassportType);
            return passports.AsEnumerable();
        }

        // GET api/Passport/5
        public Passport GetPassport(Guid id)
        {
            Passport passport = db.Passports.Find(id);
            if (passport == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return passport;
        }

        // PUT api/Passport/5
        public HttpResponseMessage PutPassport(Guid id, Passport passport)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != passport.PassportId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(passport).State = EntityState.Modified;

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

        // POST api/Passport
        public HttpResponseMessage PostPassport(Passport passport)
        {
            if (ModelState.IsValid)
            {
                db.Passports.Add(passport);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, passport);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = passport.PassportId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Passport/5
        public HttpResponseMessage DeletePassport(Guid id)
        {
            Passport passport = db.Passports.Find(id);
            if (passport == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Passports.Remove(passport);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, passport);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}