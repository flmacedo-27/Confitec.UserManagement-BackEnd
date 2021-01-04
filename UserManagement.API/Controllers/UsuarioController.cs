using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Domain.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Usuario> _repository;

        public UsuarioController(IMediator mediator, IRepository<Usuario> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetUsuarioCommand());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _repository.Get(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] InsertUsuarioCommand command)
        {
            await _mediator.Send(command);
            return Ok("Criação realizada com sucesso.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUsuarioCommand command)
        {
            if (await _mediator.Send(command))
                return Ok("Atualização realizada com sucesso.");
            else
                return this.StatusCode((int)HttpStatusCode.Forbidden, new ApplicationException("O usuário referente a chave informada não existe na base de dados."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteUsuarioCommand { Id = id };

            if (await _mediator.Send(command))
                return Ok("Exclusão realizada com sucesso.");
            else
                return this.StatusCode((int)HttpStatusCode.Forbidden, new ApplicationException("O usuário referente a chave informada não existe na base de dados."));
        }
    }
}
