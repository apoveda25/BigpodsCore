namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IVariantOnAttributeModel : IBaseModel
{
    Guid VariantId { get; set; }
    Guid AttributeId { get; set; }
}
