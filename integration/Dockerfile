FROM microsoft/dotnet:2.2-sdk
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
WORKDIR /app/src/SC.DevChallenge.IntegrationTests
RUN ["dotnet", "build"]
CMD ["dotnet", "test"]