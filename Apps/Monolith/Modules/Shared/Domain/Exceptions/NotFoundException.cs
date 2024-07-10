namespace Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

public class NotFoundException(string message) : Exception($"Not found: {message}");
