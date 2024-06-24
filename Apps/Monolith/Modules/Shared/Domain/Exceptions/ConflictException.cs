namespace Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

public class ConflictException(string message) : Exception($"Conflict: {message}");