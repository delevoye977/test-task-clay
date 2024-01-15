using ClayDoorsModel.Models;

namespace ClayDoorsModel.Services
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
    }
}
