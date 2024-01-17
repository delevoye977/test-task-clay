
using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Models
{
    public class DoorPermission : IDoorPermission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DoorPermission(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
    }
}
