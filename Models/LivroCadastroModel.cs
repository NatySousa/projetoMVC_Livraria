using System.ComponentModel.DataAnnotations; 

namespace Projeto_Estudo_MVC.Models
{
    public class LivroCadastroModel
    {
        
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")] //MinLength minimo de caracteres aceito
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")] //MaxLength maximo de caracteres que foi definido no banco de dados(nvarchar)
        //{1} usado para não precisar escrever o nº de caracteres na msg para o usuário, pq se for preciso mudar o o nº de caracteres, muda só ErroMessage
        [Required(ErrorMessage = "Por favor, informe o nome do livro.")] //Required faz com que o campo seja de preenchimento obrigatório
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o preço do livro.")]
        public decimal? Preco { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade do livro.")]
        public int? Quantidade { get; set; }
    }
}