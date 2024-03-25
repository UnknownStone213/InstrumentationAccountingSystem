using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentationAccountingSystem.Common.Dto;
using InstrumentationAccountingSystem.Models;

namespace InstrumentationAccountingSystem.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        void Create(UserCreateDto userCreateDto);

        List<User> GetAll();

        User Get(UserLogInDto userLogInDto);

        User GetUserById(int id);

        void DeleteUserById(int id);

        void Edit(User user);
    }
}
