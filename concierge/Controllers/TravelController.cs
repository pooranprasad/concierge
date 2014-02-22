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
    public class TravelController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/Travel
        public IEnumerable<Travel> GetTravels()
        {
            var travels = db.Travels.Include(t => t.AppUser).Include(t => t.TimeZone).Include(t => t.TimeZone1).Include(t => t.TravelMode).Include(t => t.TravelOperator);
            return travels.AsEnumerable();
        }

        // GET api/Travel/5
        public Travel GetTravel(Guid id)
        {
            Travel travel = db.Travels.Find(id);
            if (travel == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return travel;
        }

        // PUT api/Travel/5
        public HttpResponseMessage PutTravel(Guid id, Travel travel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != travel.TravelId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(travel).State = EntityState.Modified;

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

        // POST api/Travel
        public HttpResponseMessage PostTravel(Travel travel)
        {
            if (ModelState.IsValid)
            {
                db.Travels.Add(travel);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, travel);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = travel.TravelId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Travel/5
        public HttpResponseMessage DeleteTravel(Guid id)
        {
            Travel travel = db.Travels.Find(id);
            if (travel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Travels.Remove(travel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, travel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}