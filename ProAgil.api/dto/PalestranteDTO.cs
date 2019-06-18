using System.Collections.Generic;

namespace ProAgil.api.dto
{
    public class PalestranteDTO
    {
         
        public int id{get;set;}
        public string nome{get;set;}
        public string telefone{get;set;}
        public string email{get;set;}
        public string imagem{get;set;}
        public List<RedeSocialDTO> RedeSociais{get;set;}
        public List<EventoDTO> eventos {get;set;}
    }
}