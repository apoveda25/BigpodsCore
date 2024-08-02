namespace Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

public class ConflictException(string message) : Exception(message)
{
    public string Code => "CONFLICT";
    public string Status => "409";
}
