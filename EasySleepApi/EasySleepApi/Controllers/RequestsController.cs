﻿using System;
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
    [RoutePrefix("api/Requests")]
    public class RequestsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Requests
        public IQueryable<Request> GetRequests()
        {
            return db.Requests;
        }

        // GET: api/Requests/5
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> GetRequest(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        // GET: api/Requests/RequestByOffer?offerId=
        [Route("RequestByOffer")]
        public IQueryable<Request> GetRequestByOffer(int offerId)
        {
            var request =
                from r in db.Requests
                where (r.OfferId == offerId) && (r.IsRequestAccepted == false)
                select r;
            return request;
        }

        // PUT: api/Requests/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            db.Entry(request).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> PostRequest(Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Requests.Add(request);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> DeleteRequest(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            db.Requests.Remove(request);
            await db.SaveChangesAsync();

            return Ok(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(int id)
        {
            return db.Requests.Count(e => e.Id == id) > 0;
        }
    }
}