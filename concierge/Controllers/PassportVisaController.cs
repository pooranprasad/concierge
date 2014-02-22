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
    public class PassportVisaController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/PassportVisa
        public IEnumerable<PassportVisa> GetPassportVisas()
        {
            var passportvisas = db.PassportVisas.Include(p => p.CountryVisa).Include(p => p.Passport);
            return passportvisas.AsEnumerable();
        }

        // GET api/PassportVisa/5
        public PassportVisa GetPassportVisa(Guid id)
        {
            PassportVisa passportvisa = db.PassportVisas.Find(id);
            if (passportvisa == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return passportvisa;
        }

        // PUT api/PassportVisa/5
        public HttpResponseMessage PutPassportVisa(Guid id, PassportVisa passportvisa)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != passportvisa.PassportVisaId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(passportvisa).State = EntityState.Modified;

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

        // POST api/PassportVisa
        public HttpResponseMessage PostPassportVisa(PassportVisa passportvisa)
        {
            if (ModelState.IsValid)
            {
                db.PassportVisas.Add(passportvisa);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, passportvisa);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = passportvisa.PassportVisaId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/PassportVisa/5
        public HttpResponseMessage DeletePassportVisa(Guid id)
        {
            PassportVisa passportvisa = db.PassportVisas.Find(id);
            if (passportvisa == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.PassportVisas.Remove(passportvisa);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, passportvisa);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}