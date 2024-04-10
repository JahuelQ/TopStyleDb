using TopStyleDb.Models.Entities;

namespace TopStyleDb.Core.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(Customer customer);
    }
}
