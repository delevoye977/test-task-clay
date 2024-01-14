using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsProject.Entities
{

    [Table("door")]
    public class DoorEntity
    {
        [Column("door_id")]
        public int Id { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }
    }
}
