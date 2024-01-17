
using ClayDoorsModel.Models.Definitions;

namespace ClayDoorsModel.Models
{
    public class DoorUnlockLog : IDoorUnlockLog
    {
        public int? Id { get; set; }

        public DateTime ActionTime { get; set; }

        public DoorUnlockResult ActionResult { get; set; }

        public int? DoorId { get; set; }

        public string? Username { get; set; }

        public DoorUnlockLog(DateTime actionTime, DoorUnlockResult actionResult, int? doorId, string? username)
        {
            ActionTime = actionTime;
            ActionResult = actionResult;
            DoorId = doorId;
            Username = username;
        }

        public DoorUnlockLog(int? id, DateTime actionTime, DoorUnlockResult actionResult, int? doorId, string? username)
            : this(actionTime, actionResult, doorId, username)
        {
            Id = id;
            ActionTime = actionTime;
            ActionResult = actionResult;
            DoorId = doorId;
            Username = username;
        }
    }
}
