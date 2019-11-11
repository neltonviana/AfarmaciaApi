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
    public class MedicamentoController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        private ConfiguracoesController config = new ConfiguracoesController();
        // GET: api/Medicamento
        public IEnumerable<Medicamento> GetMedicamento()
        {
            return config.Medicamento_refinado(db.Medicamento.ToList());
        }

        // GET: api/Medicamento/5
        [ResponseType(typeof(Medicamento))]
        public async Task<IHttpActionResult> GetMedicamento(long id)
        {
            var lista = await db.Medicamento.Where(e=>e.id_medicamento==id).ToListAsync();
            Medicamento medicamento = config.Medicamento_refinado(lista).FirstOrDefault();
            if (medicamento == null)
            {
                return NotFound();
            }

            return Ok(medicamento);
        }

        // PUT: api/Medicamento/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedicamento(long id, Medicamento medicamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicamento.id_medicamento)
            {
                return BadRequest();
            }

            db.Entry(medicamento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoExists(id))
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

        // POST: api/Medicamento
        [ResponseType(typeof(Medicamento))]
        public async Task<IHttpActionResult> PostMedicamento(Medicamento medicamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medicamento.Add(medicamento);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MedicamentoExists(medicamento.id_medicamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = medicamento.id_medicamento }, medicamento);
        }

        // DELETE: api/Medicamento/5
        [ResponseType(typeof(Medicamento))]
        public async Task<IHttpActionResult> DeleteMedicamento(long id)
        {
            Medicamento medicamento = await db.Medicamento.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            db.Medicamento.Remove(medicamento);
            await db.SaveChangesAsync();

            return Ok(medicamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicamentoExists(long id)
        {
            return db.Medicamento.Count(e => e.id_medicamento == id) > 0;
        }
    }
}