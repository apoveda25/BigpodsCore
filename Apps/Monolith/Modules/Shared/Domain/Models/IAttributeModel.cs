namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IAttributeModel : IBaseModel
{
    public string Value { get; set; }
    public string MeasuringUnit { get; set; }
    public Guid AttributeTypeId { get; set; }
}
