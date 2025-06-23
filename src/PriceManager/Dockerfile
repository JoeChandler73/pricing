FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY PriceManager/*.csproj ./PriceManager/
COPY Pricing.ServiceDefaults/*.csproj ./Pricing.ServiceDefaults/
COPY Messaging.Application/*.csproj ./Messaging.Application/
COPY Messaging.Infrastructure/*.csproj ./Messaging.Infrastructure/

RUN dotnet restore PriceManager/

COPY PriceManager/ ./PriceManager/
COPY Pricing.ServiceDefaults ./Pricing.ServiceDefaults/
COPY Messaging.Application ./Messaging.Application/
COPY Messaging.Infrastructure ./Messaging.Infrastructure/

WORKDIR /app/PriceManager
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build-env /app/PriceManager/out .

EXPOSE 80
ENV DOTNET_URLS=http://+:80

ENTRYPOINT ["dotnet", "PriceManager.dll"]

# from src directory run
# build docker image using: docker build -f PriceManager/Dockerfile -t pricemanager-image .
# then run the container using: docker run -d -p 8080:80 --name pricemanager-container pricemanager-image