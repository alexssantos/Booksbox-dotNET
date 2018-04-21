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
using AT_ASP.API;

namespace AT_ASP.API.Controllers
{
    public class Autor_LivroController : ApiController
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: api/Autor_Livro
        public IQueryable<Autor_Livro> GetAutor_Livro()
        {
            return db.Autor_Livro;
        }
              

        // POST: api/Autor_Livro
        [ResponseType(typeof(Autor_Livro))]
        public async Task<IHttpActionResult> PostAutor_Livro(Autor_Livro autor_Livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autor_Livro.Add(autor_Livro);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = autor_Livro.Id }, autor_Livro);
        }

        // DELETE: api/Autor_Livro/5
        [ResponseType(typeof(Autor_Livro))]
        public async Task<IHttpActionResult> DeleteAutor_Livro(int id)
        {
            Autor_Livro autor_Livro = await db.Autor_Livro.FindAsync(id);
            if (autor_Livro == null)
            {
                return NotFound();
            }

            db.Autor_Livro.Remove(autor_Livro);
            await db.SaveChangesAsync();

            return Ok(autor_Livro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Autor_LivroExists(int id)
        {
            return db.Autor_Livro.Count(e => e.Id == id) > 0;
        }
    }
}