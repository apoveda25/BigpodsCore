using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record PriceVO
{
    public decimal Value { get; init; }

    public PriceVO(decimal price)
    {
        if (price <= 0)
        {
            throw new ConflictException("Price must be greater than 0");
        }

        Value = price;
    }
}
