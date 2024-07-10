using System.Text.RegularExpressions;
using Bigpods.Monolith.Modules.Shared.Domain.Constants;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record ModelVO
{
    public string Value { get; init; }

    public ModelVO(string model)
    {
        string value = model.Trim();

        if (!Regex.Match(value, Patterns.Model).Success)
        {
            throw new ConflictException("Model is not matching the pattern");
        }

        Value = value;
    }
}
