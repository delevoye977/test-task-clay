
using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Models
{
    public class Door : IDoor
    {
        public Door(int id, string location, string description, IEnumerable<IDoorPermission> permissions)
        {
            Id = id;
            Location = location;
            Description = description;
            RequiredPermissions = permissions;
        }

        public int Id { get; }

        public string Location { get; set; }

        public string Description { get; set; }

        public IEnumerable<IDoorPermission> RequiredPermissions { get; set; }

        public bool CanBeUnlockedBy(IDoorUser user)
        {
            return user != null
                && RequiredPermissions.Any()
                && RequiredPermissions.All(user.HasPermission);
        }
    }
}
