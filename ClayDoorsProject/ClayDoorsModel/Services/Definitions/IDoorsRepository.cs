using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Services.Definitions
{
    /// <summary>
    /// Repository to access the data.
    /// </summary>
    public interface IDoorsRepository
    {
        /// <summary>
        /// Returns the list of all doors.
        /// </summary>
        /// <returns>The list of all doors.</returns>
        Task<IEnumerable<IDoor>> GetAllDoors();

        /// <summary>
        /// Returns the door which has the given ID.
        /// </summary>
        /// <param name="doorId">ID of the door to get.</param>
        /// <returns>The door with the given ID if it exists, null otherwise</returns>
        IDoor? GetDoor(int doorId);

        /// <summary>
        /// Returns the logs between the given dates, by the given user.
        /// </summary>
        /// <param name="fromDate">Begining date to retrieve the logs. If missing, uses DateTime.MinValue.</param>
        /// <param name="toDate">End date to retrieve the logs. If missing, uses DateTime.MaxValue.</param>
        /// <param name="userToSearch">Username to search. If missing, searches for all.</param>
        /// <returns>The logs between the given dates.</returns>
        IEnumerable<IDoorUnlockLog> GetLogs(DateTime? fromDate, DateTime? toDate, string? userToSearch);

        /// <summary>
        /// Logs an unlock action and its result.
        /// </summary>
        void LogUnlock(IDoorUnlockLog log);
    }
}
