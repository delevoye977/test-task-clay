using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Services.Definitions
{
    public interface IDoorUsersRepository
    {
        public IDoorUser? GetUser(string username);
    }
}
