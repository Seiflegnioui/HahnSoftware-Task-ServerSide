using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.ValueObject;
using MediatR;

namespace hahn.Application.Seller.Commands
{
    public class CreateSellerCommand : IRequest<CustomResult<SellerDTO>> 
    {
        public string shopName { get; set; } = string.Empty;
        public IFormFile? shopLogo { get; set; }  = null!;
        public string shopDescription { get; set; }  = string.Empty;
        public Adress? adress { get; set; } = null!;
        public Boolean hasLocal { get; set; } = false;
        public Adress? localAdress { get; set; } = null!;
        public string field { get; set; } = string.Empty;
        public string? personalSite { get; set; }
        public string? facebook { get; set; }
        public string? instagram { get; set; }
        public Sources mySource { get; set; } = Sources.Other;
    }
}