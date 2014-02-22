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
    public class AccomodationController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/Accomodation
        public IEnumerable<Accomodation> GetAccomodations()
        {
            var accomodations = db.Accomodations.Include(a => a.AccomodationProvider).Include(a => a.AppUser).Include(a => a.Travel);
            return accomodations.AsEnumerable();
        }

        // GET api/Accomodation/5
        public Accomodation GetAccomodation(Guid id)
        {
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return accomodation;
        }

        // PUT api/Accomodation/5
        public HttpResponseMessage PutAccomodation(Guid id, Accomodation accomodation)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != accomodation.AccomodationId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(accomodation).State = EntityState.Modified;

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

        // POST api/Accomodation
        public HttpResponseMessage PostAccomodation(Accomodation accomodation)
        {
            if (ModelState.IsValid)
            {
                db.Accomodations.Add(accomodation);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, accomodation);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = accomodation.AccomodationId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Accomodation/5
        public HttpResponseMessage DeleteAccomodation(Guid id)
        {
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Accomodations.Remove(accomodation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, accomodation);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}