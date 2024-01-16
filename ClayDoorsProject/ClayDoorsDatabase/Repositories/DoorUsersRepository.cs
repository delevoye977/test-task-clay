using ClayDoorsModel.Models;
using ClayDoorsModel.Services;
using Microsoft.EntityFrameworkCore;

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
            var entity = ctx.Users
                .Include(e => e.Roles).ThenInclude(e => e.Permissions)
                .FirstOrDefault(u => string.Equals(u.Username, username));
            if (entity == null) return null;
            return entity.MapToModel();
        }
    }
}
