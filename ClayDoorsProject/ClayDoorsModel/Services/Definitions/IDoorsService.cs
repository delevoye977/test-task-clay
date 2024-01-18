using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Services.Definitions
{
    /// <summary>
    /// Service for the read actions for doors.
    /// </summary>
    public interface IDoorsService : ICRUDService<IDoor, int>
    {
        /// <summary>
        /// Get the door unlock logs between the given times.
        /// </summary>
        /// <param name="fromDate">Begining date to retrieve the logs. If missing, uses DateTime.MinValue.</param>
        /// <param name="toDate">End date to retrieve the logs. If missing, uses DateTime.MaxValue.</param>
        /// <param name="userToSearch">Username to search. If missing, searches for all.</param>
        /// <returns>The logs between the given dates.</returns>
        IEnumerable<IDoorUnlockLog> GetDoorUnlockLogs(DateTime? fromDate, DateTime? toDate, string? userToSearch);

        /// <summary>
        /// Unlocks a door.
        /// </summary>
        /// <param name="doorId">Id of the door to unlock.</param>
        /// <param name="username">Username of the user trying to unlock the door.</param>
        /// <returns>The result of the unlock operation.</returns>
        Task<DoorUnlockResult> UnlockDoor(int doorId, string username);
    }
}
