# URL Shortener project

A simple and easy to use URL shortening API and UI

## Demo

coming soon!

## Features

- Simple intuitive UI
- Extremely fast, thanks to Redis and Asp.Net 8
- Idempotent posts: Return the same URL if it's already shortened
- Re-hash on duplicate

## Tech Stack

**Client:** React, Typescript, TailwindCSS

**Server:** Asp.Net 8 Api, C#, Redis, Docker

## Optimizations

Utilized Redis as the primary database for optimal performance

## Run Locally

You will need to spin up a Redis instance. The easiest way to do that is with a Docker Redis instance. If you're using a different port, make sure to update the connection string for Redis in the `appsettings.json` file.

```bash
docker run -d -p 5002:6379 --name my-redis-container redis
```

## Run Locally with Docker Compose
Easily build and launch your application in a local environment using Docker Compose.
```bash
git clone https://github.com/G19m99/UrlShortener.git
cd UrlShortener
docker compose up -d
```
Once the services are up, visit the application in your browser at: http://localhost:8080/
