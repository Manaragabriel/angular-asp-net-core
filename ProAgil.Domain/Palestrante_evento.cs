namespace ProAgil.Domain
{
    public class Palestrante_evento
    { 
        public int PalestranteId{get;set;}
        public Palestrante palestrante{get;set;}
        public int EventoId{get;set;}
        public Evento evento{get;set;}

    }
}