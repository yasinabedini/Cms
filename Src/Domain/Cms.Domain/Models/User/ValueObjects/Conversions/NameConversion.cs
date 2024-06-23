using Cms.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.User.ValueObjects.Conversions
{
    public class NameConversion : ValueConverter<Name, string>
    {
        public NameConversion() : base(c => c.Value, c => Name.FromString(c))
        {
        }
    }
}
