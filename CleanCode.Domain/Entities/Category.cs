using CleanCode.Domain.Validation;

namespace CleanCode.Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; } = string.Empty;

    public Category(string name)
    {
        ValidateDomain(name);
    }

    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id. Id is required");
        ValidateDomain(name);

        Id = id;
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");

        Name = name;
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }
}
