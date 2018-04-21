using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.API2.Models
{
    public class AutorViewModel
    {
        public int      id          { get; set; }
        public string   Nome        { get; set; }
        public string   Sobrenome   { get; set; }
        public DateTime Nascimento  { get; set; }
        public List<Livro_AutorViewModel> LivrosEscritos { get; set; }
    }

    public class Livro_AutorViewModel
    {
        public string titulo { get; set; }
        public string isbn { get; set; }
        public int    ano { get; set; }
    }
}