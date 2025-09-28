# URL Shortener project

A simple and easy to use URL shortening service

## Features

- Simple intuitive UI
- Fast performance using Redis as the primary data store
- Idempotent requests: existing URLs return the same shortened result
- Re-hash on key collision

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
