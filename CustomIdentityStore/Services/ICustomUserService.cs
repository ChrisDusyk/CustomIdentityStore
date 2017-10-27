using CustomIdentityStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityStore.Services
{
	public interface ICustomUserService
	{
		Task<ServiceResult> CreateAsync(ApplicationUser applicationUser);

		Task<ApplicationUser> FindAsync(string userId);

		Task<ApplicationUser> FindByUsernameAsync(string username);

		Task<ApplicationUser> FindByEmailAsync(string email);

		Task<ServiceResult> UpdateAsync(ApplicationUser applicationUser);

		Task<ServiceResult> DeleteAsync(string userId);
	}
}
