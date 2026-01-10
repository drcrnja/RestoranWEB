using AutoMapper;
using Restoran.BLL.DTOs;
using Restoran.DAL.Entities;

namespace Restoran.BLL.Mappings
{
    public class MappingProfile : Profile
    {
        //sebicni mapper
        public MappingProfile()
        {
         
            CreateMap<Rezervacija, RezervacijaDto>()
                .ForMember(d => d.ImeGosta, opt => opt.MapFrom(s => s.Gost.ImeGosta))
                .ForMember(d => d.PrezimeGosta, opt => opt.MapFrom(s => s.Gost.PrezimeGosta))
                .ForMember(d => d.BrojStola, opt => opt.MapFrom(s => s.Sto.BrojStola))
                .ForMember(d => d.NarudzbinaProizvod,
                            opt => opt.MapFrom(s => s.Narudzbina == null
                                                    ? null
                                                    : s.Narudzbina.Proizvod));

 
            CreateMap<RezervacijaDto, Rezervacija>()
                .ForMember(e => e.Narudzbina, opt => opt.Ignore());   
        }
    }
}