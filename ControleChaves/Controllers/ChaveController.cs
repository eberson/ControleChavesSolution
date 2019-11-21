using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleChaves.Application.Services;
using ControleChaves.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleChaves.Controllers
{
    public class ChaveController : Controller
    {
        private readonly IChaveService _chaveService;
        private readonly ILocalizacaoService _localizacaoService;

        public ChaveController(IChaveService chaveService, ILocalizacaoService localizacaoService)
        {
            _chaveService = chaveService;
            _localizacaoService = localizacaoService;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            ViewBag.Localizacao = _localizacaoService.Find(id).Result;
            return View(_chaveService.FindAll(id).Result);
        }

        [Authorize]
        public IActionResult Create(int id)
        {
            ViewBag.Localizacao = _localizacaoService.Find(id).Result;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ChaveViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var task = _chaveService.Create(vm);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index", new { id = task.Result.LocalizacaoID});

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro ao salvar a chave para o localização.");
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var vm = _chaveService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var localizacaoID = _chaveService.Find(id).Result.LocalizacaoID;
            ViewBag.Localizacao = _localizacaoService.Find(localizacaoID).Result;
            
            var task = _chaveService.Remove(id);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index", new { id = localizacaoID });

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro ao remover a chave.");
            }

            var vm = _chaveService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }
    }
}