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
    //[Authorize]
    public class AutoresController : ApiController
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: api/Autores         //ALL
        [HttpGet]
        public List<AutorViewModel> GetAutores()
        {
            //pega todos os autores
            List<Autores> ListaAutoresDB = db.Autores.ToList();

            if (ListaAutoresDB == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return ToAutoresViewModel(ListaAutoresDB);
        }

        // GET: api/Autores/5       //DETAILS
        [ResponseType(typeof(Autores))]
        [HttpGet]
        public async Task<IHttpActionResult> GetAutor(int id)
        {
            Autores autores = await db.Autores.FindAsync(id);
            if (autores == null)
            {
                return NotFound();
            }

            var retorno = ToAutorDetails(autores);
            return Ok(retorno);
        }

        // -----------------------------PUT:  colocar validações no App MVC
        // PUT: api/Autores/5       //UPDATE
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAutor(int id, Autores autores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autores.Id)
            {
                return BadRequest();
            }
                       
            db.Entry(autores).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoresExists(id))
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

        // POST: api/Autores        //CREATE
        [ResponseType(typeof(Autores))]
        [HttpPost]
        public async Task<IHttpActionResult> CreateAutor(Autores autores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autores.Add(autores);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = autores.Id }, autores);
        }

        // DELETE: api/Autores/5    //DELETE
        [ResponseType(typeof(Autores))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAutor(int id)
        {
            //Pega Autor pelo ID e Verifica
            Autores autores = await db.Autores.FindAsync(id);
            if (autores == null)
            {
                return NotFound();
            }

            //Deleta
            db.Autores.Remove(autores);
            await db.SaveChangesAsync();

            return Ok(autores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutoresExists(int id)
        {
            return db.Autores.Count(e => e.Id == id) > 0;
        }

        private List<AutorViewModel> ToAutoresViewModel(List<Autores> ListaAutores)
        {

            //---- Modelando objeto de retorno
            List<AutorViewModel> ListaRetorno = new List<AutorViewModel>();

            //Mapeando aobjeto
            foreach (var item in ListaAutores)
            {
                var novo = new AutorViewModel
                {
                    id = item.Id,
                    Nome = item.Nome,
                    Sobrenome = item.Sobrenome,
                    Nascimento = item.Nascimento.Date
                };

                List<Livro_AutorViewModel> LivrosDoAutor = new List<Livro_AutorViewModel>();

                //Lista de Objetos com pares de:  Id_livro <-> Id_Autor
                foreach (var par in item.Autor_Livro)                       //Cada Par Livro-Autor tem um ID
                {
                    //Pega no DB Obj_Livros desse Autor
                    var livro = db.Livros.Single(x => x.Id == par.Id_Livro);

                    //mapear o livro e adiciona a lista                   
                    if (livro != null) {
                        LivrosDoAutor.Add(new Livro_AutorViewModel { titulo = livro.titulo,
                                                                     isbn = livro.isbn,
                                                                     ano = livro.ano });
                    }
                };
                novo.LivrosEscritos = LivrosDoAutor;
                ListaRetorno.Add(novo);
            }

            return ListaRetorno;
        }

        private AutorDetails ToAutorDetails(Autores Autor)
        {
            //Mapeando aobjeto            
            var autorDetalhes = new AutorDetails
            {
                Id = Autor.Id,
                Nome = Autor.Nome,
                Sobrenome = Autor.Sobrenome,
                Email = Autor.Email,
                Nascimento = Autor.Nascimento.Date
            };

            List<Livro_AutorViewModel> LivrosDoAutor = new List<Livro_AutorViewModel>();

            //Lista de Objetos com pares de:  Id_livro <-> Id_Autor
            foreach (var par in Autor.Autor_Livro)                       //Cada Par Livro-Autor tem um ID
            {
                //Pega no DB Obj_Livros desse Autor
                var livro = db.Livros.Single(x => x.Id == par.Id_Livro);

                //Add Livro na lista                   
                if (livro != null)
                {
                    LivrosDoAutor.Add(new Livro_AutorViewModel
                    {
                        titulo = livro.titulo,
                        isbn = livro.isbn,
                        ano = livro.ano
                    });
                }
            };
            autorDetalhes.LivrosEscritos = LivrosDoAutor;                     

            return autorDetalhes;
        }

    }
}