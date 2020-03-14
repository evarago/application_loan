FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 52582

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ApplicationLoan.Api/ApplicationLoan.Api.csproj", "ApplicationLoan.Api/"]
COPY ["ApplicationLoan.Infra.CrossCutting/ApplicationLoan.Infra.CrossCutting.csproj", "ApplicationLoan.Infra.CrossCutting/"]
COPY ["ApplicationLoan.Service/ApplicationLoan.Service.csproj", "ApplicationLoan.Service/"]
COPY ["ApplicationLoan.Domain/ApplicationLoan.Domain.csproj", "ApplicationLoan.Domain/"]
COPY ["ApplicationLoan.Infra.Data/ApplicationLoan.Infra.Data.csproj", "ApplicationLoan.Infra.Data/"]
RUN dotnet restore "ApplicationLoan.Api/ApplicationLoan.Api.csproj"
COPY . .
WORKDIR "/src/ApplicationLoan.Api"
RUN dotnet build "ApplicationLoan.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ApplicationLoan.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApplicationLoan.Api.dll"]