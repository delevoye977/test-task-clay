namespace ClayDoorsProject.Models
{
    public class Door
    {
        public Door(int id, string location, string description)
        {
            Id = id;
            Location = location;
            Description = description;
        }

        public int Id { get; }

        public string Location { get; set; }

        public string Description { get; set; }
    }
}
