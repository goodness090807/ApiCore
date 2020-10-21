using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICore.Dtos.TodoLists;
using APICore.Models;
using APICore.Repositorys;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICore.Controllers
{
    [Route("api/todolists")]
    [ApiController]
    [Authorize]
    public class TodoListsController : ControllerBase
    {

        private readonly TodoListRepository _todoListRepository;
        private readonly IMapper _mapper;

        public TodoListsController(TodoListRepository todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoList>> GetTodoLists()
        {
            var todoLists = _todoListRepository.GetTodoLists();

            return Ok(_mapper.Map<IEnumerable<TodoListReadDto>>(todoLists));
        }

        [HttpGet("{Id}", Name = "GetTodoList")]
        public ActionResult<TodoList> GetTodoList(string Id)
        {
            var todoLists = _todoListRepository.GetTodoListById(Id);

            return Ok(_mapper.Map<TodoListReadDto>(todoLists));
        }

        [HttpPost]
        public ActionResult<TodoListReadDto> CreateTodoList(TodoListCreateDto todoListCreateDto)
        {
            if (User.Identity.Name == null)
            {
                return Unauthorized();
            }

            var todoList = _mapper.Map<TodoList>(todoListCreateDto);

            _todoListRepository.CreateTodoList(User.Identity.Name, todoList);

            var todoListReadDto = _mapper.Map<TodoListReadDto>(todoList);

            return CreatedAtRoute("GetTodoList", new { Id = todoListReadDto.listId }, todoListReadDto);
        }

        [HttpPut("{Id}")]
        public ActionResult<TodoListReadDto> UpdateTodoList(string Id, TodoListUpdateDto todoListUpdateDto)
        {
            string userId = _todoListRepository.GetUserIdByTodoListId(Id);

            if (userId != User.Identity.Name)
            {
                return Unauthorized();
            }

            var todoList = _todoListRepository.GetTodoListById(Id);

            if (todoList == null)
            {
                return NotFound();
            }

            _mapper.Map(todoListUpdateDto, todoList);

            _todoListRepository.UpdateTodoList();

            return NoContent();
        }


        [HttpDelete("{Id}")]
        public ActionResult<TodoListReadDto> DeleteUser(string Id)
        {
            string userId = _todoListRepository.GetUserIdByTodoListId(Id);

            if (userId != User.Identity.Name)
            {
                return Unauthorized();
            }
            var todoList = _todoListRepository.GetTodoListById(Id);

            if (todoList == null)
            {
                return NotFound();
            }

            _todoListRepository.DeleteTodoList(todoList);

            return NoContent();
        }
    }
}
