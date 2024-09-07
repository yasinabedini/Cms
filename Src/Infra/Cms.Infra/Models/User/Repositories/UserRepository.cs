using Cms.Domain.Models.Token.Entities;
using Cms.Domain.Models.User.Entities;
using Cms.Domain.Models.User.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cms.Infra.Models.User.Repositories
{
    public class UserRepository : BaseRepository<Domain.Models.User.Entities.User>, IUserRepository
    {
        private readonly CmsDbContext _context;

        public UserRepository(CmsDbContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckUserIsExits(string phoneNumber, string password)
        {
            return _context.Users.Any(t => t.PhoneNumber == new Domain.Common.ValueObjects.PhoneNumber(phoneNumber) && t.Password == password);

        }
        public bool ConfirmPhoneNumber(string phoneNumber, string code)
        {
            var user = _context.Users.FirstOrDefault(t => t.PhoneNumber == new Domain.Common.ValueObjects.PhoneNumber(phoneNumber));
            if (user is null) return false;
            var result = user.CheckVerificationCode(code);
            if (result is true)
            {
                user.ConfirmPhoneNumber();
                Update(user);
                Save();
            }
            return result;
        }
        public List<Domain.Models.User.Entities.User> GetList()
        {
            throw new NotImplementedException();
        }
        public Domain.Models.User.Entities.User GetUserByPhoneNumber(string phoneNumber)
        {
            return _context.Users.FirstOrDefault(t => t.PhoneNumber == new Domain.Common.ValueObjects.PhoneNumber(phoneNumber));
        }
        public bool Login(string phoneNumber, string password)
        {
            var user = _context.Users.FirstOrDefault(t => t.PhoneNumber == new Domain.Common.ValueObjects.PhoneNumber(phoneNumber));
            if (user is null) return false;
            var result = user.Login(password);
            Update(user);
            Save();
            return result;
        }
        public bool PhoneNumberIsExits(string phoneNumber)
        {
            return _context.Users.Any(t => t.PhoneNumber == new Domain.Common.ValueObjects.PhoneNumber(phoneNumber));
        }
        public UserRefreshToken AddUserRefreshTokens(UserRefreshToken user)
        {
            _context.RefreshTokens.Add(user);
            Save();
            return user;
        }
        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _context.RefreshTokens.FirstOrDefault(x => x.PhoneNumber == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _context.RefreshTokens.Remove(item);
                Save();
            }
        }
        public UserRefreshToken GetSavedRefreshTokens(string phoneNumber, string refreshToken)
        {
            return _context.RefreshTokens.FirstOrDefault(x => x.PhoneNumber == phoneNumber && x.RefreshToken == refreshToken);
        }

        public bool PhoneVerified(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, "^0[0-9]{6,10}$");
        }

        public List<Degree> GetAllDegree()
        {
            return _context.Degrees.ToList();
        }
    }
}
