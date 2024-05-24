# The Paris Olympic Games Ticketing 2024

## Table of contents
- [The Paris Olympic Games Ticketing 2024](#the-paris-olympic-games-ticketing-2024)
  - [Table of contents](#table-of-contents)
  - [General info](#general-info)
  - [Technologies](#technologies)
  - [Setup](#setup)

## General info
This project, Billeterie for JO 2024 or Olympic Games Ticketing 2024, is developed as final project of the Bachelor developer of applications at Studi. The aim of this project is to create an ticketing e-store for olympic games to showcase the tickets they offer to visitors. This repository contains the back-end and fron-end part of the project. Front-end is developped using React.js and hosted on http://localhost:3000/. Back-end is developed using ASP.NET and API (swagger) on http://localhost:5000/. The application respects onion architecture.
	
## Technologies
Project is created with:
*React Framework: Utilizing the latest React features for a dynamic and responsive user interface.
*ASP.NET and API: Providing a backend with an easy-to-use API for frontend applications.
*API Integration: Seamlessly communicating with the backend API to retrieve and manage product data.
*User Experience: Focusing on simple old-fashion design to reach all users with effective experience.

*Fetch API for REST requests
*Entity Framework Core w/ EF Migrations, JSON Web Token (JWT) authorization
*Docker used for development PostgreSQL database

REACT version 18.3.1, Fetch API for REST requests
ASP.NET version 8.0
POSTGRESQL using Docker 
Cloudify for images
	
## Setup
To run this project, install it locally using npm and dotnet:
Install the following:
1.ASP.NET version 8.0
2.Node.js 12.x or higher
3.npm or yarn package manager
4.git clone https://github.com/BarbFlegel/BilleterieJO2024


Run following:
FRONT-END part:
1.cd client 
2.npm install and npm start
3.npm run
4.http://localhost:3000/
Back-END part:
1.cd API 
2.dotnet ef migrations add (maybe)
3.dotnet ef database update (maybe)
4.dotnet run
5.http://localhost:5000/
