using AutoMapper;
using Beamer.Domain.Models;
using Task = Beamer.Domain.Models.Task;

namespace Beamer.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDTO>();
            CreateMap<Project, ProjectDetailsDTO>();
            CreateMap<Task, TaskDTO>();
            CreateMap<Task, TaskDetailsDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserDetailsDTO>();
        }
    }
}
