using Bigpods.Monolith.Modules.Shared.Domain.Policies;

namespace Bigpods.Monolith.Modules.Shared.Application.Policies;

public record ReadOnePolicy(string Name, string Resource) : IBasePolicy
{
    public string Scope => "scopes:read-one";
}
