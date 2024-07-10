using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "AttributeTypeType")]
public sealed class AttributeTypeModel : BaseModel, IAttributeTypeModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ValuePattern { get; set; } = default!;
    public string MeasuringUnitPattern { get; set; } = default!;

    public ICollection<AttributeModel> Attributes { get; set; } = default!;
}
