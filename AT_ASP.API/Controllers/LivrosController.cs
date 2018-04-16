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
using Library.API2;
using Library.API2.Models;

namespace Library.API2.Controllers
{
    [Authorize]
    public class LivrosController : ApiController
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: api/Livros
        [HttpGet]
        public List<LivroViewModel> GetLivros()
        {
            List<Livros> ListaLivrosDB = db.Livros.ToList();

            if (ListaLivrosDB == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return ToLivrosViewModel(ListaLivrosDB);
        }


        // GET: api/Livros/5        //DETAILS
        [ResponseType(typeof(Livros))]
        [HttpGet]
        public async Task<IHttpActionResult> DetailsLivro(int id)
        {
            Livros livros = await db.Livros.FindAsync(id);
            if (livros == null)
            {
                return NotFound();
            }
            var retorno = ToLivroDetails(livros);

            return Ok(retorno);
        }

        // PUT: api/Livros/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> EditLivros(int id, Livros livros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livros.Id)
            {
                return BadRequest();
            }

            db.Entry(livros).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivrosExists(id))
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

        // POST: api/Livros
        [ResponseType(typeof(Livros))]
        [HttpPost]
        public async Task<IHttpActionResult> CreateLivros(Livros livros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Livros.Add(livros);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = livros.Id }, livros);
        }

        // DELETE: api/Livros/5
        [ResponseType(typeof(Livros))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteLivros(int id)
        {
            Livros livros = await db.Livros.FindAsync(id);
            if (livros == null)
            {
                return NotFound();
            }

            db.Livros.Remove(livros);
            await db.SaveChangesAsync();

            return Ok(livros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LivrosExists(int id)
        {
            return db.Livros.Count(e => e.Id == id) > 0;
        }

        private List<LivroViewModel> ToLivrosViewModel(List<Livros> listaLivrosDB)
        {                    

            //---- Modelando objeto de retorno
            List<LivroViewModel> ListaRetorno = new List<LivroViewModel>();

            //Mapeando aobjeto
            foreach (var item in listaLivrosDB)
            {
                var novo = new LivroViewModel
                {
                    titulo = item.titulo,
                    isbn = item.isbn,
                    ano = item.ano
                };

                List<Autor_LivroViewModel> AutoresEncontrados = new List<Autor_LivroViewModel>();

                //Lista pares de:  Id_livro <-> Id_Autor
                foreach (var par in item.Autor_Livro)                       //Cada Par Livro-Autor tem um ID
                {
                    //Pega no DB Obj_Livros desse Autor
                    var autor = db.Autores.Single(x => x.Id == par.Id_Autor);

                    //mapear o livro e adiciona a lista                   
                    if (autor != null)
                    {
                        AutoresEncontrados.Add(new Autor_LivroViewModel
                        {
                            Nome = autor.Nome,
                            Sobrenome = autor.Sobrenome,
                            Email = autor.Email
                        });
                    }
                };

                novo.AutoresDoLivro = AutoresEncontrados;
                ListaRetorno.Add(novo);
            }

            return ListaRetorno;
        }

        private LivroDetails ToLivroDetails(Livros livro)
        {
            //Mapeando aobjeto            
            var LivroDetails = new LivroDetails
            {
                Id = livro.Id,
                titulo = livro.titulo,
                isbn = livro.isbn,
                ano = livro.ano,
            };

            List<Autor_LivroViewModel> AutoresDoLivro = new List<Autor_LivroViewModel>();

            //Lista de Objetos com pares de:  Id_livro <-> Id_Autor
            foreach (var par in livro.Autor_Livro)                       //Cada Par Livro-Autor tem um ID
            {
                //Pega no DB Obj_Livros desse Autor
                var autor = db.Autores.Single(x => x.Id == par.Id_Autor);

                //Add Livro na lista                   
                if (autor != null)
                {
                    AutoresDoLivro.Add(new Autor_LivroViewModel
                    {
                        Nome = autor.Nome,
                        Sobrenome = autor.Sobrenome,
                        Email= autor.Email
                    });
                }
            };
            LivroDetails.AutoresDoLivro = AutoresDoLivro;

            return LivroDetails;
        }

    }
}