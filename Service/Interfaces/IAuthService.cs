using BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.DTO.User.Auth;
using BusinessObjects.Metadata;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<UserDTO> RegisterAsync(RegisterRequest request);
    }
}