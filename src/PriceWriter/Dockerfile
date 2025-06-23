FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY PriceWriter/*.csproj ./PriceWriter/
COPY Pricing.ServiceDefaults/*.csproj ./Pricing.ServiceDefaults/
COPY Messaging.Application/*.csproj ./Messaging.Application/
COPY Messaging.Infrastructure/*.csproj ./Messaging.Infrastructure/

RUN dotnet restore PriceWriter/

COPY PriceWriter/ ./PriceWriter/
COPY Pricing.ServiceDefaults ./Pricing.ServiceDefaults/
COPY Messaging.Application ./Messaging.Application/
COPY Messaging.Infrastructure ./Messaging.Infrastructure/

WORKDIR /app/PriceWriter
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build-env /app/PriceWriter/out .

EXPOSE 80
ENV DOTNET_URLS=http://+:80

ENTRYPOINT ["dotnet", "PriceWriter.dll"]

# from src directory run
# build docker image using: docker build -f PriceWriter/Dockerfile -t pricewriter-image .
# then run the container using: docker run -d -p 8082:80 --name pricewriter-container pricewriter-image