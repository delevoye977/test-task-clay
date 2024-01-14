using ClayDoorsProject.Dtos;
using ClayDoorsProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClayDoorsProject.Controllers
{
    /// <summary>
    /// Controller for the doors.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DoorsController : ControllerBase
    {
        private readonly IDoorsReadService readService;
        private readonly ILogger<DoorsController> logger;

        public DoorsController(
            IDoorsReadService readService,
            ILogger<DoorsController> logger) 
        {
            this.readService = readService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the list of all doors.
        /// </summary>
        /// <returns>The list of all doors.</returns>
        [HttpGet]
        public async Task<IEnumerable<DoorResponseDto>> Get()
        {
            return (await readService.GetDoors())
                .Select(d => new DoorResponseDto(d));
        }
    }
}
