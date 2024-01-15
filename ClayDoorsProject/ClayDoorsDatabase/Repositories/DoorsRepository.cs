using ClayDoorsModel.Models;
using ClayDoorsModel.Services;

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
            //return await Task.Run(() => new List<Door>() { new Door(1, "test", "bidon")});
            return ctx.Doors.Select(d => new Door(d.Id, d.Location, d.Description));
        }
    }
}
