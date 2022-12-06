using System.Threading.Tasks;

namespace MvcStartApp.Models.DB
{
    public interface IBlogRepository
    {
        Task AddUser(WebUser user);
        Task<WebUser[]> GetUsers();
    }
}
