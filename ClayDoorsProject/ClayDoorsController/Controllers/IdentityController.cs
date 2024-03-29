﻿using ClayDoorsController.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using ClayDoorsModel.Services.Definitions;

namespace ClayDoorsController.Controllers
{

    /// <summary>
    /// Controller to manage identity.
    /// </summary>
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly byte[] issuerKey;
        private readonly string audience;
        private readonly string issuer;
        private readonly int tokenLifetime;

        private readonly IDoorUserService doorUserService;

        public IdentityController(IConfiguration configuration,
            IDoorUserService doorUserService)
        {
            audience = configuration.GetValue<string>("JwtSettings:Audience")!;
            issuer = configuration.GetValue<string>("JwtSettings:Issuer")!;
            issuerKey = Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:IssuerKey")!);
            tokenLifetime = configuration.GetValue<int>("JwtSettings:TokenLifetimeSeconds");

            this.doorUserService = doorUserService;
        }

        /// <summary>
        /// Creates a token to use for authentication.
        /// </summary>
        /// <param name="request">The reauired parameters for the authentication.</param>
        /// <returns>The generated token if the authentication succeeded.</returns>
        [HttpPost("token")]
        public IActionResult CreateToken([FromBody]CreateTokenRequest request)
        {
            var user = doorUserService.GetUser(request.Username);
            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Name, user.Username),
                new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            };

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = audience,
                Issuer = issuer,
                Expires = DateTime.UtcNow.AddSeconds(tokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(issuerKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDesc);

            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
