using Cms.Domain.Common.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Common.ValueObjects
{
    public class Image : BaseValueObject<Image>
    {
        #region Properties
        public string Value { get; private set; }
        #endregion

        #region Constructors and Factories
        public static Image FromString(string value) => new(value);
        public Image(string value)
        {
            
            Value = value;
        }
        private Image() { }
        #endregion

        #region Equality Check
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        #endregion

        #region Operator Overloading
        public static explicit operator string(Image description) => description.Value;

        public static implicit operator Image(string value) => new(value);
        #endregion

        #region Methods
        public override string ToString() => Value;
        #endregion
    }

}
