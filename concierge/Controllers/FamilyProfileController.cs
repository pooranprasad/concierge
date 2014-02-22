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
    public class FamilyProfileController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/FamilyProfile
        public IEnumerable<FamilyProfile> GetFamilyProfiles()
        {
            var familyprofiles = db.FamilyProfiles.Include(f => f.AppUser).Include(f => f.RelationType);
            return familyprofiles.AsEnumerable();
        }

        // GET api/FamilyProfile/5
        public FamilyProfile GetFamilyProfile(Guid id)
        {
            FamilyProfile familyprofile = db.FamilyProfiles.Find(id);
            if (familyprofile == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return familyprofile;
        }

        // PUT api/FamilyProfile/5
        public HttpResponseMessage PutFamilyProfile(Guid id, FamilyProfile familyprofile)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != familyprofile.FamilyId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(familyprofile).State = EntityState.Modified;

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

        // POST api/FamilyProfile
        public HttpResponseMessage PostFamilyProfile(FamilyProfile familyprofile)
        {
            if (ModelState.IsValid)
            {
                db.FamilyProfiles.Add(familyprofile);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, familyprofile);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = familyprofile.FamilyId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/FamilyProfile/5
        public HttpResponseMessage DeleteFamilyProfile(Guid id)
        {
            FamilyProfile familyprofile = db.FamilyProfiles.Find(id);
            if (familyprofile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.FamilyProfiles.Remove(familyprofile);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, familyprofile);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}