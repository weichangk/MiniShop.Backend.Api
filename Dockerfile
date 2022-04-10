#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.cn-hangzhou.aliyuncs.com/newbe36524/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM registry.cn-hangzhou.aliyuncs.com/newbe36524/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MiniShop.Backend.Api/MiniShop.Backend.Api.csproj", "MiniShop.Backend.Api/"]
RUN dotnet restore "MiniShop.Backend.Api/MiniShop.Backend.Api.csproj"
COPY . .
WORKDIR "/src/MiniShop.Backend.Api"
RUN dotnet build "MiniShop.Backend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiniShop.Backend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV INITDB=initdb
ENTRYPOINT ["dotnet", "MiniShop.Backend.Api.dll"]