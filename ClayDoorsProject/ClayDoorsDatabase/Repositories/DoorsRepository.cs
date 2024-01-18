using ClayDoorsDatabase.Entities;
using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services.Definitions;

namespace ClayDoorsDatabase.Repositories
{
    public class DoorsRepository : GenericCRUDRepository<IDoor, int>, IDoorsRepository
    {
        private readonly ClayDoorDatabaseContext ctx;

        public DoorsRepository(ClayDoorDatabaseContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }
        //IDatabaseContext<DoorEntity, IDoor, int>
        public async void LogUnlock(IDoorUnlockLog log)
        {
            await ctx.DoorUnlockLogs.AddAsync(new DoorUnlockLogEntity()
            {
                ActionTime = log.ActionTime,
                ActionResult = log.ActionResult.ToString(),
                DoorId = log.DoorId,
                Username = log.Username,
            });
            ctx.SaveChangesAsync();
        }

        public IEnumerable<IDoorUnlockLog> GetLogs(DateTime? fromDate, DateTime? toDate, string? userToSearch)
        {
            return ctx.DoorUnlockLogs
                .Where(log => 
                    log.ActionTime > fromDate 
                    && log.ActionTime < toDate 
                    && (userToSearch == null || log.Username == userToSearch))
                .Select(e => e.MapToModel())
                .ToList();
        }

        protected override AbstractEntity<IDoor, int> MapToEntity(IDoor model)
        {
            return new DoorEntity(model);
        }
    }
}
