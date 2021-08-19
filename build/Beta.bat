@echo off

echo =======================================================================
echo Cosmos.Validation
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
dotnet pack src/Cosmos.Validation/Cosmos.Validation.csproj -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Validation.Annotations/Cosmos.Validation.Annotations.csproj -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Validation.Dependency/Cosmos.Validation.Dependency.csproj -c Release -o nuget_packages --no-restore

::ex-validator
dotnet pack src/Cosmos.Validation.Extensions.Email/Cosmos.Validation.Extensions.Email.csproj                 -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Validation.Extensions.ChinaIdNumber/Cosmos.Validation.Extensions.ChinaIdNumber.csproj -c Release -o nuget_packages --no-restore

::ex-dependency
dotnet pack src/Cosmos.Validation.Extensions.AspectCoreInjector/Cosmos.Validation.Extensions.AspectCoreInjector.csproj   -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Validation.Extensions.Autofac/Cosmos.Validation.Extensions.Autofac.csproj                         -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Validation.Extensions.DependencyInjection/Cosmos.Validation.Extensions.DependencyInjection.csproj -c Release -o nuget_packages --no-restore

::ex-sink
dotnet pack src/Cosmos.Validation.Sinks.DataAnnotations/Cosmos.Validation.Sinks.DataAnnotations.csproj -c Release -o nuget_packages --no-restore
dotnet pack src/Cosmos.Validation.Sinks.FluentValidation/Cosmos.Validation.Sinks.FluentValidation.csproj -c Release -o nuget_packages --no-restore

for /R "nuget_packages" %%s in (*symbols.nupkg) do (
    del "%%s"
)

echo.
echo.

::push nuget packages to server
for /R "nuget_packages" %%s in (*.nupkg) do ( 	
    dotnet nuget push "%%s" -s "Beta"  --skip-duplicate
	echo.
)

::get back to build folder
cd build