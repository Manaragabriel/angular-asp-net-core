using System;

namespace ProAgil.Domain
{
    public class Lote
    {
        public int id{get;set;}
        public string nome_lote{get;set;}

        public decimal preco{get;set;}
        public int quantidade{get;set;}
        public DateTime? inicio{get;set;}
        public DateTime? fim{get;set;}
       
        public int eventoid{get;set;}
        public Evento evento{get;set;}
    }
}