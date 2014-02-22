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
    public class CountryController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/Country
        public IEnumerable<Country> GetCountries()
        {
            return db.Countries.AsEnumerable();
        }

        // GET api/Country/5
        public Country GetCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return country;
        }

        // PUT api/Country/5
        public HttpResponseMessage PutCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != country.CountryId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(country).State = EntityState.Modified;

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

        // POST api/Country
        public HttpResponseMessage PostCountry(Country country)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, country);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = country.CountryId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Country/5
        public HttpResponseMessage DeleteCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Countries.Remove(country);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, country);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}