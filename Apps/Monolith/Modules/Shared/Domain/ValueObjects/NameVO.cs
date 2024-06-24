using System.Text.RegularExpressions;

using Bigpods.Monolith.Modules.Shared.Domain.Constants;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record NameVO
{
    public string Value { get; init; }

    public NameVO(string name)
    {
        if (!Regex.Match(name, Patterns.Name).Success)
        {
            throw new ConflictException("Invalid name");
        }

        Value = name;
    }
}
