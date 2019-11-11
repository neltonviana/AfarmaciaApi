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
    public class ReservaController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        ConfiguracoesController config = new ConfiguracoesController();
        // GET: api/Reserva
        public IEnumerable<Reserva> GetReserva()
        {
            return config.Reserva_refinada(db.Reserva.ToList());
        }

        // GET: api/Reserva/5
        [ResponseType(typeof(Reserva))]
        public async Task<IHttpActionResult> GetReserva(long id)
        {
            Reserva reserva = await db.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }

        public async Task<IHttpActionResult> GetReserva(string ordem)
        {
            var lista = await db.Reserva.Where(e=>e.ordem.Equals(ordem)).ToListAsync();
            Reserva reserva = config.Reserva_refinada(lista).FirstOrDefault();
            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }

        // PUT: api/Reserva/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReserva(long id, Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserva.id_medicamento)
            {
                return BadRequest();
            }

            db.Entry(reserva).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
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

        // POST: api/Reserva
        [ResponseType(typeof(Reserva))]
        public async Task<IHttpActionResult> PostReserva(Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reserva.Add(reserva);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReservaExists(reserva.id_medicamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reserva.id_medicamento }, reserva);
        }

        // DELETE: api/Reserva/5
        [ResponseType(typeof(Reserva))]
        public async Task<IHttpActionResult> DeleteReserva(long id)
        {
            Reserva reserva = await db.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            db.Reserva.Remove(reserva);
            await db.SaveChangesAsync();

            return Ok(reserva);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservaExists(long id)
        {
            return db.Reserva.Count(e => e.id_medicamento == id) > 0;
        }
    }
}