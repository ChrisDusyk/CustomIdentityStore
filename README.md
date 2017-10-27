# ASP.NET Core Custom Identity Store

## Intro

I recently had to override the default Identity user store in ASP.NET Core 2.0, to use a remote Web API instead of a provisioned database. Initially Microsoft seems to have good documentation for this, however it's very basic and doesn't explain exactly what you need to do for basic functionality. This is a quick overview of my trial-and-error learning.

## Requirements

* [Visual Studio 2017](https://www.visualstudio.com/downloads/)
* [.NET Core 2.0](https://www.microsoft.com/net/download/core)

