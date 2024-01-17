using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services.Definitions;

namespace ClayDoorsModel.Services
{
    public class DoorsService : IDoorsService
    {
        private readonly IDoorsRepository doorsRepository;
        private readonly IDoorUserService doorUserReadService;

        public DoorsService(
            IDoorsRepository doorsRepository,
            IDoorUserService doorUserReadService)
        {
            this.doorsRepository = doorsRepository;
            this.doorUserReadService = doorUserReadService;
        }

        public async Task<IEnumerable<IDoor>> GetDoors() //Wishing covariant return types makes it to C# soon
        {
            return await doorsRepository.GetAllDoors();
        }


        public Task<DoorUnlockResult> UnlockDoor(int doorId, string username)
        {
            var result = DoorUnlockResult.Failure;

            var door = doorsRepository.GetDoor(doorId);
            var user = doorUserReadService.GetUser(username);

            if (door == null) result = DoorUnlockResult.DoorNotFound;
            else if (user == null) result = DoorUnlockResult.UserNotFound;
            else if (!door.CanBeUnlockedBy(user)) result = DoorUnlockResult.Unauthorized;
            else
            {
                // TODO : Actual call to unlock the door.

                result = DoorUnlockResult.Success;
            }
            this.doorsRepository.LogUnlock(DateTime.UtcNow, result, doorId, username);

            return Task.FromResult(result);
        }
    }
}
