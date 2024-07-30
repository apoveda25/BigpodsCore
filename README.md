# Bigpods Core :star:

## Getting started :rocket:

### Prerequisites:

* [Dotnet](https://dotnet.microsoft.com/en-es/download)
* [Dotnet tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
* [Docker and compose](https://docs.docker.com/engine/install/)
* [Pulumi](https://www.pulumi.com/docs/install/)

### Run the application:

1. Run the containers:

```bash
docker compose up -d
```

2. Run IaC with Pulumi:

```bash
# Go to the folder where the keycloak app is with the following syntax:
cd ./Apps/MonolithIaC
```

```bash
# Login in keycloak with the following syntax:
pulumi login
```

```bash
# Remove the `dev` stack with the following syntax:
pulumi stack rm --force dev
```

```bash
# Create the `dev` stack with the following syntax:
pulumi stack init dev
```

```bash
# Set environment variables for the keycloak provider with the following syntax:
pulumi config set keycloak:url http://localhost:8080 && \
pulumi config set keycloak:clientId admin-cli --secret && \
pulumi config set keycloak:username admin && \
pulumi config set keycloak:password Secret123 --secret
```

```bash
# Create the resources in keycloak using the `dev` stack with the following syntax:
pulumi up --stack dev
```

3. Run the migrations:

```bash
dotnet ef database update --project ./Apps/Monolith/Bigpods.Monolith.csproj
```

4. Run the application:

```bash
dotnet run --project ./Apps/Monolith/Bigpods.Monolith.csproj
```

### Happy coding 👨‍💻 🐱