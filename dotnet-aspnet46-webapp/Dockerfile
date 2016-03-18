#Build ASP.NET 4.6 application from source and deploy to IIS
#
#  Builds UI tier (windows container portion) of RushHourWeatherApp
#
#  This approach should work for any ASP.NET 4.6 application
  
FROM microsoft/iis

ENV RushHourWeatherAppServiceUrl http://localhost:5000

# use chocolatey pkg mgr to facilitate downloading jdk without prompting
#install ASP.NET 4.6 (though it says "net45" it is ASP.NET 4.6)
#install tools and targets to allow build (via msbuild) without VS installed
RUN @powershell -NoProfile -ExecutionPolicy unrestricted -Command "(iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))) >$null 2>&1" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin
RUN powershell add-windowsfeature web-asp-net45 \
  && choco install microsoft-build-tools -y \
  && choco install dotnet4.6-targetpack -y \
  && choco install nuget.commandline -y \
  && nuget install MSBuild.Microsoft.VisualStudio.Web.targets

#copy web app source and utility script(s) to image
COPY . /inetpub/wwwroot/RushHourWeatherApp

#set to base web app dir
WORKDIR /inetpub/wwwroot/RushHourWeatherApp/RushHourWeatherApp

#restore packages (which are intentionally not in source control)
#Build (via msbuild)
#Create IIS ASP.NET web application
RUN nuget restore -PackagesDirectory ..\packages \
  && powershell /inetpub/wwwroot/RushHourWeatherApp/Run-MsBuild \
  && powershell /inetpub/wwwroot/RushHourWeatherApp/Create-WebApplication -name "RushHourWeatherApp" -physicalPath "c:\inetpub\wwwroot\RushHourWeatherApp\RushHourWeatherApp"

#Default entrypoint: idle powershell prompt (use CMD instead of ENTRYPOINT for increased flexibility)
CMD powershell
