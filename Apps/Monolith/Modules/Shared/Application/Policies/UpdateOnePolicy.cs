using Bigpods.Monolith.Modules.Shared.Domain.Policies;

namespace Bigpods.Monolith.Modules.Shared.Application.Policies;

public record UpdateOnePolicy(string Name, string Resource) : IBasePolicy
{
    public string Scope { get; } = "scopes:update-one";
}
