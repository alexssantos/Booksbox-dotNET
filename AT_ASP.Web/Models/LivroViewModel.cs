using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_ASP.Web.Models
{    
    public class LivroViewModel
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string isbn { get; set; }
        public int ano { get; set; }
        public List<AutoresDoLivro> AutoresDoLivro { get; set; }
    }

    public class AutoresDoLivro
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
    }
}