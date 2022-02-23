using System;
using System.Collections.Generic;
using Projeto_Estudo_MVC.Entities;
using Projeto_Estudo_MVC.Models;

namespace Projeto_Estudo_MVC.Interfaces
{
    public interface ILivroRepository
    {
        //métodos abstratos
         void Inserir(Livro livro);
         void Alterar(Livro livro);
         void Excluir(Livro livro);
         
         List<Livro> Consultar();
        Livro ObterPorId(Guid idLivro);
        List<Livro> ConsultarPorDatas(DateTime dataMin, DateTime dataMax);
        List<LivroGraficoModel> ConsultarTotal();
    }
}