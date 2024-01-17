
using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Models
{
    public class DoorUserRole : IDoorUserRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<IDoorUserPermission> Permissions { get; set;}

        public DoorUserRole(int id, string name, string description, IEnumerable<IDoorUserPermission> permissions)
        {
            Id = id;
            Name = name;
            Description = description;
            Permissions = permissions;
        }
    }
}
