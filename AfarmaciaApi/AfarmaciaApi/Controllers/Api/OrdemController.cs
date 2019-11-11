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
    public class OrdemController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        private ConfiguracoesController config = new ConfiguracoesController();

        // GET: api/Ordem
        public IEnumerable<Ordem> GetOrdem()
        {
            return config.Ordem_refinada(db.Ordem.ToList());
        }

        // GET: api/Ordem/5
        [ResponseType(typeof(Ordem))]
        public async Task<IHttpActionResult> GetOrdem(string id)
        {
            var lista = await db.Ordem.Where(e=>e.ordem1.Equals(id)).ToListAsync();
            Ordem ordem = config.Ordem_refinada(lista).FirstOrDefault();
            if (ordem == null)
            {
                return NotFound();
            }

            return Ok(ordem);
        }

        // PUT: api/Ordem/5
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> PutOrdem(long id_usuario, Ordem ordem)
        {
            string mensagem = config.Confirmar_venda(ordem.ordem1,id_usuario, ordem.observacao);
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {

            }

            return Ok(mensagem);
        }
        
        // POST: api/Ordem
        [ResponseType(typeof(Ordem))]
        public async Task<IHttpActionResult> PostOrdem(Ordem ordem,int raio,string data,string local)
        {
            ordem = config.Adicionar_ordem(ordem,local,raio,data);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrdemExists(ordem.ordem1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            List<Ordem> lista = new List<Ordem>();
            lista.Add(ordem);
            return Ok(config.Ordem_refinada(lista).FirstOrDefault());
        }

        // DELETE: api/Ordem/5
        [ResponseType(typeof(Ordem))]
        public async Task<IHttpActionResult> DeleteOrdem(string id)
        {
            Ordem ordem = await db.Ordem.FindAsync(id);
            if (ordem == null)
            {
                return NotFound();
            }

            db.Ordem.Remove(ordem);
            await db.SaveChangesAsync();

            return Ok(ordem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdemExists(string id)
        {
            return db.Ordem.Count(e => e.ordem1 == id) > 0;
        }
    }
}