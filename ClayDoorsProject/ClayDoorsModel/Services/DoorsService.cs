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
            var door = doorsRepository.GetDoor(doorId);
            var user = doorUserReadService.GetUser(username);

            if (door != null && door.CanBeUnlockedBy(user))
            {
                // Call to actually unlock the door
                return Task.FromResult(DoorUnlockResult.Success);
            }
            return Task.FromResult(DoorUnlockResult.Failure);
        }
    }
}
