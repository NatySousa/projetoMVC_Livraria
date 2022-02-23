using System;
using System.Collections.Generic;
using System.Linq;
using Projeto_Estudo_MVC.Data;
using Projeto_Estudo_MVC.Entities;
using Projeto_Estudo_MVC.Interfaces;
using Projeto_Estudo_MVC.Models;

namespace Projeto_Estudo_MVC.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ApplicationDbContext _database;
        public LivroRepository(ApplicationDbContext database)
        {
            _database = database;
        }
        public void Alterar(Livro livro)
        {
            _database.Update(livro);
            _database.SaveChanges();
        }

        public List<Livro> Consultar()
        {
            return _database.Livros.ToList();
        }

        public List<Livro> ConsultarPorDatas(DateTime dataMin, DateTime dataMax)
        {

            var lista = Consultar();
            // usei linq para filtrar a consulta 
            return lista.Where(l => l.DataCadastro >= dataMin && l.DataCadastro <= dataMax).ToList();
            
        }
        

        public List<LivroGraficoModel> ConsultarTotal()
        {

            var lista = Consultar();
            //o 'l' está fazendo a vez do foreach, de forma que eu tenha acesso aos atributos da entidade
            // var qq = lista.Select(l => new LivroGraficoModel {DataCadastro = l.DataCadastro.ToShortDateString(), 
            //             Total = lista.Sum(x => x.Quantidade)}).ToList();


            var listaGrafico = (from l in lista 
                    group l by new {l.DataCadastro} into k
                    select new LivroGraficoModel()
                    {
                        DataCadastro = k.Key.DataCadastro.ToShortDateString(),
                        Total = k.Sum(x => x.Quantidade)
                    }).ToList();

           return listaGrafico;
        }

        public void Excluir(Livro livro)
        {
            _database.Remove(livro);
            _database.SaveChanges();
        }

        public void Inserir(Livro livro)
        {
            _database.Add(livro);
            _database.SaveChanges(); //
        }

        public Livro ObterPorId(Guid idLivro)
        {
            return _database.Livros.Find(idLivro);
        }
    }
}