namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IBaseModel
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAtDatetime { get; set; }
    public DateTime? UpdatedAtDatetime { get; set; }
    public DateTime? DeletedAtDatetime { get; set; }
    public string CreatedAtTimezone { get; set; }
    public string? UpdatedAtTimezone { get; set; }
    public string? DeletedAtTimezone { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public Guid? DeletedBy { get; set; }
}
