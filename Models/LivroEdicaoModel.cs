using System;
using System.ComponentModel.DataAnnotations;

namespace Projeto_Estudo_MVC.Models
{
    public class LivroEdicaoModel
    {
           //campo oculto no formulário
        public Guid IdLivro { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]

        [Required(ErrorMessage = "Por favor, informe o nome do livro.")]
        public string Nome { get; set; }

       [Required(ErrorMessage = "Por favor, informe o preço do livro.")]
       public decimal? Preco { get; set; }

      [Required(ErrorMessage = "Por favor, informe a quantidade do livro.")]
      public int? Quantidade { get; set; }
    }
}