using Cms.Domain.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.User.Repositories
{
    public interface IUserRepository : IRepository<User.Entities.User>
    {
   
        bool ConfirmPhoneNumber(string phoneNumber,string code);
        bool PhoneNumberIsExits(string phoneNumber);
        bool CheckUserIsExits(string phoneNumber, string password);
        User.Entities.User GetUserByPhoneNumber(string phoneNumber);
        bool Login(string phoneNumber, string password);
    }
}
