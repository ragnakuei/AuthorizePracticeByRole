using System.Configuration;

namespace DAL.Repository
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