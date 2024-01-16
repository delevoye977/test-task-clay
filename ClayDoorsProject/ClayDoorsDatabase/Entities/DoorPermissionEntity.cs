
using ClayDoorsModel.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsDatabase.Entities
{
    [Table("permission")]
    internal class DoorPermissionEntity
    {
        [Column("permission_id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        internal IDoorPermission MapToModel()
        {
            return new DoorPermission(
                Id, 
                Name, 
                Description
                );
        }
    }
}
