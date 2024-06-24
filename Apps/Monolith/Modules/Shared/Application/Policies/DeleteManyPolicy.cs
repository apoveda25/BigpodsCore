using Bigpods.Monolith.Modules.Shared.Domain.Policies;

namespace Bigpods.Monolith.Modules.Shared.Application.Policies;

public record DeleteManyPolicy(string Name, string Resource) : IBasePolicy
{
    public string Scope { get; } = "scopes:delete-many";
}