using CleanCode.Domain.Entities;
using FluentAssertions;

namespace CleanCode.Domain.Tests;

public class ProductUnitTest1
{
    [Fact(DisplayName = "Create Product Object With Valid State")]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 10, "product-image.png");
        action.Should()
              .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Product Object With Invalid Name")]
    public void CreateProduct_WithInvalidName_DomainExceptionInvalidName()
    {
        Action action = () => new Product(1, "", "Product Description", 9.99m, 10, "product-image.png");
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid name. Name is required");
    }

    [Fact(DisplayName = "Create Product Object With Short Name")]
    public void CreateProduct_WithShortName_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 10, "product-image.png");
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact(DisplayName = "Create Product Object With Negative Price")]
    public void CreateProduct_NegativePrice_DomainExceptionInvalidPrice()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 10, "product-image.png");
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid price value");
    }

    [Theory(DisplayName = "Create Product Object With Negative Stock")]
    [InlineData(-10)]
    public void CreateProduct_NegativeStock_DomainExceptionInvalidStock(int value)
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "product-image.png");
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid stock value");
    }

    [Fact(DisplayName = "Create Product Object With Long Image Name")]
    public void CreateProduct_WithLongImageName_DomainExceptionLongImageName()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 10, new string('a', 251));
        action.Should()
              .Throw<Validation.DomainExceptionValidation>()
              .WithMessage("Invalid image name, too long, maximum 250 characters");
    }

    [Fact(DisplayName = "Create Product Object With Image Null")]
    public void CreateProduct_WithImageNull_NoDomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 10, null!);
        action.Should()
              .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Product Object Null Reference With Image Null")]
    public void CreateProduct_WithImageNull_NoReferenceException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 10, null!);
        action.Should()
              .NotThrow<NullReferenceException>();
    }

    [Fact(DisplayName = "Update Product Object With Valid State")]
    public void UpdateProduct_WithValidParameters_ResultObjectValidState()
    {
        // Arrange
        var product = new Product(1, "Product Name", "Product Description", 9.99m, 10, "product-image.png");
        // Act
        Action action = () => product.Update("New Product Name", "New Product Description", 19.99m, 20, "new-product-image.png", 2);
        // Assert
        action.Should()
              .NotThrow<Validation.DomainExceptionValidation>();
        product.Name.Should().Be("New Product Name");
        product.Description.Should().Be("New Product Description");
        product.Price.Should().Be(19.99m);
        product.Stock.Should().Be(20);
        product.Image.Should().Be("new-product-image.png");
        product.CategoryId.Should().Be(2);
    }
}
