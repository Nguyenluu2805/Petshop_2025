using PetShop.Core.DTOs;

namespace PetShop.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto?> RegisterAsync(RegisterUserDto registerUserDto);
        Task<string?> LoginAsync(LoginUserDto loginUserDto);
    }
}
