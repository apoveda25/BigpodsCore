using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Inputs;

public interface ICreateOneVariantInput<T>
{
    public Optional<Guid> Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float Cost { get; set; }
    public int Stock { get; set; }
    public DateTimeZone CreatedAtTimezone { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ProductId { get; set; }
    public T[] VariantsOnAttributes { get; set; }
}

public interface ICreateOneVariantOnAttributeInput
{
    public Optional<Guid> Id { get; set; }
    public DateTimeZone CreatedAtTimezone { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid AttributeId { get; set; }
    public Guid VariantId { get; set; }
}