namespace Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
    public string Code => "NOT_FOUND";
    public string Status => "404";
}
