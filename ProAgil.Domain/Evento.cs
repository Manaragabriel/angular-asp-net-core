using System;
using System.Collections.Generic;
namespace ProAgil.Domain
{
    public class Evento
    {
        public int id {get;set;}
        public string nome{get;set;}
        public DateTime data{get;set;}
        public string local{get;set;}
        public string imagem{get;set;}
        public List <Lote> lotes{get;set;}
        public List<RedeSocial> RedeSociais{get;set;}
        public List<Palestrante_evento> palestrante_evento{get;set;}
    }
}