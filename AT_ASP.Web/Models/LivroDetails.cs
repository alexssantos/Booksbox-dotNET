using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_ASP.Web.Models
{
    public class LivroDetails
    {
        public int Id { get; set; }
        public string titulo { get; set; }
        public string isbn { get; set; }
        public int ano { get; set; }
        public List<Autor_LivroViewModel> AutoresDoLivro { get; set; }
    }
}