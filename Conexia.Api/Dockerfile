FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["Conexia.Api/Conexia.Api.csproj", "Conexia.Api/"]
COPY ["Conexia.InfraStructure.Ioc/Conexia.InfraStructure.Ioc.csproj", "Conexia.InfraStructure.Ioc/"]
COPY ["Conexia.InfraStructure.AntiCorruption/Conexia.InfraStructure.AntiCorruption.csproj", "Conexia.InfraStructure.AntiCorruption/"]
COPY ["Conexia.Domain/Conexia.Domain.csproj", "Conexia.Domain/"]
COPY ["Conexia.InfraStructure.Data/Conexia.InfraStructure.Data.csproj", "Conexia.InfraStructure.Data/"]
COPY ["Conexia.InfraStructure.CrossCutting/Conexia.InfraStructure.CrossCutting.csproj", "Conexia.InfraStructure.CrossCutting/"]
RUN dotnet restore "Conexia.Api/Conexia.Api.csproj"
COPY . .
WORKDIR "/src/Conexia.Api"
RUN dotnet build "Conexia.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Conexia.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Conexia.Api.dll"]