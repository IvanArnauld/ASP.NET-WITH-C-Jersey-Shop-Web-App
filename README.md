# ASP.NET-WITH-C-Jersey-Shop-Web-App
Overview
This project is a web application built with Vue.js and ASP.NET Core. It serves as a platform for managing a football-related store, providing functionalities such as user authentication, order history, product selection, and a store locator using map integration.

## Table of Contents
# Features
# Technologies Used
# Project Structure
# Setup and Configuration
# Usage
# API Endpoints
# Map Functionality
# License
# Features
# User Authentication:

Allows users to register and log in securely.
Implements JWT authentication for secure communication between the Vue.js frontend and ASP.NET Core backend.
Order History:

Displays a user's order history with details such as order number, date, and product quantities.
Users can view detailed information about each order.
Product Selection:

Enables users to browse and select products from different brands.
Products are categorized by brand, and users can view details and add products to their cart.
Shopping Cart:

Allows users to add products to their shopping cart.
Provides a view cart option to see the added items and proceed to checkout.
Store Locator:

Utilizes map functionality to find the three closest store branches based on the user's current address.
Users can input their address, click "Find 3," and view the map with the nearest store branches.
Technologies Used
Frontend:

Vue.js
Quasar Framework
Vuex (state management)
Backend:

ASP.NET Core
Entity Framework Core
JWT Authentication
Database:

SQL Server
Project Structure
The project follows a modular structure, separating frontend and backend concerns. Key folders include:

frontend: Contains Vue.js components, store, and utility files.

backend: Houses ASP.NET Core controllers, data access layer, and database context.

Setup and Configuration
Frontend:

Navigate to the frontend folder.
Run npm install to install dependencies.
Update the src/utils/apiutil.js file with the correct server URL.
Backend:

Open the solution in Visual Studio or your preferred IDE.
Update the connection string in appsettings.json with your SQL Server details.
Run the application.
Usage
User Registration/Login:

Access the web app.
Register a new account or log in.
Order History:

After logging in, go to the "Order History" section to view past orders.
Product Selection:

Explore the "Our Brands" section to select products from different brands.
Shopping Cart:

Add products to the cart and view the cart contents.
Store Locator:

Visit the "Find 3 Closest Branches" section.
Enter your current address, click "Find 3," and view the map with nearby stores.
API Endpoints
POST /api/register:

Registers a new user.
POST /api/login:

Authenticates a user and provides a JWT token.
GET /api/order/{email}:

Retrieves order history for a user based on their email.
GET /api/order/{orderId}/{email}:

Retrieves details for a specific order.
GET /api/Brand:

Retrieves a list of brands.
GET /api/Product/{brandId}:

Retrieves products for a specific brand.
GET /api/Branch/{lat}/{lon}:

Retrieves the three closest store branches based on latitude and longitude.
Map Functionality
The "Find 3 Closest Branches" section integrates with TomTom Maps API to display a map.
Users input their current address, click "Find 3," and the map shows the three nearest store branches.
