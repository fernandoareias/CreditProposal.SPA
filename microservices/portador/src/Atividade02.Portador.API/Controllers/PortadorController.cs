using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atividade02.Core.Common.Validators.Interfaces;
using Atividade02.Core.Mediator.Interfaces;
using Atividade02.Proponent.API.Controllers;
using Atividade02.Proponent.API.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Atividade02.Portador.API.Controllers
{
    [Route("portadores")]
    [ApiController]
    public class PortadorController : CommonController
    {
        private readonly IPortadorRepository _portadorRepository;

        public PortadorController(IPortadorRepository PortadorRepository)
        {
            _portadorRepository = PortadorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var views = await _portadorRepository.GetAll();

            if (views is null || !views.Any()) return NotFound();

            return Ok(views);

        }
    }
}