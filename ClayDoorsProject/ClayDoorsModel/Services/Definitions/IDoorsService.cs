using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Services.Definitions
{
    /// <summary>
    /// Service for the read actions for doors.
    /// </summary>
    public interface IDoorsService
    {
        /// <summary>
        /// Gets the list of doors.
        /// </summary>
        /// <returns>The list of all doors.</returns>
        public Task<IEnumerable<IDoor>> GetDoors();

        /// <summary>
        /// Unlocks a door.
        /// </summary>
        /// <param name="doorId">Id of the door to unlock.</param>
        /// <param name="username">Username of the user trying to unlock the door.</param>
        /// <returns>The result of the unlock operation.</returns>
        Task<DoorUnlockResult> UnlockDoor(int doorId, string username);
    }
}
