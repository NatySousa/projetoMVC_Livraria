using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_Estudo_MVC.Entities;
using Projeto_Estudo_MVC.Interfaces;
using Projeto_Estudo_MVC.Models;
using Projeto_Estudo_MVC.Reports;
using Projeto_Estudo_MVC.Repositories;

namespace Projeto_Estudo_MVC.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroRepository _livroRepository; //Injeção de dependencia

        public LivroController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        public IActionResult Cadastro() // IActionResult  é o método que abre a página de cadastro

        {
            return View();
        }

        [HttpPost] //abre quando clica no SUBMIT(botão REALIZAR CADASTRO da página), o  [HttpPost] envia pro Controller os dados preenchidos na View
        public IActionResult Cadastro(LivroCadastroModel model) // fazendo isso eu não preciso instaciar com o  var e new
        {
            //verificando se todos os campos da model
            //passaram nas regras de validação..
            if (ModelState.IsValid)
            {
                try
                {
                    //cadastrar no banco de dados..
                    Livro livro = new Livro();
                    livro.DataCadastro = DateTime.Now;//Data atual
                    livro.Nome = model.Nome;// eu uso no lugar do Console.ReadLine
                    livro.Preco = Convert.ToDecimal(model.Preco);//Convert.To eu uso no lugar do Parse
                    livro.Quantidade = Convert.ToInt32(model.Quantidade);//Convert.To eu uso no lugar do Parse

                    //inserir o livro no banco de dados..
                    _livroRepository.Inserir(livro);

                    TempData["Mensagem"] = $"Livro {livro.Nome}, cadastrado com sucesso.";
                    ModelState.Clear(); //limpa os campos do formulário, para aparecer só a msg cadastrado com sucesso 
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao cadastrar o livro: " + e.Message;
                }
            }

            return View();
        }

        public IActionResult Consulta()// IActionResult  método que abre a página de consulta
        {
            var model = new LivroConsultaModel();
            try
            {
                //executar a consulta no banco de dados 
                //e armazenar o resultado
                //no atributo 'Livros' da classe LivroConsultaModel
                model.Livros = _livroRepository.Consultar();//O método consultar no repository vai no banco, executa a procedure consultarlivros e devolve uma lista de todos os livros que foram cadastrados no banco
            }
            catch (Exception e)
            {
                //exibir mensagem  na página..
                TempData["Mensagem"] = "Erro ao consultar o livro: " + e.Message;
            }

            //enviando o objeto 'model' para a página..
            return View(model);
        }
        public IActionResult Exclusao(Guid id)
        {

            try
            {
                //buscar no banco de dados o Livro atraves do id..
                var livro = _livroRepository.ObterPorId(id);
                //excluindo o Livro..
                _livroRepository.Excluir(livro);

                TempData["Mensagem"] = "Livro excluído com sucesso.";
            }
            catch (Exception e)
            {
                //exibir mensagem de erro na página..
                TempData["Mensagem"] = "Erro ao excluir o livro: " + e.Message;
            }

            //redirecionamento do usuário de volta para a página de consulta..
            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(Guid id)

        {//classe de modelo de dados..
            var model = new LivroEdicaoModel();
            try
            {
                //buscar o livro no banco de dados atraves do id..
                var livro = _livroRepository.ObterPorId(id);

                //transferir os dados do livro para a classe model..
                model.IdLivro = livro.IdLivro;
                model.Nome = livro.Nome;
                model.Preco = livro.Preco;
                model.Quantidade = livro.Quantidade;
            }
            catch (Exception e)
            {
                //exibir mensagem de erro na página..
                TempData["Mensagem"] = "Erro ao exibir o livro: " + e.Message;
            }
            //enviando o objeto model para a página..
            return View(model);
        }

        [HttpPost] //recebe o evento SUBMIT do formulário
        public IActionResult Edicao(LivroEdicaoModel model)
        {
            //verifica se todos os campos da model passaram nas regras
            //de validação do formulário (se foram validados com sucesso)
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o livro no banco de dados atraves do ID..
                    var livro = _livroRepository.ObterPorId(model.IdLivro);

                    //alterando os dados do livro..
                    livro.Nome = model.Nome;
                    livro.Preco = Convert.ToDecimal(model.Preco);
                    livro.Quantidade = Convert.ToInt32(model.Quantidade);

                    //atualizando no banco de dados..
                    _livroRepository.Alterar(livro);
                    TempData["Mensagem"] = "Livro atualizado com sucesso.";
                    //redirecionamento de volta para a página de consulta..
                    return RedirectToAction("Consulta");
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao atualizar o livro: " + e.Message;
                }
            }
            return View();
        }
        public IActionResult Relatorio()
        {
            return View();
        }

        [HttpPost] //recebe os dados enviados pelo formulário
        public IActionResult Relatorio(LivroRelatorioModel model)
        {
            //verifica se todos os campos da model foram validados com sucesso!
            if (ModelState.IsValid)
            {
                try
                {
                    //capturando as datas informadas na página (formulario)
                    var filtroDataMin = model.DataMin;
                    var filtroDataMax = model.DataMax;

                    //executando a consulta de livros no banco de dados..
                    var livros = _livroRepository.ConsultarPorDatas
                                  (filtroDataMin, filtroDataMax);

                    //gerando o arquivo PDF..
                    var livroReport = new LivroReport();
                    var pdf = livroReport.GerarPdf
                              (filtroDataMin, filtroDataMax, livros);

                    //fazer o download do arquivo..
                    // Response.Clear();
                    // Response.ContentType = "application/pdf";
                    // Response.Headers.Add("content-disposition",
                    //                      "attachment; filename=livros.pdf");
                    // Response.Body.WriteAsync(pdf, 0, pdf.Length);
                    // Response.Body.Flush();
                    // Response.StatusCode = StatusCodes.Status200OK;

                    var nomeRelatorio = "Relatorio_" + DateTime.Now.ToShortDateString() + ".pdf";

                    return File(pdf, "application/force-download", nomeRelatorio);
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao gerar relatório: "
                                           + e.Message;
                }

            }

            return View();
        }
        //método que será chamado (executado) por um código JavaScript
        //localizado em alguma página no sistema..
        public JsonResult ObterDadosGrafico()
        {
            try
            {
                //retornar para o javascript, o conteudo 
                //da consulta feita no banco de dados..
                var qq = Json(_livroRepository.ConsultarTotal());
                return qq;
            }

            catch (Exception e)
            {
                //retornando mensagem de erro..
                return Json(e.Message);
            }
        }
    }
}
