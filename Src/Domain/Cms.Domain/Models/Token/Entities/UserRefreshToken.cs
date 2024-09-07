using Cms.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.Token.Entities
{
    public class UserRefreshToken
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
        
        public string RefreshToken { get; set; }
        public bool IsDelete { get; set; }

        public UserRefreshToken(string phoneNumber, string refreshToken)
        {
            PhoneNumber = phoneNumber;
            RefreshToken = refreshToken;
        }
    }
}
