using System;
using System.ComponentModel.DataAnnotations;

namespace Projeto_Estudo_MVC.Entities
{
   
    public class Livro
    {
        [Key] //Guid como chave primaria
        public Guid IdLivro { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}