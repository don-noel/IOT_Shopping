# Vulnérabilité 5 – Exécution du conteneur en root

FROM ubuntu:22.04

WORKDIR /app

# Vulnérabilité 6 – Packages système supplémentaires augmentant la surface d’attaque
RUN apt-get update && apt-get install -y curl wget vim

RUN apt-get update && \
    apt-get install -y libicu70 && \
    rm -rf /var/lib/apt/lists/*

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

COPY publish/ .

RUN chmod +x /app/IOT_Shopping

ENTRYPOINT ["./IOT_Shopping"]
