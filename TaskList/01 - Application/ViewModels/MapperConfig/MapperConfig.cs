using TaskList._01___Domain;
using TaskList._01___Application.ViewModels;

namespace TaskList._01___Application.ViewModels.MapperConfig
{
    
    public class MapperConfig : AutoMapper.Profile
    {
        public MapperConfig()
        {

            CreateMap<Tasks, TasksViewModel>().ReverseMap();
            CreateMap<TasksViewModel, Tasks>().ReverseMap();

        }
    }
}