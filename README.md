# Leaderboard Backend using .NET Core and Redis


Welcome to the Leaderboard Backend repository! This project is a backend implementation of a leaderboard system using .NET Core for the backend and Redis as the main database. The backend is designed to manage scores of players in various games or activities efficiently.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Efficient Backend:** The .NET Core backend is designed for performance, handling high traffic and updates efficiently.
- **Multiple Games:** You can configure the backend to manage leaderboards for different games or activities.
- **Persistent Storage:** Redis, used as the main database, ensures fast and reliable data storage for the leaderboard.
- **Dockerized Redis:** The Redis database is containerized using Docker, making deployment and scaling hassle-free.
- Search by name and ID, insert of new score and incrementing old ones, easy to use and manage.

## Installation

Follow these steps to set up the Leaderboard Backend locally:

1. Clone this repository to your local machine using:
git clone https://github.com/renatotabossi/LeaderBoard-Redis.git

Copy code

2. Navigate to the project directory:
cd leaderboard

Copy code

3. Install the necessary dependencies:
dotnet restore

Copy code

4. Set up Redis using Docker:
docker run -d -p 6379:6379 redis

Copy code

5. Configure the connection to the Redis database:
In the `appsettings.json` file within the `backend` directory, update the Redis connection string as needed.

6. Build and run the backend:
dotnet build
dotnet run

Copy code

## Usage

1. The backend is configured to listen for incoming requests related to leaderboard management.
2. You can use API calls to add, update, retrieve, and manage player scores and leaderboards.
3. Customize the app for different games by modifying configurations in the backend.

## Architecture

The Leaderboard Backend follows a server architecture:

- **Backend:** Built on .NET Core, the backend manages incoming API requests, interacts with the Redis database, and serves leaderboard data.

- **Database:** Redis, used as the main database, offers fast and efficient storage for leaderboard data. It is containerized using Docker for easy deployment.

## Technologies Used

- .NET Core
- Redis
- Docker

## Contributing

Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and ensure the project still builds and runs.
4. Commit and push your changes to your fork.
5. Create a pull request detailing your changes.

## License

This project is licensed under the [MIT License](LICENSE).

