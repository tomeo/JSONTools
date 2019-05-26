FROM microsoft/dotnet:2.2-sdk
WORKDIR /app
COPY . .
RUN dotnet build -c Release JSONTools/JSONTools.csproj && \
    dotnet test . && \
    dotnet pack -c Release --include-source --include-symbols JSONTools/JSONTools.csproj --output ../