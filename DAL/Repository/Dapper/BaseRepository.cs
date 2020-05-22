using System.Configuration;

namespace DAL.Repository.Dapper
{
    public abstract class BaseRepository
    {
        protected readonly string ConnectionString;

        protected BaseRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["AuthorizePracticeByRole"]?.ConnectionString;
        }
    }
}