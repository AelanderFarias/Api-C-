using DevTour.Repository;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevTour.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var clientes = await _repository.GetClientes();
            return clientes.Any() ? Ok(clientes) : NoContent();
        }  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var cliente = await _repository.GetClienteById(id);
            return cliente != null ? Ok(cliente) : NotFound("Cliente não encontrado.");
        }  
        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente){
            _repository.AddCliente(cliente);
            return await _repository.SaveChangesAsync() ? Ok("Cliente adicionado") : BadRequest("Algo deu errado");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Cliente cliente) {
            var clienteExiste = await _repository.GetClienteById(id);

            if(clienteExiste == null) return NotFound("Cliente não encotrado");
            
            clienteExiste.Nome = cliente.Nome ?? clienteExiste.Nome;
            clienteExiste.Sobrenome = cliente.Sobrenome != new DateTime() ? cliente.Sobrenome : clienteExiste.Sobrenome;

            _repository.AtualizarCliente(clienteExiste);

            return await _repository.SaveChangesAsync() ? Ok("´Cliente Atualizado.") : BadRequest("Algo deu errado.") ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var clienteExiste = await _repository.GetClienteById(id);
            if(clienteExiste == null)
            return NotFound("Cliente não encontrado.");

            _repository.DeletarCliente(clienteExiste);
             return await _repository.SaveChangesAsync() ? Ok("´Cliente deletado.") : BadRequest("Algo deu errado.") ;
        }
    }
}