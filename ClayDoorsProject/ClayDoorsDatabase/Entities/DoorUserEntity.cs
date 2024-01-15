using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsDatabase.Entities
{
    [Table("door_user")]
    public class DoorUserEntity
    {
        [Column("user_id")]
        public int Id { get; set; }

        public required string Username {  get; set; }
    }
}
