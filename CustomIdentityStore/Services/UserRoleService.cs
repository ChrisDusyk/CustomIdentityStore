using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentityStore.Models;

namespace CustomIdentityStore.Services
{
	public class UserRoleService : IUserRoleService
	{
		public Task<ServiceResult> CreateAsync(string userId, string roleName)
		{
			// Call store to create user-role

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}

		public Task<ServiceResult> DeleteRoleFromUserAsync(string userId, string roleName)
		{
			// Call store to delete user-role association

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}

		public Task<List<ApplicationRole>> GetRolesForUserAsync(string userId)
		{
			// Call store to get roles for the user

			// Return results
			return Task.FromResult(new List<ApplicationRole>());
		}

		public Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName)
		{
			// Call store to get users in role

			// Return results
			return Task.FromResult(new List<ApplicationUser>());
		}
	}
}
