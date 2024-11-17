# Netafim_Assignment

## Project Overview
This project is a .NET-based web application that handles specific tasks related to the Netafim assignment. The application integrates various services, utilizes webhooks, and can be tested using Postman. It is designed to perform operations based on timers and dynamic data from webhooks.
This project allows users to create timers with a specified duration and a webhook URL. It tracks the timers' statuses and triggers the provided webhook when the timer expires. The API also provides endpoints to check the remaining time or the status of a specific timer.
## Features
- Webhook Integration
- API testing with Postman
- Timer-based operations
- Configuration via `appsettings.json`

## Setup and Installation

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Postman](https://www.postman.com/)

### Local Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/UC1297/Netafim_Assignment.git
### Code Flow
When a timer is created via the API, a new TimerInfo object is generated with the specified duration and callback URL.
The TimerService manages the active timers, stores them in memory, and persists them to a file.
The background TimerWorker periodically checks for expired timers, triggers the associated webhook, and updates the timer’s status.
The API allows users to check the status of any timer by providing its unique timerId.
