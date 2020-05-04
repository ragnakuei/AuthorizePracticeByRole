using SharedLibrary.Models;

namespace DAL.Repository
{
    public interface IAuthorizeRepository
    {
        bool Auth(AuthorizationDto dto);
    }
}