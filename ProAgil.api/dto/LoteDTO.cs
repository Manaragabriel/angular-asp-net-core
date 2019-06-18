namespace ProAgil.api.dto
{
    public class LoteDTO
    {
        public int id{get;set;}
        public string nome_lote{get;set;}

        public decimal preco{get;set;}
        public int quantidade{get;set;}
        public string inicio{get;set;}
        public string fim{get;set;}
       
        public int eventoid{get;set;}
        public EventoDTO evento{get;set;}
    }
}