using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //Eventos
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include( e => e.Lotes)
                .Include( e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include( e => e.PalestranteEventos)
                    .ThenInclude( pe => pe.Palestrante);
            }

            query = query.OrderByDescending( e => e.DataEvento );

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoAsyncById(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include( e => e.Lotes)
                .Include( e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include( e => e.PalestranteEventos)
                    .ThenInclude( pe => pe.Palestrante);
            }

            query = query.AsNoTracking()
                .OrderByDescending( e => e.DataEvento )
                .Where( e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include( e => e.Lotes)
                .Include( e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include( e => e.PalestranteEventos)
                    .ThenInclude( pe => pe.Palestrante);
            }

            query = query.AsNoTracking()
                .OrderByDescending( e => e.DataEvento )
                .Where( e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        //Palestrantes
        public async Task<Palestrante> GetAllPalestranteAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include( p => p.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include( p => p.PalestranteEventos)
                    .ThenInclude( pe => pe.Evento);
            }

            query = query.AsNoTracking()
                .Where( p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include( c => c.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include( pe => pe.PalestranteEventos)
                    .ThenInclude( p => p.Evento);
            }

            query = query.AsNoTracking()
                .OrderByDescending( c => c.Nome )
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}