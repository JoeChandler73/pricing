FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY PriceReader/*.csproj ./PriceReader/
COPY Pricing.ServiceDefaults/*.csproj ./Pricing.ServiceDefaults/
COPY Messaging.Application/*.csproj ./Messaging.Application/
COPY Messaging.Infrastructure/*.csproj ./Messaging.Infrastructure/

RUN dotnet restore PriceReader/

COPY PriceReader/ ./PriceReader/
COPY Pricing.ServiceDefaults ./Pricing.ServiceDefaults/
COPY Messaging.Application ./Messaging.Application/
COPY Messaging.Infrastructure ./Messaging.Infrastructure/

WORKDIR /app/PriceReader
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build-env /app/PriceReader/out .

EXPOSE 80
ENV DOTNET_URLS=http://+:80

ENTRYPOINT ["dotnet", "PriceReader.dll"]

# from src directory run
# build docker image using: docker build -f PriceReader/Dockerfile -t pricereader-image .
# then run the container using: docker run -d -p 8081:80 --name pricereader-container pricereader-image