FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Chibbis.TestProject.CartService.Web/Chibbis.TestProject.CartService.Web.csproj", "Chibbis.TestProject.CartService.Web/"]
RUN dotnet restore "Chibbis.TestProject.CartService.Web/Chibbis.TestProject.CartService.Web.csproj"
COPY . .
WORKDIR "/src/Chibbis.TestProject.CartService.Web"
RUN dotnet build "Chibbis.TestProject.CartService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chibbis.TestProject.CartService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chibbis.TestProject.CartService.Web.dll"]
