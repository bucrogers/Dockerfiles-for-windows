# Build ruby sinatra "hello world" web app port 4567 image
#
# IMAGE USAGE:
#   detached:
#     docker run -d -p <hostport>:4567 <imagename>
#   interactive:
#     docker run -it -p <hostport>:4567 <imagename> "powershell"
#
# Implementation notes:
#  This rev of the docker file has run reliably on various TP4 hosts.
#  There are two things commented-out (Plan A below) that work only on an Azure TP4 host,
#  but not a Windows 10 Hyper-V host - (Plan B described in parens)
#    x Installing ruby devkit2 using chocolatey (instead installing devkit manually)
#    x Installing ruby thin gem (instead relying on WEBRick web server)

FROM windowsservercore:10.0.14300.1000

MAINTAINER Buc Rogers

ENV DEVKITFILENAME DevKit-mingw64-64-4.7.2-20130224-1432-sfx.exe
ENV APPDIR c:\\app
ENV DEVKITDIR devkit

#install ruby
RUN powershell -Command \
	$ErrorActionPreference = 'Stop'; \
	Invoke-WebRequest -Method Get -Uri http://dl.bintray.com/oneclick/rubyinstaller/rubyinstaller-2.2.4-x64.exe -OutFile c:\rubyinstaller-2.2.4-x64.exe ; \
	Start-Process c:\rubyinstaller-2.2.4-x64.exe -ArgumentList '/verysilent' -Wait ; \
	Remove-Item c:\rubyinstaller-2.2.4-x64.exe -Force

RUN mkdir %APPDIR%
WORKDIR /app

# use chocolatey pkg mgr to facilitate command-line installations
RUN @powershell -NoProfile -ExecutionPolicy unrestricted -Command "(iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))) >$null 2>&1" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin

# Plan A: install ruby devkit using chocolatey, use thin web server
# WORKS ON SOME TP4 HOSTS (Azure) BUT NOT OTHERS (Windows 10 Hyper-V)
# RUN choco install ruby2.devkit -y

#Plan B: download devkit and manually install, use WEBRick

RUN powershell -command "wget 'http://dl.bintray.com/oneclick/rubyinstaller/%DEVKITFILENAME%' \
  -outfile %DEVKITFILENAME%"

RUN choco install 7zip -y

RUN powershell -command "& '%PROGRAMFILES%\7-zip\7z' e -r -y -o%DEVKITDIR% %DEVKITFILENAME%"

RUN powershell -command "cd %DEVKITDIR%; C:\Ruby22-x64\bin\ruby dk.rb init"

RUN powershell -command "cd %DEVKITDIR%; add-content config.yml '- C:\Ruby22-x64'"

RUN powershell -command "cd %DEVKITDIR%; C:\Ruby22-x64\bin\ruby dk.rb install"

# install sinatra gem (Plan A: thin fails to install on some TP4 hosts (Windows 10 Hyper-V),
#  falling back to Plan B: WEBRick as web server)
RUN C:\Ruby22-x64\bin\gem install sinatra
#    && C:\Ruby22-x64\bin\gem install thin

# copy app to image
COPY . /app

# run sinatra app, specifying remote network access
ENTRYPOINT [ "C:/Ruby22-x64/bin/ruby.exe app.rb -o 0.0.0.0" ]
