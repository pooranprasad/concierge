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
    public class AssociationTypeController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/AssociationType
        public IEnumerable<AssociationType> GetAssociationTypes()
        {
            return db.AssociationTypes.AsEnumerable();
        }

        // GET api/AssociationType/5
        public AssociationType GetAssociationType(Guid id)
        {
            AssociationType associationtype = db.AssociationTypes.Find(id);
            if (associationtype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return associationtype;
        }

        // PUT api/AssociationType/5
        public HttpResponseMessage PutAssociationType(Guid id, AssociationType associationtype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != associationtype.AssociationTypeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(associationtype).State = EntityState.Modified;

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

        // POST api/AssociationType
        public HttpResponseMessage PostAssociationType(AssociationType associationtype)
        {
            if (ModelState.IsValid)
            {
                db.AssociationTypes.Add(associationtype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, associationtype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = associationtype.AssociationTypeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AssociationType/5
        public HttpResponseMessage DeleteAssociationType(Guid id)
        {
            AssociationType associationtype = db.AssociationTypes.Find(id);
            if (associationtype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AssociationTypes.Remove(associationtype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, associationtype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}