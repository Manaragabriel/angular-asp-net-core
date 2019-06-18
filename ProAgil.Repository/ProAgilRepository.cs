using ProAgil.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProAgil.Domain;
using System.Threading.Tasks;
namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public readonly DataContext _contexto;
        public ProAgilRepository(DataContext contexto)
        {
            _contexto= contexto;
            _contexto.ChangeTracker.QueryTrackingBehavior= QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T: class{
            _contexto.Add(entity);
        }
        public void Update<T>(T entity) where T: class{
            _contexto.Update(entity);
            
        }
        public void Delete<T>(T entity) where T: class{
            _contexto.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync(){
            return (await _contexto.SaveChangesAsync()) > 0;
        }

        public async Task<Lote> Get_lote(int id){
            IQueryable<Lote> query= _contexto.Lotes.Where(l => l.id==id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<RedeSocial> Get_rede(int id){
            IQueryable<RedeSocial> query= _contexto.RedeSociais.Where(l => l.id==id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Evento[]> GetAllEventosByTema(string tema,bool include_palestrante){
            IQueryable<Evento> query= _contexto.Eventos.Include(c => c.lotes)
            .Include(c => c.RedeSociais); 
            if(include_palestrante){
                query= query.Include(pe => pe.palestrante_evento).ThenInclude(
                    p => p.palestrante
                );
            }
            query = query.OrderByDescending(d => d.data).Where(t => t.nome.Contains(tema));
            return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosAsync(bool include_palestrante=false){
            IQueryable<Evento> query= _contexto.Eventos.Include(c => c.lotes)
            .Include(c => c.RedeSociais); 
            if(include_palestrante){
                query= query.Include(pe => pe.palestrante_evento).ThenInclude(
                    p => p.palestrante
                );
            }
            query = query.OrderByDescending(d => d.data);
            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetAsyncEventoById(int id,bool include_palestrante){
            IQueryable<Evento> query= _contexto.Eventos.Include(c => c.lotes)
            .Include(c => c.RedeSociais); 
            if(include_palestrante){
                query= query.Include(pe => pe.palestrante_evento).ThenInclude(
                    p => p.palestrante
                );
            }
            query = query.OrderByDescending(d => d.data).Where(i => i.id==id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome,bool include_eventos=false){
            IQueryable<Palestrante> query= _contexto.Palestrantes.Include(c => c.RedeSociais); 
            if(include_eventos){
                query= query.Include(pe => pe.palestrante_evento).ThenInclude(
                    p => p.evento
                );
            }
            query = query.OrderBy(n => n.nome).Where(p => p.nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();            
        }

        public async Task<Palestrante> GetPalestranteAsync(int id,bool include_eventos=false){
            IQueryable<Palestrante> query= _contexto.Palestrantes.Include(c => c.RedeSociais); 
            if(include_eventos){
                query= query.Include(pe => pe.palestrante_evento).ThenInclude(
                    p => p.evento
                );
            }
            query = query.OrderBy(n => n.nome).Where(p => p.id==id);
            return await query.FirstOrDefaultAsync();            
        }
        }
    
    }
    
