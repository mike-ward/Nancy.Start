nuget restore Nancy.Start.sln
if ERRORLEVEL 1 goto END
msbuild Nancy.Start.sln /t:Clean;Rebuild "/p:configuration=Release;platform=Any CPU" /verbosity:minimal
