using ClayDoorsModel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClayDoorsController.Reponses;

namespace ClayDoorsProject.Controllers
{
    /// <summary>
    /// Controller for the doors.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DoorsController : ControllerBase
    {
        private readonly IDoorsReadService doorReadService;
        private readonly IDoorsWriteService doorWriteService;
        private readonly ILogger<DoorsController> logger;

        public DoorsController(
            IDoorsReadService doorReadService,
            IDoorsWriteService doorWriteService,
            ILogger<DoorsController> logger) 
        {
            this.doorReadService = doorReadService;
            this.doorWriteService = doorWriteService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the list of all doors.
        /// </summary>
        /// <returns>The list of all doors.</returns>
        [HttpGet]
        public async Task<IEnumerable<DoorResponseDto>> GetAllDoors()
        {
            return (await doorReadService.GetDoors())
                .Select(d => new DoorResponseDto(d));
        }

        /// <summary>
        /// Unlock a door.
        /// </summary>
        /// <param name="doorId">Id of the door to unlock.</param>
        /// <returns>If the unlock operation is a success.</returns>
        [HttpPost("/{doorId}/unlock")]
        public async Task<DoorUnlockResponseDto> UnlockDoor([FromRoute] int doorId)
        {
            return new DoorUnlockResponseDto(await doorWriteService.UnlockDoor(doorId));
        }
    }
}
