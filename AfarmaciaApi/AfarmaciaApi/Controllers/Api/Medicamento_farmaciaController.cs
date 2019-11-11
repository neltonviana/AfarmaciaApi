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
    public class Medicamento_farmaciaController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        private ConfiguracoesController config = new ConfiguracoesController();

        // GET: api/Medicamento_farmacia
        public IQueryable<Medicamento_farmacia> GetMedicamento_farmacia()
        {
            return db.Medicamento_farmacia;
        }

        // GET: api/Medicamento_farmacia/5
        [ResponseType(typeof(Medicamento_farmacia))]
        public async Task<IHttpActionResult> GetMedicamento_farmacia(long id)
        {
            Medicamento_farmacia medicamento_farmacia = await db.Medicamento_farmacia.FindAsync(id);
            if (medicamento_farmacia == null)
            {
                return NotFound();
            }

            return Ok(medicamento_farmacia);
        }

        // PUT: api/Medicamento_farmacia/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedicamento_farmacia(long id, Medicamento_farmacia medicamento_farmacia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicamento_farmacia.id_medicamento)
            {
                return BadRequest();
            }

            db.Entry(medicamento_farmacia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Medicamento_farmaciaExists(id))
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

        // POST: api/Medicamento_farmacia
        [ResponseType(typeof(Medicamento_farmacia))]
        public async Task<IHttpActionResult> PostMedicamento_farmacia(Medicamento_farmacia medicamento_farmacia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medicamento_farmacia.Add(medicamento_farmacia);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Medicamento_farmaciaExists(medicamento_farmacia.id_medicamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = medicamento_farmacia.id_medicamento }, medicamento_farmacia);
        }

        // DELETE: api/Medicamento_farmacia/5
        [ResponseType(typeof(Medicamento_farmacia))]
        public async Task<IHttpActionResult> DeleteMedicamento_farmacia(long id)
        {
            Medicamento_farmacia medicamento_farmacia = await db.Medicamento_farmacia.FindAsync(id);
            if (medicamento_farmacia == null)
            {
                return NotFound();
            }

            db.Medicamento_farmacia.Remove(medicamento_farmacia);
            await db.SaveChangesAsync();

            return Ok(medicamento_farmacia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Medicamento_farmaciaExists(long id)
        {
            return db.Medicamento_farmacia.Count(e => e.id_medicamento == id) > 0;
        }
    }
}