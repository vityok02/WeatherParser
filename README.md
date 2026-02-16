# WeatherParser

WeatherParser is a Telegram bot designed to provide up-to-date weather information. It is built using modern development practices to ensure reliable and scalable performance.

## Features
- Fetch current weather for any city
- Get weather forecasts (today and tomorrow)
- Multi-language support (English and Ukrainian)
- Location-based weather information (manual entry or geolocation)
- Manage user requests with **EF Core** and **SQL Server**
- Robust error handling to ensure the bot runs smoothly
- Dockerized deployment and hosting on **Azure**
- Structured logging with **Serilog** and **Seq**

## Technologies Used
- **C#**, **ASP.NET Core**
- **EF Core**, **SQL Server**
- **Domain-Driven Design (DDD)**, **CQRS**
- **Telegram.Bot** library
- **Docker**, **Azure**
- **Serilog** for logging

## Project Structure
The project follows a clean architecture pattern with the following layers:
- **Domain**: Core business logic and entities
- **Application**: Application services and CQRS handlers
- **Infrastructure**: Data access, external services, and implementation details
- **Bot**: Telegram bot presentation layer
- **Common**: Shared utilities and abstractions

## Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or later
- [Docker](https://www.docker.com/get-started) (for containerized deployment)
- SQL Server (provided via Docker Compose)
- Telegram Bot Token (from [@BotFather](https://t.me/botfather))
- Weather API credentials

## Installation

### Option 1: Using Docker (Recommended)

1. Clone the repository:
   ```bash
   git clone https://github.com/vityok02/WeatherParser.git
   cd WeatherParser
   ```

2. Configure your settings in `Bot/appsettings.json`:
   - Add your Telegram bot token
   - Add weather API credentials
   - Configure geocoding service token

3. Run with Docker Compose:
   ```bash
   docker-compose up -d
   ```

The bot will be available along with SQL Server and Seq (logging dashboard at http://localhost:5341).

### Option 2: Manual Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/vityok02/WeatherParser.git
   cd WeatherParser
   ```

2. Configure the connection string and settings in `Bot/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your SQL Server connection string"
     },
     "BotConfiguration": {
       "BotToken": "Your Telegram Bot Token"
     },
     "WeatherApi": {
       "BaseUrl": "Weather API base URL",
       "Key": "Your Weather API key"
     },
     "GeocodingConfiguration": {
       "Path": "Geocoding API path",
       "Token": "Your geocoding token"
     }
   }
   ```

3. Restore dependencies and run:
   ```bash
   dotnet restore
   dotnet run --project Bot/Bot.csproj
   ```

## Configuration

The bot requires the following configuration in `appsettings.json`:

- **BotConfiguration.BotToken**: Your Telegram bot token from BotFather
- **WeatherApi**: Weather service API credentials
- **GeocodingConfiguration**: Geocoding service for location lookup
- **ConnectionStrings.DefaultConnection**: SQL Server connection string
- **Serilog**: Logging configuration (Console, File, and Seq)

## Usage

1. Start a conversation with your bot on Telegram
2. Use `/start` to begin
3. Select your preferred language (English or Ukrainian)
4. Set your location by:
   - Sharing your geolocation
   - Manually entering your city name
5. Choose from available options:
   - **Current Weather**: Get current weather conditions
   - **Forecast Today**: Get today's forecast
   - **Forecast Tomorrow**: Get tomorrow's forecast
   - **Change Location**: Update your location
   - **Change Language**: Switch between languages

## Development

### Running Tests
```bash
dotnet test
```

### Building the Project
```bash
dotnet build
```

## License

This project is open source and available under the MIT License.
