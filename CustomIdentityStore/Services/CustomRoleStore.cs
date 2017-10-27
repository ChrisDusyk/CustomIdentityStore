using CustomIdentityStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace CustomIdentityStore.Services
{
	public class CustomRoleStore : IRoleStore<ApplicationRole>
	{
		private readonly ICustomRoleService _roleService;
		private readonly ILogger<CustomRoleStore> _logger;

		public CustomRoleStore(ICustomRoleService customRoleService, ILoggerFactory loggerFactory)
		{
			_roleService = customRoleService;
			_logger = loggerFactory.CreateLogger<CustomRoleStore>();
		}

		public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _roleService.CreateAsync(role);

				if (result == ServiceResult.Success)
				{
					return IdentityResult.Success;
				}
				else
				{
					return IdentityResult.Failed(new IdentityError()
					{
						Code = "1000",
						Description = "Something happened"
					});
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return IdentityResult.Failed(new IdentityError()
				{
					Code = "1000",
					Description = ex.Message
				});
			}
		}

		public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _roleService.DeleteAsync(role.Id);

				if (result == ServiceResult.Success)
				{
					return IdentityResult.Success;
				}
				else
				{
					return IdentityResult.Failed(new IdentityError()
					{
						Code = "1000",
						Description = "Something happened"
					});
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return IdentityResult.Failed(new IdentityError()
				{
					Code = "1000",
					Description = ex.Message
				});
			}
		}

		public void Dispose()
		{
			// Dispose anything instantiated for the store
			// Nothing to dispose in this case
		}

		public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
		{
			try
			{
				var role = await _roleService.FindAsync(roleId);

				return role;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				// returns a null from an async call
				return default(ApplicationRole);
			}
		}

		public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			try
			{
				var role = await _roleService.FindByNameAsync(normalizedRoleName);

				return role;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				// returns a null from an async call
				return default(ApplicationRole);
			}
		}

		public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _roleService.UpdateAsync(role);

				if (result == ServiceResult.Success)
				{
					return IdentityResult.Success;
				}
				else
				{
					return IdentityResult.Failed(new IdentityError()
					{
						Code = "1000",
						Description = "Something happened"
					});
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return IdentityResult.Failed(new IdentityError()
				{
					Code = "1000",
					Description = ex.Message
				});
			}
		}

		// In most cases you'll return what's in the ApplicationRole
		// These need to be implemented in case there's something extra being done to normalize or modify data

		public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				return role.NormalizedName;
			});
		}

		public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				return role.Id;
			});
		}

		public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				return role.Name;
			});
		}

		public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				role.NormalizedName = normalizedName;
			});
		}

		public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				role.Name = roleName;
			});
		}
	}
}
