using System.Collections.Generic;
namespace ProAgil.Domain
{
    public class Palestrante
    {
        public int id{get;set;}
        public string nome{get;set;}
        public string telefone{get;set;}
        public string email{get;set;}
        public string imagem{get;set;}
        public List<RedeSocial> RedeSociais{get;set;}
        public List<Palestrante_evento> palestrante_evento{get;set;}
    }
}