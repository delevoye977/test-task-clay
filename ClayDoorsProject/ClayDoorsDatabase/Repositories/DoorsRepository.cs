using ClayDoorsModel.Models;
using ClayDoorsModel.Services;
using Microsoft.EntityFrameworkCore;

namespace ClayDoorsDatabase.Repositories
{
    public class DoorsRepository : IDoorsRepository
    {
        private readonly ClayDoorDatabaseContext ctx;

        public DoorsRepository(ClayDoorDatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<IEnumerable<IDoor>> GetAllDoors()
        {
            return await Task.Run(
                () => ctx.Doors.Include(e => e.Permissions).Select(
                    d => d.MapToModel())
                .ToList());
        }

        public IDoor? GetDoor(int doorId)
        {
            var entity = ctx.Doors.Include(e => e.Permissions)
                .FirstOrDefault(d => d.Id == doorId);
            if (entity == null) return null;
            return entity.MapToModel();
        }
    }
}
