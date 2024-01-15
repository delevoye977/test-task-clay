using ClayDoorsModel.Models;

namespace ClayDoorsProject.Dtos
{
    public class DoorResponseDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public DoorResponseDto(IDoor d)
        {
            Id = d.Id;
            Location = d.Location;
            Description = d.Description;
        }
    }
}
