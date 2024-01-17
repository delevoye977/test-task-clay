
namespace ClayDoorsModel.Models.Definitions
{
    /// <summary>
    /// Describes the log of an unlock action.
    /// </summary>
    public interface IDoorUnlockLog
    {
        public int? Id { get; }

        /// <summary>
        /// Time when the action happened.
        /// </summary>
        public DateTime ActionTime { get; }

        /// <summary>
        /// Result of the action.
        /// </summary>
        public DoorUnlockResult ActionResult { get; }

        /// <summary>
        /// ID of the door which was to be unlocked.
        /// </summary>
        public int? DoorId { get; }

        /// <summary>
        /// Username of the user unlocking a door.
        /// </summary>
        public string? Username { get; }

    }
}
