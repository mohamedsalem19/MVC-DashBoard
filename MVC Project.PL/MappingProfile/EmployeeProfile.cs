using AutoMapper;
using MVC_Project.DAL.Models;
using MVC_Project.PL.ViewModels;

namespace MVC_Project.PL.MappingProfile
{
    public class EmployeeProfile :Profile
    {

        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
