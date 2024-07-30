using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "AttributeType")]
public sealed class AttributeModel : BaseModel, IAttributeModel
{
    public string Value { get; set; } = default!;
    public string MeasuringUnit { get; set; } = default!;
    public Guid AttributeTypeId { get; set; } = Guid.NewGuid();

    public ICollection<VariantOnAttributeModel> VariantsOnAttributes { get; set; } = default!;
    public AttributeTypeModel AttributeType { get; set; } = default!;
}
