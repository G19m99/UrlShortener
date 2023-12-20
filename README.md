# URL Shortener project

A simple and easy to use URL shortening service

## Features

- Simple intuitive UI
- Extremely fast, thanks to Redis and Asp.Net 8
- Idempotent posts: Return the same URL if it's already shortened
- Re-hash on duplicate

## Tech Stack

**Client:** React, TailwindCSS

**Server:** Asp.Net 8, C#, Redis, Docker

## Optimizations

Utilized Redis as the primary database for optimal performance

## Run Locally

You will need to spin up a docker redis instance (update the connection string for redis in the appsettings.json file)

```bash
  docker run -d -p 5002:6379 --name my-redis-container redis
```
