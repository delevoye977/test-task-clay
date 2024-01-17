using ClayDoorsModel.Models;
using ClayDoorsModel.Models.Definitions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClayDoorsDatabase.Entities
{
    [Table("door_user")]
    internal class DoorUserEntity
    {
        [Column("user_id")]
        public int Id { get; set; }

        public required string Username {  get; set; }

        public required IEnumerable<UserRoleEntity> Roles { get; set; }

        internal IDoorUser MapToModel()
        {
            return new DoorUser(
                Id,
                Username, 
                Roles?.Select(r => r.MapToModel()).ToList() ?? Enumerable.Empty<IDoorUserRole>()
                );
        }
    }
}
