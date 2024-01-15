using ClayDoorsModel.Models;

namespace ClayDoorsProject.Dtos
{
    public class DoorResponseDto : IDoor
    {
        public int Id { get; }
        public string Location { get; }
        public string Description { get; }

        public DoorResponseDto(IDoor d)
        {
            Id = d.Id;
            Location = d.Location;
            Description = d.Description;
        }
    }
}
