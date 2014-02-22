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
    public class TravelOperatorController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/TravelOperator
        public IEnumerable<TravelOperator> GetTravelOperators()
        {
            var traveloperators = db.TravelOperators.Include(t => t.TravelMode);
            return traveloperators.AsEnumerable();
        }

        // GET api/TravelOperator/5
        public TravelOperator GetTravelOperator(Guid id)
        {
            TravelOperator traveloperator = db.TravelOperators.Find(id);
            if (traveloperator == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return traveloperator;
        }

        // PUT api/TravelOperator/5
        public HttpResponseMessage PutTravelOperator(Guid id, TravelOperator traveloperator)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != traveloperator.TravelOperatorId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(traveloperator).State = EntityState.Modified;

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

        // POST api/TravelOperator
        public HttpResponseMessage PostTravelOperator(TravelOperator traveloperator)
        {
            if (ModelState.IsValid)
            {
                db.TravelOperators.Add(traveloperator);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, traveloperator);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = traveloperator.TravelOperatorId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/TravelOperator/5
        public HttpResponseMessage DeleteTravelOperator(Guid id)
        {
            TravelOperator traveloperator = db.TravelOperators.Find(id);
            if (traveloperator == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.TravelOperators.Remove(traveloperator);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, traveloperator);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}