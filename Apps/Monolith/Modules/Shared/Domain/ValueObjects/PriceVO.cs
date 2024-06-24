using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record PriceVO
{
    public float Value { get; init; }

    public PriceVO(float price)
    {
        if (price <= 0)
        {
            throw new ConflictException("Price must be greater than 0");
        }

        Value = price;
    }
}
