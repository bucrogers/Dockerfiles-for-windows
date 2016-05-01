FROM windowsservercore:10.0.14300.1000

#install py
RUN powershell.exe -Command \
    $ErrorActionPreference = 'Stop'; \
    wget https://www.python.org/ftp/python/3.5.1/python-3.5.1.exe -OutFile c:\python-3.5.1.exe ; \
    Start-Process c:\python-3.5.1.exe -ArgumentList '/quiet InstallAllUsers=1 PrependPath=1' -Wait ; \
    Remove-Item c:\python-3.5.1.exe -Force

#install py packages
RUN pip install requests \
  && pip install Flask \
  && pip install python-dateutil \
  && pip install Flask-Cache

ADD . /app

ENTRYPOINT ["python", "/app/public-service.py"]