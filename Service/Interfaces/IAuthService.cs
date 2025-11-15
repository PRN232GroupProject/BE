using BusinessObjects.DTO;
using BusinessObjects.Metadata;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
    }
}