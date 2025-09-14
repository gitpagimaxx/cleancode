using CleanCode.Domain.Entities;
using FluentAssertions;

namespace CleanCode.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Create Category Object With Valid State")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should()
              .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Category Object With Invalid Name")]
    public void CreateCategory_WithInvalidName_DomainExceptionInvalidName()
    {
        Action action = () => new Category(1, "");
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid name. Name is required");
    }

    [Fact(DisplayName = "Create Category Object With Short Name")]
    public void CreateCategory_WithShortName_DomainExceptionShortName()
    {
        Action action = () => new Category(1, "Ca");
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact(DisplayName = "Create Category Object With Negative Id")]
    public void CreateCategory_NegativeId_DomainExceptionInvalidId()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid Id. Id is required");
    }

    [Fact(DisplayName = "Update Category Object With Valid State")]
    public void UpdateCategory_WithValidParameters_ResultObjectValidState()
    {
        // Arrange
        var category = new Category(1, "Category Name");
        // Act
        Action action = () => category.Update("New Category Name");
        // Assert
        action.Should()
              .NotThrow<Validation.DomainExceptionValidation>();
        category.Name.Should().Be("New Category Name");
    }

    [Fact(DisplayName = "Update Category Object With Invalid Name")]
    public void UpdateCategory_WithInvalidName_DomainExceptionInvalidName()
    {
        // Arrange
        var category = new Category(1, "Category Name");
        // Act
        Action action = () => category.Update("");
        // Assert
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid name. Name is required");
    }

    [Fact(DisplayName = "Update Category Object With Short Name")]
    public void UpdateCategory_WithShortName_DomainExceptionShortName()
    {
        // Arrange
        var category = new Category(1, "Category Name");
        // Act
        Action action = () => category.Update("Ca");
        // Assert
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid name, too short, minimum 3 characters");
    }
}
