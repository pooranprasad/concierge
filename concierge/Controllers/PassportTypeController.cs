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
    public class PassportTypeController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/PassportType
        public IEnumerable<PassportType> GetPassportTypes()
        {
            return db.PassportTypes.AsEnumerable();
        }

        // GET api/PassportType/5
        public PassportType GetPassportType(Guid id)
        {
            PassportType passporttype = db.PassportTypes.Find(id);
            if (passporttype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return passporttype;
        }

        // PUT api/PassportType/5
        public HttpResponseMessage PutPassportType(Guid id, PassportType passporttype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != passporttype.PassportTypeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(passporttype).State = EntityState.Modified;

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

        // POST api/PassportType
        public HttpResponseMessage PostPassportType(PassportType passporttype)
        {
            if (ModelState.IsValid)
            {
                db.PassportTypes.Add(passporttype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, passporttype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = passporttype.PassportTypeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/PassportType/5
        public HttpResponseMessage DeletePassportType(Guid id)
        {
            PassportType passporttype = db.PassportTypes.Find(id);
            if (passporttype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.PassportTypes.Remove(passporttype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, passporttype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}