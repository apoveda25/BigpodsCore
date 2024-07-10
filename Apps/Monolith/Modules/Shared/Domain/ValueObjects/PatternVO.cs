using System.Text.RegularExpressions;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record PatternVO
{
    public string Value { get; init; }

    public PatternVO(string value, string pattern)
    {
        if (!Regex.Match(value, pattern).Success)
        {
            throw new ConflictException("Value is not matching the pattern");
        }

        Value = value;
    }
}
