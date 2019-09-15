using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral
         void Add<T> (T entity) where T : class;
         void Update<T> (T entity) where T : class;
         void Delete<T> (T entity) where T : class;
         Task<bool> SaveChangesAsync();

         //Eventos
         Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);
         Task<Evento> GetAllEventoAsyncById(int eventoId, bool includePalestrantes);
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);

         //Palestrantes
         Task<Palestrante> GetAllPalestranteAsync(int palestranteId, bool includeEventos);
         Task<Palestrante[]> GetAllPalestranteAsyncByName(string nome, bool includeEventos);

    }
}