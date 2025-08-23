using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Enums;
using MediatR;

namespace hahn.Application.Product.Commands
{
    public class CreateProductCommand : IRequest<CustomResult<ProductDTO>>
    {
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public CategoryEnum category { get; set; }
        public IFormFile image { get; set; } = null!;
        public int price { get; set; } 
        public int quantity { get; set; } 
     
    }
}