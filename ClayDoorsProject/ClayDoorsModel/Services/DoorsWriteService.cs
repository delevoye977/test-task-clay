namespace ClayDoorsModel.Services
{
    public class DoorsWriteService : IDoorsWriteService
    {
        private readonly IDoorsRepository doorsRepository;

        public DoorsWriteService(IDoorsRepository doorsRepository)
        {
            this.doorsRepository = doorsRepository;
        }

        public Task<DoorUnlockResult> UnlockDoor(int doorId)
        {
            if (doorsRepository.GetDoor(doorId) != null)
            {
                // Call to unlock the door
                return Task.FromResult(DoorUnlockResult.Success);
            }
            return Task.FromResult(DoorUnlockResult.Failure);
        }
    }
}
