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
using afarmaciaApi.Models;

namespace afarmaciaApi.Controllers.Api
{
    public class FarmaciaController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();

        // GET: api/Farmacia
        public IQueryable<Farmacia> GetFarmacia()
        {
            return db.Farmacia;
        }

        // GET: api/Farmacia/5
        [ResponseType(typeof(Farmacia))]
        public async Task<IHttpActionResult> GetFarmacia(long id)
        {
            Farmacia farmacia = await db.Farmacia.FindAsync(id);
            if (farmacia == null)
            {
                return NotFound();
            }

            return Ok(farmacia);
        }

        // PUT: api/Farmacia/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFarmacia(long id, Farmacia farmacia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != farmacia.id_farmacia)
            {
                return BadRequest();
            }

            db.Entry(farmacia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FarmaciaExists(id))
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

        // POST: api/Farmacia
        [ResponseType(typeof(Farmacia))]
        public async Task<IHttpActionResult> PostFarmacia(Farmacia farmacia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Farmacia.Add(farmacia);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FarmaciaExists(farmacia.id_farmacia))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = farmacia.id_farmacia }, farmacia);
        }

        // DELETE: api/Farmacia/5
        [ResponseType(typeof(Farmacia))]
        public async Task<IHttpActionResult> DeleteFarmacia(long id)
        {
            Farmacia farmacia = await db.Farmacia.FindAsync(id);
            if (farmacia == null)
            {
                return NotFound();
            }

            db.Farmacia.Remove(farmacia);
            await db.SaveChangesAsync();

            return Ok(farmacia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FarmaciaExists(long id)
        {
            return db.Farmacia.Count(e => e.id_farmacia == id) > 0;
        }
    }
}