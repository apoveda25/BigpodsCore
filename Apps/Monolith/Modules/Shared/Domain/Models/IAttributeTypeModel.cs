namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IAttributeTypeModel : IBaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ValuePattern { get; set; }
    public string MeasuringUnitPattern { get; set; }
}
