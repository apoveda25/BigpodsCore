using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

public abstract class BaseModel : IBaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = default!;
    public DateTime CreatedAtDatetime { get; set; } = default!;
    public DateTime? UpdatedAtDatetime { get; set; } = default!;
    public DateTime? DeletedAtDatetime { get; set; } = default!;
    public string CreatedAtTimezone { get; set; } = default!;
    public string? UpdatedAtTimezone { get; set; } = default!;
    public string? DeletedAtTimezone { get; set; } = default!;
    public Guid CreatedBy { get; set; } = Guid.NewGuid();
    public Guid? UpdatedBy { get; set; } = default!;
    public Guid? DeletedBy { get; set; } = default!;
}
