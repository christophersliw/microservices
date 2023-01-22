#docker build -f RecruitmentService/Recruitment.API/Dockerfile -t recruitment_service:v01 .
#docker run --rm -it -p 7140:80 candidate_service:v01
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
COPY ["/RecruitmentService/Recruitment.API/*.csproj", "RecruitmentService/Recruitment.API/"]
COPY ["/RecruitmentService/Recruitment.Application/*.csproj", "RecruitmentService/Recruitment.Application/"]
COPY ["/RecruitmentService/Recruitment.Domain/*.csproj", "RecruitmentService/Recruitment.Domain/"]
COPY ["/RecruitmentService/Recruitment.Persistence.EF/*.csproj", "RecruitmentService/Recruitment.Persistence.EF/"]
COPY ["/RecruitmentService/Recruitment.API.Contract/*.csproj", "RecruitmentService/Recruitment.API.Contract/"]

COPY ["/Library/Candidate.API.Client/*.csproj", "Library/Candidate.API.Client/"]
COPY ["/Library/CommunicationClient/*.csproj", "Library/CommunicationClient/"]
COPY ["/Library/MQ/*.csproj", "Library/MQ/"]
COPY ["/Library/RabbitMQ/*.csproj", "Library/RabbitMQ/"]
COPY ["/Library/Recruitment.API.Client/*.csproj", "Library/Recruitment.API.Client/"]

RUN dotnet restore "RecruitmentService/Recruitment.API/Recruitment.API.csproj"

#kopiujemy pliki
COPY  RecruitmentService RecruitmentService
COPY  Library Library

WORKDIR "/src/RecruitmentService/Recruitment.API"

RUN dotnet build "Recruitment.API.csproj"  -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recruitment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recruitment.API.dll"]