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
    public class ContactController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/Contact
        public IEnumerable<Contact> GetContacts()
        {
            var contacts = db.Contacts.Include(c => c.AppUser).Include(c => c.ContactType);
            return contacts.AsEnumerable();
        }

        // GET api/Contact/5
        public Contact GetContact(Guid id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return contact;
        }

        // PUT api/Contact/5
        public HttpResponseMessage PutContact(Guid id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != contact.ContactId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(contact).State = EntityState.Modified;

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

        // POST api/Contact
        public HttpResponseMessage PostContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contact);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contact.ContactId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Contact/5
        public HttpResponseMessage DeleteContact(Guid id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Contacts.Remove(contact);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}