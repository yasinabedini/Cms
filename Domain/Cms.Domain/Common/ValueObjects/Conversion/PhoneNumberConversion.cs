using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cms.Domain.Common.ValueObjects.Conversion;

public class PhoneNumberConversion : ValueConverter<PhoneNumber, string>
{
    public PhoneNumberConversion() : base(c => c.Value, c => PhoneNumber.FromString(c))
    {

    }
}