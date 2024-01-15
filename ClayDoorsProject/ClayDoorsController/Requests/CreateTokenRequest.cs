using System.ComponentModel.DataAnnotations;

namespace ClayDoorsController.Requests
{
    public class CreateTokenRequest
    {
        [Required]
        public required string Username { get; set; }
    }
}
