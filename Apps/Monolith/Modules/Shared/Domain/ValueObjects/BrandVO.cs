using System.Text.RegularExpressions;

using Bigpods.Monolith.Modules.Shared.Domain.Constants;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record BrandVO
{
    public string Value { get; init; }

    public BrandVO(string brand)
    {
        string value = brand.Trim();

        if (!Regex.Match(value, Patterns.Brand).Success)
        {
            throw new ConflictException("Brand is not matching the pattern");
        }

        Value = value;
    }
}
