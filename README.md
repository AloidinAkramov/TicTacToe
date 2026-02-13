⚠️ IMPORTANT
Please open the application using two different browsers (for example: Chrome and Edge) to properly test real-time multiplayer functionality.

🎮 Real-Time Multiplayer Tic Tac Toe (ASP.NET Core MVC)

A clean and practical real-time Tic Tac Toe game built with ASP.NET Core MVC, SignalR, and Entity Framework Core, designed to demonstrate multiplayer game logic and real-time communication.

This project focuses on backend architecture, real-time synchronization, and clean separation of responsibilities using production-style development practices.

🌐 Live Demo

🚀 Live Website:
👉 http://tictoe.runasp.net/

You can test:

Real-time multiplayer gameplay

Create and join games

Win / draw detection

Score tracking

Leaderboard ranking

Game restart functionality

🚀 Features

Real-time multiplayer using SignalR

Session-based player identity tracking

Game lobby system

Server-side move validation

Turn-based enforcement

Win and draw detection

Score tracking system

Leaderboard (Top 10 players)

Microsoft SQL Server integration

Clean MVC architecture

🛠 Tech Stack

ASP.NET Core MVC

SignalR

Entity Framework Core

Microsoft SQL Server

Razor Views

Session management

Bootstrap

📸 Screenshots & Feature Overview

1️⃣ Player Entry Screen

The application starts with a simple screen where the user enters their name before accessing the game.

The name is stored in session and used as the player identity during gameplay.

Key Points

Session-based player identity

Server-side validation (prevents empty name)

Clean and minimal UI

Redirects to Lobby after submission

<img width="1916" height="1113" alt="image" src="https://github.com/user-attachments/assets/3a730e93-23b1-4919-926b-471bc03a8eb1" />


2️⃣ Game Lobby

After entering a name, the user is redirected to the Game Lobby.

The lobby displays all available games that are waiting for a second player.

Key Points

Create a new game

Join an existing game

View leaderboard

Only unfinished games are shown

Each game supports a maximum of two players

This page acts as the central hub for starting and joining multiplayer matches.

<img width="1919" height="1112" alt="image" src="https://github.com/user-attachments/assets/a34e24f7-b4cc-448d-96a6-0f4e7905ab04" />

3️⃣ Leaderboard

The Leaderboard displays the top 10 players ranked by performance.

Players are sorted primarily by wins and secondarily by total games.

Key Points

Shows top 10 players

Ranked by wins (descending)

Displays wins, losses, draws, total games

Calculates win rate percentage

Top 3 players are highlighted with medals

This feature reflects persistent player statistics stored in the database.

<img width="1913" height="1123" alt="image" src="https://github.com/user-attachments/assets/a57f0103-4982-475d-a118-f6329d17adb4" />

4️⃣ Real-Time Gameplay

This screen shows the active multiplayer game.

Two players join the same game and play in real time.

Key Points

3x3 game board

Turn-based system (Your turn / Opponent's turn)

Real-time move synchronization using SignalR

Live score display

Server-side move validation

Each move is validated on the server and instantly updated for both players.

The system prevents:

Playing out of turn

Overwriting occupied cells

Playing after the game is finished

<img width="1922" height="1071" alt="image" src="https://github.com/user-attachments/assets/6471f99e-cf9a-4d69-ada4-4e56283ae58e" />
<img width="1918" height="1064" alt="image" src="https://github.com/user-attachments/assets/4e71169a-33bb-47a4-8e7f-5f1c403351c1" />


5️⃣ Game Result & Restart

After a game ends, the system clearly displays the result.

Players can see whether they won or lost, along with the updated score.

Key Points

Displays YOU WIN or YOU LOSE

Highlights winning combination

Updates score in real time

Disables further moves after game ends

Provides Play Again button

Restart happens without leaving the game session

All results are validated and updated on the server before being shown to both players.

<img width="1920" height="1117" alt="image" src="https://github.com/user-attachments/assets/4371aa58-7e9f-42ae-b381-9d5b8fa1d943" />
<img width="1910" height="1117" alt="image" src="https://github.com/user-attachments/assets/dc388b6d-fead-4465-822d-037bb8e66a02" />


6️⃣ Playing After Game is Finished

Once the game is finished, the system prevents any further moves.

Even if a player tries to click on the board:

Moves are blocked on the client side

Server also validates and rejects invalid actions

The board remains locked

This ensures full game integrity and prevents cheating.

Key Points

Prevents moves after game is finished
Server-side validation for every move
Board becomes read-only
Game state remains consistent for both players

The game can only continue if the Play Again button is clicked.

<img width="1919" height="1124" alt="image" src="https://github.com/user-attachments/assets/306f30e0-867a-4618-a8e4-8c802824846b" />

<img width="1913" height="1117" alt="image" src="https://github.com/user-attachments/assets/9afe10a7-c1c0-4df4-8ad3-0e7b8b7fcacf" />

🧩 Architecture Overview

Controllers – Handle HTTP requests and navigation
Services – Contain game logic (move, win, draw, score)
Hubs – Real-time communication using SignalR
Data – Entity Framework Core DbContext
Models – Game and Player entities
Views – Razor (MVC frontend)
JavaScript – Real-time UI updates

The project follows a layered structure, keeping responsibilities separated and the codebase clean.

🎮 Game Logic

Every move is validated on the server

Turn order is strictly enforced

Winner combinations are checked server-side

Moves are blocked after the game is finished

Scores are stored in the database

This ensures game integrity and prevents invalid actions.

⚡ Real-Time System

SignalR enables live multiplayer interaction:

Moves update instantly for both players

Results are synchronized in real time

Restart works without page refresh

📬 Author

Aloidin Akramov
GitHub: https://github.com/AloidinAkramov

Live Demo: http://tictoe.runasp.net/


