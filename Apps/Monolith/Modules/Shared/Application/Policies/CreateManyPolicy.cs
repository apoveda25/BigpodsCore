using Bigpods.Monolith.Modules.Shared.Domain.Policies;

namespace Bigpods.Monolith.Modules.Shared.Application.Policies;

public record CreateManyPolicy(string Name, string Resource) : IBasePolicy
{
    public string Scope { get; } = "scopes:create-many";
}
