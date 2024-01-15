namespace ClayDoorsModel.Services
{
    public interface IDoorsWriteService
    {
        /// <summary>
        /// Unlocks a door.
        /// </summary>
        /// <param name="doorId">Id of the door to unlock.</param>
        /// <returns>The result of the unlock operation.</returns>
        Task<DoorUnlockResult> UnlockDoor(int doorId);
    }
}
