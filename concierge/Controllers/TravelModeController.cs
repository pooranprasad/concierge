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
    public class TravelModeController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/TravelMode
        public IEnumerable<TravelMode> GetTravelModes()
        {
            return db.TravelModes.AsEnumerable();
        }

        // GET api/TravelMode/5
        public TravelMode GetTravelMode(byte id)
        {
            TravelMode travelmode = db.TravelModes.Find(id);
            if (travelmode == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return travelmode;
        }

        // PUT api/TravelMode/5
        public HttpResponseMessage PutTravelMode(byte id, TravelMode travelmode)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != travelmode.TravelModeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(travelmode).State = EntityState.Modified;

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

        // POST api/TravelMode
        public HttpResponseMessage PostTravelMode(TravelMode travelmode)
        {
            if (ModelState.IsValid)
            {
                db.TravelModes.Add(travelmode);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, travelmode);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = travelmode.TravelModeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/TravelMode/5
        public HttpResponseMessage DeleteTravelMode(byte id)
        {
            TravelMode travelmode = db.TravelModes.Find(id);
            if (travelmode == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.TravelModes.Remove(travelmode);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, travelmode);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}