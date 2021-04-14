# foundation-net-core

## The Solution

Default username and password of admin@example.com / Episerver123!

---

## Dev Nuget Package Location

http://nuget-dev.ep.se/nuget

---

## Template Installation

```
dotnet new -i EPiServer.Net.Templates::1.0.0-inte-017362 --nuget-source https://pkgs.dev.azure.com/EpiserverEngineering/netCore/_packaging/beta-program/nuget/v3/index.json --force
```
---

## CLI Installation

```
dotnet tool install EPiServer.Net.Cli --global --add-source https://pkgs.dev.azure.com/EpiserverEngineering/netCore/_packaging/beta-program/nuget/v3/index.json --version 1.0.0-inte-017362
```
---

## Create empty DB

```
cd projectname
dotnet-episerver create-cms-database ProjectName.csproj -S . -E
dotnet-episerver create-commerce-database ProjectName.csproj -S . -E --reuse-cms-user 
