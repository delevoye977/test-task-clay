using ClayDoorsModel.Models;
using ClayDoorsModel.Services;

namespace ClayDoorsDatabase.Repositories
{
    public class DoorUsersRepository : IDoorUsersRepository
    {
        private readonly ClayDoorDatabaseContext ctx;

        public DoorUsersRepository(ClayDoorDatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public IDoorUser? GetUser(string username)
        {
            var entity = ctx.Users.FirstOrDefault(u => string.Equals(u.Username, username));
            if (entity == null) return null;
            return new DoorUser(entity.Id, entity.Username);
        }
    }
}
