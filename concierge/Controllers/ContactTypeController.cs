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
    public class ContactTypeController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/ContactType
        public IEnumerable<ContactType> GetContactTypes()
        {
            return db.ContactTypes.AsEnumerable();
        }

        // GET api/ContactType/5
        public ContactType GetContactType(Guid id)
        {
            ContactType contacttype = db.ContactTypes.Find(id);
            if (contacttype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return contacttype;
        }

        // PUT api/ContactType/5
        public HttpResponseMessage PutContactType(Guid id, ContactType contacttype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != contacttype.ContactTypeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(contacttype).State = EntityState.Modified;

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

        // POST api/ContactType
        public HttpResponseMessage PostContactType(ContactType contacttype)
        {
            if (ModelState.IsValid)
            {
                db.ContactTypes.Add(contacttype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contacttype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contacttype.ContactTypeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ContactType/5
        public HttpResponseMessage DeleteContactType(Guid id)
        {
            ContactType contacttype = db.ContactTypes.Find(id);
            if (contacttype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ContactTypes.Remove(contacttype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contacttype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}