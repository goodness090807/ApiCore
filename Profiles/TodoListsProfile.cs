using APICore.Dtos.TodoLists;
using APICore.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Profiles
{
    public class TodoListsProfile : Profile
    {
        public TodoListsProfile()
        {
            CreateMap<TodoList, TodoListReadDto>();
            CreateMap<TodoListCreateDto, TodoList>();
            CreateMap<TodoListUpdateDto, TodoList>();
        }
    }
}
