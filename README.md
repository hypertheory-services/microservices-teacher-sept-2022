# Microservices

## Dev Environment

In the `dev-environment` folder, right-click on the `docker-compose.yml` file and select `Compose Up`. This will start the following containers:

- mongo: at 27017, username root and password TokyoJoe138!
- zookeeper: at 2181
- broker (kafka): at 9092
- kafka-web-ui at 8080

## Microservices run with Tye

In the root of this folder, run `tye run --watch`. This will start the following services:

- web-presence-bff at 4201
  - This is the backend for frontend service that will be used by the web application
- training-admin at 4202
  - This is the "internal" admin application for adding courses, offerings, etc.

## frontend

In the `web-presence\hypertheory` folder, run `npm start`. This will start the frontend application at http://localhost:4200

You will be able to see the frontend at localhost:4200

## Updated for Sept 22