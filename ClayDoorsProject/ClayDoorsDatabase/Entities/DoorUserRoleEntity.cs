

using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsDatabase.Entities
{
    [Table("role")]
    internal class DoorUserRoleEntity
    {
        [Column("role_id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<DoorUserPermissionEntity> Permissions { get; set; }

        internal IDoorUserRole MapToModel()
        {
            return new DoorUserRole(
                Id,
                Name,
                Description,
                Permissions?.Select(p => p.MapToModel()).ToList() ?? Enumerable.Empty<IDoorUserPermission>()
                );
        }
    }
}
