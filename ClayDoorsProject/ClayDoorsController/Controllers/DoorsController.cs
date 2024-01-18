using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClayDoorsController.Reponses;
using Microsoft.AspNetCore.Authorization;
using ClayDoorsModel.Services.Definitions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ClayDoorsProject.Controllers
{
    /// <summary>
    /// Controller for the doors.
    /// </summary>
    [Authorize]
    [Route("api/doors")]
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
        [HttpPost("{doorId}/unlock")]
        public async Task<ActionResult<DoorUnlockResponseDto>> UnlockDoor([FromRoute] int doorId)
        {
            var username = User.Identity?.Name;

            var result = await doorsService.UnlockDoor(doorId, username);

            var resultResponse = new DoorUnlockResponseDto(result);
            switch (result)
            {
                case ClayDoorsModel.Models.DoorUnlockResult.UserNotFound:
                case ClayDoorsModel.Models.DoorUnlockResult.Unauthorized:
                    return Unauthorized(resultResponse);
                case ClayDoorsModel.Models.DoorUnlockResult.DoorNotFound:
                    return NotFound(resultResponse);
                case ClayDoorsModel.Models.DoorUnlockResult.Success:
                    return Ok(resultResponse);
                default: return BadRequest(resultResponse);
            }
        }

        /// <summary>
        /// Gets the logs at the given dates by the given user.
        /// </summary>
        /// <param name="fromDate">Date to start retrieving the logs.</param>
        /// <param name="toDate">Date for the end of the logs.</param>
        /// <param name="userToSearch">User to retrieve the logs from.</param>
        /// <returns>The list of logs.</returns>
        [Authorize(Policy = "CanViewDoorLogs")]
        [HttpGet("logs")]
        public async Task<ActionResult<DoorUnlockLogsResponseDto>> GetLogs(
            [FromQuery(Name = "from")] DateTime? fromDate, // Format is like 2024-01-16T01:00:00.000Z
            [FromQuery(Name = "to")] DateTime? toDate,
            [FromQuery(Name = "user")] string? userToSearch)
        {
            return Ok(doorsService
                .GetDoorUnlockLogs(fromDate, toDate, userToSearch)
                .Select(model => new DoorUnlockLogsResponseDto
                {
                    Username = model.Username,
                    DoorId = model.DoorId,
                    ActionResult = model.ActionResult,
                    ActionTime = model.ActionTime
                }));
        }

    }
}
