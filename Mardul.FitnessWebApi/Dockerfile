#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Mardul.FitnessWebApi/Mardul.FitnessWebApi.csproj", "Mardul.FitnessWebApi/"]
RUN dotnet restore "Mardul.FitnessWebApi/Mardul.FitnessWebApi.csproj"
COPY . .
WORKDIR "/src/Mardul.FitnessWebApi"
RUN dotnet build "Mardul.FitnessWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mardul.FitnessWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mardul.FitnessWebApi.dll"]