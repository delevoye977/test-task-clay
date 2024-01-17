using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using ClayDoorsModel.Services.Definitions;

namespace ClayDoorsModel.Services
{
    public class DoorsService : IDoorsService
    {
        private readonly IDoorsRepository doorsRepository;
        private readonly IDoorUserService doorUserService;

        public DoorsService(
            IDoorsRepository doorsRepository,
            IDoorUserService doorUserService)
        {
            this.doorsRepository = doorsRepository;
            this.doorUserService = doorUserService;
        }

        public async Task<IEnumerable<IDoor>> GetDoors() //Wishing covariant return types makes it to C# soon
        {
            return await doorsRepository.GetAllDoors();
        }


        public Task<DoorUnlockResult> UnlockDoor(int doorId, string username)
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
                    var door = doorsRepository.GetDoor(doorId);
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

            return Task.FromResult(result);
        }
    }
}
