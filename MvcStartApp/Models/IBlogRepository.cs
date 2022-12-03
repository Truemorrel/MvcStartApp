using System.Threading.Tasks;

namespace MvcStartApp.Models
{
    public interface IBlogRepository
    {
        Task AddUser(WebUser user);
        Task<WebUser[]> GetUsers();
    }
}
