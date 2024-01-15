using ClayDoorsModel;

namespace ClayDoorsController.Reponses
{
    public class DoorUnlockResponseDto
    {
        public bool IsSuccess { get; }

        public DoorUnlockResponseDto(DoorUnlockResult result)
        {
            IsSuccess = result == DoorUnlockResult.Success;
        }
    }
}
