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
    public class AccomodationProviderController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/AccomodationProvider
        public IEnumerable<AccomodationProvider> GetAccomodationProviders()
        {
            return db.AccomodationProviders.AsEnumerable();
        }

        // GET api/AccomodationProvider/5
        public AccomodationProvider GetAccomodationProvider(Guid id)
        {
            AccomodationProvider accomodationprovider = db.AccomodationProviders.Find(id);
            if (accomodationprovider == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return accomodationprovider;
        }

        // PUT api/AccomodationProvider/5
        public HttpResponseMessage PutAccomodationProvider(Guid id, AccomodationProvider accomodationprovider)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != accomodationprovider.AccomodationProviderId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(accomodationprovider).State = EntityState.Modified;

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

        // POST api/AccomodationProvider
        public HttpResponseMessage PostAccomodationProvider(AccomodationProvider accomodationprovider)
        {
            if (ModelState.IsValid)
            {
                db.AccomodationProviders.Add(accomodationprovider);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, accomodationprovider);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = accomodationprovider.AccomodationProviderId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AccomodationProvider/5
        public HttpResponseMessage DeleteAccomodationProvider(Guid id)
        {
            AccomodationProvider accomodationprovider = db.AccomodationProviders.Find(id);
            if (accomodationprovider == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AccomodationProviders.Remove(accomodationprovider);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, accomodationprovider);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}