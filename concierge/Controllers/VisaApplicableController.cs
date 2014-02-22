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
    public class VisaApplicableController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/VisaApplicable
        public IEnumerable<VisaApplicable> GetVisaApplicables()
        {
            var visaapplicables = db.VisaApplicables.Include(v => v.Country).Include(v => v.CountryVisa);
            return visaapplicables.AsEnumerable();
        }

        // GET api/VisaApplicable/5
        public VisaApplicable GetVisaApplicable(Guid id)
        {
            VisaApplicable visaapplicable = db.VisaApplicables.Find(id);
            if (visaapplicable == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return visaapplicable;
        }

        // PUT api/VisaApplicable/5
        public HttpResponseMessage PutVisaApplicable(Guid id, VisaApplicable visaapplicable)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != visaapplicable.VisaApplicableId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(visaapplicable).State = EntityState.Modified;

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

        // POST api/VisaApplicable
        public HttpResponseMessage PostVisaApplicable(VisaApplicable visaapplicable)
        {
            if (ModelState.IsValid)
            {
                db.VisaApplicables.Add(visaapplicable);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, visaapplicable);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = visaapplicable.VisaApplicableId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/VisaApplicable/5
        public HttpResponseMessage DeleteVisaApplicable(Guid id)
        {
            VisaApplicable visaapplicable = db.VisaApplicables.Find(id);
            if (visaapplicable == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.VisaApplicables.Remove(visaapplicable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, visaapplicable);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}