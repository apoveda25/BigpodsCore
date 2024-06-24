using System.Text.RegularExpressions;

using Bigpods.Monolith.Modules.Shared.Domain.Constants;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record WordVO
{
    public string Value { get; init; }

    public WordVO(string word)
    {
        if (!Regex.Match(word, Patterns.Word).Success)
        {
            throw new ConflictException("Word is not matching the pattern");
        }

        Value = word;
    }
}
