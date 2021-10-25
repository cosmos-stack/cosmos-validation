@echo off

echo =======================================================================
echo CosmosStack.Validation
echo =======================================================================

::go to parent folder
cd ..

::create nuget_packages
if not exist nuget_packages (
    md nuget_packages
    echo Created nuget_packages folder.
)

::clear nuget_packages
for /R "nuget_packages" %%s in (*) do (
    del "%%s"
)
echo Cleaned up all nuget packages.
echo.

::start to package all projects
dotnet pack src/CosmosStack.Validation -c Release  -o nuget_packages --no-restore
dotnet pack src/CosmosStack.Validation.Annotations -c Release -o nuget_packages --no-restore
dotnet pack src/CosmosStack.Validation.Dependency  -c Release -o nuget_packages --no-restore

::ex-validator
dotnet pack src/CosmosStack.Validation.Extensions.Email         -c Release -o nuget_packages --no-restore
dotnet pack src/CosmosStack.Validation.Extensions.ChinaIdNumber -c Release -o nuget_packages --no-restore

::ex-dependency
dotnet pack src/CosmosStack.Validation.Extensions.AspectCoreInjector  -c Release -o nuget_packages --no-restore
dotnet pack src/CosmosStack.Validation.Extensions.Autofac             -c Release -o nuget_packages --no-restore
dotnet pack src/CosmosStack.Validation.Extensions.DependencyInjection -c Release -o nuget_packages --no-restore

::ex-sink
dotnet pack src/CosmosStack.Validation.Sinks.DataAnnotations  -c Release -o nuget_packages --no-restore
dotnet pack src/CosmosStack.Validation.Sinks.FluentValidation -c Release -o nuget_packages --no-restore

for /R "nuget_packages" %%s in (*symbols.nupkg) do (
    del "%%s"
)

::get back to build folder
cd scripts