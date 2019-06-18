using ProAgil.Domain;
using System.Threading.Tasks;
namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<Lote> Get_lote(int id);
         Task<RedeSocial> Get_rede(int id);
         Task<bool> SaveChangesAsync();
         Task<Evento[]> GetAllEventosByTema(string tema,bool include_palestrante);
         Task<Evento[]> GetAllEventosAsync(bool include_palestrante);
         Task<Evento> GetAsyncEventoById(int id,bool include_palestrante);
         Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome,bool include_eventos);
         Task<Palestrante> GetPalestranteAsync(int id,bool include_eventos);

    }
}