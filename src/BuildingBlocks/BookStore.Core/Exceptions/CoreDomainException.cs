namespace BookStore.Core.Exceptions;

public class CoreDomainException(string message) : Exception(message)
{
    public static CoreDomainException Exception(string message) => new(message);

    public static CoreDomainException DuplicateEntity(string entityName, string key)
        => new($"{entityName} with id {key} already exists.");
}
