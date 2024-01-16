namespace ClayDoorsModel.Services
{
    public class DoorsWriteService : IDoorsWriteService
    {
        private readonly IDoorsRepository doorsRepository;
        private readonly IDoorUserReadService doorUserReadService;

        public DoorsWriteService(
            IDoorsRepository doorsRepository,
            IDoorUserReadService doorUserReadService)
        {
            this.doorsRepository = doorsRepository;
            this.doorUserReadService = doorUserReadService;
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
