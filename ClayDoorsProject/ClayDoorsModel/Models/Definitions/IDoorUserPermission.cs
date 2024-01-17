namespace ClayDoorsModel.Models.Definitions
{
    /// <summary>
    /// Describes the permissions for doors.
    /// </summary>
    public interface IDoorUserPermission
    {
        int Id { get; }

        string Name { get; }

        string Description { get; }
    }
}
