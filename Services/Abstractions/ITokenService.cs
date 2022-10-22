using CatalogAPI.DTOs;

namespace CatalogAPI.Services.Abstractions
{
    public interface ITokenService
    {
        UserToken GenerateToken(UserDTO userInfo);
    }
}
