using Cms.Domain.Common.Exceptions;

namespace Cms.Domain.Common.Rules
{
    public class TheValueMustNotBeEmpty : IBusinessRule
    {
        private readonly string _value;

        public TheValueMustNotBeEmpty(string value)
        {
            _value = value;
        }

        public bool HasValidRule()
        {
            if (string.IsNullOrWhiteSpace(_value)) return false;
            return true;
        }


        public string Message => "The Value Must Not Be Empty. ";
    }
}
