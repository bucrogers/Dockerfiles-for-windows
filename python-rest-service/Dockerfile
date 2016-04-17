FROM microsoft/python

RUN pip install requests \
  && pip install Flask \
  && pip install python-dateutil

ADD . /app

ENTRYPOINT ["python", "/app/public-service.py"]