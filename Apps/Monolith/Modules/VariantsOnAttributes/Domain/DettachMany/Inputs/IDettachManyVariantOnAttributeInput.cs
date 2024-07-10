using NodaTime;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Inputs;

public interface IDettachManyVariantOnAttributeInput<T>
{
    public Guid ProductId { get; set; }
    public T[] VariantsOnAttributes { get; set; }
}

public interface IDeleteOneVariantOnAttributeInput
{
    public Guid Id { get; set; }
    public DateTimeZone DeletedAtTimezone { get; set; }
    public Guid DeletedBy { get; set; }
}
