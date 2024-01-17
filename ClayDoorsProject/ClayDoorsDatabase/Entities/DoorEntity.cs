using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsDatabase.Entities
{

    [Table("door")]
    internal class DoorEntity
    {
        [Column("door_id")]
        public int Id { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public IList<DoorUserPermissionEntity> Permissions { get; set; }

        internal IDoor MapToModel()
        {
            return new Door(
                Id,
                Location,
                Description,
                Permissions?.Select(p => p.MapToModel()).ToList() ?? Enumerable.Empty<IDoorUserPermission>()
                );
        }
    }
}
