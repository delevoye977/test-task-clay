using ClayDoorsProject.Models;

namespace ClayDoorsProject.Services
{
    /// <summary>
    /// Service for the read actions for doors.
    /// </summary>
    public interface IDoorsReadService
    {
        /// <summary>
        /// Gets the list of doors.
        /// </summary>
        /// <returns>The list of all doors.</returns>
        public Task<IEnumerable<Door>> GetDoors();
    }
}
