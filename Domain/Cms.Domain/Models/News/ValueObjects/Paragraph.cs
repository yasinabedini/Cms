using Cms.Domain.Common.Rules;
using Cms.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.News.ValueObjects
{
    public class Paragraph : BaseValueObject<Paragraph>
    {
        #region Properties
        public string Value { get; private set; }
        #endregion

        #region Constructors and Factories
        public static Paragraph FromString(string value) => new(value);
        public Paragraph(string value)
        {
          
            Value = value;
        }
        private Paragraph() { }
        #endregion

        #region Equality Check
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        #endregion

        #region Operator Overloading
        public static explicit operator string(Paragraph description) => description.Value;

        public static implicit operator Paragraph(string value) => new(value);
        #endregion

        #region Methods
        public override string ToString() => Value;
        #endregion
    }



}
