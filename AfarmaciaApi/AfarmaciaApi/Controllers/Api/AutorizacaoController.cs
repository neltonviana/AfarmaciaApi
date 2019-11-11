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
    public class AutorizacaoController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        private ConfiguracoesController config = new ConfiguracoesController();

        // GET: api/Autorizacao
        public IEnumerable<Autorizacao> GetAutorizacaos()
        {
            return db.Autorizacaos;
        }


        public IEnumerable<Autorizacao> GetAutorizacaos(long id_medicamento, string retorno)
        {
            return config.Autorizacao_refinada(id_medicamento,retorno);            
        }
        // GET: api/Autorizacao/5
        [ResponseType(typeof(Autorizacao))]
        public async Task<IHttpActionResult> GetAutorizacao(long id)
        {
            Autorizacao autorizacao = await db.Autorizacaos.FindAsync(id);
            if (autorizacao == null)
            {
                return NotFound();
            }

            return Ok(autorizacao);
        }

        // PUT: api/Autorizacao/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAutorizacao(long id, Autorizacao autorizacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autorizacao.id)
            {
                return BadRequest();
            }

            db.Entry(autorizacao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorizacaoExists(id))
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

        // POST: api/Autorizacao
        [ResponseType(typeof(Autorizacao))]
        public async Task<IHttpActionResult> PostAutorizacao(Autorizacao autorizacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autorizacaos.Add(autorizacao);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = autorizacao.id }, autorizacao);
        }

        // DELETE: api/Autorizacao/5
        [ResponseType(typeof(Autorizacao))]
        public async Task<IHttpActionResult> DeleteAutorizacao(long id)
        {
            Autorizacao autorizacao = await db.Autorizacaos.FindAsync(id);
            if (autorizacao == null)
            {
                return NotFound();
            }

            db.Autorizacaos.Remove(autorizacao);
            await db.SaveChangesAsync();

            return Ok(autorizacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutorizacaoExists(long id)
        {
            return db.Autorizacaos.Count(e => e.id == id) > 0;
        }
    }
}