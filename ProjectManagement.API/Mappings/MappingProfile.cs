using AutoMapper;
using ProjectManagement.Domain.Models;
using Task = ProjectManagement.Domain.Models.Task;

namespace ProjectManagement.API.Mappings
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
