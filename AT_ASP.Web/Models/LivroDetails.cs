using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AT_ASP.Web.Models
{
    public class LivroDetails
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Título")]
        [MaxLength(50)]
        public string titulo { get; set; }

        //Regex - ISBN 10 Digitos -> [RegularExpression(@"^(?:ISBN(?:-10)?:?\)?(?=[0-9X]{10}$|(?=(?:[0-9]+[-\]){3})[-\0-9X]{13}$)[0-9]{1,5}[-\]?[0 - 9]+[-\]?[0 - 9]+[-\]?[0 - 9X]$", ErrorMessage = "Número ISBN Incorreto. 10 Digitos")]       
        [Required(ErrorMessage = "Campo Obrigatorio. Ex.: XXX-XXX-XXXXX-X-X")]
        [Display(Name = "ISBN", Description = "XXX-XXX-XXXXX-X-X")]
        public string isbn { get; set; }

        [Required]
        [Display(Name = "Ano")]        
        public int ano { get; set; }

        public List<AutoresDoLivro> AutoresDoLivro { get; set; }

        public int AutorId { get; set; }
        public IList<SelectListItem> AutoresDisponiveis { get; set; }

        public LivroDetails()
        {
            AutoresDisponiveis = new List<SelectListItem>();
        }
    }
}