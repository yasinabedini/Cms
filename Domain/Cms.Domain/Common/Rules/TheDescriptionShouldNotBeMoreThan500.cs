using Cms.Domain.Common.Exceptions;

namespace Cms.Domain.Common.Rules;

public class TheDescriptionShouldNotBeMoreThan2000 : IBusinessRule
{
    public string value { get; set; }

    public TheDescriptionShouldNotBeMoreThan2000(string value)
    {
        this.value = value;
    }

    public bool HasValidRule()
    {
        if (value.Length <= 2000) return true;
        return false;
    }
    public string Message => "The description should not be more than 500. ";
}
