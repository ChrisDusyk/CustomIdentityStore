using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentityStore.Models;

namespace CustomIdentityStore.Services
{
	public class CustomUserService : ICustomUserService
	{
		public Task<ServiceResult> CreateAsync(ApplicationUser applicationUser)
		{
			// Call store to create user

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}

		public Task<ServiceResult> DeleteAsync(string userId)
		{
			// Call store to delete user

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}

		public Task<ApplicationUser> FindAsync(string userId)
		{
			// Get user from store by ID

			return Task.FromResult(new ApplicationUser());
		}

		public Task<ApplicationUser> FindByEmailAsync(string email)
		{
			// Get user from store by email

			return Task.FromResult(new ApplicationUser());
		}

		public Task<ApplicationUser> FindByUsernameAsync(string username)
		{
			// Get user from store by username

			return Task.FromResult(new ApplicationUser());
		}

		public Task<ServiceResult> UpdateAsync(ApplicationUser applicationUser)
		{
			// Call store to update user

			return Task.FromResult(ServiceResult.Success);
		}
	}
}
