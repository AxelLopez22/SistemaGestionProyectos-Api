using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Models;
using AutoMapper;

namespace Api_ProjectManagement.Common.Util
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CrearUsuarioDTO, Usuario>()
                .ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<CreateProyectoDTO, Proyecto>().ReverseMap();
            CreateMap<AgregarTareasDTO, Tarea>()
                .ForMember(dest => dest.InverseIdTareaPadreNavigation, opt => opt.Ignore());
            CreateMap<ComentariosDTO, Comentario>().ReverseMap();
            CreateMap<Estado, EstadoDTO>().ReverseMap();
            CreateMap<PrioridadDTO, Prioridad>().ReverseMap();
        }
    }
}
