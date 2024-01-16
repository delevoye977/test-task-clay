namespace ClayDoorsModel.Models
{
    /// <summary>
    /// Describes a role of the user.
    /// </summary>
    public interface IDoorUserRole
    {
        string Name { get; }

        string Description { get; }

        /// <summary>
        /// List of the permissions this role grants.
        /// </summary>
        IEnumerable<IDoorPermission> Permissions { get; }
    }
}
