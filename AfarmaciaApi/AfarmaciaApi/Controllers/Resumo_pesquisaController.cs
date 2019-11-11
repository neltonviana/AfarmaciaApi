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
    public class Resumo_pesquisaController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        private ConfiguracoesController config = new ConfiguracoesController();
        // GET: api/Resumocs
        public IQueryable<Resumocs> GetResumo_pesquisa()
        {
            return db.Resumocs;
        }

        public IEnumerable<Resumocs> GetResumo_pesquisa(string local, string data, long id_usuario, float? raio)
        {
            return config.Procura_refinada(config.Procura_farmacias(local, data, id_usuario, raio));
        }

        // GET: api/Resumocs/5
        [ResponseType(typeof(Resumocs))]
        public async Task<IHttpActionResult> GetResumo_pesquisa(long id)
        {
            Resumocs resumocs = await db.Resumocs.FindAsync(id);
            if (resumocs == null)
            {
                return NotFound();
            }

            return Ok(resumocs);
        }

        // PUT: api/Resumocs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutResumo_pesquisa(long id, Resumocs resumocs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resumocs.id)
            {
                return BadRequest();
            }

            db.Entry(resumocs).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Resumo_pesquisaExists(id))
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

        // POST: api/Resumocs
        [ResponseType(typeof(Resumocs))]
        public async Task<IHttpActionResult> PostResumo_pesquisa(Resumocs resumocs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resumocs.Add(resumocs);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = resumocs.id }, resumocs);
        }

        // DELETE: api/Resumocs/5
        [ResponseType(typeof(Resumocs))]
        public async Task<IHttpActionResult> DeleteResumo_pesquisa(long id)
        {
            Resumocs resumocs = await db.Resumocs.FindAsync(id);
            if (resumocs == null)
            {
                return NotFound();
            }

            db.Resumocs.Remove(resumocs);
            await db.SaveChangesAsync();

            return Ok(resumocs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Resumo_pesquisaExists(long id)
        {
            return db.Resumocs.Count(e => e.id == id) > 0;
        }
    }
}