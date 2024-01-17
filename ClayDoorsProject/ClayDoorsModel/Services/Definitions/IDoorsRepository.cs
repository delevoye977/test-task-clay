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
    }
}
