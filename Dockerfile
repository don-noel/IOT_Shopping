FROM ubuntu:22.04

WORKDIR /app

RUN apt-get update && \
    apt-get install -y libicu70 && \
    rm -rf /var/lib/apt/lists/*

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

COPY publish/ .

RUN chmod +x /app/IOT_Shopping

ENTRYPOINT ["./IOT_Shopping"]
