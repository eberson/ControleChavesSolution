using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleChaves.Application.Services;
using ControleChaves.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleChaves.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MovimentosController : ControllerBase
    {
        private readonly IControleService _controleService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IChaveService _chaveService;

        public MovimentosController(IControleService controleService, IFuncionarioService funcionarioService, IChaveService chaveService)
        {
            _controleService = controleService;
            _funcionarioService = funcionarioService;
            _chaveService = chaveService;
        }

        [Produces("application/json")]
        [HttpGet("movimentacao/abertos")]
        public async Task<IActionResult> MovimentosAbertos()
        {
            try
            {
                var result = await _controleService.FindAll(DateTime.Now);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("movimentacao")]
        public async Task<IActionResult> Save(EmprestimoViewModel vm)
        {
            try
            {
                var result = await _controleService.Lend(vm);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("movimentacao")]
        public async Task<IActionResult> Devolver(DevolucaoViewModel vm)
        {
            try
            {
                var result = await _controleService.GiveBack(vm);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("funcionarios")]
        public async Task<IActionResult> Funcionarios()
        {
            try
            {
                var result = await _funcionarioService.FindAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("chaves/disponiveis")]
        public async Task<IActionResult> Chaves()
        {
            try
            {
                var result = await _chaveService.FindAvailable();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}