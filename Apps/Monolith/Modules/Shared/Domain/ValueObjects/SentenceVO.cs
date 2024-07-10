using System.Text.RegularExpressions;
using Bigpods.Monolith.Modules.Shared.Domain.Constants;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record SentenceVO
{
    public string Value { get; init; }

    public SentenceVO(string sentence)
    {
        string value = sentence.Trim();

        if (!Regex.Match(value, Patterns.Sentence).Success)
        {
            throw new ConflictException("Sentence is not matching the pattern");
        }

        Value = value;
    }
}
