using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.ValueObject;
using MediatR;

namespace hahn.Application.Buyer.Commands
{
    public class CreateBuyerCommand : IRequest<CustomResult<BuyerDTO>>
    {
        public DateTime brthdate { get; set; }
        public Adress adress { get; set; } = null!;
        public string bio { get; set; } = string.Empty;
        public Sources mySource { get; set; }
    }
}