namespace ClayDoorsModel.Services
{
    public interface IDoorsWriteService
    {
        /// <summary>
        /// Unlocks a door.
        /// </summary>
        /// <param name="doorId">Id of the door to unlock.</param>
        /// <param name="username">Username of the user trying to unlock the door.</param>
        /// <returns>The result of the unlock operation.</returns>
        Task<DoorUnlockResult> UnlockDoor(int doorId, string username);
    }
}
