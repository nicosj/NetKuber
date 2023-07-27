using AutoMapper;
using NetKuber.Dtos.InmuebleDtos;
using NetKuber.Models;

namespace NetKuber.Profiles;

public class InmuebleProfile:Profile
{
    public InmuebleProfile()
    {
        CreateMap<Inmueble, InmuebleResponseDto>();
        CreateMap<InmuebleRequestDto, Inmueble>();
    }
}