using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_ASP.Web.Models
{
    public class Autor_Livro
    {
        public int Id { get; set; }
        public int IdAutor { get; set; }
        public int IdLivro { get; set; }
    }
}