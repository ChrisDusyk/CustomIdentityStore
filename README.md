# ASP.NET Core Custom Identity Store

## Intro

I recently had to override the default Identity user store in ASP.NET Core 2.0, to use a remote Web API instead of a provisioned database. Initially Microsoft seems to have good documentation for this, however it's very basic and doesn't explain exactly what you need to do for basic functionality. This is a quick overview of my trial-and-error learning.

## Requirements

* [Visual Studio 2017](https://www.visualstudio.com/downloads/)
* [.NET Core 2.0](https://www.microsoft.com/net/download/core)
* [Microsoft Documentation](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-custom-storage-providers)

## Required Data

These are the fields you need to store somewhere in your data store to have all functionality working.

### Users

Column | Datatype | Note
--- | --- | ---
Id | string, 40 character | This is a GUID in the default implementation, however you can override it to be any datatype you want
AccessFailedCount | int | Used to count failed login attempts, required for account lockout to work
ConcurrencyStamp | string | A general stamp use to determine if the current version of the record in memory is out-of-sync with the data store
Email | string, 256 character | Email Address
EmailConfirmed | bool | Whether the email address has been confirmed by the user, if this is a security requirement for your application
LockoutEnabled | bool | Whether the account can be locked out. This doesn't mean that the account is locked out, but whether it can be
LockoutEnd | DateTimeOffset | The time until the account unlocks
NormalizedEmail | string, 256 character | This is the normalized email address. By default, Identity normalizes by making it all caps (ex. Email = john.smith@gmail.com, NormalizedEmail = JOHN.SMITH@GMAIL.COM)
NormalizedUserName | string, 256 character | This is the normalized username. By default, Identity normalizes by making it all caps (ex. UserName = john.smith@gmail.com, NormalizedUserName = JOHN.SMITH@GMAIL.COM)
PasswordHash | string | This is the hashed user password, more info [here](https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing)
PhoneNumber | string | Holds the user phone number, used for Two Factor setup
PhoneNumberConfirmed | bool | Whether the phone number has been confirmed by the user
SecurityStamp | string | A general stamp used to determine if the User session is valid. SignInManager compares the one in memory to the one in the session variable to determine if it's a valid session for the user. This can be anything, but is often a GUID
TwoFactorEnabled | bool | Whether Two Factor Authentication is enabled for the account
UserName | string, 256 character | The username for the record. This is often implemented using the Email as the UserName as well

#### User Lockout

The user account can be locked out by exceeding the max number of login attempts, configured in the Startup. The default for Identity is 5 failed attempts.

The account is locked by adding the lockout timespan to the LockoutEnd field. The SignInManager checks for a lockout date and then compares it to the current DateTime to determine if the account is currently locked out. This also causes the AccessFailedCount to reset to 0.

### UserLogins

This is only required if you plan on supporting third party login providers, such as Facebook, Google, etc.

Column | Datatype | Note
--- | --- | ---
LoginProvider | string | Name of the external login provider (ex. Facebook, Google, etc)
ProviderKey | string | Key supplied by the login provider for security
ProviderDisplayName | string | Display name for the external login provider, to be displayed in the UI
UserId | string, 40 character | FK Id to the Users data store

### UserTokens

This stores the security tokens for each external login provider. This is only required if you plan on supporting third party login providers, such as Facebook, Google, etc.

Column | Datatype | Note
--- | --- | ---
UserId | string, 40 character | FK Id to the Users data store
LoginProvider | string | Name of the external login provider (ex. Facebook, Google, etc)
Name | string | Name of the external login provider (ex. Facebook, Google, etc)
Value | string | Token for the external login provider

### Roles

This is only required if you plan on using Role-based security.

Column | Datatype | Note
--- | --- | ---
Id | string, 40 character | This is a GUID in the default implementation
ConcurrencyStamp | string | A general stamp use to determine if the current version of the record in memory is out-of-sync with the data store
Name | string, 256 character | The name of the Role
NormalizedName | string, 256 character | This is the normalized name. By default, Identity normalizes by making it all caps (ex. Name = Admin, NormalizedName = ADMIN)

### UserRoles

This is only required if you plan on using Role-based security.

Column | Datatype | Note
--- | --- | ---
UserId | string, 40 character | FK Id to the Users data store
RoleId | string, 40 character | DK Id to the Roles data store

### UserClaims

This is only required if you plan on using Claims-based security.

Column | Datatype | Note
--- | --- | ---
Id | int | Incrementing integer ID
ClaimType | string | Type of claim
ClaimValue | string | Actual claim value
UserId | string, 40 character | FK Id to the Users data store

### RoleClaims

This is only required if you plan on using Role-based security with Claims implemented per Role.

Column | Datatype | Note
--- | --- | ---
Id | int | Incrementing integer ID
ClaimType | string | Type of claim
ClaimValue | string | Actual claim value
RoleId | string, 40 character | DK Id to the Roles data store