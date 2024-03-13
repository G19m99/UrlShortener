# URL Shortener project

A simple and easy to use URL shortening API and UI

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
