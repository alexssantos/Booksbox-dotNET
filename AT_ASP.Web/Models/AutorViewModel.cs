using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_ASP.Web.Models
{    
    public class AutorViewModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime Nascimento { get; set; }
        public List<LivrosDoAutorModel> LivrosEscritos { get; set; }
    }

    public class LivrosDoAutorModel
    {
        public string titulo { get; set; }
        public string isbn { get; set; }
        public int ano { get; set; }
    }
}