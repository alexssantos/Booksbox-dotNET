using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AT_ASP.Web.Models
{
    public class AutorDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Sobrenome")]
        [MaxLength(50)]
        public string Sobrenome { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Informe o email")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }

        public List<LivrosDoAutorModel> LivrosEscritos { get; set; }
    }
}