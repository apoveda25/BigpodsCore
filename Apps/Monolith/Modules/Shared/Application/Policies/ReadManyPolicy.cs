using Bigpods.Monolith.Modules.Shared.Domain.Policies;

namespace Bigpods.Monolith.Modules.Shared.Application.Policies;

public record ReadManyPolicy(string Name, string Resource) : IBasePolicy
{
    public string Scope { get; } = "scopes:read-many";
}
