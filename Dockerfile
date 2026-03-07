FROM ubuntu:22.04
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY publish/ .
RUN cat IOT_Shopping > /tmp/IOT_Shopping && mv /tmp/IOT_Shopping /app/IOT_Shopping && chmod 755 /app/IOT_Shopping

ENTRYPOINT ["./IOT_Shopping"]
