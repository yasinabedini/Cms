using Cms.Domain.Models.User.Repositories;
using Cms.Infra.Common.Repository;
using Cms.Infra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
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
    }
}
