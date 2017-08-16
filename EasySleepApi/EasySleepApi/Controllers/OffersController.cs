using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EasySleepApi.Models;

namespace EasySleepApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Offers")]
    public class OffersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Offers
        public IQueryable<Offer> GetOffers()
        {
            return db.Offers;
        }

        // GET: api/Offers/5
        [ResponseType(typeof(Offer))]
        public async Task<IHttpActionResult> GetOffer(int id)
        {
            Offer offer = await db.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            return Ok(offer);
        }

        // GET: api/Offers/OfferByUserId?userId=
        [Route("OfferByUserId")]
        public IQueryable<Offer> GetOfferByUserId(string userId)
        {
            var offer =
                from o in db.Offers
                where o.OwnerId == userId
                select o;
            return offer;
        } 

        // GET: api/Offers/AllActiveOffers?userId=
        [Route("AllActiveOffers")]
        public IQueryable<Offer> GetAllActiveOffers(string userId)
        {
            var offers =
                from o in db.Offers 
                where (o.IsActive == true) && (o.OwnerId != userId) 
                select o;
            return offers;
        }

        // PUT: api/Offers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOffer(int id, Offer offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offer.Id)
            {
                return BadRequest();
            }

            db.Entry(offer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Offers
        [ResponseType(typeof(Offer))]
        public async Task<IHttpActionResult> PostOffer(Offer offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Offers.Add(offer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = offer.Id }, offer);
        }

        // DELETE: api/Offers/5
        [ResponseType(typeof(Offer))]
        public async Task<IHttpActionResult> DeleteOffer(int id)
        {
            Offer offer = await db.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            db.Offers.Remove(offer);
            await db.SaveChangesAsync();

            return Ok(offer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfferExists(int id)
        {
            return db.Offers.Count(e => e.Id == id) > 0;
        }
    }
}