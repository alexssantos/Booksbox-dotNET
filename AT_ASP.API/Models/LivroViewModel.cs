using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.API2.Models
{
    public class LivroViewModel
    {
        public string titulo { get; set; }
        public string isbn { get; set; }
        public int ano { get; set; }
        public List<Autor_LivroViewModel> AutoresDoLivro { get; set; }
    }

    public class Autor_LivroViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
    }
}