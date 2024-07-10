using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record CostVO
{
    public decimal Value { get; init; }

    public CostVO(decimal cost)
    {
        if (cost <= 0)
        {
            throw new ConflictException("Cost must be greater 0");
        }

        Value = cost;
    }
}
