using CustomIdentityStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityStore.Services
{
	public interface ICustomRoleService
	{
		Task<ServiceResult> CreateAsync(ApplicationRole applicationRole);

		Task<ApplicationRole> FindAsync(string roleId);

		Task<ApplicationRole> FindByNameAsync(string roleName);

		Task<ServiceResult> UpdateAsync(ApplicationRole applicationRole);

		Task<ServiceResult> DeleteAsync(string roleId);
	}
}
