using Model;

namespace DevTour.Repository
{
    public interface IDestinoRepository
    {
        Task<IEnumerable<Destino>> GetDestinos();
        Task<Destino> GetDestinoById(int id);

        void AddDestino(Destino destino);
        void AtualizarDestino(Destino destino);
        void DeletarDestino(Destino destino);

        Task<bool> SaveChangesAsync();
        
    }
}