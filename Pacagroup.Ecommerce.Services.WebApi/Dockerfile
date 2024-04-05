#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj", "Pacagroup.Ecommerce.Services.WebApi/"]
#COPY ["Pacagroup.Ecommerce.Application.DTO/Pacagroup.Ecommerce.Application.DTO.csproj", "Pacagroup.Ecommerce.Application.DTO/"]
#COPY ["Pacagroup.Ecommerce.Application.Interface/Pacagroup.Ecommerce.Application.Interface.csproj", "Pacagroup.Ecommerce.Application.Interface/"]
#COPY ["Pacagroup.Ecommerce.Common/Pacagroup.Ecommerce.Transversal.Common.csproj", "Pacagroup.Ecommerce.Common/"]
#COPY ["Pacagroup.Ecommerce.Application.Main/Pacagroup.Ecommerce.Application.Main.csproj", "Pacagroup.Ecommerce.Application.Main/"]
#COPY ["Pacagroup.Ecommerce.Domain.Interface/Pacagroup.Ecommerce.Domain.Interface.csproj", "Pacagroup.Ecommerce.Domain.Interface/"]
#COPY ["Pacagroup.Ecommerce.Domain.Entity/Pacagroup.Ecommerce.Domain.Entity.csproj", "Pacagroup.Ecommerce.Domain.Entity/"]
#COPY ["Pacagroup.Ecommerce.Transversal.Mapper/Pacagroup.Ecommerce.Transversal.Mapper.csproj", "Pacagroup.Ecommerce.Transversal.Mapper/"]
#COPY ["Pacagroup.Ecommerce.Domain.Core/Pacagroup.Ecommerce.Domain.Core.csproj", "Pacagroup.Ecommerce.Domain.Core/"]
#COPY ["Pacagroup.Infrastructure.Interface/Pacagroup.Ecommerce.Infrastructure.Interface.csproj", "Pacagroup.Infrastructure.Interface/"]
#COPY ["Pacagroup.Ecommerce.Infrastructure.Data/Pacagroup.Ecommerce.Infrastructure.Data.csproj", "Pacagroup.Ecommerce.Infrastructure.Data/"]
#COPY ["Pacagroup.Ecommerce.Infrastructure.Repository/Pacagroup.Ecommerce.Infrastructure.Repository.csproj", "Pacagroup.Ecommerce.Infrastructure.Repository/"]
#COPY ["Pacagroup.Ecommerce.Transversal.Logging/Pacagroup.Ecommerce.Transversal.Logging.csproj", "Pacagroup.Ecommerce.Transversal.Logging/"]
RUN dotnet restore "Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj"
COPY . .
WORKDIR "/src/Pacagroup.Ecommerce.Services.WebApi"
RUN dotnet build "Pacagroup.Ecommerce.Services.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pacagroup.Ecommerce.Services.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN ln -fs /usr/share/zoneinfo/America/Lima /etc/localtime
RUN dpkg-reconfigure --frontend noninteractive tzdata

ENTRYPOINT ["dotnet", "Pacagroup.Ecommerce.Services.WebApi.dll"]