using CustomIdentityStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomIdentityStore.Services
{
	public interface IUserClaimService
	{
		Task<ServiceResult> AddClaimsAsync(string userId, IEnumerable<Claim> claims);

		Task<List<Claim>> GetClaimsForUserAsync(string userId);

		Task<List<ApplicationUser>> GetUsersForClaimAsync(Claim claim);

		Task<ServiceResult> ReplaceClaimAsync(string userId, Claim claim, Claim newClaim);

		Task<ServiceResult> DeleteClaimAsync(string userId, Claim claim);
	}
}
