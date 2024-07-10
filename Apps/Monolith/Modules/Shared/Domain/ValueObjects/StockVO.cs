using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

public record StockVO
{
    public int Value { get; init; }

    public StockVO(int stock)
    {
        if (stock < 0)
        {
            throw new ConflictException("Stock must be greater than or equal to 0");
        }

        Value = stock;
    }
}
