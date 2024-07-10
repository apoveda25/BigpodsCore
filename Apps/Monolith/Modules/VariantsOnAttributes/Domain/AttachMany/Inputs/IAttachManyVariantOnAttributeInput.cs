using NodaTime;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Inputs;

public interface IAttachManyVariantOnAttributeInput<T>
{
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
