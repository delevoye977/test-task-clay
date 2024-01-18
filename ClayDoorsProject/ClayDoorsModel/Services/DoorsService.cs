using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services.Definitions;
using ClayDoorsModel.Services.Generic;

namespace ClayDoorsModel.Services
{
    public class DoorsService : GenericCRUDService<IDoor, int>, IDoorsService
    {
        private readonly IDoorsRepository doorsRepository;
        private readonly IDoorUserService doorUserService;

        public DoorsService(
            IDoorsRepository doorsRepository,
            IDoorUserService doorUserService)
            : base(doorsRepository)
        {
            this.doorsRepository = doorsRepository;
            this.doorUserService = doorUserService;
        }

        public IEnumerable<IDoorUnlockLog> GetDoorUnlockLogs(DateTime? fromDate, DateTime? toDate, string? userToSearch)
        {
            if (fromDate == null) fromDate = DateTime.MinValue;
            if (toDate == null) toDate = DateTime.MaxValue;

            return doorsRepository.GetLogs(fromDate, toDate, userToSearch);
        }

        public async Task<DoorUnlockResult> UnlockDoor(int doorId, string username)
        {
            DoorUnlockResult result;

            if (username == null)
                result = DoorUnlockResult.Unauthorized;
            else
            {
                var user = doorUserService.GetUser(username);

                if (user == null)
                    result = DoorUnlockResult.UserNotFound;
                else
                {
                    var door = await doorsRepository.Get(doorId);
                    if (door == null) result = DoorUnlockResult.DoorNotFound;
                    else if (!door.CanBeUnlockedBy(user)) result = DoorUnlockResult.Unauthorized;
                    else
                    {
                        // TODO : Actual call to unlock the door.

                        result = DoorUnlockResult.Success;
                    }
                }
            }
            
            
            this.doorsRepository.LogUnlock(new DoorUnlockLog(DateTime.UtcNow, result, doorId, username));

            return result;
        }
    }
}
