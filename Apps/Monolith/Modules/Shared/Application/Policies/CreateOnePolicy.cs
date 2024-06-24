using Bigpods.Monolith.Modules.Shared.Domain.Policies;

namespace Bigpods.Monolith.Modules.Shared.Application.Policies;

public record CreateOnePolicy(string Name, string Resource) : IBasePolicy
{
    public string Scope { get; } = "scopes:create-one";
}