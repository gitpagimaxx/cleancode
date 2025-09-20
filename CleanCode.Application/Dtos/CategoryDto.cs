using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanCode.Application.Dtos;

public class CategoryDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [MinLength(3)]
    [MaxLength(100)]
    [DisplayName("Name")]
    public string? Name { get; set; }
}
