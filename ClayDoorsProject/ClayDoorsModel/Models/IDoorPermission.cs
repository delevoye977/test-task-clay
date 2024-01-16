namespace ClayDoorsModel.Models
{
    /// <summary>
    /// Describes the permissions for doors.
    /// </summary>
    public interface IDoorPermission
    {
        int Id { get; }

        string Name { get; }

        string Description { get; }
    }
}
