using CustomIdentityStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityStore.Services
{
	public interface IUserRoleService
	{
		Task<ServiceResult> CreateAsync(string userId, string roleName);

		Task<List<ApplicationRole>> GetRolesForUserAsync(string userId);

		Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName);

		Task<ServiceResult> DeleteRoleFromUserAsync(string userId, string roleName);
	}
}
