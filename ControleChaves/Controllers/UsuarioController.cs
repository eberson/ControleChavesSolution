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
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_usuarioService.FindAll().Result);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuarioViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var task = _usuarioService.Save(vm);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index");

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro ao salvar o usuário.");
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = _usuarioService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EditUsuarioViewModel>(vm));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUsuarioViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var task = _usuarioService.Save(_mapper.Map<UsuarioViewModel>(vm));

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index");

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro editando usuário.");
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vm = _usuarioService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EditUsuarioViewModel>(vm));
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _usuarioService.Remove(id);

            if (task.IsCompletedSuccessfully)
                return RedirectToAction("Index");

            if (task.IsFaulted)
            {
                ModelState.AddModelError("ErrorSaving", "Erro ao remover o usuário.");
            }

            var vm = _usuarioService.Find(id).Result;

            if (vm == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EditUsuarioViewModel>(vm));
        }

    }
}