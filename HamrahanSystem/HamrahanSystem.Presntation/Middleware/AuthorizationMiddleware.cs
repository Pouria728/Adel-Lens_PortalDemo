namespace HamrahanSystem.Presntation.Middleware
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Authorization.Infrastructure;
	using Microsoft.AspNetCore.Authorization.Policy;
    using Microsoft.Identity.Client;
    using System.Security.Claims;


	public class PermissionHandler : IAuthorizationHandler
	{
		private readonly IHttpContextAccessor contextAccessor;

		public PermissionHandler(IHttpContextAccessor contextAccessor)
		{

			this.contextAccessor = contextAccessor;

		}

		public Task HandleAsync(AuthorizationHandlerContext context)
		{
			var succeed = false;
			var aa = context.User.Claims;
			//			var pendingRequirements = context.PendingRequirements.ToList();
			var Requirements = context.Requirements.ToList();

			var requiredRoles = new List<string>();
			var requiredPolicies = new List<string>();
			var listRoles = contextAccessor.HttpContext.Session.Get<List<string>>("Roles");



			foreach (var x in Requirements)
			{
				if (x is RolesAuthorizationRequirement)
				{
					requiredRoles.AddRange(((RolesAuthorizationRequirement)x).AllowedRoles.Select(x => x).ToList());
				}

			}


			foreach (var x in requiredRoles.Distinct())
			{

				if (listRoles != null && listRoles.Any(s => s.ToLower() == x.ToLower()))
				{
					succeed = true;
				}

			}
			;
			if (requiredRoles.Count > 0 && !succeed)
			{
				Requirements.ForEach(x => context.Fail());
				return Task.CompletedTask;
			}
			if (succeed)
			{
				Requirements.ForEach(x => context.Succeed(x));
				return Task.CompletedTask;
			}
			return Task.CompletedTask;
		}
		protected  Task HandleRequirementAsync(AuthorizationHandlerContext context)
		{
            context.Fail();
			return Task.CompletedTask;
		}
	}

}
