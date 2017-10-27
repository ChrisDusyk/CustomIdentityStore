using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomIdentityStore.Models;

namespace CustomIdentityStore.Services
{
	public class UserClaimService : IUserClaimService
	{
		public Task<ServiceResult> AddClaimsAsync(string userId, IEnumerable<Claim> claims)
		{
			// Call store to create user-claim records

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}

		public Task<ServiceResult> DeleteClaimAsync(string userId, Claim claim)
		{
			// Call store to remove a claim from a user

			// Return success
			return Task.FromResult(ServiceResult.Success);
		}

		public Task<List<Claim>> GetClaimsForUserAsync(string userId)
		{
			// Call store to get claims for user

			return Task.FromResult(new List<Claim>());
		}

		public Task<List<ApplicationUser>> GetUsersForClaimAsync(Claim claim)
		{
			// Call store to get users for a claim

			return Task.FromResult(new List<ApplicationUser>());
		}

		public Task<ServiceResult> ReplaceClaimAsync(string userId, Claim claim, Claim newClaim)
		{
			// Call store to replace claim with newClaim, however you've implemented the custom storage

			return Task.FromResult(ServiceResult.Success);
		}
	}
}
