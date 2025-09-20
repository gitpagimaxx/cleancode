using CleanCode.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.Products.Commands;

public class ProductRemoveCommand(int id) : IRequest<Product>
{
    public int Id { get; set; } = id;
}
