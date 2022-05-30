# Dockerfiles for Windows

All have been built and tested under Windows Server 2016 TP5. 

* sqlexpress: SQL Server 2014 Express
  * See sqlexpress\dockerfile for usage details
  * See also my blog post [Dockerfile to create SQL Server Express windows container image](http://26thcentury.com/2016/01/03/dockerfile-to-create-sql-server-express-windows-container-image/) for a detailed description
* dotnet-aspnet46-webapp: ASP.NET 4.6 Web UI app ("bcw" Bike Commuter Weather app), running under IIS - built in the Dockerfile using msbuild
  * There is a docker-compose.yml file in dotnet-aspnet46-webapp, to run both ui and service locally - however, docker-compose network mode functionality is limited as of TP5: The app will be accessible from outside only if run via docker run, not docker-compose
* python-rest-service: Python REST web service consumed by the "bcw" UI above
* swarm-windows: Docker Swarm executable for Windows
* postgresql: PostgreSQL 9.5
* ruby-sinatra-helloworld: simple "hello world" ruby sinatra web app
* jdk8: Java JDK 8

Each of the above is entirely self-contained - all you need is what's in the dockerfile folder:
* docker build -t sqlexpress ./sqlexpress
  * docker run -d -p 1433:1433 sqlexpress
  * The sa password (specified in the Dockerfile) is: thepassword2#
* docker build -t bcwui ./dotnet-aspnet46-webapp
  * docker run -d -e WeatherServiceUrl=http://**pythonRestServiceHost**.eastus.cloudapp.azure.com:5000 -p 80:80 bcwui
  * navigate to http://**publichostname**/rushhourweatherapp
* docker build -t bcwservice ./python-rest-service
  * docker run -d -e WUNDERGROUND_API_KEY=YourWundergroundApiKey -p 5000:5000 bcwservice
* docker build -t swarm ./swarm-windows
  * docker run --rm swarm create
* docker build -t postgresql ./postgresql
  * docker run -d -p 5432:5432 postgresql
  * test using a client such as pgAdmin III, psql or the "DB Navigator" plugin for Jetbrains IDEs IntelliJ IDEA and others - login as user 'postgres', empty password
* docker build -t ruby-sinatra-helloworld ./ruby-sinatra-helloworld
  * docker run -d -p 4567:4567 ruby-sinatra-helloworld
* docker build -t jdk8 ./jdk8
  * (intended as a base image for app requiring jdk to build)

# Azure Resource Manager Template to create Docker Windows Container Host

Below is an example of using an Azure Resource Template to install a Windows 
Server 2016 TP5 Docker Windows Containers host on Azure, using Azure CLI
(which may be configured to run on Windows using a Cygwin terminal window 
as described [here](https://github.com/docker/labs/blob/master/windows/dotnet-linux-het/readme.md)):

<pre>
$ azure group create -n "cliEastUsRG" -l "East US"
</pre>

_Substitute a unique node name for the "dnsNameForPublicIP" 
parameter before running the following command in place of
**uniqueWindowsNodeName**, such as "myname-win-node" (you can also substitute
for the Azure123 and Azure!23 default admin username and password, respectively) - 
this operation can take up to 20 minutes to complete:_

<pre>
$ azure group deployment create cliEastUsRG win-node --template-uri https://raw.githubusercontent.com/bucrogers/Dockerfiles-for-windows/master/dotnet-aspnet46-webapp/azuredeploy.json -p '{"adminUsername": {"value": "Azure123"}, "adminPassword": {"value": "Azure!23"}, "dnsNameForPublicIP": {"value": "<b>uniqueWindowsNodeName</b>"}, "VMName": {"value": "win-node"},"location": {"value": "East US"}}'
</pre>

Because Azure Marketplace does not currently offer a Container-ready TP5 image,
you'll need to take the following additional step, which takes about 45min:

* Remote Desktop: **uniqueWindowsNodeName**.eastus.cloudapp.azure.com
* Command Prompt (Admin)

<pre>
> powershell.exe -NoProfile -ExecutionPolicy Bypass \install-containerhost
> docker images
REPOSITORY                TAG                 IMAGE ID            CREATED             SIZE
windowsservercore         10.0.14300.1000     dbfee88ee9fd        5 weeks ago         9.344 GB
</pre>

# Credits
* [Stefan Scherer: Windows Dockerfiles](https://github.com/StefanScherer/dockerfiles-windows) - includes Compose, Consul, Golang, Jenkins-swarm-slave, node.js, Swarm <br />
* [Msft: Windows Containers Sample Dockerfiles](https://github.com/Microsoft/Virtualization-Documentation/tree/master/windows-container-samples) - includes dockerfiles for base images including iis, dotnet35; also samples
* [Stefan Scherer: How to run a Windows Docker Engine in Azure]
(https://stefanscherer.github.io/how-to-run-windows-docker-engine-in-azure/) - defines an Azure template which improves on the 
[Msft Azure TP4 quickstart-templates](https://github.com/Azure/azure-quickstart-templates), including enabling public TCP listening<br />
* [Stefan Scherer: Build Docker Swarm binary for Windows the "Docker way"]
(https://stefanscherer.github.io/build-docker-swarm-for-windows-the-docker-way/)- dockerized Swarm image for Windows<br />
* [Stefan Sherer: Docker-themed blog](https://stefanscherer.github.io/) - includes using Chocolatey to get set up to run Docker Linux containers on Windows, and posts describing Docker on Raspberry Pi<br />

# Other Docker Windows Containers references
* [Docker Labs: Tutorial: Run Swarm on a mix of Linux and Windows Nodes](https://github.com/docker/labs/blob/master/windows/dotnet-linux-het/readme.md) - use swarm label to specify target Linux or Windows host
* [26thCentury: Docker Windows-Containers-specific links](https://26thcentury.com/reading-list-devops-build-test-deploy-automation-monitoring/#dockerWindowsContainersSpecific)

