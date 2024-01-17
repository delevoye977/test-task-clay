using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Services.Definitions
{
    public interface IDoorUserService
    {
        IDoorUser GetUser(string username);
    }
}
