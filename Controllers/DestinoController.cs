using DevTour.Repository;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevTour.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinoController : ControllerBase
    {
        private readonly IDestinoRepository _repository;

        public DestinoController(IDestinoRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var destinos = await _repository.GetDestinos();
            return destinos.Any() ? Ok(destinos) : NoContent();
        }  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var destino = await _repository.GetDestinoById(id);
            return destino != null ? Ok(destino) : NotFound("Destino não encontrado.");
        }  
        [HttpPost]
        public async Task<IActionResult> Post(Destino destino){
            _repository.AddDestino(destino);
            return await _repository.SaveChangesAsync() ? Ok("Destino adicionado") : BadRequest("Algo deu errado");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Destino destino) {
            var destinoExiste = await _repository.GetDestinoById(id);

            if(destinoExiste == null) return NotFound("Destino não encotrado");
            
            destinoExiste.DestinoViagem = destino.DestinoViagem ?? destinoExiste.DestinoViagem;
        
            _repository.AtualizarDestino(destinoExiste);

            return await _repository.SaveChangesAsync() ? Ok("´Destino Atualizado.") : BadRequest("Algo deu errado.") ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var destinoExiste = await _repository.GetDestinoById(id);
            if(destinoExiste == null)
            return NotFound("Destino não encontrado.");

            _repository.DeletarDestino(destinoExiste);
             return await _repository.SaveChangesAsync() ? Ok("´Destino deletado.") : BadRequest("Algo deu errado.") ;
        }
    }
}