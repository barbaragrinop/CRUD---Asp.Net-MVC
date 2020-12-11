using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class Times
    {
        [Display(Name = "Código")] //para apresentar na tela 
        public int cd_time { get; set; }
        [Required(ErrorMessage = "Informe o nome do time.")]
        public string nm_time { get; set; }
        [Required(ErrorMessage = "informe as cores")]
        public string nm_cores { get; set; }
        [Required(ErrorMessage = "Infrome o nome do estado.")]
        public string sg_estado { get; set; }
    }
}