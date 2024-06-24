namespace Bigpods.Monolith.Modules.Shared.Domain.Policies;

public interface IBasePolicy
{
    public string Name { get; }
    public string Resource { get; }
    public string Scope { get; }
}