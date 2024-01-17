using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClayDoorsController.Reponses;
using Microsoft.AspNetCore.Authorization;
using ClayDoorsModel.Services.Definitions;

namespace ClayDoorsProject.Controllers
{
    /// <summary>
    /// Controller for the doors.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoorsController : ControllerBase
    {
        private readonly IDoorsService doorsService;
        private readonly ILogger<DoorsController> logger;

        public DoorsController(
            IDoorsService doorsService,
            ILogger<DoorsController> logger) 
        {
            this.doorsService = doorsService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the list of all doors.
        /// </summary>
        /// <returns>The list of all doors.</returns>
        [HttpGet]
        public async Task<IEnumerable<DoorResponseDto>> GetAllDoors()
        {
            return (await doorsService.GetDoors())
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
            var username = User.Identity?.Name;
            return new DoorUnlockResponseDto(await doorsService.UnlockDoor(doorId, username));
        }
    }
}
