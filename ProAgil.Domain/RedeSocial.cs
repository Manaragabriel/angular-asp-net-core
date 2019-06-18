using System.Collections.Generic;
namespace ProAgil.Domain
{
    public class RedeSocial
    {
        public int id{get;set;}
        public string nome{get;set;} 
        public string url{get;set;}
        public int? EventoId{get;set;}
        public Evento evento{get;set;}
        public int? PalestranteId{get;set;}
        public Palestrante palestrante{get;set;}
    }
}