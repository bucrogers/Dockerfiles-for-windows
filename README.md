# Dockerfiles-for-windows

* sqlexpress: SQL Server 2014 Express
* dotnet-aspnet46-webapp: ASP.NET 4.6 Web UI app, running under IIS 0- build using msbuild
* swarm-windows: Docker Swarm
* postgresql: PostgreSQL 9.5
* ruby-sinatra-helloworld: simple "hello world" ruby sinatra web app
* jdk8: Java JDK 8

Each of the above is entirely self-contained - all you need is what's in the dockerfile folder:
* docker build -t sqlexpress ./sqlexpress
  * docker run -d -p 1433:1433 sqlexpress
* docker build -t dotnet-aspnet46-webapp ./dotnet-aspnet46-webapp
  * docker run -d -p 80:80 dotnet-aspnet46-webapp
* docker build -t swarm ./swarm-windows
  * docker run --rm swarm create
* docker build -t postgresql ./postgresql
  * docker run -d -p 5432:5432 postgresql
  * test using a client such as pgAdmin III, psql or the "DB Navigator" plugin for Jetbrains IDEs IntelliJ IDEA and others - login as user 'postgres', empty password
* docker build -t ruby-sinatra-helloworld ./ruby-sinatra-helloworld
  * docker run -d -p 4567:4567
* docker build -t jdk8 ./jdk8

Please see my blog post [Dockerfile to create SQL Server Express windows container image](http://26thcentury.com/2016/01/03/dockerfile-to-create-sql-server-express-windows-container-image/) for a detailed description of the SQL Server dockerfile

# Credits
* [Stefan Scherer: Windows Dockerfiles](https://github.com/StefanScherer/dockerfiles-windows) - includes Compose, Consul, Golang, Jenkins-swarm-slave, node.js, Swarm <br />
* [Stefan Scherer: How to run a Windows Docker Engine in Azure]
(https://stefanscherer.github.io/how-to-run-windows-docker-engine-in-azure/) - defines an Azure template which improves on the 
[Msft Azure TP4 quickstart-templates](https://github.com/Azure/azure-quickstart-templates), including enabling public TCP listening<br />
* [Stefan Scherer: Build Docker Swarm binary for Windows the "Docker way"]
(https://stefanscherer.github.io/build-docker-swarm-for-windows-the-docker-way/)- dockerized Swarm image for Windows<br />
* [Stefan Sherer: Docker-themed blog](https://stefanscherer.github.io/) - includes using Chocolatey to get set up to run Docker Linux containers on Windows, and posts describing Docker on Raspberry Pi<br />
