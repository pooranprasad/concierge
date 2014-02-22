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
    public class RelationTypeController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/RelationType
        public IEnumerable<RelationType> GetRelationTypes()
        {
            return db.RelationTypes.AsEnumerable();
        }

        // GET api/RelationType/5
        public RelationType GetRelationType(byte id)
        {
            RelationType relationtype = db.RelationTypes.Find(id);
            if (relationtype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return relationtype;
        }

        // PUT api/RelationType/5
        public HttpResponseMessage PutRelationType(byte id, RelationType relationtype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != relationtype.RelationTypeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(relationtype).State = EntityState.Modified;

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

        // POST api/RelationType
        public HttpResponseMessage PostRelationType(RelationType relationtype)
        {
            if (ModelState.IsValid)
            {
                db.RelationTypes.Add(relationtype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, relationtype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = relationtype.RelationTypeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/RelationType/5
        public HttpResponseMessage DeleteRelationType(byte id)
        {
            RelationType relationtype = db.RelationTypes.Find(id);
            if (relationtype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.RelationTypes.Remove(relationtype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, relationtype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}