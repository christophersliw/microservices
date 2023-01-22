#docker build -f CandidateService/Candidate.API/Dockerfile -t candidate_service:v01 .
#docker run --rm -it -p 7130:80 candidate_service:v01
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
COPY ["/CandidateService/Candidate.API/*.csproj", "CandidateService/Candidate.API/"]
COPY ["/CandidateService/Candidate.Application/*.csproj", "CandidateService/Candidate.Application/"]
COPY ["/CandidateService/Candidate.Domain/*.csproj", "CandidateService/Candidate.Domain/"]
COPY ["/CandidateService/Candidate.Persistence.EF/*.csproj", "CandidateService/Candidate.Persistence.EF/"]
COPY ["/CandidateService/Candidate.Services/*.csproj", "CandidateService/Candidate.Services/"]

COPY ["/Library/Candidate.API.Client/*.csproj", "Library/Candidate.API.Client/"]
COPY ["/Library/CommunicationClient/*.csproj", "Library/CommunicationClient/"]
COPY ["/Library/MQ/*.csproj", "Library/MQ/"]
COPY ["/Library/RabbitMQ/*.csproj", "Library/RabbitMQ/"]
COPY ["/Library/Recruitment.API.Client/*.csproj", "Library/Recruitment.API.Client/"]

RUN dotnet restore "CandidateService/Candidate.API/Candidate.API.csproj"

#kopiujemy pliki
COPY  CandidateService CandidateService
COPY  Library Library

WORKDIR "/src/CandidateService/Candidate.API"

RUN dotnet build "Candidate.API.csproj"  -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Candidate.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Candidate.API.dll"]