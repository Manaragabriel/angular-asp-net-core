using System.Linq;
using AutoMapper;
using ProAgil.api.dto;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.api.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles(){
            CreateMap<Evento,EventoDTO>().ForMember(
                ev=>ev.palestrantes,opt=>{
                    opt.MapFrom(src=> src.palestrante_evento.Select(x=>x.palestrante).ToList());
                }
            );
            CreateMap<EventoDTO,Evento>();
            CreateMap<Lote,LoteDTO>();
            CreateMap<RedeSocial,RedeSocialDTO>();
            CreateMap<RedeSocialDTO,RedeSocial>();
            CreateMap<Palestrante,PalestranteDTO>().ForMember(
                dest => dest.eventos, opt=>{
                    opt.MapFrom(src=> src.palestrante_evento.Select(x=> x.evento).ToList());
                }
            );
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<User,UserLoginDTO>().ReverseMap();
            
        }
    }
}