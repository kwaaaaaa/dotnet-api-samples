# Overview

This is just a quick .NET Core project to pull data from different Free Rest APIs and create something fun. .NET Core is excellent for deployment using Docker (since it can run on both OS and Windows and its lighter), so most of the README will just introduce Containerization.

# Secrets

* Initially, I used the Secrets Manager Tool to set up API key variables to simulate if they were sensitive, but this method sucks for production and it doesnt work well for Docker
* You can use Docker environment variables, but then you dont want to commit your docker file with the variables to git
* You can also set environment variables at runtime, but this still risks secrets being exposed if someone looks at command line logs
* So finally, I just created an env file that is excluded from git
* This method looks secure for AWS: https://aws.amazon.com/blogs/security/how-to-manage-secrets-for-amazon-ec2-container-service-based-applications-by-using-amazon-s3-and-docker/
* This option also looks secure: https://docs.docker.com/engine/reference/commandline/run/#set-environment-variables--e-env-env-file

# API Keys

API Keys are stored in secrets but they are generally passed in the query string. This also presents security issues but for now, I'm not using any paid APIs.

# Docker Containerization

## What?

Containers are like VMs except you can run several containers on one VM. This means that is less isolation, but also you can run multiple containers and consume less resources.  [Docker](https://www.docker.com/) is the most popular container platform. Containerization is most of a philosophy though - “a container does one thing, and does it in one process.”

## Some Terms

* image - a package with all the dependencies and execution configurations to run the container
* container - instance of an image
* dockerfile - text file with instructions for how to build the image
* cluster - collection of containers for scaling
* orchestrator - tool for managing container clusters
* registry - register of cloud-stored static images
* data volumes - persistent data between containers (on the same VM)

## Step By Step Up and Running with .NET Core Docker

1. Install [Docker](https://www.docker.com/community-edition)
2. You also need .NET Core Installed (but that should be a given)
3. Switch Docker to your the container of your OS (Windows / Linux) (Linux is the default, in Windows the Docker icon is on your taskbar)
4. To run and test your app locally:
```
dotnet run
```
5. Build for Docker
```
docker build -t dotnet-api-collections .
```
* https://docs.docker.com/engine/reference/commandline/build/#options
* --tag , -t		Name and optionally a tag in the ‘name:tag’ format
