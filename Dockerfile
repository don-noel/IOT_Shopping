FROM ubuntu:22.04
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY publish/ .
RUN chmod +x IOT_Shopping

ENTRYPOINT ["./IOT_Shopping"]
