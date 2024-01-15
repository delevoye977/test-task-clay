using ClayDoorsModel.Models;

namespace ClayDoorsModel.Services
{
    public interface IDoorUsersRepository
    {
        public IDoorUser? GetUser(string username);
    }
}
