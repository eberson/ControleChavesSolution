using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleChaves.Application.Services;
using ControleChaves.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleChaves.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;

        public FuncionarioController(IFuncionarioService funcionarioService, IMapper mapper)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_funcionarioService.FindAll().Result);
        }

        [Authorize]
        public IActionResult Create()
        {
            PrepareSelect();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FuncionarioViewModel vm)
        {
            PrepareSelect();

            if (!ModelState.IsValid) return View(vm);

            var task = _funcionarioService.Create(vm);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index");

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro ao salvar o funcionário.");
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            PrepareSelect();
            var vm = _funcionarioService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        private void PrepareSelect()
        {
            ViewBag.Funcoes = new[]
            {
                new SelectListItem(){ Value = "DIRETORIA", Text = "Diretoria"},
                new SelectListItem(){ Value = "FUNCIONARIO", Text = "Funcionário"},
                new SelectListItem(){ Value = "PROFESSOR", Text = "Professor"},
                new SelectListItem(){ Value = "VIGILANTE", Text = "Vigilante"},
            };
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FuncionarioViewModel vm)
        {
            PrepareSelect();

            if (!ModelState.IsValid) return View(vm);

            var task = _funcionarioService.Update(vm);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index");

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro editando funcionário.");
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vm = _funcionarioService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _funcionarioService.Remove(id);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index");

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro ao remover o funcionário.");
            }

            var vm = _funcionarioService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }
    }
}