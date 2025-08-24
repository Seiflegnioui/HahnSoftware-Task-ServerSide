using hahn.Domain.Entities;

namespace hahn.Application.Services{
    public interface ICreateJwtService
    {
        string CreateJwtToken(User user);
    }
}