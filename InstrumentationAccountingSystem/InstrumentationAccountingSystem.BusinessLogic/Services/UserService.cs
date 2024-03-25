//using InstrumentationAccountingSystem.BusinessLogic.Interfaces;
using AutoMapper;
using InstrumentationAccountingSystem.BusinessLogic.Interfaces;
using InstrumentationAccountingSystem.Common.Dto;
using InstrumentationAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentationAccountingSystem.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public UserService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public void Create(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<UserCreateDto, User>(userCreateDto);

            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            var users = _applicationContext.Users.ToList();

            return users;
        }

        public User Get(UserLogInDto userLogInDto)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Login == userLogInDto.Login && u.Password == userLogInDto.Password);

            return user;
        }

        public User GetUserById(int id)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public void DeleteUserById(int id)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Id == id);

            _applicationContext.Users.Remove(user);
            _applicationContext.SaveChanges();
        }

        public void Edit(User user)
        {
            _applicationContext.Users.Update(user);
            _applicationContext.SaveChanges();
        }
    }
}
