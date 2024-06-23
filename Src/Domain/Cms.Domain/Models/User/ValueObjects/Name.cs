using Cms.Domain.Common.Rules;
using Cms.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Domain.Models.User.ValueObjects
{
    public class Name:BaseValueObject<Name>
    {
        #region Properties
        public string Value { get; private set; }
        #endregion

        #region Constructors and Factories
        public static Name FromString(string value) => new Name(value);
        public Name(string value)
        {
            CheckRule(new TheValueMustNotBeEmpty(value));          

            Value = value;
        }
        private Name()
        {

        }
        #endregion


        #region Equality Check
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        #endregion

        #region Operator Overloading
        public static explicit operator string(Name title) => title.Value;
        public static implicit operator Name(string value) => new(value);
        #endregion

        #region Methods
        public override string ToString() => Value;

        #endregion
    }
}
