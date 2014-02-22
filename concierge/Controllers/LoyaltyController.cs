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
    public class LoyaltyController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/Loyalty
        public IEnumerable<Loyalty> GetLoyalties()
        {
            var loyalties = db.Loyalties.Include(l => l.AssociationType);
            return loyalties.AsEnumerable();
        }

        // GET api/Loyalty/5
        public Loyalty GetLoyalty(Guid id)
        {
            Loyalty loyalty = db.Loyalties.Find(id);
            if (loyalty == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return loyalty;
        }

        // PUT api/Loyalty/5
        public HttpResponseMessage PutLoyalty(Guid id, Loyalty loyalty)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != loyalty.LoyaltyId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(loyalty).State = EntityState.Modified;

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

        // POST api/Loyalty
        public HttpResponseMessage PostLoyalty(Loyalty loyalty)
        {
            if (ModelState.IsValid)
            {
                db.Loyalties.Add(loyalty);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, loyalty);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = loyalty.LoyaltyId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Loyalty/5
        public HttpResponseMessage DeleteLoyalty(Guid id)
        {
            Loyalty loyalty = db.Loyalties.Find(id);
            if (loyalty == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Loyalties.Remove(loyalty);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, loyalty);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}