using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.API2.Models
{
    public class AutorDetails
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }
        public List<Livro_AutorViewModel> LivrosEscritos { get; set; }       
    }
}