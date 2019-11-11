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
    public class PesquisasController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        private ConfiguracoesController config = new ConfiguracoesController();
        // GET: api/Pesquisas
        public IEnumerable<Pesquisas> GetPesquisas()
        {
            return db.Pesquisas;
        }
        public IEnumerable<Pesquisas> GetResumo_pesquisa(string local, string data, long id_usuario, float? raio)
        {
            return config.Procura_refinada(config.Procura_farmacias(local, data, id_usuario, raio));
        }

        // GET: api/Pesquisas/5
        [ResponseType(typeof(Pesquisas))]
        public async Task<IHttpActionResult> GetPesquisas(long id)
        {
            Pesquisas pesquisas = await db.Pesquisas.FindAsync(id);
            if (pesquisas == null)
            {
                return NotFound();
            }

            return Ok(pesquisas);
        }

        // PUT: api/Pesquisas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPesquisas(long id, Pesquisas pesquisas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pesquisas.id)
            {
                return BadRequest();
            }

            db.Entry(pesquisas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PesquisasExists(id))
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

        // POST: api/Pesquisas
        [ResponseType(typeof(Pesquisas))]
        public async Task<IHttpActionResult> PostPesquisas(Pesquisas pesquisas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pesquisas.Add(pesquisas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pesquisas.id }, pesquisas);
        }

        // DELETE: api/Pesquisas/5
        [ResponseType(typeof(Pesquisas))]
        public async Task<IHttpActionResult> DeletePesquisas(long id)
        {
            Pesquisas pesquisas = await db.Pesquisas.FindAsync(id);
            if (pesquisas == null)
            {
                return NotFound();
            }

            db.Pesquisas.Remove(pesquisas);
            await db.SaveChangesAsync();

            return Ok(pesquisas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PesquisasExists(long id)
        {
            return db.Pesquisas.Count(e => e.id == id) > 0;
        }
    }
}