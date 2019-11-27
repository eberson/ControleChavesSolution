using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleChaves.Application.Services;
using ControleChaves.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleChaves.Controllers
{
    public class MovimentacaoController : Controller
    {

        private readonly IControleService _controleService;

        public MovimentacaoController(IControleService controleService) => _controleService = controleService;
        

        public IActionResult Create()
        {
            return PartialView();
        }

        public IActionResult Devolver(int controleID)
        {
            var controle = _controleService.Find(controleID).Result;

            ViewBag.Controle = controle;

            return PartialView();
        }
    }
}