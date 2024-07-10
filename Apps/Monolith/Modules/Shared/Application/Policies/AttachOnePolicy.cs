using Bigpods.Monolith.Modules.Shared.Domain.Policies;

namespace Bigpods.Monolith.Modules.Shared.Application.Policies;

public record AttachOnePolicy(string Name, string Resource) : IBasePolicy
{
    public string Scope { get; } = "scopes:attach-one";
}
