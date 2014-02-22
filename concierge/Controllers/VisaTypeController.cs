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
    public class VisaTypeController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/VisaType
        public IEnumerable<VisaType> GetVisaTypes()
        {
            return db.VisaTypes.AsEnumerable();
        }

        // GET api/VisaType/5
        public VisaType GetVisaType(byte id)
        {
            VisaType visatype = db.VisaTypes.Find(id);
            if (visatype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return visatype;
        }

        // PUT api/VisaType/5
        public HttpResponseMessage PutVisaType(byte id, VisaType visatype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != visatype.VisaTypeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(visatype).State = EntityState.Modified;

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

        // POST api/VisaType
        public HttpResponseMessage PostVisaType(VisaType visatype)
        {
            if (ModelState.IsValid)
            {
                db.VisaTypes.Add(visatype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, visatype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = visatype.VisaTypeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/VisaType/5
        public HttpResponseMessage DeleteVisaType(byte id)
        {
            VisaType visatype = db.VisaTypes.Find(id);
            if (visatype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.VisaTypes.Remove(visatype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, visatype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}