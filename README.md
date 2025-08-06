# Sports Equipment Online Store (Pet Project)
This is a microservices-based online store API for selling sports equipment, built using modern technologies and best practices.
## Features
* Product Catalog with filtering
* Shopping Cart: Add, remove, and manage items in a user’s cart.
* Coupons: Apply discount coupons to orders.
* Orders: Create and manage orders with payment status.
* Email Notifications: Sends confirmation emails upon successful payment.
* Authentication & Authorization: Secure user access using Auth0.
## Technologies
* React – frontend UI
* ASP.NET Core – for building RESTful APIs
* Entity Framework Core – for data persistence with PostgreSQL
* PostgreSQL – as the primary database
* MassTransit with RabbitMQ – for messaging between microservices
* AutoMapper – for mapping DTOs and entities
* Auth0 – for user authentication and role-based authorization
## Architecture Overview
* Frontend React app communicates with backend APIs for products, carts, orders, and coupons.
* Backend microservices handle business logic, data persistence, and messaging.
* RabbitMQ enables communication between order, payment, and email notification services.
* Auth0 secures APIs and frontend routes.
