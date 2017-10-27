using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentityStore.Models;

namespace CustomIdentityStore.Services
{
	/// <summary>
	/// This service will interact with the actual Role Store to feed into your custom IRoleStore implementation.
	/// </summary>
	public class CustomRoleService : ICustomRoleService
	{
		public Task<ServiceResult> CreateAsync(ApplicationRole applicationRole)
		{
			// Call store to create role

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}

		public Task<ServiceResult> DeleteAsync(string roleId)
		{
			// Call store to delete role

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}

		public Task<ApplicationRole> FindAsync(string roleId)
		{
			// Get role from store by ID

			return Task.FromResult(new ApplicationRole());
		}

		public Task<ApplicationRole> FindByNameAsync(string roleName)
		{
			// Get role from store by role name

			return Task.FromResult(new ApplicationRole());
		}

		public Task<ServiceResult> UpdateAsync(ApplicationRole applicationRole)
		{
			// Call store to update role

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}
	}
}
