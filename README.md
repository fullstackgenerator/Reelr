[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/en-us/download)
[![Stars](https://img.shields.io/github/stars/fullstackgenerator/reelr)](https://github.com/fullstackgenerator/reelr/stargazers)

# Reelr 🎬

Reelr is an ASP.NET Core Razor server web application that integrates with the TMDB API to deliver movie discovery features. It supports user authentication using ASP.NET Identity and allows users to save favorite movies and explore titles randomly.

<small>*Mobile view shown on the right.*</small>

<img width="2248" height="874" alt="1" src="https://github.com/user-attachments/assets/6d49937e-96cd-4c6a-af62-4f6036a4b6da" />

## Tech Stack

- **Backend**: ASP.NET Core Razor server
- **Authentication**: ASP.NET Identity
- **Database**: Entity Framework Core
- **UI**: Bootstrap 5
- **External API**: [TMDB API](https://www.themoviedb.org/documentation/api)

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- A TMDB API key – [sign up here](https://www.themoviedb.org/settings/api)

### Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/reelr.git
   cd reelr

### License & Contribution

TechBoard is released under the **MIT License**, encouraging you to explore, modify, and adapt the code as you see fit. Feel free to fork the repository and give it a star!

## Features

### Home
- Lists movies fetched from the TMDB API.
- Filter by:
  - Title
  - Release year
  - Genre
- Sort alphabetically or by rating.

Browse a curated selection of movies sourced from TMDB. The home page includes an automated filtering system allowing users to search by title, release year, genre, and sort results by name or rating.

<img width="2248" height="870" alt="2" src="https://github.com/user-attachments/assets/3ff98453-bde5-4d26-bf3d-f32b7a326b45" />

### Favorites
- Requires user login via Identity.
- Add/remove movies from favorites.
- “Watch online” button opens a Google search:  
  `"{movie title} watch online free"`

Authenticated users can save movies to their favorites. Once logged in, an "Add to Favorites" button becomes available on each movie card. Additionally, a "Watch Online" button launches a Google search like:
{movie title} watch online free, helping users locate the movie quickly.

<img width="2256" height="872" alt="3" src="https://github.com/user-attachments/assets/dd802bba-4682-40f0-9b7e-66e8358c5c52" />

<img width="2256" height="873" alt="4" src="https://github.com/user-attachments/assets/bc786806-a04c-44b5-a729-0ff45e4a31eb" />

<img width="2256" height="873" alt="5" src="https://github.com/user-attachments/assets/ed4cfbf5-713e-4121-b303-4761cb082e6a" />

<img width="2268" height="874" alt="7" src="https://github.com/user-attachments/assets/7c58e6b5-1f04-43c1-be84-fc9305225a5f" />

<img width="1778" height="875" alt="8" src="https://github.com/user-attachments/assets/b9135f98-2dac-4bb2-aa61-616555cf7f7c" />

### Random Movie
- Displays a random movie from the TMDB catalog.

Feeling indecisive? The Random Movie page delivers a randomly selected title from the TMDB catalog, perfect for spontaneous viewing.

<img width="2256" height="875" alt="6" src="https://github.com/user-attachments/assets/3dcb2abd-1c14-4376-9cf4-6ddbe61f41f6" />
