using System;
using System.Collections.Generic;
using APICore.Dtos.TodoLists;
using APICore.Dtos.UserInfos;
using APICore.Models;
using APICore.Repositorys;
using APICore.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APICore.Controllers
{
    [Route("api/userinfos")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Manager")]
    public class UserInfosController : ControllerBase
    {
        private readonly UserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;

        public UserInfosController(UserInfoRepository userInfoRepository, IMapper mapper)
        {
            _userInfoRepository = userInfoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// GET api/users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult<IEnumerable<UserInfo>> GetUsers()
        {
            var userInfos = _userInfoRepository.GetUserInfos();

            return Ok(_mapper.Map<IEnumerable<UserInfoReadDto>>(userInfos));
        }

        [HttpGet("{Id}", Name = "GetUser")]
        public ActionResult<UserInfoReadDto> GetUser(string Id)
        {
            if (Id != User.Identity.Name)
            {
                return Unauthorized();
            }

            var userInfo = _userInfoRepository.GetUserInfoById(Id);

            if (userInfo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserInfoReadDto>(userInfo));
        }

        [HttpGet("{Id}/todolists")]
        public ActionResult<IEnumerable<TodoListReadDto>> GetUserTodoLists(string Id)
        {
            var todoLists = _userInfoRepository.GetTodoListsByUserId(Id);

            return Ok(_mapper.Map<IEnumerable<TodoListReadDto>>(todoLists));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UserInfoReadDto> CreateUser(UserInfoCreateDto userInfoCreateDto)
        {
            userInfoCreateDto.userId = Guid.NewGuid().ToString();
            userInfoCreateDto.userPassword = userInfoCreateDto.userPassword.ConvertToSha256();
            userInfoCreateDto.userRoles = "Admin";

            var userInfo = _mapper.Map<UserInfo>(userInfoCreateDto);

            _userInfoRepository.CreateUser(userInfo);

            var userInfoReadDto = _mapper.Map<UserInfoReadDto>(userInfo);

            return CreatedAtRoute("GetUser", new { Id = userInfoCreateDto.userId }, userInfoReadDto);
        }

        [HttpPut("{Id}")]
        public ActionResult<UserInfoReadDto> UpdateUser(string Id, UserInfoUpdateDto userInfoUpdateDto)
        {
            if (Id != User.Identity.Name)
            {
                return Unauthorized();
            }
            var userInfo = _userInfoRepository.GetUserInfoById(Id);

            if (userInfo == null)
            {
                return NotFound();
            }

            _mapper.Map(userInfoUpdateDto, userInfo);

            _userInfoRepository.UpdateUser();

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public ActionResult<UserInfoReadDto> DeleteUser(string Id)
        {
            if (Id != User.Identity.Name)
            {
                return Unauthorized();
            }
            var userInfo = _userInfoRepository.GetUserInfoById(Id);

            if (userInfo == null)
            {
                return NotFound();
            }

            _userInfoRepository.DeleteUser(userInfo);

            return NoContent();
        }

    }
}
