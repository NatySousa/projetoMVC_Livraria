using System;
using System.ComponentModel.DataAnnotations;

namespace Projeto_Estudo_MVC.Models
{
    public class LivroRelatorioModel
    {
          [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public DateTime DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public DateTime DataMax { get; set; }
    }
}