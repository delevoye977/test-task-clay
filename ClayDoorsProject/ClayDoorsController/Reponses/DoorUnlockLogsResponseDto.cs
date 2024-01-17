
using ClayDoorsModel.Models;

namespace ClayDoorsController.Reponses
{
    public class DoorUnlockLogsResponseDto
    {
        public DateTime ActionTime { get; set; }

        public DoorUnlockResult ActionResult { get; set; }

        public int? DoorId { get; set; }

        public string? Username { get; set; }
    }
}
