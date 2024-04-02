using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Common.ValueObjects.Conversion;

public class ImageConversion : ValueConverter<Image, string>
{
    public ImageConversion() : base(c => c.Value, c => Image.FromString(c))
    {

    }
}
