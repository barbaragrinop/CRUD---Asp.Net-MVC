using CRUD.Models;
using CRUD.Repositorio;
using System;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class TimeController : Controller
    {
        private TimeRepositorio _repositorio; //isso aqui referencia a variavel pra poder trabalhar com ela

        [HttpGet]
        public ActionResult MostrarTimes()
        {
            _repositorio = new TimeRepositorio();
            _repositorio.MostraTimes();
            ModelState.Clear();
            return View(_repositorio.MostraTimes());
        }

        //quando for trabalhar com inclusão ou alteração dos dados, precisa ter 2 métodos - 1 get e o outro post 
        //GET é o padrão

        [HttpGet]
        public ActionResult AdicionarTime()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarTime(Times objTimes)
        {
            try
            {
                if (ModelState.IsValid) //se ele for válido e não tive nenhum erro no envio do form
                {
                    _repositorio = new TimeRepositorio(); //vai chamar todo o repositorio pra essa variavel 
                    if (_repositorio.AdicionarTime(objTimes)) //se o reposotirio for realizado com sucesso
                    {
                        ViewBag.Mensagem = "Time Cadastrado com Sucesso";
                    }
                }
                return View();

            }
            catch(Exception)
            {
                return View("MostrarTimes");
            }
        }

        [HttpGet]
        public ActionResult EditarTime(int id)
        {
            _repositorio = new TimeRepositorio();
            return View(_repositorio.MostraTimes().Find(t => t.cd_time == id)); //vai retornar a view fazendo uma comparação e mostrando o resultado
        }

        [HttpPost]
        public ActionResult EditarTime(int id, Times objTime)
        {
            try //se der tudo certo entra aqui
            {
                _repositorio = new TimeRepositorio();
                _repositorio.AlterarTime(objTime); 
                return RedirectToAction("MostrarTimes"); 
            }
            catch(Exception)
            { //se não der  
                return View("MostrarTimes");
            }

        }

        public ActionResult ExcluirTime(int id)
        {
            try
            {
                _repositorio = new TimeRepositorio();
                _repositorio.ExcluirTime(id);
                return RedirectToAction("MostrarTimes");
            }
            catch (Exception)
            {
                return View("MostrarTimes");
            }
        }
    }
}