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
    public class ReceitaController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        private ConfiguracoesController config = new ConfiguracoesController();

        // GET: api/Receita
        public IEnumerable<Receita> GetReceita()
        {
            return config.Receita_refinada(db.Receita.Where(e=>e.situacao==1).ToList());
        }

        public IEnumerable<Receita> GetReceita(long id_usuario,int situacao)
        {
            DateTime data = DateTime.Now;
            string registo = data.Year + "" + data.Month + "" + data.Day;
            return config.Receita_refinada(db.Receita.Where(e => e.situacao == situacao && e.id_usuario==id_usuario && e.data.Equals(registo)).ToList());
        }
        // GET: api/Receita/5
        [ResponseType(typeof(Receita))]
        public async Task<IHttpActionResult> GetReceita(long id_registo,long id_medicamento,long id_usuario,string data)
        {

            var lista = await db.Receita.Where(e => e.id_registo == id_registo && e.id_medicamento == id_medicamento && e.id_usuario == id_usuario && e.data.Equals(data)).ToListAsync();
            Receita receita = config.Receita_refinada(lista).FirstOrDefault();
            if (receita == null)
            {
                return NotFound();
            }

            return Ok(receita);
        }

        // PUT: api/Receita/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReceita(long id, Receita receita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receita.id_registo)
            {
                return BadRequest();
            }

            db.Entry(receita).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceitaExists(id))
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

        // POST: api/Receita
        [ResponseType(typeof(Receita))]
        public async Task<IHttpActionResult> PostReceita(Receita receita)
        {

            receita = config.Adicionar_receita(receita.id_medicamento,receita.composicao,receita.quantidade,receita.forma,receita.id_usuario);
            await db.SaveChangesAsync();
            if (receita==null)
            {
                return Ok(new { mensagem="Não foi possível registar a receita! Tente novamente."});
            }
            return CreatedAtRoute("DefaultApi", new { id_registo = receita.id_registo , id_medicamento=receita.id_medicamento, id_usuario=receita.id_usuario, data=receita.data }, receita);
        }

        // DELETE: api/Receita/5
        [ResponseType(typeof(Receita))]
        public async Task<IHttpActionResult> DeleteReceita(long id)
        {
            Receita receita = await db.Receita.FindAsync(id);
            if (receita == null)
            {
                return NotFound();
            }

            db.Receita.Remove(receita);
            await db.SaveChangesAsync();

            return Ok(receita);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReceitaExists(long id)
        {
            return db.Receita.Count(e => e.id_registo == id) > 0;
        }
    }
}