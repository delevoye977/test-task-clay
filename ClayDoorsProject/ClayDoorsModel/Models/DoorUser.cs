
using ClayDoorsModel.Models.Definitions;
using System.Security;

namespace ClayDoorsModel.Models
{
    public class DoorUser : IDoorUser
    {
        public int Id { get; }

        public string Username { get; }

        public IEnumerable<IDoorUserRole> Roles { get; }

        public DoorUser(int id, string username, IEnumerable<IDoorUserRole> roles)
        {
            Id = id;
            Username = username;
            Roles = roles;
        }

        public bool HasPermission(IDoorUserPermission permission)
        {
            return HasPermission(permission.Name);
        }

        public bool HasPermission(string? permissionName)
        {
            if (permissionName == null) return false;

            return Roles.Any(
                r => r.Permissions.Any(
                    p => string.Equals(p.Name, permissionName)));
        }
    }
}
