using CustomIdentityStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CustomIdentityStore.Services
{
	public class CustomUserStore :	IUserStore<ApplicationUser>,				// Core user information store
									IUserLoginStore<ApplicationUser>,			// Stores logins for external login providers (i.e. Microsoft, Facebook)
									IUserPasswordStore<ApplicationUser>,		// Contains methods for interacting with the hashed password
									IUserSecurityStampStore<ApplicationUser>,	// Contains methods for interacting with the Security Stamp (used to force a logout if account info changes)
									IUserRoleStore<ApplicationUser>,			// Contains CRUD methods for dealing with user roles, for role-based security
									IUserClaimStore<ApplicationUser>,			// Contains CRUD methods for dealing with user claims, for claims-based security
									IUserEmailStore<ApplicationUser>,			// Contains methods for interacting with the user's email
									IUserLockoutStore<ApplicationUser>			// Contains methods for handling user lockout
	{
		private readonly ICustomUserService _userService;
		private readonly IUserRoleService _userRoleService;
		private readonly IUserClaimService _userClaimService;
		private readonly ILogger<CustomUserStore> _logger;

		public CustomUserStore(ICustomUserService customUserService, IUserRoleService userRoleService, IUserClaimService userClaimService, ILoggerFactory loggerFactory)
		{
			_userService = customUserService;
			_userRoleService = userRoleService;
			_userClaimService = userClaimService;
			_logger = loggerFactory.CreateLogger<CustomUserStore>();
		}

		#region IUserStore Methods

		public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _userService.CreateAsync(user);

				if (result == ServiceResult.Success)
				{
					return IdentityResult.Success;
				}
				else
				{
					return IdentityResult.Failed(new IdentityError()
					{
						Code = "2000",
						Description = "Something happened"
					});
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return IdentityResult.Failed(new IdentityError()
				{
					Code = "2000",
					Description = ex.Message
				});
			}
		}

		public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _userService.DeleteAsync(user.Id);

				if (result == ServiceResult.Success)
				{
					return IdentityResult.Success;
				}
				else
				{
					return IdentityResult.Failed(new IdentityError()
					{
						Code = "2000",
						Description = "Something happened"
					});
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return IdentityResult.Failed(new IdentityError()
				{
					Code = "2000",
					Description = ex.Message
				});
			}
		}

		public void Dispose()
		{
			// Dispose anything instantiated for the store
			// Nothing to dispose in this case
		}

		public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _userService.FindAsync(userId);

				return user;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				// returns a null from an async call
				return default(ApplicationUser);
			}
		}

		public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _userService.FindByUsernameAsync(normalizedUserName);

				return user;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				// returns a null from an async call
				return default(ApplicationUser);
			}
		}

		public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.NormalizedUserName);
		}

		public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Id);
		}

		public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.UserName);
		}

		public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.NormalizedUserName = normalizedName;
			});
		}

		public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.UserName = userName;
			});
		}

		public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _userService.UpdateAsync(user);

				if (result == ServiceResult.Success)
				{
					return IdentityResult.Success;
				}
				else
				{
					return IdentityResult.Failed(new IdentityError()
					{
						Code = "2000",
						Description = "Something happened"
					});
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return IdentityResult.Failed(new IdentityError()
				{
					Code = "2000",
					Description = ex.Message
				});
			}
		}

		#endregion IUserStore Methods

		#region IUserLoginStore Methods

		public Task RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		#endregion IUserLoginStore Methods

		#region IUserPasswordStore Methods

		public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.PasswordHash = passwordHash;
			});
		}

		public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				if (user.PasswordHash == null)
				{
					return false;
				}

				if (user.PasswordHash.Length > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			});
		}

		#endregion IUserPasswordStore Methods

		#region IUserSecurityStampStore Methods

		public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.SecurityStamp = stamp;
			});
		}

		public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.SecurityStamp);
		}

		#endregion IUserSecurityStampStore Methods

		#region IUserRoleStore Methods

		public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _userRoleService.CreateAsync(user.Id, roleName);

				if (result != ServiceResult.Success)
				{
					_logger.LogWarning("Unsuccessful adding role {roleName} to user {userId}", roleName, user.Id);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);
			}
		}

		public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _userRoleService.DeleteRoleFromUserAsync(user.Id, roleName);

				if (result != ServiceResult.Success)
				{
					_logger.LogWarning("Unsuccessful removing role {roleName} from user {userId}", roleName, user.Id);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);
			}
		}

		public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			try
			{
				var roles = new List<string>();

				var results = await _userRoleService.GetRolesForUserAsync(user.Id);

				if (results.Count == 0)
				{
					_logger.LogWarning("No roles found for user {userId}", user.Id);
				}

				foreach (var result in results)
				{
					roles.Add(result.Name);
				}

				return roles;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return default(List<string>);
			}
		}

		public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
		{
			try
			{
				var results = await _userRoleService.GetRolesForUserAsync(user.Id);

				if (results.Count == 0)
				{
					_logger.LogWarning("No roles found for user {userId}", user.Id);
				}

				if (results.FirstOrDefault(role => role.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase)) == null)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return false;
			}
		}

		public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
		{
			try
			{
				var users = new List<ApplicationUser>();

				var results = await _userRoleService.GetUsersInRoleAsync(roleName);

				if (results.Count == 0)
				{
					_logger.LogWarning("No users found in role {roleName}", roleName);
				}

				foreach (var result in results)
				{
					users.Add(result);
				}

				return users;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return default(List<ApplicationUser>);
			}
		}

		#endregion IUserRoleStore Methods

		#region IUserClaimStore Methods

		public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			try
			{
				var claims = new List<Claim>();

				var results = await _userClaimService.GetClaimsForUserAsync(user.Id);

				if (results.Count == 0)
				{
					_logger.LogWarning("No claims found for user {userId}", user.Id);
				}

				foreach (var result in results)
				{
					claims.Add(result);
				}

				return claims;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return default(List<Claim>);
			}
		}

		public async Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _userClaimService.AddClaimsAsync(user.Id, claims);

				if (result != ServiceResult.Success)
				{
					_logger.LogWarning("Unsuccessful adding claims {claims} to user {userId}", string.Join(",", claims.Select(c => c.Value).ToList()), user.Id);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);
			}
		}

		public async Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _userClaimService.ReplaceClaimAsync(user.Id, claim, newClaim);

				if (result != ServiceResult.Success)
				{
					_logger.LogWarning("Unsuccessful replacing {claim} with {newClaim}, user {userId}", claim.Value, newClaim.Value, user.Id);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);
			}
		}

		public async Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
		{
			try
			{
				foreach (var claim in claims)
				{
					var result = await _userClaimService.DeleteClaimAsync(user.Id, claim);

					if (result != ServiceResult.Success)
					{
						_logger.LogWarning("Unsuccessful removing {claim} from user {userId}", claim.Value, user.Id);
					} 
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);
			}
		}

		public async Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
		{
			try
			{
				var users = await _userClaimService.GetUsersForClaimAsync(claim);

				if (users.Count == 0)
				{
					_logger.LogWarning("No users found for claim {claim}", claim.Value);
				}

				return users;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				return default(List<ApplicationUser>);
			}
		}

		#endregion IUserClaimStore Methods

		#region IUserEmailStore Methods

		public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.Email = email;
			});
		}

		public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.EmailConfirmed);
		}

		public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.EmailConfirmed = confirmed;
			});
		}

		public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _userService.FindByEmailAsync(normalizedEmail);

				return user;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something happened: {message}", ex.Message);

				// returns a null from an async call
				return default(ApplicationUser);
			}
		}

		public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.NormalizedEmail);
		}

		public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.NormalizedEmail = normalizedEmail;
			});
		}

		#endregion IUserEmailStore Methods

		#region IUserLockoutStore Methods

		public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.LockoutEnd);
		}

		public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.LockoutEnd = lockoutEnd;
			});
		}

		public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				return user.AccessFailedCount += 1;
			});
		}

		public Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.AccessFailedCount = 0;
			});
		}

		public Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.AccessFailedCount);
		}

		public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.LockoutEnabled);
		}

		public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				user.LockoutEnabled = enabled;
			});
		}

		#endregion IUserLockoutStore Methods
	}
}
