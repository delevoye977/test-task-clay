using ClayDoorsModel.Models;

namespace ClayDoorsModel.Services
{
    public interface IDoorUserReadService
    {
        IDoorUser GetUser(string username);
    }
}
