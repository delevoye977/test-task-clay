using ClayDoorsModel.Services.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;

namespace ClayDoorsController.Middlewares
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }

        public HasPermissionRequirement(string permissionName) 
        {
            PermissionName = permissionName;
        }

    }

    public class HasPermissionPolicyHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        private readonly IDoorUserService doorUserService;

        public HasPermissionPolicyHandler(IDoorUserService doorUserService) 
        {
            this.doorUserService = doorUserService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, HasPermissionRequirement requirement)
        {
            var username = context.User.Identity?.Name;

            if (username == null)
            {
                FailedAuthorize(context, "No user provided for authorization.");
                return Task.CompletedTask;
            }
            
            var user = doorUserService.GetUser(username);

            if (user.HasPermission(requirement.PermissionName))
                context.Succeed(requirement);
            else
                FailedAuthorize(context, "User does not have the required permissions.");
            
            return Task.CompletedTask;
        }

        private async void FailedAuthorize(AuthorizationHandlerContext context, string failMessage)
        {
            context.Fail(new AuthorizationFailureReason(this, failMessage));

            if (context.Resource is HttpContext httpContext)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpContext.Response.ContentType = "application/json";

                var response = new
                {
                    Message = failMessage,
                };

                var json = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }

}
