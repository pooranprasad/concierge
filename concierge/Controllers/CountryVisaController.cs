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
    public class CountryVisaController : ApiController
    {
        private ConciergeEntities db = new ConciergeEntities();

        // GET api/CountryVisa
        public IEnumerable<CountryVisa> GetCountryVisas()
        {
            var countryvisas = db.CountryVisas.Include(c => c.Country).Include(c => c.VisaType1);
            return countryvisas.AsEnumerable();
        }

        // GET api/CountryVisa/5
        public CountryVisa GetCountryVisa(Guid id)
        {
            CountryVisa countryvisa = db.CountryVisas.Find(id);
            if (countryvisa == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return countryvisa;
        }

        // PUT api/CountryVisa/5
        public HttpResponseMessage PutCountryVisa(Guid id, CountryVisa countryvisa)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != countryvisa.CountryVisaId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(countryvisa).State = EntityState.Modified;

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

        // POST api/CountryVisa
        public HttpResponseMessage PostCountryVisa(CountryVisa countryvisa)
        {
            if (ModelState.IsValid)
            {
                db.CountryVisas.Add(countryvisa);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, countryvisa);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = countryvisa.CountryVisaId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/CountryVisa/5
        public HttpResponseMessage DeleteCountryVisa(Guid id)
        {
            CountryVisa countryvisa = db.CountryVisas.Find(id);
            if (countryvisa == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.CountryVisas.Remove(countryvisa);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, countryvisa);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}