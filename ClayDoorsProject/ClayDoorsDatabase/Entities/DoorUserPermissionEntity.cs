
using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsDatabase.Entities
{
    [Table("permission")]
    internal class DoorUserPermissionEntity
    {
        [Column("permission_id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        internal IDoorUserPermission MapToModel()
        {
            return new DoorUserPermission(
                Id, 
                Name, 
                Description
                );
        }
    }
}
