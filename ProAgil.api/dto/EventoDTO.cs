using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
namespace ProAgil.api.dto
{
    public class EventoDTO
    {
        public int id {get;set;}
        [Required(ErrorMessage="Nome do evento é necessário")]
        public string nome{get;set;}
        public string data{get;set;}
        public string local{get;set;}
        public string imagem{get;set;}
        public List <LoteDTO> lotes{get;set;}
        public List<RedeSocialDTO> RedeSociais{get;set;}
        public List<PalestranteDTO> palestrantes{get;set;}
     
    }
}