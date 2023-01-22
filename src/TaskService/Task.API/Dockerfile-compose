#docker build -f EventService/Event.API/Dockerfile -t event_service:v01 .
#docker run --rm -it -p 7150:80 event_service:v01
#docker images
#docker container ls

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \
apt-get install -y build-essential nodejs
# End Install
WORKDIR /src

#kopiujemy csproj i pobierz brakujace paczki NUGET
COPY ["/EventService/Event.API/*.csproj", "EventService/Event.API/"]
COPY ["/EventService/Event.Application/*.csproj", "EventService/Event.Application/"]
COPY ["/EventService/Event.Persistance.EF/*.csproj", "EventService/Event.Persistance.EF/"]

COPY ["/Library/Candidate.API.Client/*.csproj", "Library/Candidate.API.Client/"]
COPY ["/Library/CommunicationClient/*.csproj", "Library/CommunicationClient/"]
COPY ["/Library/MQ/*.csproj", "Library/MQ/"]
COPY ["/Library/RabbitMQ/*.csproj", "Library/RabbitMQ/"]
COPY ["/Library/Recruitment.API.Client/*.csproj", "Library/Recruitment.API.Client/"]

RUN dotnet restore "EventService/Event.API/Event.API.csproj"

#kopiujemy pliki
COPY  EventService EventService
COPY  Library Library

WORKDIR "/src/EventService/Event.API"

RUN dotnet build "Event.API.csproj"  -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Event.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Event.API.dll"]