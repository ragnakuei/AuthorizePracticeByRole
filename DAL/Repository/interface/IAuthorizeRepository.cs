using SharedLibrary.Models;

namespace DAL.Repository.@interface
{
    public interface IAuthorizeRepository
    {
        bool Auth(AuthorizationDto dto);
    }
}