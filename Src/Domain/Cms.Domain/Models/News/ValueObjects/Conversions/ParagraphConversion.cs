using Cms.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.News.ValueObjects.Conversions
{
    public class ParagraphConversion : ValueConverter<Paragraph, string>
    {
        public ParagraphConversion() : base(c => c.Value, c => Paragraph.FromString(c))
        {

        }
    }
}
