using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleChaves.Application.Services;
using ControleChaves.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleChaves.Controllers
{
    public class LocalizacaoController : Controller
    {
        private readonly ILocalizacaoService _localizacaoService;

        public LocalizacaoController(ILocalizacaoService localizacaoService) => _localizacaoService = localizacaoService;

        [Authorize]
        public IActionResult Index()
        {
            return View(_localizacaoService.FindAll().Result);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LocalizacaoViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var task = _localizacaoService.Create(vm);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index", "Chave", new { id = task.Result.ID });

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro ao salvar a localização.");
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = _localizacaoService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LocalizacaoViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var task = _localizacaoService.Update(vm);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index", "Chave", new { id = task.Result.ID });

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro editando localização.");
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vm = _localizacaoService.Find(id).Result;

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
            var task = _localizacaoService.Remove(id);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index");

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro ao remover o localização.");
            }

            var vm = _localizacaoService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            var vm = _localizacaoService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }
    }
}