using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsDatabase.Entities
{

    [Table("door")]
    internal class DoorEntity : AbstractEntity<IDoor, int>
    {
        public DoorEntity(IDoor model) 
            : base(model)
        {
            this.Location = model.Location;
            this.Description = model.Description;
        }

        [Column("door_id")]
        public new int Id { get => base.Id; set => base.Id = value; }

        public string Location { get; set; }

        public string Description { get; set; }

        public IList<DoorUserPermissionEntity> Permissions { get; set; }

        public override IDoor MapToModel()
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
