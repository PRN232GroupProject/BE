using BusinessObjects.DTO;
using BusinessObjects.Metadata;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        //Task<ServiceResult<string>> RegisterAsync(string fullName, string email, string password, int roleId);
        //Task<ServiceResult<string>> RegisterAsync(RegisterRequest request);
    }
}