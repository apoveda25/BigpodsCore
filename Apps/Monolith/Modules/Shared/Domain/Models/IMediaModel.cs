namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IMediaModel : IBaseModel
{
    public string Path { get; set; }
    public string Url { get; set; }
    public int Position { get; set; }
    public string ContentType { get; set; }
    public string Extension { get; set; }
}
