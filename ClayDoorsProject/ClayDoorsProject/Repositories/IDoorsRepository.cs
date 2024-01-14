using ClayDoorsProject.Models;

namespace ClayDoorsProject.Repositories
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
        Task<IEnumerable<Door>> GetAllDoors();
    }
}
