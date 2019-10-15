FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8983

# Setup NodeJs -- walkaround :|
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["homework.webspa/homework.webspa.csproj", "homework.webspa/"]
RUN dotnet restore "homework.webspa/homework.webspa.csproj"
COPY . .
WORKDIR "/src/homework.webspa"
RUN dotnet build "homework.webspa.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "homework.webspa.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "homework.webspa.dll"]