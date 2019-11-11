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
using afarmaciaApi.Controllers;
using System.Text;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace afarmaciaApi.Controllers.Api
{
    
    public class UsuarioController : ApiController
    {
        private afarmaciaEntities db = new afarmaciaEntities();
        private ConfiguracoesController config = new ConfiguracoesController();
        private const string cryptoKey = "ifarmacia_encripting_key";

        // The Initialization Vector for the DES encryption routine
        private static readonly byte[] IV =
            new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };
        // GET: api/Usuario
        public IEnumerable<Usuario> GetUsuario()
        {
            return config.Usuario_refinado(db.Usuario.ToList());
        }

        // GET: api/Usuario/5
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> GetUsuario(long id)
        {
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // GET: api/Usuario/5
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> GetUsuario(string user, string senha)
        {
            senha = Mensagiar(senha);
            var lista = await db.Usuario.Where(e => e.estado == 1 && e.usuario1.Equals(user) && e.senha.Equals(senha)).ToListAsync();
            Usuario usuario = config.Usuario_refinado(lista).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            string token = config.Mensagiar(usuario.id_usuario + "" + usuario.usuario1 + DateTime.Now);
            usuario.recover = Actualiza_usuario(usuario.id_usuario,token);
            return Ok(usuario);
        }


        // PUT: api/Usuario/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsuario(long id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.id_usuario)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuario
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuario.Add(usuario);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.id_usuario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = usuario.id_usuario }, usuario);
        }
        [System.Web.Http.AllowAnonymous]
        private string Mensagiar(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;

            string result = string.Empty;

            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);

                TripleDESCryptoServiceProvider des =
                    new TripleDESCryptoServiceProvider();

                MD5CryptoServiceProvider MD5 =
                    new MD5CryptoServiceProvider();

                des.Key =
                    MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));

                des.IV = IV;
                result = Convert.ToBase64String(
                    des.CreateEncryptor().TransformFinalBlock(
                        buffer, 0, buffer.Length));
            }
            catch
            {
                throw;
            }

            return result;
        }
        // DELETE: api/Usuario/5
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> DeleteUsuario(long id)
        {
            Usuario usuario = await db.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuario.Remove(usuario);
            await db.SaveChangesAsync();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string Actualiza_usuario(long id,string token)
        {
            Usuario usuario = db.Usuario.Find(id);
            usuario.actualizacao = DateTime.Now;
            usuario.recover = token;
            db.Entry(usuario).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return null;

            }
            return usuario.recover;
        }
        private bool UsuarioExists(long id)
        {
            return db.Usuario.Count(e => e.id_usuario == id) > 0;
        }
    }
}