using Cms.Domain.Common.Repositories;
using Cms.Domain.Models.Token.Entities;
using Cms.Domain.Models.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.User.Repositories
{
    public interface IUserRepository:IRepository<Entities.User>
    {
   
        bool ConfirmPhoneNumber(string phoneNumber,string code);
        bool PhoneNumberIsExits(string phoneNumber);
        bool CheckUserIsExits(string phoneNumber, string password);
        User.Entities.User GetUserByPhoneNumber(string phoneNumber);
        bool Login(string phoneNumber, string password);
        UserRefreshToken AddUserRefreshTokens(UserRefreshToken user);
        UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken);
        void DeleteUserRefreshTokens(string username, string refreshToken);
        bool PhoneVerified(string phoneNumber);
        List<Degree> GetAllDegree();
    }
}
