namespace ClayDoorsModel.Models.Definitions
{
    /// <summary>
    /// Describes a door.
    /// </summary>
    public interface IDoor
    {
        public int Id { get; }

        public string Location { get; }

        public string Description { get; }

        /// <summary>
        /// List of the permissions required to open the door.
        /// </summary>
        public IEnumerable<IDoorPermission> RequiredPermissions { get; }

        /// <summary>
        /// Tells if the door can be unlocked by the given user.
        /// 
        /// If the door has no permissions set, none can open it.
        /// </summary>
        /// <param name="user">User trying to open the door.</param>
        /// <returns>True if the door can be unlocked by the user, false otherwise.</returns>
        bool CanBeUnlockedBy(IDoorUser user);
    }
}
