FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LabAssignment4/LabAssignment4.csproj", "LabAssignment4/"]
RUN dotnet restore "LabAssignment4/LabAssignment4.csproj"
COPY . .
WORKDIR "/src/LabAssignment4"
RUN dotnet build "LabAssignment4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LabAssignment4.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LabAssignment4.dll"]
