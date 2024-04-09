using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cms.Domain.Common.ValueObjects.Conversion;

public class TitleConversion : ValueConverter<Title, string>
{
    public TitleConversion() : base(c => c.Value, c => Title.FromString(c))
    {
    }
}

