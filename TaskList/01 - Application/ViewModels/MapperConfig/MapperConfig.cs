using TaskList._01___Domain;

namespace TaskList._01___Application.ViewModels.MapperConfig
{

    public class MapperConfig : AutoMapper.Profile
    {
        public MapperConfig()
        {

            CreateMap<Tasks, TasksViewModel>().ReverseMap();
            CreateMap<TasksViewModel, Tasks>().ReverseMap();

            CreateMap<Tasks, TasksViewModelResult>().ReverseMap();
            CreateMap<TasksViewModelResult, Tasks>().ReverseMap();

            CreateMap<TasksViewModelResult, TasksViewModel>().ReverseMap();
            CreateMap<TasksViewModel, TasksViewModelResult>().ReverseMap();

        }
    }
}