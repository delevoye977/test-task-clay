namespace ClayDoorsModel.Models.Definitions
{
    /// <summary>
    /// Describes a user of the doors.
    /// </summary>
    public interface IDoorUser
    {
        int Id { get; }

        string Username { get; }

        /// <summary>
        /// List of roles the user has.
        /// </summary>
        IEnumerable<IDoorUserRole> Roles { get; }

        /// <summary>
        /// Checks if the user has the given permission.
        /// </summary>
        /// <param name="permission">Permission to check.</param>
        /// <returns>True if the user has the permission, false otherwise.</returns>
        bool HasPermission(IDoorPermission permission);
    }
}
